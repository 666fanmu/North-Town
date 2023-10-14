using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controls;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterBase:MonoBehaviour
{
    #region 特殊的BUFF状态

    
    //守护
    public enum Protect_Status
    {
        NONE,
        PROTECTER,
        PROTECTED
    }

    public Protect_Status protect_Status;
    //标记，优先攻击对象
    public enum Target_Status
    {
        NONE,
        TARGETED
    }
    public Target_Status target_Status;
    //眩晕
    public bool isAbletoMove;
    #endregion
        public Attribute attribute;

        public float Blood, //血量
            HighAttack,//攻击力最大值
            LowAttack,//攻击力最小值
            Defence, //防御力
            Dodge, //闪避率
            Precision, //精准度
            CriticalHit, //暴击率
            Speed; //速度

        public int Role;//角色技能描述索引
        
        [Header("角色等级")]
        public int Level;
        public int levelNeedCoins; //角色升级所需金币数
        [HideInInspector]
        public PlayerUIControl playerUIcontrol;
        public Skill[] availableSkills;
        //记录人物所在位置
        public int Place;
        
        public WeaponBase Weapon;
        public WeaponBase Armor;
        public WeaponBase Ring;
        public WeaponBase NeckLace;

        public List<buff> Buffs = new List<buff>();
        public List<buff> Debuffs = new List<buff>();

        public bool ifSettleBuffs=false;

        //public String[] SkillIntroduce;
        
        //状态
        [HideInInspector] public bool isDead;
        /// <summary>
        /// 判断人物是否能行动
        /// </summary>
         public bool ifActive=false;

        

        void Start()
        {
            isDead = false;
            setStates();
            UpdateWeapon();
            playerUIcontrol = FindObjectOfType<PlayerUIControl>();
        }
        

        // Update is called once per frame
        void Update()
        {
            if (ifActive)
            {

                if (ifSettleBuffs)
                {
                    switch (GameManager.Instance.ActionRound)
                    {
                        case GameRounds.BeginRound:
                            SettleBuffs();
                            break;
                        case GameRounds.EndRound:
                            SettleDebuffs();
                            break;
                    }
                }
            }
        }
        

        /// <summary>
        /// 结算buff
        /// 删除buff也在这里
        /// </summary>
        private void SettleBuffs()
        {
            if (!this.isDead)
            {
                for (int i = 0; i < this.Buffs.Count; i++)
                {
                    this.Buffs[i].SettleBuffOnce(this);
                    this.Buffs[i].round -= 1;
                    Debug.Log(this.Buffs[i].name+"剩余回合数"+this.Buffs[i].round);
//               playerUIcontrol.UpdateBuffPanel(this.Buffs[i],this.Buffs[i].round);
                }

            
                for (int i=0; i<this.Buffs.Count;i++ )
                {
                    buff buf=this.Buffs[i];
                    if (buf.round <= 0)
                    {
                        this.Buffs.Remove(buf);
//                    playerUIcontrol.DeleteBuffPanel(buf);
                        buf.destroythis(this); 
                    
                    }
                }
                GameManager.Instance.ChangeGameRound();
                Debug.Log("结算buff");    
            }
            
        }
        
        /// <summary>
        /// 结算debuff
        /// </summary>
        private void SettleDebuffs()
        {
            if (!this.isDead)
            {
                for (int i = 0; i < Debuffs.Count; i++)
                {
                    Debuffs[i].SettleBuffOnce(this);
                    Debuffs[i].round -= 1;
                    Debug.Log(this.Buffs[i].name+"剩余回合数"+this.Buffs[i].round);
                    
                }

                for (int i=0; i<this.Debuffs.Count;i++ )
                {
                    buff buf=this.Debuffs[i];
                    if (buf.round <= 0)
                    {
                        this.Debuffs.Remove(buf);
//                    playerUIcontrol.DeleteBuffPanel(buf);
                        buf.destroythis(this); 
                    }
                }
                GameManager.Instance.ChangeGameRound();
                Debug.Log("结算debuff");
                ifActive = false;     
            }
            
        }

        public void ChangeIfSettleBuffs()
        {
            if (GameManager.Instance.ActionRound==GameRounds.DuringRound)
            {
                ifSettleBuffs = false;
            }
            else
            {
                ifSettleBuffs = true;
            }
        }
        
        
        

        void setStates()
        {
            //Debug.Log(attribute.SkillName);
            //Debug.Log(attribute.AttributeList[attribute.currentLevel-1].Blood);
            Blood = attribute.AttributeList[attribute.currentLevel - 1].Blood;
            HighAttack = attribute.AttributeList[attribute.currentLevel - 1].HighAttack;
            LowAttack = attribute.AttributeList[attribute.currentLevel - 1].LowAttack;
            Defence = attribute.AttributeList[attribute.currentLevel - 1].Defence;
            Dodge = attribute.AttributeList[attribute.currentLevel - 1].Dodge;
            Precision = attribute.AttributeList[attribute.currentLevel - 1].Precision;
            CriticalHit = attribute.AttributeList[attribute.currentLevel - 1].CriticalHit;
            Speed = attribute.AttributeList[attribute.currentLevel - 1].Speed;
            levelNeedCoins = attribute.AttributeList[attribute.currentLevel - 1].levelNeedCoins;
            Level = attribute.currentLevel;
            target_Status = Target_Status.NONE;
            protect_Status = Protect_Status.NONE;
            isAbletoMove = true;
            
            availableSkills = attribute.skills;
            
            
            
        }

        public void UpdateWeapon()
        {
            if (Weapon != null)
            {
                Weapon.SetStates();
                SetWeapon(Weapon);
            }

            if (Armor != null)
            {
                Armor.SetStates();
                SetWeapon(Armor);
            }

            if (Ring!=null)
            {
                Ring.SetStates();
                SetWeapon(Ring);
            }

            if (NeckLace!=null)
            {
                NeckLace.SetStates();
                SetWeapon(NeckLace);
            }
        }
        
        void SetWeapon(WeaponBase weapon)
        {
            Blood += weapon.Blood;
            HighAttack += weapon.HighAttack;
            LowAttack += weapon.LowAttack;
            Defence += weapon.Defence;
            Dodge += weapon.Dodge;
            Precision+=weapon.Precision;
            CriticalHit += weapon.CriticalHit;
            Speed += weapon.Speed;
        }
    

}

