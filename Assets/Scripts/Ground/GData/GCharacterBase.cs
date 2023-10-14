using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GCharacterBase : MonoBehaviour
{
    public Attribute attribute;
    
    public Image TopImage;
    public String LevelUPIntroduce;
    public String SkillUpIntroduce;
    
    public int levelNeedCoins; //角色升级所需金币数
}
