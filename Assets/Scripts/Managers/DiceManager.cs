using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Controls;
using TheGameUI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

namespace Managers
{
    
        //骰子
        //因为不知道要做什么先空在这里了

    public class DiceManager : MonoBehaviour
    {
        
        public enum PAYDICE
        {
            SMALL,
            MEDIUM,
            BIG
        }
        [Header("最大幸运值")]
        public int MaxLuckyPoint;
        [Header("每次投骰子积累的幸运值")]
        public int LuckyPointPerTime;

        [Header("净化值")] public int BossValue;
        [Header("级别，1到4")] 
        public int rank;

        [HideInInspector]
        [Header("投骰子的机会")]
        public int ChanceRoll;//
        [Header("最大点数,不填为6")]
        public int MaxPoint;
        [Header("选为幸运值 不选为不幸值")] 
        public bool P2N;
        [Header("幸运值")]
        public int LuckyPoint;
        public static DiceManager Instance;
        [Header("从1点到最大点加的buff，每个element对应一个点数的buff")]
        public List<buff> BuffCollection=new List<buff>();
        public PlayerUIControl playerUIControl;

        public PlayerView playerView;

        private int nround;
        // Start is called before the first frame update
        void Awake()
        {
        Instance=this;
        LuckyPoint = 0;
        playerUIControl.InitDiceUI();
        ChanceRoll = 2;
        BossValue = 0;
        
        playerUIControl.OnchangeDiceChance(ChanceRoll,P2N);
        if (MaxPoint == 0) MaxPoint = 6;
        
        if (P2N)
        {
            playerView.LuckyPoint.text = "幸运值：" + LuckyPoint;  
        }
            
        else
        {
            playerView.UNLuckyPoint.text = "不幸值：" + LuckyPoint;
        }
        
        

        nround = GameManager.Instance.CurrentRound;
        }
        void Start()
        {
            
        }
        // Update is called once per frame
        void Update()
        {
            if (nround != GameManager.Instance.CurrentRound)
            {
                nround = GameManager.Instance.CurrentRound;
                GetRollPoint();
            }
        }
        [Description("获取净化值/混乱值")]
        public int GetPercentLPoint()
        {
            return LuckyPoint*100/MaxLuckyPoint;
        }
        
        public void GetRollPoint()
        {
            ChanceRoll++;
            playerUIControl.OnchangeDiceChance(ChanceRoll,P2N);
        }
        
        [Description("投骰子")]
        //
        public void Roll()
        {
            if(ChanceRoll>=1)
            {
                int point=DiceGetPoint();
                playerUIControl.PlayDiceAnimation(point);
                GetBuff(point);
                ChanceRoll--;
            }
            else
            {
                playerView.BroadcastText.text = "没有机会投骰子";
            }

            BossValue = GetPercentLPoint();
            playerUIControl.OnchangeDiceChance(ChanceRoll,P2N);
        }

