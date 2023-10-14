using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using Controls;
using TheGameUI;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using static Unity.Burst.Intrinsics.X86;
using Random = UnityEngine.Random;

//using UnityEditor.Experimental.GraphView;

namespace Managers
{
    //游戏阶段
    public enum GameRounds
    {
      BeginRound,
      DuringRound,
      EndRound,
    }

    

    public class GameManager : MonoBehaviour
    {
        public static PlayerView playerView;
        //
        public static GameManager Instance;
        [Header("高亮特效")]
        public GameObject HighLightPressedP;
        public GameObject HighLightPressedE;

        
        //
        public GameRounds ActionRound = GameRounds.BeginRound;
        //判断玩家是否获胜
        public bool IfWin=false;
        
        //总回合数，超过则游戏失败
        public int GameRound=20;
        //现在的回合数
        public int CurrentRound=1;
        
        
        ///由双方阵容组成的行动阵容
        public List<CharacterBase> ActionList = new List<CharacterBase>();
        
        ///现在是谁行动
        public int CurrentActionNum;

        private PlayerUIControl _playerUIControl;

        public int BossRoom = -1;

        public Sprite WeaponPicture;//空武器栏图片
        
        
        /// <summary>
        /// 金币
        /// </summary>
        public int coins=10;

        private bool ifChooseSkillEnemytarget = false;
        private bool ifChooseSkillCharacterTarget=false;

        public GameObject CheckOut;
        
        #region 初始化
        private void Awake()
        {
            Instance = this;
            _playerUIControl = FindObjectOfType<PlayerUIControl>();
            playerView = FindObjectOfType<PlayerView>();

            
        }


        private void Start()
        {
            if (SceneManager.GetActiveScene().name=="Main")
            {
                PlayerManager.Instance.CharacterList = GameObject.Find("TeamData").GetComponent<TeamData>().CharacterList;
                LoadCoins();
                InitCharacter();
                InitSetting();
                InitPress();
                foreach (Button buttonA in PlayerUIControl.playerView.skill)
                {
                    buttonA.onClick.AddListener(() => SkillPressed());
                }

            
                foreach (Button buttonB in PlayerUIControl.playerView.enemy)
                {
                    buttonB.onClick.AddListener(() => EnemyPressed());
                }
                
                
                PlayerUIControl.playerView.Changeplacebutton.onClick.AddListener(() => StartChange());

                foreach (Button buttonA in PlayerUIControl.playerView.character)
                {
                    buttonA.onClick.AddListener((() => CharacterPressed()));
                    buttonA.onClick.AddListener(() =>ChangeCahracterPlace());
                }

            }
        }

        

        public void InitSetting()
        {
            
            InitEnemy();
            setActionList();
            UIControl.Instance.UpdateActionRoundUI();
            UIControl.Instance.UpdateGameRound();
            ActionList.First().ifActive = true;
            ActionList.First().ifSettleBuffs = true;

        }

        public void InitPress()
        {
            
            
        }
        
        
        
        #region 换位置
        bool ifChangeplace=false;
        public void StartChange()
        {
            ifChangeplace = true;
        }
        public void ChangeCahracterPlace()
        {
            if (ifChangeplace)
            {
                int a=PlayerManager.Instance.CharacterList.IndexOf(ActionList[CurrentActionNum]),
                    b = GetCharacterID();
                if (Mathf.Abs(a-b)==1)
                {
                    PlayerManager.Instance.CharacterList.Swap(a,b);
                    InitCharacter();
                    ifChangeplace = false;
                }
            }
        }
        //后退一位
        public void SetChangePlaceBuff(CharacterBase characterBase,bool isBack=true)
        {
            if (isBack)
            {
                int a = PlayerManager.Instance.CharacterList.IndexOf(characterBase),
                    b = a + 1;
                if (b<=3)
                {
                    PlayerManager.Instance.CharacterList.Swap(a,b);
                    InitCharacter();
                }                
            }
            else
            {
                int a = PlayerManager.Instance.CharacterList.IndexOf(characterBase),
                    b = a - 1;
                if (b>=0)
                {
                    PlayerManager.Instance.CharacterList.Swap(a,b);
                    InitCharacter();
                }      
            }
            
        }
        #endregion

        /// <summary>
        /// 排列行动次序，每轮开始时使用
        /// </summary>
        public void setActionList()
        {
            //合并列表
            List<CharacterBase>combinedlist = PlayerManager.Instance.CharacterList.Concat(EnemyManager.Instance.currentEnemyList).ToList();
            //按照速度属性由大到小排序
            ActionList = combinedlist.OrderByDescending(obj => obj.GetComponent<CharacterBase>().Speed).ToList();
        }

