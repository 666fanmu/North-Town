using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

public class GainBuffs : buff
{    
    
    [Header("下面所列均为增加值")]
    public int Blood,//血量
        HighAttack,//攻击力最大值
        LowAttack,//攻击力最小值
        Defence,//防御力
        Dodge,//闪避率
        Precision,//精准度
        CriticalHit,//暴击率
        Speed;//速度
    [HideInInspector]
    public bool isSettled;

    [HideInInspector]
   // public int 
    private void Start()
    {
        Initialize();
        isSettled = false;
        Debug.Log("初始化");
    }

    public override void Oncast()
   {
       
   }
    public override void SettleBuffOnce(CharacterBase characterBase)
    {
        characterBase.Blood += this.Blood;
        if (!isSettled)
        {
            characterBase.LowAttack += this.LowAttack;
            characterBase.HighAttack += this.HighAttack;
            characterBase.Defence += this.Defence;
            characterBase.Dodge += this.Dodge;
            characterBase.Precision += this.Precision;
            characterBase.CriticalHit += this.CriticalHit;
            characterBase.Speed += this.Speed;
            isSettled = true;
        }

        Debug.Log("增益buff生效");
    }

    public override void DestroyEffect(CharacterBase characterBase)
    {
        characterBase.LowAttack -= this.LowAttack;
        characterBase.HighAttack -= this.HighAttack;
        characterBase.Defence -= this.Defence;
        characterBase.Dodge -= this.Dodge;
        characterBase.Precision -= this.Precision;
        characterBase.CriticalHit -= this.CriticalHit;
        characterBase.Speed -= this.Speed;
    }
    
}







/// <summary>
/// debuff
/// </summary>
/*
public class GainDeBuffs : debuff
{
    private void Start()
    {
        round = gainDeBuffEffect.round;
    }

    public GainBuffEffect gainDeBuffEffect;
    public override void SettleDeBuffOnce(CharacterBase characterBase)
    {
        characterBase.Blood -= gainDeBuffEffect.Blood;
        characterBase.Attack -= gainDeBuffEffect.Attack;
        characterBase.Defence -= gainDeBuffEffect.Defence;
        characterBase.Dodge -= gainDeBuffEffect.Dodge;
        characterBase.Precision -= gainDeBuffEffect.Precision;
        characterBase.CriticalHit -= gainDeBuffEffect.CriticalHit;
        characterBase.Speed -= gainDeBuffEffect.Speed;
        Debug.Log("减益buff生效");
    }
}
*/
