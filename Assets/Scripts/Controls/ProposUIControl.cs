using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProposUIControl : MonoBehaviour
{
    public static ProposUIControl Instance;

    private PropsView _propsView;

    //public int[] PropsNumber; 
    // Start is called before the first frame update
    public void Start()
    {
        Instance = this;
        _propsView = FindObjectOfType<PropsView>();
        UpdateProposShow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //更新道具数量显示
    public void UpdateProposShow()
    {
        for (int i = 0; i < _propsView.PropsNumberShow.Length; i++)
        {
            _propsView.PropsNumberShow[i].text = PropsManager.Instance.PropList[i].Amount.ToString();
        }
    }
}
