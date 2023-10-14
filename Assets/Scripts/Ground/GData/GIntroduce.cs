using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GIntroduce : MonoBehaviour
{
    //public String[][] Warrier=new string[4][];

    public List<roleSkill> allRole;

    public void Start()
    {
        /*
        Warrier[0][0] = "基础精准：90%\n暴击修正：5%";
        Warrier[0][1] = "基础精准：100%\n伤害修正：80%\n有120%基础概率眩晕敌人";
        Warrier[1][0] = " ";
        Warrier[2][0] = " ";
        Warrier[3][0] = " ";
        
        */
        
    }
    
    public class roleSkill
    {
        public string Skill1;
        public string Skill2;
        public string Skill3;
        public string Skill4;
    }
}
