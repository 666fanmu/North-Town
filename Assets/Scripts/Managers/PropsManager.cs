

using System;
using System.Collections;
using System.Collections.Generic;
using Controls;
using Managers;
using UnityEngine;

public class PropsManager : MonoBehaviour
{
    public PlayerUIControl playerUIControl;
    public List<Propbase> PropList;
    [HideInInspector]
    public static PropsManager Instance;
    //public int[] PropsNumber;
    // Start is called before the first frame update
    private void Awake()
    {
       
        Instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddProp(Propbase prop)
    {
        int index = PropList.FindIndex(x => x == prop);
        if (index==-1)
        {
            PropList.Add(prop);            
        }

        else
        {
            if(!PropList[index].propsAttribute.isBigSize)
            PropList[index].Amount++;
            else
            {
                
                PropList.Add(prop);
            }
        }
        playerUIControl.SetPropsUI(0);
    }
    public void RemoveProp(Propbase prop)
    {
        int index = PropList.FindIndex(x => x == prop);
        if (index != -1)
        {
            if (PropList[index].Amount > 1)
            {
                PropList[index].Amount--;
                prop.Destoryeffect();
            }
            else if(PropList[index].Amount == 1)
            {
                PropList.Remove(prop);
                prop.Destroythis();
            }
        }
        else
        {
            Debug.LogError("没有这个道具");
        }
    }
    public void RemoveProp(int index)
    {
        if (index != -1)
        {
            if (PropList[index].Amount >= 1)
            {
                PropList[index].Amount--;
                //销毁prop带来的增益
                PropList[index].Destoryeffect();
                ProposUIControl.Instance.UpdateProposShow();
            }
            /*
            else if(PropList[index].Amount == 1)
            {
                Propbase p = PropList[index];
                //PropList.Remove(p);
                //p.Destroythis();
            }
            */
        }
        else
        {
            Debug.LogError("没有这个道具");
        }
    }

    public void UseProp(Propbase prop,CharacterBase characterBase)
    {
        int index = PropList.FindIndex(x => x == prop);
        if (index != -1)
        {
            prop.Oncast(characterBase);
            RemoveProp(index);
            Debug.Log(characterBase.name+"使用了"+prop.propsAttribute.name);
        }
        else
        {
            Debug.LogError("没有这个道具");
        }
    }

    public void TestProp()
    {
        UseProp(PropList[0],PlayerManager.Instance.CharacterList[GameManager.Instance.GetCharacterID()]);
    }

    public void Damage()
    {
        UseProp(PropList[1],PlayerManager.Instance.CharacterList[GameManager.Instance.GetCharacterID()]);
    }

    public void Dodge()
    {
        UseProp(PropList[2],PlayerManager.Instance.CharacterList[GameManager.Instance.GetCharacterID()]);
    }

    public void Speed()
    {
        UseProp(PropList[3],PlayerManager.Instance.CharacterList[GameManager.Instance.GetCharacterID()]);
    }

    public void Precise()
    {
        UseProp(PropList[4],PlayerManager.Instance.CharacterList[GameManager.Instance.GetCharacterID()]);
    }

    public void Damage_Big()
    {
        UseProp(PropList[5],PlayerManager.Instance.CharacterList[GameManager.Instance.GetCharacterID()]);
    }

    public void Defence()
    {
        UseProp(PropList[6],PlayerManager.Instance.CharacterList[GameManager.Instance.GetCharacterID()]);
    }

    public void Dodge_Big()
    {
        UseProp(PropList[7],PlayerManager.Instance.CharacterList[GameManager.Instance.GetCharacterID()]);
    }

    public void Speed_Big()
    {
        UseProp(PropList[8],PlayerManager.Instance.CharacterList[GameManager.Instance.GetCharacterID()]);
    }

    public void Precision_Big()
    {
        UseProp(PropList[9],PlayerManager.Instance.CharacterList[GameManager.Instance.GetCharacterID()]);
    }
}
