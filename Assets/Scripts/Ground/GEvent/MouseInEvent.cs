using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseInEvent : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public float zoomSize=1.1f;
    public GameObject Message;
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(zoomSize, zoomSize, -10.0f);
        Message.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
        Message.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
