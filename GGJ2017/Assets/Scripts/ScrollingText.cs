using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollingText : MonoBehaviour {
RectTransform rectTransform;
public float speed = 10.0f;
public Text txt;
    // Use this for initialization
    void Start () {
        txt = GetComponent<Text>();
        rectTransform = GetComponent<RectTransform>();
        Debug.Log(txt.preferredWidth);
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 pos = rectTransform.anchoredPosition;
        pos.x -= speed * Time.deltaTime;
        if(pos.x < -txt.preferredWidth){
            pos.x = 1024f;
        }
        rectTransform.anchoredPosition = pos;
        
    }
}
