using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


/*
[System.Serializable]
public abstract class debuff:MonoBehaviour
{
    public int round;
    public abstract void SettleDeBuffOnce(CharacterBase characterBase);
}
*/

/// <summary>
/// buff
/// </summary>
[System.Serializable]
public abstract class buff:MonoBehaviour
{
    [Header("buff名字")]
    public string name;
    [Header("buff描述")]
    public string description;
    [Header("buff图标")]
    public Sprite Sprite;
    [Header("buff回合数")]
    public int maxround;
    [Header("请勿修改")]
    public int _size;
    [Header("请勿修改")]    
    public GameObject bufflabel;
    public abstract void SettleBuffOnce(CharacterBase characterBase);
    public abstract void Oncast();

    public void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        this.round = this.maxround;
    }
    [Description("销毁buff带来的增益")]
    public abstract void DestroyEffect(CharacterBase characterBase);

    public void destroythis(CharacterBase characterBase)
    {
        DestroyEffect(characterBase);
        Destroy(this.gameObject);
    }


    [HideInInspector]

    public int round;
}


