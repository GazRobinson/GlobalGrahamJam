using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleBin : MonoBehaviour {
    public static RecycleBin instance;
    public System.Action OnFileTrash;
    RectTransform rectt;
Rect box;
    void Awake(){
        instance = this;
        rectt = GetComponent<RectTransform>();
		box = new Rect(rectt.anchoredPosition-(rectt.sizeDelta*0.5f), rectt.sizeDelta);
    }
    void Update()
    {
    }
	public void SetDraggable(DragHook drag){
        drag.Released += Release;
    }
	void Release(){
		if (DragHook.selected != null)
        {
            if( box.Contains( DragHook.selected.rectTransform.anchoredPosition)){
                DragHook.selected.gameObject.SetActive(false);
				if(OnFileTrash!=null){
                    OnFileTrash();
                }
            }
        }
        DragHook.selected = null;
    }
}
