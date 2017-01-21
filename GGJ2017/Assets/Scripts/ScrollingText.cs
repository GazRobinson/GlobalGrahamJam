using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingText : MonoBehaviour {
RectTransform rectTransform;
public float speed = 10.0f;
    // Use this for initialization
    void Start () {
        rectTransform = GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 pos = rectTransform.anchoredPosition;
        pos.x -= speed * Time.deltaTime;
        rectTransform.anchoredPosition = pos;
    }
}
