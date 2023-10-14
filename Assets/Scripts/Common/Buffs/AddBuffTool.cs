using System.Collections;
using System.Collections.Generic;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

public class AddBuffTool : MonoBehaviour
{
    [HideInInspector]
    public AddBuffTool Instance;
    // Start is called before the first frame update
    [Header("需要注册的buff")]
    public GameObject thebuff;
 //   [Header("buff名字")]
 //   public string BuffName;
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterBuff()
    {
        if (thebuff != null)
        {
            if (this.gameObject.GetComponent<BuffManager>().BuffList.Find(x => x == thebuff.GetComponent<buff>()) == null)
            {
                string realname;
                GameObject ggj=Instantiate(thebuff, this.transform);
                if (thebuff.GetComponent<buff>().name != null)
                {
                realname=thebuff.GetComponent<buff>().name;
                }
                else
                {
                    realname = thebuff.gameObject.name;
                }
                ggj.gameObject.name = realname;
                ggj.GetComponent<buff>().name = realname;  
                this.gameObject.GetComponent<BuffManager>().BuffList.Add(ggj.GetComponent<buff>());
                Debug.Log("注册buff成功");
            }
            else
            {
                Debug.LogError("buff重复");
            }
        }
        else
        {
            Debug.LogError("buff不能为空");
        }

        
        /*
        if (!string.IsNullOrWhiteSpace(BuffName))
        {
            Debug.Log("Assets/Resources/Buffs/"+BuffName+".prefab");
            GameObject pref = Instantiate("Assets/Resources/Buffs/"+BuffName+".prefab",this.transform);
            if (pref != null)
            {
                pref.transform.SetParent(this.transform);
                //Instantiate(pref, this.transform);
                this.gameObject.GetComponent<BuffManager>().BuffList.Add
                    (pref.GameObject().GetComponent<buff>());
                Debug.Log("注册buff成功");
            }
            else
            {
                Debug.LogError("资源不存在");
            }
            
            if (this.gameObject.GetComponent<BuffManager>().BuffList.Find(x => 
                    { return x.SkillName == BuffName; })==null)
            {
                
            }
            else
            {
                Debug.LogError("buff名字重复");
            }
        
        }
        else
        {
            Debug.LogError("buff名字不能为空");
        }
        */
        
    }
    
}
