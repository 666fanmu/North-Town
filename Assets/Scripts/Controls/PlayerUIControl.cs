using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using TheGameUI;

namespace Controls
{
    public class PlayerUIControl : MonoBehaviour
    {
        public static PlayerUIControl Instance;
        public static PlayerView playerView;
        public GameObject BloodSlider;
        public GameObject BuffPanel;
        public GameObject Buffshow;
        //血条是否完成初始化
        public bool InitCS = false, InitES = false;
        private void Awake()
        {
            Instance = this;
            playerView = FindObjectOfType<PlayerView>();
        }

        private void Start()
        {
            UpdateCharacterTeam();
            Invoke("ShowCharacter",0.4f);
            Invoke("InstantiateCharacterBloodSlider",0.1f);
            Invoke("InitiateCharacterBloodSlider",0.1f);
            Invoke("InitiateEnemyBloodSlider",0.1f);
            Invoke("InitAvailableEffect",0.1f);
            
        }

        private void Update()
        {
            if(InitCS)
                SetCharacterBloodSlider();
            if(InitES)
                SetEnemyBloodSlider();
                
        }

        /// <summary>
        /// 当出现人物数量变化时调用
        /// </summary>
        public void UpdateCharacterTeam()
        {
            for (int i = 0; i < PlayerManager.Instance.CharacterList.Count; i++)
            {
                playerView.character[i].gameObject.SetActive(true);

            }


        }

        public void InitAvailableEffect()
        {
            for (int i = 0; i < playerView.character.Count; i++)
            {
                playerView.AvailEffectP[i].SetActive(false);
            }

            for (int i = 0; i < playerView.enemy.Count; i++)
            {
                playerView.AvailEffectE[i].SetActive(false);
            }
        }
        
        
        public void InitDiceUI()
        {
            playerView.DiceText.enabled = false;
           // playerView.ChanceRollTextA.text="古神的骰子\n剩余骰子次数：0";
           // playerView.ChanceRollTextB.text="命运女神的骰子\n剩余骰子次数：0";
        }
        public void PlayDiceAnimation(int point)
        {
            playerView.DiceText.text = "骰子点数为" + point.ToString();
            playerView.DiceText.enabled = true;
            Invoke("DisableDiceText",0.6f);

        }
        public void ShowChoosePerson()
        {
            
        }
        private void DisableDiceText()
        {
            playerView.DiceText.enabled = false;            
        }

        public void OnchangeDiceChance(int chance,bool P2N)
        {
            if(P2N)
            playerView.ChanceRollTextA.text="古神的骰子\n剩余骰子次数："+chance.ToString();
            else
            { 
                playerView.ChanceRollTextB.text="命运女神的骰子\n剩余骰子次数："+chance.ToString();   
            }
           
        }

        #region 血条

        public void InstantiateCharacterBloodSlider()
        {
            for (int i = 0; i < playerView.character.Count; i++)
            {
                if(playerView.character[i].gameObject.GetComponentInChildren<Slider>()==false)
                    Instantiate(BloodSlider, playerView.character[i].gameObject.transform
                    );                
            }
            InitCS = true;
        }
        public void InstantiateEnemyBloodSlider()
        {
            Debug.Log(playerView.enemy.Count);
            for (int i = 0; i < playerView.enemy.Count; i++)
            {
                if (playerView.enemy[i].gameObject.GetComponentInChildren<Slider>() == false)
                    Instantiate(BloodSlider, playerView.enemy[i].gameObject.transform);
            }
            InitES = true;
        }
        public void InitiateCharacterBloodSlider()
        {
            for (int i = 0; i < PlayerManager.Instance.CharacterList.Count; i++)
            {
                SetCharacterBloodSlider(i);
            }


        }
        
        public void SetCharacterBloodSlider(int characterID)
        {
            
            playerView.character[characterID].GetComponentInChildren<Slider>().value
             = (float)PlayerManager.Instance.CharacterList[characterID].Blood/(float)
               PlayerManager.Instance.CharacterList[characterID].attribute.AttributeList
                   [PlayerManager.Instance.CharacterList[characterID].attribute.currentLevel - 1].Blood;

        }

