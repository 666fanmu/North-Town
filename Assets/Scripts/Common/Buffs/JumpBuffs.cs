using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
[Description("击晕效果buff")]
public class JumpBuffs : buff
{
    //public int JumpDistance;

    public override void SettleBuffOnce(CharacterBase characterBase)
    {
        if (characterBase.isAbletoMove == true)
        {
            characterBase.isAbletoMove = false; 
        }
    }

    public override void Oncast()
    {

    }

    public override void DestroyEffect(CharacterBase characterBase)
    {
        characterBase.isAbletoMove = true;
    }
    
    
}
