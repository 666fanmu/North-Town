using System.ComponentModel;
using System.Linq;
using Controls;
using Unity.VisualScripting;
using UnityEngine;
using TheGameUI;
using Random = UnityEngine.Random;
namespace Managers
{
   

    public class SkillManager : MonoBehaviour
    {

        public static SkillManager Instance;
        public PlayerView playerView;

        private void Awake()
        {
            Instance = this;
        }
        //todo
        //需要更多技能


        #region 修正数值
        /// <summary>
        /// 修正暴击率
        /// </summary>
        /// <param SkillName="CriticalHit">基础暴击率</param>
        /// <param SkillName="criticalHitCorrection">暴击修正</param>
        /// <returns></returns>
        private float CorrectCriticalHit(float CriticalHit,float criticalHitCorrection)
        {
            float finalCriticalHit = 0;
            finalCriticalHit = CriticalHit + criticalHitCorrection;
            
            return finalCriticalHit;
        }

        /// <summary>
        ///修正最终伤害
        /// </summary>
        /// <param SkillName="damage">基础伤害</param>
        /// <param SkillName="damageCorrection">伤害修正</param>
        /// <returns></returns>
        private float CorrectDamage(float damage,float damageCorrection)
        {
            damage = damage * (1 + damageCorrection);
            damage = Mathf.RoundToInt(damage);
            return damage;
        }

        #endregion