        public void SetCharacterBloodSlider()
        {
            
            for (int characterID = 0;characterID<PlayerManager.Instance.CharacterList.Count;characterID++)
            {
                
                playerView.character[characterID].GetComponentInChildren<Slider>().value
                    = (float)PlayerManager.Instance.CharacterList[characterID].Blood/(float)
                    PlayerManager.Instance.CharacterList[characterID].attribute.AttributeList
                        [PlayerManager.Instance.CharacterList[characterID].attribute.currentLevel - 1].Blood;                
            }

            
        }
        public void InitiateEnemyBloodSlider()
        {
            for (int i = 0; i < EnemyManager.Instance.currentEnemyList.Count; i++)
            {
                SetEnemyBloodSlider(i);
            }


        }
        
        public void SetEnemyBloodSlider(int characterID)
        {
           
            playerView.enemy[characterID].GetComponentInChildren<Slider>().value
                = (float)EnemyManager.Instance.currentEnemyList[characterID].Blood/(float)
                EnemyManager.Instance.currentEnemyList[characterID].attribute.AttributeList
                    [EnemyManager.Instance.currentEnemyList[characterID].attribute.currentLevel - 1].Blood;

        }
        public void SetEnemyBloodSlider()
        {
            for (int characterID = 0; characterID<EnemyManager.Instance.currentEnemyList.Count;characterID++)
            {
    
                playerView.enemy[characterID].GetComponentInChildren<Slider>().value
                    = (float)EnemyManager.Instance.currentEnemyList[characterID].Blood/(float)
                    EnemyManager.Instance.currentEnemyList[characterID].attribute.AttributeList
                        [EnemyManager.Instance.currentEnemyList[characterID].attribute.currentLevel - 1].Blood;                
            }


        }      
#endregion
        #region BuffPanel
        public void WriteBuffPanel(buff buf,int round)
        {
            Vector3 pos = BuffPanel.transform.position-new Vector3(BuffPanel.GetComponent<RectTransform>().rect.xMin,0,0);
            GameObject buffro = Instantiate(Buffshow, BuffPanel.transform);
            buffro.transform.SetPositionAndRotation(pos, this.transform.rotation);
            buf.bufflabel= buffro;
            buffro.GetComponent<Image>().sprite = buf.Sprite;
            buffro.GetComponent<BuffLabel>().Round.text = round.ToString();
            buffro.GetComponent<BuffLabel>().Description.text = buf.description;
            SetBuffPanel(GameManager.Instance.GetCharacterID());            
            //Debug.Log(BuffPanel.transform.childCount.ToString()+" v"+BuffPanel.GetComponent<RectTransform>().rect.xMin);
        }

        public void SetBuffPanel(int characterID)
        {
            
            for(int i=0;i<BuffPanel.transform.childCount;i++)
            {
                BuffPanel.transform.GetChild(i).gameObject.SetActive(false);
            }
           
            for (int i = 0; i < PlayerManager.Instance.CharacterList[characterID].Buffs.Count; i++)
            {
                PlayerManager.Instance.CharacterList[characterID].Buffs[i].bufflabel.SetActive(true);
            }
        }
        public void UpdateBuffPanel(buff buf,int round)
        {
            buf.bufflabel.GetComponent<BuffLabel>().Round.text = round.ToString();
        }        
        public void DeleteBuffPanel(buff buf)
        {
            Destroy(buf.bufflabel.gameObject);
        }
        #endregion
        //Update it
       
        /// <summary>
        /// 选择人物后更新UI界面
        /// </summary>
        public void ShowCharacter()
        {
            int characterID = GameManager.Instance.GetCharacterID();
            SetInformation(
                PlayerManager.Instance.CharacterList[characterID].attribute.CharacterHeadSprite,
                PlayerManager.Instance.CharacterList[characterID].attribute.currentLevel,
                PlayerManager.Instance.CharacterList[characterID].attribute.Name,
                PlayerManager.Instance.CharacterList[characterID].Blood,
                PlayerManager.Instance.CharacterList[characterID].LowAttack,
                PlayerManager.Instance.CharacterList[characterID].HighAttack,
                PlayerManager.Instance.CharacterList[characterID].Defence,
                PlayerManager.Instance.CharacterList[characterID].Dodge,
                PlayerManager.Instance.CharacterList[characterID].Precision,
                PlayerManager.Instance.CharacterList[characterID].CriticalHit,
                PlayerManager.Instance.CharacterList[characterID].Speed
            );
            
            SetEquipment(
                PlayerManager.Instance.CharacterList[characterID].Weapon,
                PlayerManager.Instance.CharacterList[characterID].Armor,
                PlayerManager.Instance.CharacterList[characterID].Ring,
                PlayerManager.Instance.CharacterList[characterID].NeckLace);

            SetSkill(
                PlayerManager.Instance.CharacterList[characterID],
                PlayerManager.Instance.CharacterList[characterID],
                PlayerManager.Instance.CharacterList[characterID],
                PlayerManager.Instance.CharacterList[characterID]
                );
            SetBuffPanel(characterID);
        }
        
