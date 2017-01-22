using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WindowState{
	Open,
	Opening,
	Closed,
	Closing
}
public class UIWindow : MonoBehaviour {


    public Vector2 targetSize = new Vector2(540f, 256f);
    protected WindowState state = WindowState.Closed;
    protected RectTransform rectTransform;
    protected float tweenTime = 0.3f;
    private WindowDragger dragger;
    public virtual void Open(){
		if(state == WindowState.Closed || state == WindowState.Closing){
            iTween.Stop(gameObject);
            rectTransform.sizeDelta = new Vector2(0.0f, 90.0f);
            state = WindowState.Opening;
			iTween.ValueTo(gameObject, iTween.Hash("from", rectTransform.sizeDelta, "to", new Vector2(targetSize.x, rectTransform.sizeDelta.y), "time", tweenTime, "onupdatetarget", gameObject, "onupdate", "UpdateScale", "oncompletetarget", gameObject, "oncomplete", "ScaleY"));	
        }

	}
	public virtual void Close(){
		if(state == WindowState.Open || state == WindowState.Opening){
			iTween.Stop(gameObject);
            state = WindowState.Closing;
			iTween.ValueTo(gameObject, iTween.Hash("from", rectTransform.sizeDelta, "to", new Vector2(0.0f, 0.0f), "time", tweenTime, "onupdatetarget", gameObject, "onupdate", "UpdateScale", "oncompletetarget", gameObject, "oncomplete", "FinishedClose"));	
        }

	}
	protected void FinishedOpen(){
        state = WindowState.Open;
        OnFinishedOpen();
    }
	protected void FinishedClose(){
		state = WindowState.Closed;
		OnFinishedClose();
	}
	protected virtual void OnFinishedClose(){

	}
	protected virtual void OnFinishedOpen(){

	}
	void ScaleY(){
		iTween.ValueTo(gameObject, iTween.Hash("from", rectTransform.sizeDelta, "to", new Vector2(targetSize.x, targetSize.y), "time", tweenTime, "onupdatetarget", gameObject, "onupdate", "UpdateScale", "oncompletetarget", gameObject, "oncomplete", "FinishedOpen"));
	}
	void UpdateScale(Vector2 size){
        rectTransform.sizeDelta = size;
    }
	// Use this for initialization
	void Awake () {
        rectTransform = GetComponent<RectTransform>();
        dragger = GetComponentInChildren<WindowDragger>();
        if (dragger != null)
        {
            dragger.Drag += Drag;
        }
		
    }
    void Drag(Vector2 pos){
rectTransform.anchoredPosition = pos;
    }
    // Update is called once per frame
    void Update () {
		if(Input.GetKey(KeyCode.Space)){
            Open();
        }
		if(Input.GetKey(KeyCode.Tab)){
            Close();
        }
	}
}
