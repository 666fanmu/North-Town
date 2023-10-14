using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "new item",menuName = "Inventory/new item")]
public class item : ScriptableObject
{
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
  
  //todo
  //抗性

[TextArea]
  public string Description;
/// <summary>
/// 是否能被装备
/// </summary>
  public bool ifEquiped;
}
