using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSetup : MonoBehaviour {
float res = 4f / 3f;
Camera cam;
Camera cam2;
    // Use this for initialization
    void Start () {
        cam = GetComponent<Camera>();
		cam2 = transform.GetChild(0).GetComponent<Camera>();
        float current = Screen.width / Screen.height;
Rect rect = cam.rect;
rect.width = current / res;

        rect.x = (1f - rect.width) * 0.5f;
        cam.rect = rect;
        cam2.rect = rect;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
