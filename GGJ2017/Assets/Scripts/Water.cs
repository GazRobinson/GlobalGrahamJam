using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {
public Material mat;
public float xSpeed = 1f, ySpeed = 0.5f;
public float yMag = 0.5f;
    float x = 0, y = 0;
    // Use this for initialization
    void Start () {
        mat = GetComponent<Renderer>().sharedMaterial;
    }
	
	// Update is called once per frame
	void Update () {
        x += Time.deltaTime*xSpeed;
        y = yMag*Mathf.Sin(Time.time*ySpeed);
        mat.mainTextureOffset = new Vector2(x, y);
    }
}
