using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PropsAttribute", menuName = "PropsAttribute")]
public class PropsAttribute : ScriptableObject
{
    // Start is called before the first frame update
    public string name;
    public Sprite icon;
    public string description;
    [Header("每个物品是否单独占用背包格子，也就是一个物品占用一个格子")]
    public bool isBigSize;
    [Header("永久增益")] 
    public int Blood;
    public int HighAttack;//攻击力最大值
    public int LowAttack;//攻击力最小值
    public int Defence;//防御力
    public int Dodge;//闪避率
    public int Precision;//精准度
    public int CriticalHit;//暴击率
    public int Speed;//速度
    [Header("增加的buff")] 
    public List<buff> BuffList;
}

