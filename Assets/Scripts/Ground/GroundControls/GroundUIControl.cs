using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class GroundUIControl : MonoBehaviour
{
    private GUIView _gUIView;
    private Attribute nowAttribute;
    public static GroundUIControl Instance;
    private TextAsset SkillIntroduce;


    public void Awake()
    {
        _gUIView = FindObjectOfType<GUIView>();
    }

    public void Start()
    {
        Instance = this;
        GMoney.Instance.LoadMoney();
        UpdateCoinsShow();
    }

    //打开等级升级面板
    public void OpenLevelUpInterface()
    {
        _gUIView.LevelUpInterface.SetActive(true);
    }

    //关闭等级升级面板
    public void CloseLevelUpInterface()
    {
        _gUIView.LevelUpInterface.SetActive(false);
    }

    //打开技能升级面板
    public void OpenSkillUpInterface()
    {
        _gUIView.SkillUpInterface.SetActive(true);
    }

    //关闭技能升级面板
    public void CloseSkillUpInterface()
    {
        _gUIView.SkillUpInterface.SetActive(false);
    }

    //打开角色详情面板
    public void OpenHeroInterface()
    {
        _gUIView.HeroInterface.SetActive(true);
    }

    //关闭角色详情面板
    public void CloseHeroInterface()
    {
        _gUIView.HeroInterface.SetActive(false);
    }
    
    //更新角色详情界面
    public void upDateHeroInterface()
    {
        //更新角色头像与全身像
        _gUIView.gHeroView.heroImage.sprite = GCharacterManger.Instance.nowCharacter.attribute.CharacterSprite;
        _gUIView.gHeroView.heroHeadImage.sprite = GCharacterManger.Instance.nowCharacter.attribute.CharacterHeadSprite;

        //更新角色名称等级与介绍
        _gUIView.gHeroView.heroName.text = GCharacterManger.Instance.nowCharacter.attribute.Name;
        _gUIView.gHeroView.heroLevel.text = GCharacterManger.Instance.nowCharacter.attribute.currentLevel.ToString();
        //_gUIView.gHeroView.heroIntroduce.text=

        //更新技能图标
        _gUIView.gHeroView.skill1.sprite = GCharacterManger.Instance.nowCharacter.attribute.skills[0].icon;
        _gUIView.gHeroView.skill2.sprite = GCharacterManger.Instance.nowCharacter.attribute.skills[1].icon;
        _gUIView.gHeroView.skill3.sprite = GCharacterManger.Instance.nowCharacter.attribute.skills[2].icon;
        _gUIView.gHeroView.skill4.sprite = GCharacterManger.Instance.nowCharacter.attribute.skills[3].icon;

        //更新属性显示

        #region 更新属性显示
/*
        _gUIView.gHeroView.Health.text = GCharacterManger.Instance.nowCharacter.attribute
            .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel-1].Blood.ToString();
*/
        _gUIView.gHeroView.Health.text = GCharacterManger.Instance.nowCharacter.Blood.ToString();

        _gUIView.gHeroView.Attack.text = GCharacterManger.Instance.nowCharacter.LowAttack.ToString() + "~" +
                                         GCharacterManger.Instance.nowCharacter.HighAttack.ToString();

        _gUIView.gHeroView.Defence.text = GCharacterManger.Instance.nowCharacter.Defence.ToString();

        _gUIView.gHeroView.Dodge.text = GCharacterManger.Instance.nowCharacter.Dodge.ToString();

        _gUIView.gHeroView.Critical.text = GCharacterManger.Instance.nowCharacter.CriticalHit.ToString();

        _gUIView.gHeroView.Pricision.text = GCharacterManger.Instance.nowCharacter.Precision.ToString();

        _gUIView.gHeroView.Speed.text = GCharacterManger.Instance.nowCharacter.Speed.ToString();

        /*
        _gUIView.gHeroView.Attack.text = GCharacterManger.Instance.nowCharacter.attribute
                                             .AttributeList[
                                                 GCharacterManger.Instance.nowCharacter.attribute.currentLevel]
                                             .LowAttack.ToString() + " ~ " +
                                         GCharacterManger.Instance.nowCharacter.attribute
                                             .AttributeList[
                                                 GCharacterManger.Instance.nowCharacter.attribute.currentLevel]
                                             .HighAttack.ToString();

        _gUIView.gHeroView.Defence.text = GCharacterManger.Instance.nowCharacter.attribute
            .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel].Defence.ToString();

        _gUIView.gHeroView.Dodge.text = GCharacterManger.Instance.nowCharacter.attribute
            .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel].Dodge.ToString();

        _gUIView.gHeroView.Critical.text = GCharacterManger.Instance.nowCharacter.attribute
            .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel].CriticalHit.ToString();

        _gUIView.gHeroView.Pricision.text = GCharacterManger.Instance.nowCharacter.attribute
            .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel].Precision.ToString();

        _gUIView.gHeroView.Speed.text = GCharacterManger.Instance.nowCharacter.attribute
            .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel].Speed.ToString();
        
        _gUIView.gHeroView.Bleed.text = GCharacterManger.Instance.nowCharacter.attribute
            .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel].BleedResist.ToString();

        _gUIView.gHeroView.Dizzy.text = GCharacterManger.Instance.nowCharacter.attribute
            .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel].DizzyResist.ToString();

        _gUIView.gHeroView.Debuff.text = GCharacterManger.Instance.nowCharacter.attribute
            .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel].DebuffResist.ToString();

        _gUIView.gHeroView.Move.text = GCharacterManger.Instance.nowCharacter.attribute
            .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel].MoveResist.ToString();
*/
        #endregion

        //更新武器显示
        if (GCharacterManger.Instance.nowCharacter.Weapon != null)
        {
            _gUIView.gHeroView.Skill1.SetActive(true);
            _gUIView.gHeroView.wepen1.sprite = GCharacterManger.Instance.nowCharacter.Weapon.WeaponSprite;
        }
        else
        {
            _gUIView.gHeroView.wepen1.sprite = GManger.Instance.WeaponBackGroundSrpite;
        }

        if (GCharacterManger.Instance.nowCharacter.Armor != null)
        {
            _gUIView.gHeroView.Skill2.SetActive(true);
            _gUIView.gHeroView.wepen2.sprite = GCharacterManger.Instance.nowCharacter.Armor.WeaponSprite;
        }
        else
        {
            _gUIView.gHeroView.wepen2.sprite = GManger.Instance.WeaponBackGroundSrpite;
        }

        if (GCharacterManger.Instance.nowCharacter.Ring != null)
        {
            _gUIView.gHeroView.Skill3.SetActive(true);
            _gUIView.gHeroView.wepen3.sprite = GCharacterManger.Instance.nowCharacter.Ring.WeaponSprite;
        }
        else
        {
            _gUIView.gHeroView.wepen3.sprite = GManger.Instance.WeaponBackGroundSrpite;
        }

        if (GCharacterManger.Instance.nowCharacter.NeckLace != null)
        {
            _gUIView.gHeroView.Skill4.SetActive(true);
            _gUIView.gHeroView.wepen4.sprite = GCharacterManger.Instance.nowCharacter.NeckLace.WeaponSprite;
        }
        else
        {
            _gUIView.gHeroView.wepen4.sprite = GManger.Instance.WeaponBackGroundSrpite;
        }
    }

    //打开组队面板
    public void OpenTeamInterface()
    {
        _gUIView.TeamInterface.SetActive(true);
    }
    
    //关闭组队面板
    public void CloseTeamInterface()
    {   
        _gUIView.TeamInterface.SetActive(false);
    }
    //更新金币数量显示
    public void UpdateCoinsShow()
    {
        _gUIView.Coins.text = GMoney.Instance.coins.ToString();
        
    }

    //等级升级页面更新角色信息时调用
    public void ShowLevelUpInterface()
    {
        if (_gUIView.LevelUpInterface.active)
        {
            _gUIView.LevelUpName.text = GCharacterManger.Instance.nowCharacter.attribute.Name;
            _gUIView.LevelUpHeadImage.sprite = GCharacterManger.Instance.nowCharacter.attribute.CharacterHeadSprite;

            //显示Now
            GroundUIControl.Instance.changeLevelUpNowShow();

            //如果当前角色未满级，显示升级按钮，反之则不显示
            if (GCharacterManger.Instance.nowCharacter.attribute.currentLevel != 6)
            {
                _gUIView.gAttributeView.LevelUp.SetActive(true);
            }
            else
            {
                _gUIView.gAttributeView.LevelUp.SetActive(false);
            }

            //设置Attribute页面的显示

            #region SetAttribute

            #region SetAttack

            _gUIView.gAttributeView.AttackNow.text = GCharacterManger.Instance.nowCharacter.attribute
                                                         .AttributeList[
                                                             GCharacterManger.Instance.nowCharacter.attribute
                                                                 .currentLevel - 1].LowAttack.ToString() + " ~ " +
                                                     GCharacterManger.Instance.nowCharacter.attribute
                                                         .AttributeList[
                                                             GCharacterManger.Instance.nowCharacter.attribute
                                                                 .currentLevel - 1].HighAttack.ToString();

            if (GCharacterManger.Instance.nowCharacter.attribute.currentLevel < 5)
            {
                _gUIView.gAttributeView.AttackUp.text = GCharacterManger.Instance.nowCharacter.attribute
                                                            .AttributeList[
                                                                GCharacterManger.Instance.nowCharacter.attribute
                                                                    .currentLevel].LowAttack.ToString() + " ~ " +
                                                        GCharacterManger.Instance.nowCharacter.attribute
                                                            .AttributeList[
                                                                GCharacterManger.Instance.nowCharacter.attribute
                                                                    .currentLevel].HighAttack.ToString();

                _gUIView.gAttributeView.AttackNowUp.text = GCharacterManger.Instance.nowCharacter.attribute
                                                               .AttributeList[
                                                                   GCharacterManger.Instance.nowCharacter.attribute
                                                                       .currentLevel - 1].LowAttack.ToString() + " ~ " +
                                                           GCharacterManger.Instance.nowCharacter.attribute
                                                               .AttributeList[
                                                                   GCharacterManger.Instance.nowCharacter.attribute
                                                                       .currentLevel - 1].HighAttack.ToString();
            }

            #endregion

            #region SetHealth

            _gUIView.gAttributeView.HealthNow.text = GCharacterManger.Instance.nowCharacter.attribute
                .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel - 1].Blood.ToString();

            if (GCharacterManger.Instance.nowCharacter.attribute.currentLevel < 5)
            {
                _gUIView.gAttributeView.HealthUp.text = GCharacterManger.Instance.nowCharacter.attribute
                    .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel].Blood.ToString();

                _gUIView.gAttributeView.HealthNowUp.text = GCharacterManger.Instance.nowCharacter.attribute
                    .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel - 1].Blood.ToString();
            }

            #endregion

            #region SetDodge

            _gUIView.gAttributeView.DodgeNow.text = GCharacterManger.Instance.nowCharacter.attribute
                .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel - 1].Dodge.ToString();

            if (GCharacterManger.Instance.nowCharacter.attribute.currentLevel < 5)
            {
                _gUIView.gAttributeView.DodgeUp.text = GCharacterManger.Instance.nowCharacter.attribute
                    .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel].Dodge.ToString();

                _gUIView.gAttributeView.DodgeNowUp.text = GCharacterManger.Instance.nowCharacter.attribute
                    .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel - 1].Dodge.ToString();
            }

            #endregion

            #region SetDefence

            _gUIView.gAttributeView.DefenceNow.text = GCharacterManger.Instance.nowCharacter.attribute
                .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel - 1].Defence.ToString();

            if (GCharacterManger.Instance.nowCharacter.attribute.currentLevel < 5)
            {
                _gUIView.gAttributeView.DefenceUp.text = GCharacterManger.Instance.nowCharacter.attribute
                    .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel].Defence.ToString();

                _gUIView.gAttributeView.DefenceNowUp.text = GCharacterManger.Instance.nowCharacter.attribute
                    .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel - 1].Defence
                    .ToString();
            }

            #endregion

            #region SetPrecision

            _gUIView.gAttributeView.PrecisionNow.text = GCharacterManger.Instance.nowCharacter.attribute
                .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel - 1].Precision.ToString();

            if (GCharacterManger.Instance.nowCharacter.attribute.currentLevel < 5)
            {
                _gUIView.gAttributeView.PrecisionUp.text = GCharacterManger.Instance.nowCharacter.attribute
                    .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel].Precision.ToString();

                _gUIView.gAttributeView.PrecisionNowUp.text = GCharacterManger.Instance.nowCharacter.attribute
                    .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel - 1].Precision
                    .ToString();
            }

            #endregion

            #region SetCriticalHit

            _gUIView.gAttributeView.CriticalHitNow.text = GCharacterManger.Instance.nowCharacter.attribute
                .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel - 1].CriticalHit
                .ToString();

            if (GCharacterManger.Instance.nowCharacter.attribute.currentLevel < 5)
            {
                _gUIView.gAttributeView.CriticalHitUp.text = GCharacterManger.Instance.nowCharacter.attribute
                    .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel].CriticalHit
                    .ToString();

                _gUIView.gAttributeView.CriticalHitNowUp.text = GCharacterManger.Instance.nowCharacter.attribute
                    .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel - 1].CriticalHit
                    .ToString();
            }

            #endregion

            #region SetSpeed

            _gUIView.gAttributeView.SpeedNow.text = GCharacterManger.Instance.nowCharacter.attribute
                .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel - 1].Speed.ToString();

            if (GCharacterManger.Instance.nowCharacter.attribute.currentLevel < 5)
            {
                _gUIView.gAttributeView.SpeedUp.text = GCharacterManger.Instance.nowCharacter.attribute
                    .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel].Speed.ToString();

                _gUIView.gAttributeView.SpeedNowUp.text = GCharacterManger.Instance.nowCharacter.attribute
                    .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel - 1].Speed.ToString();
            }

            #endregion
            /*

            #region SetBleed

            _gUIView.gAttributeView.BleedNow.text = GCharacterManger.Instance.nowCharacter.attribute
                .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel - 1].BleedResist
                .ToString();

            if (GCharacterManger.Instance.nowCharacter.attribute.currentLevel < 5)
            {
                _gUIView.gAttributeView.BleedUp.text = GCharacterManger.Instance.nowCharacter.attribute
                    .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel].BleedResist
                    .ToString();
            }

            #endregion

            #region SetDizzy

            _gUIView.gAttributeView.DizzyNow.text = GCharacterManger.Instance.nowCharacter.attribute
                .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel - 1].BleedResist
                .ToString();

            if (GCharacterManger.Instance.nowCharacter.attribute.currentLevel < 5)
            {
                _gUIView.gAttributeView.DizzyUp.text = GCharacterManger.Instance.nowCharacter.attribute
                    .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel].BleedResist
                    .ToString();
            }

            #endregion

            #region SetDeBuff

            _gUIView.gAttributeView.DebuffNow.text = GCharacterManger.Instance.nowCharacter.attribute
                .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel - 1].DebuffResist
                .ToString();

            if (GCharacterManger.Instance.nowCharacter.attribute.currentLevel < 5)
            {
                _gUIView.gAttributeView.DebuffUp.text = GCharacterManger.Instance.nowCharacter.attribute
                    .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel].DebuffResist
                    .ToString();
            }

            #endregion

            #region SetMove

            _gUIView.gAttributeView.MoveNow.text = GCharacterManger.Instance.nowCharacter.attribute
                .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel - 1].MoveResist.ToString();

            if (GCharacterManger.Instance.nowCharacter.attribute.currentLevel < 5)
            {
                _gUIView.gAttributeView.MoveUp.text = GCharacterManger.Instance.nowCharacter.attribute
                    .AttributeList[GCharacterManger.Instance.nowCharacter.attribute.currentLevel].MoveResist.ToString();
            }

            #endregion
*/
            #endregion

            _gUIView.Attribute.SetActive(true);
        }

    }

    //技能升级页面更新角色时调用
    public void ShowSkillUpInterface()
    {
        //更新头像和名称
        _gUIView.gSkillView.skillName.text = GCharacterManger.Instance.nowCharacter.attribute.Name;
        _gUIView.gSkillView.skillHeadImage.sprite = GCharacterManger.Instance.nowCharacter.attribute.CharacterHeadSprite;

        //更新技能贴图
        _gUIView.gSkillView.Skill1.sprite = GCharacterManger.Instance.nowCharacter.attribute.skills[0].icon;
        _gUIView.gSkillView.Skill2.sprite = GCharacterManger.Instance.nowCharacter.attribute.skills[1].icon;
        _gUIView.gSkillView.Skill3.sprite = GCharacterManger.Instance.nowCharacter.attribute.skills[2].icon;
        _gUIView.gSkillView.Skill4.sprite = GCharacterManger.Instance.nowCharacter.attribute.skills[3].icon;
        
        //更新技能升级界面的人物描述
        string introducePath="RoleIntroduce/SkillUpInterface/" + GCharacterManger.Instance.thisRole.ToString();
        SkillIntroduce = Resources.Load(introducePath) as TextAsset;
        _gUIView.gSkillView.skillIntroduce.text = SkillIntroduce.text;
        
        
        //更新技能介绍
        for (int i = 0; i < 4; i++)
        {
            //构建路径
            string Path = "SkillIntroduce/"+GCharacterManger.Instance.thisRole.ToString()+"/"+i.ToString()+"/"+GCharacterManger.Instance.nowCharacter.attribute.skills[i].SkillLevel.ToString();

            SkillIntroduce = Resources.Load(Path) as TextAsset;

            _gUIView.gSkillView.SkillsNow[i].text = SkillIntroduce.text;
            
            //如果技能可升级，更新升级后的技能介绍
            if (GCharacterManger.Instance.nowCharacter.attribute.skills[i].SkillLevel < 3)
            {
                Path = "SkillIntroduce/"+GCharacterManger.Instance.thisRole.ToString()+"/"+i.ToString()+"/"+(GCharacterManger.Instance.nowCharacter.attribute.skills[i].SkillLevel+1).ToString();

                SkillIntroduce = Resources.Load(Path) as TextAsset;

                _gUIView.gSkillView.SkillsUp[i].text = SkillIntroduce.text;
            }
        }
        //关闭升级技能介绍显示
        for (int i = 0; i < 4; i++)
        {
            _gUIView.gSkillView.skillUp[i].SetActive(false);
        }
        
        _gUIView.gSkillView.Skill.SetActive(true);
        
    }
    
    //显示Attribute中的Now
    public void changeLevelUpNowShow()
    {
        _gUIView.gAttributeView.Now.SetActive(true);
        _gUIView.gAttributeView.Up.SetActive(false);
    }
    
    //显示Attribute中的Up
    public void changeLevelUpUpShow()
    {
        _gUIView.gAttributeView.Now.SetActive(false);
        _gUIView.gAttributeView.Up.SetActive(true);
    }
    
    //具体选择显示技能展示页面
    public void changeSkillUpIntroduce(int skill,bool upShow)
    {
        if (upShow)
        {
            _gUIView.gSkillView.skillUp[skill].SetActive(true);
            _gUIView.gSkillView.skillNow[skill].SetActive(false);
        }
        else
        {
            _gUIView.gSkillView.skillUp[skill].SetActive(false);
            _gUIView.gSkillView.skillNow[skill].SetActive(true);
        }
    }
    
    //等级升级页面初始化
    public void ResetLevelUpInterface()
    {
        //将Attribute组件设置为不可见
        _gUIView.Attribute.SetActive(false);
        
        //重置名称和介绍栏
        _gUIView.LevelUpName.text = "";
        _gUIView.LevelUpIntroduce.text = "";
    }
    
    //技能界面初始化
    public void ResectSkillUpInterface()
    {
        //重置名称和介绍栏
        _gUIView.LevelUpName.text = "";
        _gUIView.LevelUpIntroduce.text = "";
        
        //将Skill组件设置为不可见
        _gUIView.gSkillView.Skill.SetActive(false);
    }
    
    //更新组队角色显示
    public void UpdateTeamMenberShow(int member)
    {
        _gUIView.gTeamView.TeamMember[member].SetActive(true);
        //更新头像
        _gUIView.gTeamView.teamMemberSprite[member].sprite =
            GCharacterManger.Instance.nowCharacter.attribute.CharacterSprite;
        //更新全身像
        _gUIView.gTeamView.teamMemberHead[member].sprite =
            GCharacterManger.Instance.nowCharacter.attribute.CharacterHeadSprite;
    }
    
    //重置组队角色显示
    public void resetTeamMemberShow(int member)
    {
        /*
        _gUIView.gTeamView.teamMemberSprite[member].sprite = null;
        _gUIView.gTeamView.teamMemberHead[member].sprite = null;
        */
        
        _gUIView.gTeamView.TeamMember[member].SetActive(false);
    }
    
    //打开武器界面
    public void OpenWeaponPanel()
    {
        _gUIView.WeaponPanel.SetActive(true);
    }
    
    //关闭武器界面
    public void CloseWeaponPanel()
    {
        _gUIView.WeaponPanel.SetActive(false);
    }

    public GUIView Get_guiViwe()
    {
        return _gUIView;
    }
    
    //金币变动时调用
    public void ChangeMoney(int change)
    {
        GMoney.Instance.ChangeCoins(change);
        UpdateCoinsShow();
    }
    
    
    
    
    
    //
    public bool CheckLevelUp()
    {
        return true;
    }
}