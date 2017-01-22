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
        if (transform.childCount > 0)
        {
            cam2 = transform.GetChild(0).GetComponent<Camera>();
        }
       // Debug.Log(Screen. + ", " +Screen.currentResolution.height);
		float current = (float)Screen.width/ (float)Screen.height;
		
        Debug.Log("PIdsdsE " + Screen.height +", " + Screen.width);
        Debug.Log("Current: " + current + ", Res: " + res);
        Rect rect = cam.rect;
		rect.width = res / current;
Debug.Log(rect.width);
        rect.x = (1f - rect.width) * 0.5f;
        cam.rect = rect;
        if (cam2 != null)
        {
            cam2.rect = rect;
        }
    }
	
}
