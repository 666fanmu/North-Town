using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Managers;
using UnityEngine.SceneManagement;

public class GManger : MonoBehaviour
{
    public static GManger Instance;

    public int Place=-1;

    public int nowWeapon;

    public GameObject Setting;

    public TeamData _teamData;
    private int teamCount;

    public Sprite WeaponBackGroundSrpite;//武器背景图片

    public void Start()
    {
        Instance = GetComponent<GManger>();
        Place = -1;
        nowWeapon = -1;
        teamCount = 0;
        _teamData = FindObjectOfType<TeamData>();
        ResetTeamInterface();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Setting.activeSelf)
            {
                Setting.SetActive(false);
            }
            else
            {
                Setting.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 开始游戏切换场景
    /// </summary>
    public void LoadScene()
    {
        for (int i = 0; i < 4; i++)
        {
            if (_teamData.CharacterList[i] != null)
            {
                teamCount++;
            }
        }
        if (teamCount ==4)
        {
            SceneManager.LoadScene("Scenes/Main");
            teamCount = 0;
        }
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    public void QuickGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    /// <summary>
    /// 返回主菜单
    /// </summary>
    public void BackToStart()
    {
        SceneManager.LoadScene("Start");
    }
    
    public void MoneyTest()
    {
        GroundUIControl.Instance.ChangeMoney(100);
    }

    //等级升级
    public void LevelUpSet()
    {
        if (GMoney.Instance.coins>GCharacterManger.Instance.nowCharacter.levelNeedCoins)
        {
            GMoney.Instance.ChangeCoins(-GCharacterManger.Instance.nowCharacter.levelNeedCoins);
            GCharacterManger.Instance.nowCharacter.attribute.currentLevel += 1;
            GroundUIControl.Instance.UpdateCoinsShow();
        }
        GroundUIControl.Instance.ShowLevelUpInterface();
    }

    //技能升级
    public void SkillUpSet(int k)
    {
        if (GMoney.Instance.coins > GCharacterManger.Instance.nowCharacter.attribute.skills[k].skillNeedCoins[GCharacterManger.Instance.nowCharacter.attribute.skills[k].SkillLevel])
        {
            GCharacterManger.Instance.nowCharacter.attribute.skills[k].SkillLevel += 1;
            
            if (GCharacterManger.Instance.nowCharacter.attribute.skills[k].SkillLevel == 3)
            {
                GroundUIControl.Instance.ShowSkillUpInterface();
                GroundUIControl.Instance.changeSkillUpIntroduce(k,false);
            }
            else
            {
                GroundUIControl.Instance.ShowSkillUpInterface();
            }
            GMoney.Instance.ChangeCoins(-GCharacterManger.Instance.nowCharacter.levelNeedCoins);
            GroundUIControl.Instance.UpdateCoinsShow();
        }
    }
    
    //更新组队角色显示
    public void UpdateTeamMenber()
    {
        GroundUIControl.Instance.UpdateTeamMenberShow(Place);
    }
    
    //重置组队界面
    public void ResetTeamInterface()
    {
        for (int i = 0; i < 4; i++)
        {
            GroundUIControl.Instance.resetTeamMemberShow(i);
        }
    }
    
    //更新角色是否在队伍中的信息
    public void upDateroleInfo(int place)
    {
        if (GCharacterManger.Instance.TeamMember[place] != -1)
        {
            GCharacterManger.Instance.isInTeam[GCharacterManger.Instance.TeamMember[place]] = false;
        }
        //Destroy(_teamData.CharacterList[place].gameObject);
        _teamData.CharacterList[place] = null;
        GCharacterManger.Instance.TeamMember[place] = -1;
    }
    
    //调整武器界面状态
    public void ChangeWeaponPanel()
    {
        if (GroundUIControl.Instance.Get_guiViwe().WeaponPanel.activeSelf)
        {
            GroundUIControl.Instance.CloseWeaponPanel();
        }
        else
        {
            GroundUIControl.Instance.OpenWeaponPanel();
        }
    }
    
    public void RectTeamMember(int member)
    {
        PlayerManager.Instance.CharacterList[member] = null;
        
    }



}
