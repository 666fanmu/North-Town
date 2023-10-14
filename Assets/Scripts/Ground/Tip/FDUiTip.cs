using UnityEngine;
using System.Collections;

public class FDUiTip : MonoBehaviour {
	public static FDUiTip thisC;
	private RectTransform childRectTra;
	private float beforePivotRefreshTime;
	private bool isUgui;
	public float pivotIntervalTime=0.1f;
	public float mouseCheckDis=10f;
	private void Awake(){
		thisC=this;
		childRectTra = this.transform.GetChild (0).GetComponent<RectTransform>();
		beforePivotRefreshTime = 0f;
		isUgui = false;
		DontDestroyOnLoad (this.gameObject);
	}
	private void Start(){
		childRectTra.gameObject.SetActive (false);
	}
	private void Update(){
		if(childRectTra.gameObject.activeSelf){
			if(Time.time>=beforePivotRefreshTime+pivotIntervalTime){
				beforePivotRefreshTime=Time.time;
				SetPivot();
			}
			SetPos();
		}
	}
	private void SetPivot(){
		int tempPivotX=((Input.mousePosition.x<=Screen.width/2f)?0:1);
		int tempPivotY=((Input.mousePosition.y<=Screen.height/2f)?0:1);
		if(childRectTra.pivot.x!=tempPivotX||childRectTra.pivot.y!=tempPivotY){
			childRectTra.pivot=new Vector2(tempPivotX,tempPivotY);
		}
	}
	private void SetPos(){
		childRectTra.position = Input.mousePosition;
	}
	public bool GetActive(){
		return childRectTra.gameObject.activeSelf;
	}
	public void Show(string theInfoStr,bool theIsUgui){
		//如果已显示Ugui,且将要显示的不是Ugui则忽略
		if(!theIsUgui&&(isUgui&&childRectTra.gameObject.activeSelf))return;
		childRectTra.GetChild (0).GetComponent<UnityEngine.UI.Text> ().text = theInfoStr;
		childRectTra.gameObject.SetActive (true);
		isUgui = theIsUgui;
		beforePivotRefreshTime=Time.time;
		SetPivot();
		SetPos();
	}
	public void Hide(bool theIsUgui){
		//如果已显示Ugui,且将要显示的不是Ugui则忽略
		if(!theIsUgui&&(isUgui&&childRectTra.gameObject.activeSelf))return;
		childRectTra.GetChild (0).GetComponent<UnityEngine.UI.Text> ().text = "";
		childRectTra.gameObject.SetActive (false);
	}
}
