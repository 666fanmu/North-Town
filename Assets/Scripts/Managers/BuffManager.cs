using System;
using System.Collections;
using System.Collections.Generic;
using Controls;
using Unity.VisualScripting;
using UnityEngine;

namespace Managers
{
    public class BuffManager : MonoBehaviour
    {
        public static BuffManager Instance;
        public List<buff> BuffList = new List<buff>();
        public PlayerUIControl playerUIControl;
        private void Awake()
        {
            Instance = this;
            playerUIControl=FindObjectOfType<PlayerUIControl>();
        }

        private void Start()
        {

        }
        
        public void AddBuff(CharacterBase character,string thebuffname)
        {
            int index=BuffList.FindIndex(x => x.name == thebuffname);
            
            if (index!=-1)
            {
                GameObject buffobj = Instantiate(BuffList[index].gameObject);

                character.Buffs.Add(buffobj.gameObject.GetComponent<buff>());
                buffobj.transform.SetParent(character.transform);
                playerUIControl.WriteBuffPanel(buffobj.gameObject.GetComponent<buff>(),buffobj.gameObject.GetComponent<buff>().maxround);
                
            }
            else
            {
                Debug.LogError("没有这个buff");
            }
        }
        public void AddDeBuff(CharacterBase character,string thebuffname)
        {
           
            
            
            int index=BuffList.FindIndex(x => x.name == thebuffname);
            
            if (index!=-1)
            {
                GameObject buffobj = Instantiate(BuffList[index].gameObject);

                character.Debuffs.Add(buffobj.gameObject.GetComponent<buff>());
                buffobj.transform.SetParent(character.transform);
                playerUIControl.WriteBuffPanel(buffobj.gameObject.GetComponent<buff>(),buffobj.gameObject.GetComponent<buff>().maxround);
                
            }
            else
            {
                Debug.LogError("没有这个buff");
            }

        }
        
    }


}