        /// <summary>
        /// 传输人物的位置
        /// </summary>
        public void InitCharacter()
        {
            foreach (var VARIABLE in PlayerUIControl.playerView.character)
            {
                VARIABLE.gameObject.SetActive(false);
            }
            for (int i = 0; i < PlayerManager.Instance.CharacterList.Count; i++)
            {
                PlayerManager.Instance.CharacterList[i].Place = i;
                PlayerUIControl.playerView.character[i].gameObject.SetActive(true);
                PlayerUIControl.playerView.character[i].image.sprite 
                    = PlayerManager.Instance.CharacterList[i].attribute.CharacterSprite;
            }
            Debug.Log("init character");

        }
        
        /// <summary>
        /// 传输敌人的位置
        /// </summary>
        public void InitEnemy()
        {
            foreach (var VARIABLE in PlayerUIControl.playerView.enemy)
            {
                VARIABLE.gameObject.SetActive(false);
            }
            for (int i = 0; i < EnemyManager.Instance.currentEnemyList.Count; i++)
            {
                EnemyManager.Instance.currentEnemyList[i].Place = i;
                PlayerUIControl.playerView.enemy[i].gameObject.SetActive(true);
                Image buttonimage = PlayerUIControl.playerView.enemy[i].image;
                buttonimage.sprite = EnemyManager.Instance.currentEnemyList[i].attribute.CharacterSprite;
            }
            
            _playerUIControl.InstantiateEnemyBloodSlider();
            _playerUIControl.InitiateEnemyBloodSlider();
            Debug.Log("init enemy");
        }



        #endregion
        //监听事件
        private void SkillPressed()
        {
            if (!ActionList[CurrentActionNum].isDead)
            {
                _playerUIControl.InitAvailableEffect();
                if (ActionList[CurrentActionNum].availableSkills[GetSkillID()].skillTarget==camp.Enemy)
                {
                
                    SetSKillAvailableE();
                    ifChooseSkillEnemytarget = true;
                    Debug.Log("choose skill");
       
                }
                if (ActionList[CurrentActionNum].availableSkills[GetSkillID()].skillTarget==camp.Player)
                {
                    SetSKillAvailableP();
                    ifChooseSkillCharacterTarget = true;
                    Debug.Log("choose skill");
          
                }    
            }
            
            
        }

        private void CharacterPressed()
        {
            if (ifChooseSkillCharacterTarget)
            {
                SkillManager.Instance.UseSkillandTool();
                ChangeGameRound();
                Invoke("InitIDs",0.6f);
                ifChooseSkillCharacterTarget = false;
            }
            SetHighLightP();
        }
        
        private void EnemyPressed()
        {
            
            if (ifChooseSkillEnemytarget)
            {
                if (SkillManager.Instance.ChargeEnemyPlace())
                {
                    if(ActionList[CurrentActionNum].isAbletoMove)
                    SkillManager.Instance.UseSkillandTool();
                    else
                    {
                        Debug.Log("角色被击晕");
                    }
                    Debug.Log("choose enemy");
                    ChangeGameRound();
                    Invoke("InitIDs",0.6f);
                    ifChooseSkillEnemytarget = false;
                }
                else
                {
                    Debug.Log("敌人位置不对");
                }
            }
            ChargeDeath();
            SetHighLightE();
        }

        private void Update()
        {
            //todo
            //可能未做完
            switch (ActionRound)
            {
                case GameRounds.BeginRound:
                    //武器被动在这个阶段触发结算
                    ActionRound = GameRounds.BeginRound;

                    break;
                case GameRounds.DuringRound:
                    //使用技能和道具
                    ActionRound = GameRounds.DuringRound;
                    
                    break;
                case GameRounds.EndRound:
                    //结算debuff
                    ActionRound = GameRounds.EndRound;

                    break;
            }

            if (EnemyManager.Instance.currentEnemyList.Count == 0)
            {
                CheckOut.SetActive(true);
                CheckOut.GetComponentInChildren<Text>().text="你赢了！";
                Time.timeScale = 0;
                Debug.Log("游戏结束");
            }

            if (PlayerManager.Instance.CharacterList.Count == 0)
            {
                CheckOut.SetActive(true);
                CheckOut.GetComponentInChildren<Text>().text="你输了";
                Time.timeScale = 0;
                Debug.Log("游戏结束");
            }
            
            

            if (Input.GetKeyDown(KeyCode.I))
            {
                BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[CharacterButtonID],"ProtectedBuff");

            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                AddCoins();
                Debug.Log("coins="+coins);
            }
            
            
         
        
        }