        public void ShowCharacter(int characterID)
        {
            
            SetInformation(
                GameManager.Instance.ActionList[characterID].attribute.CharacterHeadSprite,
                GameManager.Instance.ActionList[characterID].attribute.currentLevel,
                GameManager.Instance.ActionList[characterID].attribute.Name,
                GameManager.Instance.ActionList[characterID].Blood,
                GameManager.Instance.ActionList[characterID].LowAttack,
                GameManager.Instance.ActionList[characterID].HighAttack,
                GameManager.Instance.ActionList[characterID].Defence,
                GameManager.Instance.ActionList[characterID].Dodge,
                GameManager.Instance.ActionList[characterID].Precision,
                GameManager.Instance.ActionList[characterID].CriticalHit,
                GameManager.Instance.ActionList[characterID].Speed
            );
            
            SetEquipment(
                GameManager.Instance.ActionList[characterID].Weapon,
                GameManager.Instance.ActionList[characterID].Armor,
                GameManager.Instance.ActionList[characterID].Ring,
                GameManager.Instance.ActionList[characterID].NeckLace);

            SetSkill(
                GameManager.Instance.ActionList[characterID],
                GameManager.Instance.ActionList[characterID],
                GameManager.Instance.ActionList[characterID],
                GameManager.Instance.ActionList[characterID]
            );
            //SetBuffPanel(enemyID);
        }


        #region set informations


        public void SetInformation(Sprite characterHeadSprite, int level,
            string characterName, float blood, float Lowattack,float Highattack, float defence, float dodge, float precision,
            float criticalHit, float speed)
        {
            playerView.CharacterHeadImage.sprite = characterHeadSprite;
            playerView.CharacterLevel.text = "LV " + level;
            playerView.CharacterName.text = characterName;
            playerView.BloodText.text = "血量   " + blood;
            playerView.BloodText.color = Color.red;
            playerView.AttackText.text = "攻击 " + Lowattack + "~" + Highattack;
            playerView.DefenceText.text = "防御  " + defence;
            playerView.DodgeText.text = "闪避    " + dodge;
            playerView.PrecisionText.text = "精准    " + precision;
            playerView.CriticalHitText.text = "暴击    " + criticalHit;
            playerView.SpeedText.text = "速度  " + speed;
        }

        public void SetEquipment(WeaponBase weapon, WeaponBase armor, WeaponBase ring, WeaponBase necklace)
        {

            //获取并跟新装备图像

            if (weapon!=null)
            {
                playerView.Equipment[0].image.sprite = weapon.WeaponSprite;
            }
            else
            {
                playerView.Equipment[0].image.sprite = GameManager.Instance.WeaponPicture;
            }
            if (armor!=null)
            {
                playerView.Equipment[1].image.sprite = armor.WeaponSprite;
            }
            else
            {
                playerView.Equipment[1].image.sprite = GameManager.Instance.WeaponPicture;
            }
            if (ring!=null)
            {
                playerView.Equipment[2].image.sprite = ring.WeaponSprite;
            }
            else
            {
                playerView.Equipment[2].image.sprite = GameManager.Instance.WeaponPicture;
            }
            if (necklace!=null)
            {
                playerView.Equipment[3].image.sprite = necklace.WeaponSprite;
            }
            else
            {
                playerView.Equipment[3].image.sprite = GameManager.Instance.WeaponPicture;
            }
        }

        public void SetSkill(CharacterBase skill_1_Button, CharacterBase skill_2_Button, CharacterBase skill_3_Button, CharacterBase skill_4_Button)
        {
            playerView.skill[0].image.sprite = skill_1_Button.attribute.skills[0].icon;
            playerView.skill[1].image.sprite = skill_2_Button.attribute.skills[1].icon;
            playerView.skill[2].image.sprite = skill_3_Button.attribute.skills[2].icon;
            playerView.skill[3].image.sprite = skill_4_Button.attribute.skills[3].icon;
        }


        #endregion

        #region Props
        public void SetPropsUI(int propsID)
        {
        

        }
        

        #endregion
    }
}