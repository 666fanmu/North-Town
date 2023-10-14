using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelUpEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    public float zoomSize = 1.1f;
    private GUIView gUIView;

    public void Awake()
    {
        gUIView = FindObjectOfType<GUIView>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gUIView.LevelUpInterface.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(zoomSize, zoomSize, -10.0f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }
}