        public void ChargeDeath()
        {
            //检测是否有角色死亡
            for (int i=0;i<ActionList.Count;i++)
            {
                if (ActionList[i].Blood<=0)
                {
                    ActionList[i].isDead = true;
                    if (ActionList[i].attribute.Camp==camp.Player)
                    {
                        PlayerManager.Instance.CharacterList.Remove(ActionList[i]);
                        InitCharacter();
                        
                    }
                    else
                    {
                        EnemyManager.Instance.currentEnemyList.Remove(ActionList[i]);
                        InitEnemy();
                    }
                    ActionList.Remove(ActionList[i]);
                   
                    i--;
                }
            }
         
        }
        

        
      
        /// <summary>
        /// 跟新GameRound
        /// </summary>
        /// 在装备效果中如果是beginRound调用一次，必须有装备
        /// 在技能中如果是duringRound调用一次
        /// 在debuff效果中如果是endround调用一次，如果没有debuff需调用一次
        public void ChangeGameRound()
        {
            if (ActionList.Count == 0)
            {
                return;
            }
            int nextRound = (int)ActionRound + 1;
            if (nextRound<System.Enum.GetValues(typeof(GameRounds)).Length)
            {
                ActionRound = (GameRounds)nextRound;

                //判断是否结算buff和debuff
               ActionList.First().ChangeIfSettleBuffs();
                if(_playerUIControl.InitCS)
                _playerUIControl.SetCharacterBloodSlider();
                if(_playerUIControl.InitES)
                _playerUIControl.SetEnemyBloodSlider();
            }
            else
            {
                //一轮结束，下一个角色行动
                nextRound = 0;
                ActionRound = (GameRounds)nextRound;
                
                //判断是否结算buff和debuff
                if (CurrentActionNum <= ActionList.Count)
                {
                    if (CurrentActionNum < ActionList.Count)
                    {
                        ActionList[CurrentActionNum].ChangeIfSettleBuffs();
                    }
                }

                //更新血条
                _playerUIControl.SetCharacterBloodSlider();
                _playerUIControl.SetEnemyBloodSlider();
                //关闭当前角色行动能力
                if (CurrentActionNum < ActionList.Count)
                {
                    ActionList[CurrentActionNum].ifActive = false;
                }

                //转到下一个角色
                CurrentActionNum += 1;
                
                
                //判断行动列表是否溢出
                if (CurrentActionNum>=ActionList.Count)
                {
                    if (CurrentRound<GameRound)
                    { 
                        CurrentRound += 1;
                        UIControl.Instance.UpdateGameRound();
                        
                        setActionList();
                        CurrentActionNum = 0;
                    }
                    else 
                    { 
                        IfWin = false; 
                        Invoke("Settlement",1f);
                    }
                }
                
                //开启下一角色的行动能力
                ActionList[CurrentActionNum].ifActive = true;   
                ActionList[CurrentActionNum].ifSettleBuffs = true;   
                //显示角色信息
                if (ActionList[CurrentActionNum].attribute.Camp==camp.Player)
                {
                    _playerUIControl.ShowCharacter(CurrentActionNum);
                    Debug.Log("show character");
                }

                EnemyAI();
                
                UIControl.Instance.UpdateActionRoundUI();

           


            }
            Debug.Log("现在是第"+CurrentRound+"回合"+ActionRound.ToString());

            
        }

        //获取你按的是哪个技能
        #region GetSkillID

        private int SkillButtonID = -1; // 初始化为-1，表示没有按钮被选中

        
        /// <summary>
        /// 获取技能按钮编号
        /// </summary>
        /// <param SkillName="buttonIndex"></param>
        public void GetSkillID(int buttonIndex)
        {
            SkillButtonID = buttonIndex;
            //todo
        }

        /// <summary>
        /// 可以在其他地方调用此方法来获取最后选中的按钮编号
        /// </summary>
        /// <returns></returns>
        public int GetSkillID()
        {
            Debug.Log("return skill id:"+SkillButtonID);
            return SkillButtonID;
        }

        #endregion
        
        //获取按的是哪个人物
        #region GetCharacterID

