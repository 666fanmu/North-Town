using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GSkillUpSet : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public int skill;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        //如果技能未满级，则升级技能，能否升级的判断在SkillUpSet中
        if (GCharacterManger.Instance.nowCharacter.attribute.skills[skill].SkillLevel < 3)
        {
            GManger.Instance.SkillUpSet(skill);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GCharacterManger.Instance.hasCharacter)
        {
            if (GCharacterManger.Instance.nowCharacter.attribute.skills[skill].SkillLevel < 3)
            {
                GroundUIControl.Instance.changeSkillUpIntroduce(skill,true);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GCharacterManger.Instance.hasCharacter)
        {
            if (GCharacterManger.Instance.nowCharacter.attribute.skills[skill].SkillLevel < 3)
            {
                GroundUIControl.Instance.changeSkillUpIntroduce(skill,false);
            }
        }
            
            
    }
}