        [Description("得到骰子点数")]
        public int DiceGetPoint()
        {
            //todo
            //得到骰子点数
            int point=Random.Range(1,MaxPoint+1);
            Debug.Log("骰子点数为"+point.ToString());
            return point;
        }
        [Description("献祭功能")]
        public void PayforDice(PAYDICE paydice)
        {
            //献祭功能
            if (paydice == PAYDICE.SMALL)
            {
                PlayerManager.Instance.CharacterList[Random.Range(0,PlayerManager.Instance.CharacterList.Count)]
                    .Blood -= 10;
                ChanceRoll++;
            }
            else if(paydice==PAYDICE.MEDIUM)
            {
                //减装备
                ChanceRoll+=2;
            }
            else if(paydice==PAYDICE.BIG)
            {
                //kill someone
                ChanceRoll+=4;
            }
            playerUIControl.OnchangeDiceChance(ChanceRoll,P2N);
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        public void GetBuff(int point)
        {
            if (P2N)
            {
            List<int> per = new List<int>();
            int chose;
            //1级
            
            if(rank==1)
            {
                int chooseperson = Random.Range(0, EnemyManager.Instance.currentEnemyList.Count);
            switch (point)
            {
                //EnemyManager.Instance.currentEnemyList[Random.Range(0,EnemyManager.Instance.currentEnemyList.Count)]
                
                case 1:
                    
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[chooseperson],BuffCollection[1].name);
                    }
                    break;
                case 2:
                    
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[chooseperson],BuffCollection[2].name);
                    }
                    break;
                case 3:
                    
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[chooseperson],BuffCollection[3].name);
                    }
                    break;
                case 4:
                    
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[chooseperson],BuffCollection[4].name);
                    }
                    break;
                case 5:
                    
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[chooseperson],BuffCollection[5].name);
                    }
                    break;
                case 6:
                    
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[chooseperson],BuffCollection[6].name);
                    }
                    break;
                case 7:
                    
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[chooseperson],BuffCollection[7].name);
                    }
                    break;
                case 8:
                    
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[chooseperson],BuffCollection[8].name);
                    }
                    break;
                case 9:
                    
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[chooseperson],BuffCollection[9].name);
                    }
                    break;
                case 10:
                    
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[chooseperson],BuffCollection[10].name);
                    }
                    break;case 11:
                    
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[chooseperson],BuffCollection[11].name);
                    }
                    break;         
                default:
                    break;
            }
            //调试用
            Debug.Log(EnemyManager.Instance.currentEnemyList[chooseperson].name+"获得了"+BuffCollection[point].name+"buff");
            
            
            }
            else if(rank==2||rank==3)
            {
            while (per.Count<2)
            {
                chose = Random.Range(0, EnemyManager.Instance.currentEnemyList.Count);
                if (per.FindIndex(x=>
                x==chose)==-1) 
                    per.Add(chose);
            }

            playerView.BroadcastText.text = EnemyManager.Instance.currentEnemyList[per[0]].attribute.Name
                                            + "与"
                                            + EnemyManager.Instance.currentEnemyList[per[1]].attribute.Name
                                            + "将得到"
                                            + BuffCollection[point].description;
            switch (point)
            {
                case 1:
                    foreach (var x in per)
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[x],BuffCollection[1].name);
                    }
                    break;
                case 2:
                    foreach (var x in per)                    
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[x],BuffCollection[2].name);
                    }
                    break;
                case 3:
                    foreach (var x in per)                    
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[x],BuffCollection[3].name);
                    }
                    break;
                case 4:
                    foreach (var x in per)                    
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[x],BuffCollection[4].name);
                    }
                    break;
                case 5:
                    foreach (var x in per)                    
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[x],BuffCollection[5].name);
                    }
                    break;
                case 6:
                    foreach (var x in per)                    
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[x],BuffCollection[6].name);
                    }
                    break;
                case 7:
                    foreach (var x in per)                    
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[x],BuffCollection[7].name);
                    }
                    break;
                case 8:
                    foreach (var x in per)                   
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[x],BuffCollection[8].name);
                    }
                    break;
                case 9:
                    foreach (var x in per)               
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[x],BuffCollection[9].name);
                    }
                    break;
                case 10:
                    foreach (var x in per)       
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[x],BuffCollection[10].name);
                    }
                    break;case 11:
                    foreach (var x in per)
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[x],BuffCollection[11].name);
                    }
                    break;         
                default:
                    break;
            }                
            }
            else if(rank==4)
            {
               #region 4级
               per.Add(0);per.Add(1);per.Add(2);per.Add(3);
            while (per.Count<4)
            {
                chose = Random.Range(0, EnemyManager.Instance.currentEnemyList.Count);
                if (per.FindIndex(x=>
                        x==chose)==-1) 
                    per.Add(chose);
            }
            switch (point)
            {
                case 1:
                    foreach (var x in per)
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[x],BuffCollection[1].name);
                    }
                    break;
                case 2:
                    foreach (var x in per)
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[x],BuffCollection[2].name);
                    }
                    break;
                case 3:
                    foreach (var x in per)
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[x],BuffCollection[3].name);
                    }
                    break;
                case 4:
                    foreach (var x in per) 
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[x],BuffCollection[4].name);
                    }
                    break;
                case 5:
                    foreach (var x in per) 
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[x],BuffCollection[5].name);
                    }
                    break;
                case 6:
                    foreach (var x in per)   
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[x],BuffCollection[6].name);
                    }
                    break;
                case 7:
                    foreach (var x in per) 
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[x],BuffCollection[7].name);
                    }
                    break;
                case 8:
                    foreach (var x in per) 
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[x],BuffCollection[8].name);
                    }
                    break;
                case 9:
                    foreach (var x in per) 
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[x],BuffCollection[9].name);
                    }
                    break;
                case 10:
                    foreach (var x in per) 
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[x],BuffCollection[10].name);
                    }
                    break;case 11:
                    foreach (var x in per) 
                    {
                        BuffManager.Instance.AddBuff(EnemyManager.Instance.currentEnemyList[x],BuffCollection[11].name);
                    }
                    break;         
                default:
                    break;
            }
            #endregion 
            }
            else
            {
                Debug.Log("没有这个级别");
            }    
            }
            else
            {
            List<int> per = new List<int>();
            int chose;
            //1级
            if(rank==1)
            {
                int chooseperson = Random.Range(0, PlayerManager.Instance.CharacterList.Count);
            switch (point)
            {
                //PlayerManager.Instance.CharacterList[Random.Range(0,PlayerManager.Instance.CharacterList.Count)]
                
                case 1:
                    
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[chooseperson],BuffCollection[1].name);
                    }
                    break;
                case 2:
                    
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[chooseperson],BuffCollection[2].name);
                    }
                    break;
                case 3:
                    
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[chooseperson],BuffCollection[3].name);
                    }
                    break;
                case 4:
                    
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[chooseperson],BuffCollection[4].name);
                    }
                    break;
                case 5:
                    
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[chooseperson],BuffCollection[5].name);
                    }
                    break;
                case 6:
                    
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[chooseperson],BuffCollection[6].name);
                    }
                    break;
                case 7:
                    
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[chooseperson],BuffCollection[7].name);
                    }
                    break;
                case 8:
                    
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[chooseperson],BuffCollection[8].name);
                    }
                    break;
                case 9:
                    
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[chooseperson],BuffCollection[9].name);
                    }
                    break;
                case 10:
                    
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[chooseperson],BuffCollection[10].name);
                    }
                    break;case 11:
                    
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[chooseperson],BuffCollection[11].name);
                    }
                    break;         
                default:
                    break;
            }
            //调试用
            Debug.Log(PlayerManager.Instance.CharacterList[chooseperson].name+"获得了"+BuffCollection[point].name+"buff");
            
            
            }
            else if(rank==2||rank==3)
            {
            while (per.Count<2)
            {
                chose = Random.Range(0, PlayerManager.Instance.CharacterList.Count);
                if (per.FindIndex(x=>
                x==chose)==-1) 
                    per.Add(chose);
            }

            playerView.BroadcastText.text = PlayerManager.Instance.CharacterList[per[0]].attribute.Name
                                            + "与"
                                            + PlayerManager.Instance.CharacterList[per[1]].attribute.Name
                                            + "将得到"
                                            + BuffCollection[point].description;
            switch (point)
            {
                case 1:
                    foreach (var x in per)
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[x],BuffCollection[1].name);
                    }
                    break;
                case 2:
                    foreach (var x in per)                    
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[x],BuffCollection[2].name);
                    }
                    break;
                case 3:
                    foreach (var x in per)                    
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[x],BuffCollection[3].name);
                    }
                    break;
                case 4:
                    foreach (var x in per)                    
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[x],BuffCollection[4].name);
                    }
                    break;
                case 5:
                    foreach (var x in per)                    
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[x],BuffCollection[5].name);
                    }
                    break;
                case 6:
                    foreach (var x in per)                    
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[x],BuffCollection[6].name);
                    }
                    break;
                case 7:
                    foreach (var x in per)                    
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[x],BuffCollection[7].name);
                    }
                    break;
                case 8:
                    foreach (var x in per)                   
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[x],BuffCollection[8].name);
                    }
                    break;
                case 9:
                    foreach (var x in per)               
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[x],BuffCollection[9].name);
                    }
                    break;
                case 10:
                    foreach (var x in per)       
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[x],BuffCollection[10].name);
                    }
                    break;case 11:
                    foreach (var x in per)
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[x],BuffCollection[11].name);
                    }
                    break;         
                default:
                    break;
            }                
            }
            else if(rank==4)
            {
               #region 4级
               per.Add(0);per.Add(1);per.Add(2);per.Add(3);
            while (per.Count<4)
            {
                chose = Random.Range(0, PlayerManager.Instance.CharacterList.Count);
                if (per.FindIndex(x=>
                        x==chose)==-1) 
                    per.Add(chose);
            }
            switch (point)
            {
                case 1:
                    foreach (var x in per)
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[x],BuffCollection[1].name);
                    }
                    break;
                case 2:
                    foreach (var x in per)
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[x],BuffCollection[2].name);
                    }
                    break;
                case 3:
                    foreach (var x in per)
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[x],BuffCollection[3].name);
                    }
                    break;
                case 4:
                    foreach (var x in per) 
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[x],BuffCollection[4].name);
                    }
                    break;
                case 5:
                    foreach (var x in per) 
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[x],BuffCollection[5].name);
                    }
                    break;
                case 6:
                    foreach (var x in per)   
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[x],BuffCollection[6].name);
                    }
                    break;
                case 7:
                    foreach (var x in per) 
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[x],BuffCollection[7].name);
                    }
                    break;
                case 8:
                    foreach (var x in per) 
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[x],BuffCollection[8].name);
                    }
                    break;
                case 9:
                    foreach (var x in per) 
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[x],BuffCollection[9].name);
                    }
                    break;
                case 10:
                    foreach (var x in per) 
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[x],BuffCollection[10].name);
                    }
                    break;case 11:
                    foreach (var x in per) 
                    {
                        BuffManager.Instance.AddBuff(PlayerManager.Instance.CharacterList[x],BuffCollection[11].name);
                    }
                    break;         
                default:
                    break;
            }
            #endregion 
            }
            else
            {
                Debug.Log("没有这个级别");
            }    
            }
            
            
            //buff生效后积累幸运值
            
            LuckyPoint += LuckyPointPerTime;
            BossValue = GetPercentLPoint();
            if(P2N)
            playerView.LuckyPoint.text = "混乱值：" + BossValue+"%";
            else
            {
                playerView.UNLuckyPoint.text = "净化值：" + BossValue+"%";
            }

        }
        
        
    }
    
}