        private int CharacterButtonID = 0; // 初始化为-1，表示没有按钮被选中
        /// <summary>
        /// 获取技能按钮编号
        /// </summary>
        /// <param SkillName="buttonIndex"></param>
        ///
        /// 更新高亮功能
        /// 
        public void GetCharacterID(int buttonIndex)
        {
            
            
            CharacterButtonID = buttonIndex;
            
        }

                
        /// <summary>
        /// 可以在其他地方调用此方法来获取最后选中的按钮编号
        /// </summary>
        /// <returns></returns>
        public int GetCharacterID()
        {
            return CharacterButtonID;
        }

        #endregion
        
        //获取按的是哪个敌人
        #region GetEnemyID

        private int EnemyButtonID = 0; // 初始化为-1，表示没有按钮被选中
        /// <summary>
        /// 获取技能按钮编号
        /// </summary>
        /// <param SkillName="buttonIndex"></param>
        
        
        
        public void GetEnemyID(int buttonIndex)
        {
            
               
                EnemyButtonID = buttonIndex;
                  
            

        }
        /// <summary>
        /// 可以在其他地方调用此方法来获取最后选中的按钮编号
        /// </summary>
        /// <returns></returns>
        public int GetEnemyID()
        {
            return EnemyButtonID;
        }

        #endregion

        public void InitIDs()
        {
            SkillButtonID = -1;
            EnemyButtonID = 0;
        }
        //高亮
        public void SetHighLightP()
        {
            HighLightPressedP.transform.position=playerView.character[CharacterButtonID].transform.position;
            
            
        }
        public void SetHighLightE()
        {
            HighLightPressedE.transform.position = playerView.enemy[EnemyButtonID].transform.position;
        }

        
        public void SetSKillAvailableP()
        {
            
            foreach (var pos in ActionList[GameManager.Instance.CurrentActionNum]
                         .availableSkills[GameManager.Instance.GetSkillID()].AvailableStandPosition)
            {
                if(pos>=0&&pos<=3)
                playerView.AvailEffectP[pos].SetActive(true);
            }
        }
        public void SetSKillAvailableE()
        {
            foreach (var pos in ActionList[GameManager.Instance.CurrentActionNum]
                         .availableSkills[GameManager.Instance.GetSkillID()].AvailableAttackPosition)
            {
                if(pos>=0&&pos<=3)
                playerView.AvailEffectE[pos].SetActive(true);
            }
        }
        
        

        public void EnemyAI()
        {
            if (ActionList[CurrentActionNum].attribute.Camp==camp.Enemy)
            {
                int a =Random.Range(0,ActionList[CurrentActionNum].availableSkills.Length);
                SkillManager.Instance.UseSkillandTool(a);
                ChangeGameRound();
                ChangeGameRound();
                Debug.Log("enemy AI");
            }
        }
        
        
        //销毁人物
        public void DestoryCharacter(CharacterBase character)
        {
            if (character.Blood<=0)
            {
                //todo
                //播放死亡动画，掉落物品
                Destroy(character);
            }
        }
        
        //结算
        public void Settlement()
        {
            if (IfWin)
            {
               
                UIControl.Instance.OpenCheckOutUI(IfWin);
                //更新获得金币
                //跟新结算画面
              
            }
            else
            {
                UIControl.Instance.OpenCheckOutUI(IfWin);
              
            }
              Debug.Log(IfWin);
        }

        

        //开始游戏跳转到游戏场景
        public void StartGame()
        {
            SceneManager.LoadScene("Main");
        }
        //跳转到广场
        public void GoToSquare()
        {
            SceneManager.LoadScene("Square");
        }
        
        //退出游戏
        public void QuitGame()
        {
            SaveCoins();
            Debug.Log("quit game");
            #if UNITY_EDITOR
             UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        
        //去初始菜单
        public void GoToStartMenu()
        {
            SceneManager.LoadScene("Start");
        }

        //开发者按钮，增加金钱
        public void AddCoins()
        {
            coins += 1000;
            SaveCoins();
        }

        /// <summary>
        /// 游戏内增加金币
        /// </summary>
        /// <param SkillName="amount"></param>
        public void AddCoins(int amount)
        {
            coins += amount;
        }

        /// <summary>
        /// 保存金币数，游戏结束时启用
        /// </summary>
        public void SaveCoins()
        {
            PlayerPrefs.SetInt("Coins",coins);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// 加载金币数，游戏启动时启用
        /// </summary>
        public void LoadCoins()
        {
            coins = PlayerPrefs.GetInt("Coins", 0);
        }
    }
}

public static class ListExtensions
{
    public static void Swap<T>(this List<T> list, int index1, int index2)
    {
        T temp = list[index1];
        list[index1] = list[index2];
        list[index2] = temp;
    }
}
