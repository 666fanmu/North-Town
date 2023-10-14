using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class GRoleSet : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Text Name;
    public Text Level;
    public Image HeardImage;

    public Attribute ThisAttribute;
    public CharacterBase thisCharacterBase;
    public int thisRole;

    private bool InThisRole;//用于判断鼠标是否悬浮在物体上
    //public bool isInTeam;

    public TeamData teamData;

    private void Awake()
    {
        teamData = FindObjectOfType<TeamData>();
    }

    public void Start()
    {
        //更新角色栏的相关显示
        Name.text = ThisAttribute.Name;
        Level.text = ThisAttribute.currentLevel.ToString();
        HeardImage.sprite = ThisAttribute.CharacterHeadSprite;
        InThisRole = false;
    }

    /// <summary>
    /// 鼠标点击更新选中的角色信息
    /// </summary>
    /// <param SkillName="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        GCharacterManger.Instance.thisRole = thisRole;
        GCharacterManger.Instance.hasCharacter = true;
        GCharacterManger.Instance.nowCharacter = thisCharacterBase;
        GroundUIControl.Instance.ShowLevelUpInterface();//更新等级升级界面
        //GroundUIControl.Instance.ShowSkillUpInterface();//更新技能升级界面
        //如果正在选择角色，并且角色未在队伍中
        if (GManger.Instance.Place != -1&&!GCharacterManger.Instance.isInTeam[thisRole])
        {
            GameObject a = Instantiate(thisCharacterBase.gameObject, transform.position, Quaternion.identity);
            a.transform.parent = teamData.gameObject.transform;
            teamData.CharacterList[GManger.Instance.Place] = a.GetComponent<CharacterBase>();
            GCharacterManger.Instance.isInTeam[thisRole] = true;
            GCharacterManger.Instance.TeamMember[GManger.Instance.Place] = thisRole;
            GManger.Instance.UpdateTeamMenber();
            GManger.Instance.Place = -1;
        }
    }

    public void Update()
    {
        if (Input.GetMouseButton(1)&&InThisRole)
        {
            GCharacterManger.Instance.thisRole = thisRole;
            GCharacterManger.Instance.hasCharacter = true;
            GCharacterManger.Instance.nowCharacter = thisCharacterBase;
            GroundUIControl.Instance.upDateHeroInterface();
            GroundUIControl.Instance.OpenHeroInterface();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InThisRole = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InThisRole = false;
    }
}
