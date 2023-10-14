using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UiTipInfo : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {
	public string infoStr = "";
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
