using System.Collections;
using System.Collections.Generic;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

public class ChangePlaceBuff : buff
{
    [Header("位移方向，选了正向，不选反向")]
    public bool isFront;
    void Start()
    {
        if(this.GetComponentInParent<CharacterBase>()==true)
        GameManager.Instance.SetChangePlaceBuff(this.GetComponentInParent<CharacterBase>(),!isFront);        
    }
    // Start is called before the first frame update
    public override void SettleBuffOnce(CharacterBase characterBase)
    {
        
        
    }

    public override void Oncast()
    {
        
    }

    public override void DestroyEffect(CharacterBase characterBase)
    {

    }
}
