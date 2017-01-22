using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHook : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {
    public static DragHook selected;
    public RectTransform rectTransform;
    public System.Action Released;
    Vector2 offset = Vector2.zero;
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = ScreenController.Instance.GetScreenPos(Input.mousePosition) + offset;// (Vector2)Input.mousePosition - (ScreenController.GetScreenSize()*0.5f);
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        selected = this;
        offset = rectTransform.anchoredPosition - ScreenController.Instance.GetScreenPos(Input.mousePosition);
        RecycleBin.instance.SetDraggable(this);
    }


    // Use this for initialization
    void Start () {
        rectTransform = GetComponent<RectTransform>();
    }   

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if(Released!=null){
            Released();
        }
    }
}
