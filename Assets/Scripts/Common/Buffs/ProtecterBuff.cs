using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtecterBuff : buff
{
    public new void Initialize()
    {
        this.round = this.maxround;
        if (this.transform.parent.GetComponent<CharacterBase>())
        {
            this.transform.parent.GetComponent<CharacterBase>().protect_Status = CharacterBase.Protect_Status.PROTECTER;
        }
    }
    public override void SettleBuffOnce(CharacterBase characterBase)
    {
       
        characterBase.protect_Status = CharacterBase.Protect_Status.PROTECTER;
        
    }

    public override void Oncast()
    {

    }

    public override void DestroyEffect(CharacterBase characterBase)
    {
        characterBase.protect_Status = CharacterBase.Protect_Status.NONE;
    }
}
