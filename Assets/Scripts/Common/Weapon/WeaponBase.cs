using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    //储存武器数据
    public item Item;
    public string ItemName;

    public Sprite WeaponSprite;
    public int CurrentLevel;
    //属性
    public int Blood,//血量
        LowAttack,//最小攻击力
        HighAttack,//最大攻击力
        Defence,//防御力
        Dodge,//闪避率
        Precision,//精准度
        CriticalHit,//暴击率
        Speed;//速度
    void Start()
    {
        SetStates();
    }

    public void SetStates()
    {
        Blood = Item.Blood;
        LowAttack = Item.LowAttack;
        HighAttack = Item.HighAttack;
        Defence = Item.Defence;
        Dodge = Item.Dodge;
        Precision = Item.Precision;
        CriticalHit = Item.CriticalHit;
        Speed = Item.Speed;
        Debug.Log("has Load Weapon");
    }
    
    
}
