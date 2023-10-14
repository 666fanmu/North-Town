using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;




public enum camp
{
    Player,
    Enemy
}

[CreateAssetMenu]
public class Attribute : ScriptableObject
{
    public Sprite CharacterHeadSprite;
    public Sprite CharacterSprite;
    public string Name;
    public camp Camp;
    
    public int currentLevel;
    //判断是否升级
    [HideInInspector]
    public bool ifLevelUp=false;

    

    /// <summary>
    /// 单回合行动次数
    /// </summary>
    public int AttackNumber;
    
    
    public List<AttributeList> AttributeList;
    public Skill[] skills;
}
[System.Serializable]
public class AttributeList
{
    public float Blood,//血量
        HighAttack,//攻击力最大值
        LowAttack,//攻击力最小值
        Defence,//防御力
        Dodge,//闪避率
        Precision,//精准度
        CriticalHit,//暴击率
        Speed;//速度

    //todo
    //抗性
    public int BleedResist,//流血抗性
        DizzyResist,//眩晕抗性
        DebuffResist,//减益抗性
        MoveResist;//位移抗性


    public int levelNeedCoins; //角色升级所需金币数



}
[System.Serializable]
public class Skill
{
    [FormerlySerializedAs("name")] public string SkillName;
    /// <summary>
    /// 使用时-1计算
    /// </summary>
    public int SkillLevel;
    //技能升级所需金币数
    public int[] skillNeedCoins;

    public camp skillTarget;
    [TextArea]
    public string description;
    //技能图标
    public Sprite icon;
    [Header("可使用次数")]
    public int usableTime;//
    [Header("基础精准")]
    public float[] precision;
    [Header("暴击修正")]
    public float criticalHitCorrection;
    [Header("是否为攻击技能")]
    public bool isOffend;//
    [Header("是否为群体技能")]
    public bool isMassAttacks;
    [Header("影响人数")]
    public int MassAttacksNumbers;
    [Header("伤害修正，1+伤害修正+额外加成，为最终的数值加成")]
    public float damageCorrection;//
    [Header("允许自身站位")]
    public int[] AvailableStandPosition;
    [Header("允许被攻击对象站位")]
    public int[] AvailableAttackPosition;
    public SkillEffect[] effects;

}

[System.Serializable]
public class SkillEffect
{
    
    public enum EffectType { Damage, Heal, Buff, Debuff } // 根据需要定义更多类型
    public EffectType type;
    [Header("技能效果值,仅damage类型有效")]
    public SkillValue[] value;
    [Header("技能加的buff,damage类型无效")]
    public GameObject buffs;
}
[System.Serializable]
public class SkillValue
{
    [Header("描述")]
    public string Dsicription;
    [Header("值")]
    public float value;// 其他属性，例如持续时间、目标等
}
