using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuffLabel : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Text Description;
    public Text Round;
    private void Awake()
    {
        this.Description.gameObject.SetActive(false);
    }

    public void Init()
    {
        this.Description.gameObject.SetActive(false);        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.Description.gameObject.SetActive(true);        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.Description.gameObject.SetActive(false);
            
            
    }
}