        public bool ChargePlayerPlace()
        {
            bool i = false;
            i = GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum]
                .availableSkills[GameManager.Instance.GetSkillID()].AvailableStandPosition
                .Contains(GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum].Place);
            Debug.Log("施法判断："+i);
            return i;
        }
        [Description("检查敌人是否在角色的攻击范围内")]
        public bool ChargeEnemyPlace()
        {
            bool i = false;
            i = GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum]
                .availableSkills[GameManager.Instance.GetSkillID()].AvailableAttackPosition
                .Contains(GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum].Place);
             Debug.Log("选敌判断："+i+GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum].Place);
            return i;
           
        }
        
        /// <summary>
        /// 普通攻击
        /// </summary>
        /// <param SkillName="attacker">攻击者</param>
        /// <param SkillName="attacked">被攻击者</param>
        public void DamageSkill(CharacterBase attacker, CharacterBase attacked)
        {
            if (attacked.protect_Status == CharacterBase.Protect_Status.PROTECTED)
            {
                CharacterBase protecter = PlayerManager.Instance.CharacterList.Find(x=>
                    x.protect_Status==CharacterBase.Protect_Status.PROTECTER);
                Debug.Log("被保护者");
                DamageSkill(attacker,protecter);
                return;
            }
            float damage = 0;
            if (Random.Range(0, 101) > attacked.Precision)
            {
                //精准成功
                if (Random.Range(0, 101) > attacked.Dodge)
                {
                    //敌人闪避失败
                    if (Random.Range(0, 101) > CorrectCriticalHit(attacker.CriticalHit,attacker.attribute.skills[GameManager.Instance.GetSkillID()].criticalHitCorrection))
                    {
                        //暴击失败
                        damage = Random.Range(attacker.LowAttack, attacker.HighAttack + 1);
                        damage = CorrectDamage(damage,
                            attacker.attribute.skills[GameManager.Instance.GetSkillID()].damageCorrection);
                        attacked.Blood -= (int)damage;
                        Debug.Log(attacker.attribute.Name + " 对 " + attacked.attribute.Name + " 造成了 " + damage + "点伤害");
                    }
                    else
                    {
                        //暴击成功
                        damage = Random.Range(attacker.LowAttack, attacker.HighAttack) * 2;
                        damage = CorrectDamage(damage,
                            attacker.attribute.skills[GameManager.Instance.GetSkillID()].damageCorrection);
                        attacked.Blood -= (int)damage;
                        Debug.Log(attacker.attribute.Name + " 对 " + attacked.attribute.Name + " 暴击造成了 " + damage + "点伤害");
                    }
                }
                else
                {
                    //敌人闪避成功
                    Debug.Log("闪避");
                }
                
            }
            else
            {
                //命中失败
                Debug.Log("精准 miss");
            }
        }
        
        
        //只是加个buff 不攻击
        public void BuffSkill(CharacterBase attacker,string buffname,bool isbuff=true)
        {
            if(isbuff)
            BuffManager.Instance.AddBuff(attacker,buffname);
            else
            {
                BuffManager.Instance.AddDeBuff(attacker,buffname);
            }
        }

        //使用道具
        public void Useprops(CharacterBase attacker)
        {
            Debug.Log("使用道具");
        }
        //todo


        public void UseSkillandTool()
        {
            if (GameManager.Instance.GetSkillID()!=-1&&GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum].isDead==false)
            {
            Debug.Log(GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum].attribute.Name + "使用了" +
                      GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum]
                          .availableSkills[GameManager.Instance.GetSkillID()].SkillName);
            PlayerUIControl.playerView.BroadcastText.text =
                GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum].attribute.Name + "使用了" +
                GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum]
                    .availableSkills[GameManager.Instance.GetSkillID()].SkillName;
            if (GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum].attribute.Camp == camp.Player)
            {
                for (int i = 0;
                     i < GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum]
                         .availableSkills[GameManager.Instance.GetSkillID()].effects
                         .Length;
                     i++) //判断技能有多少个效果
                {
                    switch (GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum]
                                .availableSkills[GameManager.Instance.GetSkillID()]
                                .effects[i].type)
                    {
                        case SkillEffect.EffectType.Damage:
                            DamageSkill(GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum],
                                EnemyManager.Instance.currentEnemyList[GameManager.Instance.GetEnemyID()]);
                            break;
                        case SkillEffect.EffectType.Buff:
                       {    
                           
                           if(GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum]
                              .availableSkills[GameManager.Instance.GetSkillID()]
                              .effects[i].buffs.GetComponent<buff>()==null)
                           {
                               Debug.Log("这个技能的buff没加上");
                           }   
                           
                           
                           else
                           {
                               BuffSkill(GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum],
                                   GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum]
                                       .availableSkills[GameManager.Instance.GetSkillID()]
                                       .effects[i].buffs.GetComponent<buff>().name);   
                           }
                       }
            
                            
                            break;
                        case SkillEffect.EffectType.Heal:
                            BuffSkill(GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum],
                                 GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum]
                                    .availableSkills[GameManager.Instance.GetSkillID()]
                                    .effects[i].buffs.GetComponent<buff>().name);
                            break;
                        case SkillEffect.EffectType.Debuff:
                        {
                            if(GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum]
                               .availableSkills[GameManager.Instance.GetSkillID()]
                               .effects[i].buffs.GetComponent<buff>()!=null)
                            BuffSkill(GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum],
                                GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum]
                                    .availableSkills[GameManager.Instance.GetSkillID()]
                                    .effects[i].buffs.GetComponent<buff>().name, false);  
                            else
                            {
                                Debug.Log("这个技能的buff没加上");
                            }
                        }

                            break;
                        default:
                        break;
                    }

                }
            }    
            }
            
        }

        //敌人的
        public void UseSkillandTool(int n)
        {
            if (GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum].attribute.Camp == camp.Enemy&&
                (GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum].isDead==false))
            {
                int x = Random.Range(0, GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum]
                    .availableSkills[n].AvailableAttackPosition.Length);
                for (int i = 0;
                     i < GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum]
                         .availableSkills[n].effects
                         .Length;
                     i++) //判断技能有多少个效果
                {
                    switch (GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum]
                                .availableSkills[n]
                                .effects[i].type)
                    {
                        case SkillEffect.EffectType.Damage:
                            DamageSkill(GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum],
                                PlayerManager.Instance.CharacterList[x]);
                            break;
                        case SkillEffect.EffectType.Buff:
                            BuffSkill(GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum],
                                 GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum]
                                    .availableSkills[n]
                                    .effects[i].buffs.GetComponent<buff>().name);
                            break;
                        case SkillEffect.EffectType.Heal:
                            BuffSkill(GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum],
                                 GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum]
                                    .availableSkills[n]
                                    .effects[i].buffs.GetComponent<buff>().name);
                            break;
                        case SkillEffect.EffectType.Debuff:
                            BuffSkill(GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum],
                                 GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum]
                                    .availableSkills[n]
                                    .effects[i].buffs.GetComponent<buff>().name, false);
                            break;
                    }
                }
            }
            GameManager.Instance.ChargeDeath();
        }
    }

}