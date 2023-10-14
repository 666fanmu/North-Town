using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Controls;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GUIView : MonoBehaviour
{
    //等级升级界面Attribute组件
    public GAttributeView gAttributeView;
    public GameObject Attribute;
    
    //技能升级界面Introduce相关组件
    public GSkillView gSkillView;
    
    //组队界面相关组件
    public GTeamView gTeamView;
    
    //角色详情界面相关组件
    public GHeroView gHeroView;

    //等级升级面板
    public GameObject LevelUpInterface;

    //技能升级面板
    public GameObject SkillUpInterface;

    //角色详情面板
    public GameObject HeroInterface;
    
    //组队面板
    public GameObject TeamInterface;

    //金币显示组件
    public GameObject Money;
    public Text Coins;
    
    //武器界面
    public GameObject WeaponPanel;

    //等级升级面板相关组件
    public Text LevelUpName;
    public Text LevelUpIntroduce;
    public Image LevelUpHeadImage;
    public Image LevelUpTopImage;
    public Image LevelUpBigImage;
    public GameObject HeroShow;
    
    //技能升级面板相关组件
    public Text SkillUpName;
    public Text SkillUpIntroduce;
    public Image SkillUpHeadImage;
    public Image SkillUpTopImage;
    public Image SkillUpBigImage;

    public void Awake()
    {
        gAttributeView = FindObjectOfType<GAttributeView>();
        gSkillView = FindObjectOfType<GSkillView>();
        gTeamView = FindObjectOfType<GTeamView>();
        gHeroView = FindObjectOfType<GHeroView>();
    }
}
