using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillUiInfo : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public int SkillNumber;

    private string path;
    private TextAsset info;
    public string infoStr = "";

    public void Start()
    {
        path = "SkillIntroduce/" + GCharacterManger.Instance.thisRole.ToString() + "/" + SkillNumber.ToString();
        info=Resources.Load(path) as TextAsset;
        infoStr = info.text.ToString();
    }

    public void Update()
    {
        path = "SkillIntroduce/" + GCharacterManger.Instance.thisRole.ToString() + "/" + SkillNumber.ToString();
        info=Resources.Load(path) as TextAsset;
        infoStr = info.text.ToString();
    }

    
    public void OnPointerEnter (PointerEventData eventData){
        Debug.Log("进入FDUiTip");
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
