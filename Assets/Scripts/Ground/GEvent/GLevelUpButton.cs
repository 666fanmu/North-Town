using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GLevelUpButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GCharacterManger.Instance.nowCharacter.attribute.currentLevel < 6)
        {
            GroundUIControl.Instance.changeLevelUpUpShow();   
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GCharacterManger.Instance.nowCharacter.attribute.currentLevel < 6)
        {
            GroundUIControl.Instance.changeLevelUpNowShow();
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (GCharacterManger.Instance.nowCharacter.attribute.currentLevel < 6)
        {
            GCharacterManger.Instance.nowCharacter.attribute.currentLevel += 1;//升级
            
            GroundUIControl.Instance.ShowLevelUpInterface();//重置Attribute显示
            
            if (GCharacterManger.Instance.nowCharacter.attribute.currentLevel >= 6)
            {
                GroundUIControl.Instance.changeLevelUpNowShow(); 
            }
            else
            {
                GroundUIControl.Instance.changeLevelUpUpShow();
            }
        }
        
    }
}
