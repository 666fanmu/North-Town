using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleSkillIntroduce : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private string path;
    private TextAsset info;
    [SerializeField] private int skillNumber;
    public string infoStr = "";
    
    public void Start()
    {
        path = "SkillIntroduce/" +
               GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum].Role.ToString() +"/"+
               skillNumber.ToString();
        Debug.Log(path);
        
        info=Resources.Load(path) as TextAsset;
        
        Debug.Log(info.text);
        infoStr = info.text.ToString();
    }
//
    public void Update()
    {
        path = "SkillIntroduce/" +
               GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum].Role.ToString() +"/"+
               skillNumber.ToString();
        info=Resources.Load(path) as TextAsset;
        infoStr = info.text.ToString();
    }

    public void OnPointerEnter (PointerEventData eventData){
        FDUiTip.thisC.Show (infoStr,true);
    }
    public void OnPointerExit (PointerEventData eventData){
        FDUiTip.thisC.Hide (true);
    }
    private void OnMouseOver(){
        if(!FDUiTip.thisC.GetActive()){
            if(Vector3.Distance(this.transform.position,Camera.main.transform.position)<=FDUiTip.thisC.mouseCheckDis){
                FDUiTip.thisC.Show (infoStr,false);
            }
        }
    }
    private void OnMouseExit(){
        if(FDUiTip.thisC.GetActive()){
            FDUiTip.thisC.Hide (false);
        }

    }
}
