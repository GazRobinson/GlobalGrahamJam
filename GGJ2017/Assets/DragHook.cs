using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHook : MonoBehaviour, IDragHandler {
    RectTransform rectTransform;
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Debug.Log("Pie");
        rectTransform.anchoredPosition = ScreenController.Instance.GetScreenPos(Input.mousePosition);// (Vector2)Input.mousePosition - (ScreenController.GetScreenSize()*0.5f);
    }

    /*void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Pie");
		Text
    }*/

    // Use this for initialization
    void Start () {
        rectTransform = GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
