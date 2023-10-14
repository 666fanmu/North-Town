
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Managers;
using UnityEngine;

public class Propbase : MonoBehaviour
{
    // Start is called before the first frame update
    public PropsAttribute propsAttribute;
    public bool isActive;
    [Header("道具数量")]
    public int Amount;

    public void Awake()
    {
        Initialize();
    }
    

    public void Initialize()
    {
        this.isActive = true;
        Amount = 1;
    }
    [Description("使用道具")]
    public void Oncast(CharacterBase characterBase)
    {
        if (isActive)
        {
            characterBase.Blood+=propsAttribute.Blood;
            characterBase.LowAttack += propsAttribute.LowAttack;
            characterBase.HighAttack += propsAttribute.HighAttack;
            characterBase.Defence += propsAttribute.Defence;
            characterBase.Dodge += propsAttribute.Dodge;
            characterBase.Precision += propsAttribute.Precision;
            characterBase.CriticalHit += propsAttribute.CriticalHit;
            characterBase.Speed += propsAttribute.Speed;
            
            foreach (var buff in propsAttribute.BuffList)
            {
                BuffManager.Instance.AddBuff(characterBase,buff.name);
            }
            isActive = false;
            Debug.Log("道具使用成功");
        }
    }
    [Description("销毁buff带来的增益")]
    public void Destoryeffect()
    {
        
    }

    public void Destroythis()
    {
        Destoryeffect();
        Destroy(this.gameObject);
    }
}
