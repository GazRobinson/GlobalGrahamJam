using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WindowDragger : MonoBehaviour, IPointerDownHandler, IDragHandler {
    public System.Action<Vector2> Drag;
    Vector2 Offset = Vector2.zero;
    RectTransform parentWindow;
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
		if(Drag!=null){
            parentWindow.anchoredPosition = ScreenController.Instance.GetScreenPos(Input.mousePosition) + Offset;
          //  Drag(ScreenController.Instance.GetScreenPos(Input.mousePosition));

        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Offset = parentWindow.anchoredPosition - ScreenController.Instance.GetScreenPos(Input.mousePosition);
    }

    // Use this for initialization
    void Start () {
        parentWindow = transform.parent.GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
