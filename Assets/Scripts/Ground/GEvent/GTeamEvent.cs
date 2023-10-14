using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GTeamEvent : MonoBehaviour
{
    public int place;
    public Text ButtonName;
    public bool hasChose=false;

    public void teamButtonEvent()
    {
        if (!hasChose)
        {
            GManger.Instance.Place = place;
            ButtonName.text = "取消";
            hasChose = true;
        }
        else
        {
            GManger.Instance.Place = -1;
            ButtonName.text = "选择角色";
            GManger.Instance.upDateroleInfo(place);
            GroundUIControl.Instance.resetTeamMemberShow(place);
            hasChose = false;
        }
    }

    
    /*
    public void Update()
    {
        if (GManger.Instance._teamData.CharacterList[place]==null&& GManger.Instance.Place!=place)
        {
            ButtonName.text = "选择角色";
            hasChose = false;
        }
    }
    */
}
