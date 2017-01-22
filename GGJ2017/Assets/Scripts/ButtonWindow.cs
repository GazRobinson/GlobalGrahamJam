using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonWindow : UIWindow {

    public System.Action<int> WindowClosed;
    private PopupButton[] buttons;
    private int Selection = -1;

   void Start(){
        tweenTime = 0f;
    }
    // Update is called once per frame
    void Update () {
		
	}

    public override void Open(){
        gameObject.SetActive(true);
        rectTransform.sizeDelta = targetSize;
        FinishedOpen();
    }
    public override void Close(){
        rectTransform.sizeDelta = Vector2.zero;
        for (int i = 0; i < buttons.Length; i++)
        {
			buttons[i].gameObject.SetActive(false);
        }
        FinishedClose();
    }

	public void Initialise(string[] options){
        if (buttons == null || buttons.Length < 1)
        {
            buttons = GetComponentsInChildren<PopupButton>();
        }
        float spaceBetween = 16.0f;
        float x = spaceBetween;
       
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < options.Length; i++){
            buttons[i].gameObject.SetActive(true);
            x+=buttons[i].SetText(options[i]);
        x += spaceBetween;
        }
        Debug.Log("X: " + x);
        targetSize = new Vector2(x, targetSize.y);
        float xPos = spaceBetween + buttons[0].rectTransform.sizeDelta.x * 0.5f;
        buttons[0].rectTransform.anchoredPosition = new Vector2(-(x * 0.5f) + xPos, buttons[0].rectTransform.anchoredPosition.y);
        xPos += buttons[0].rectTransform.sizeDelta.x * 0.5f + spaceBetween + buttons[1].rectTransform.sizeDelta.x * 0.5f;
        buttons[1].rectTransform.anchoredPosition = new Vector2(-(x * 0.5f) + xPos, buttons[1].rectTransform.anchoredPosition.y);
        xPos += buttons[1].rectTransform.sizeDelta.x * 0.5f + spaceBetween + buttons[2].rectTransform.sizeDelta.x * 0.5f;
        buttons[2].rectTransform.anchoredPosition = new Vector2(-(x * 0.5f) + xPos, buttons[2].rectTransform.anchoredPosition.y);
        Open();
    }

	public void Select(int selection){
        Selection = selection;
        Debug.Log("Clicked " + selection);
        Close();
    }
protected override void OnFinishedOpen(){
  
}
	protected override void OnFinishedClose(){
		if(WindowClosed!=null){
            WindowClosed(Selection);
        }
	}
}
