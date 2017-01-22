using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caeser : MonoBehaviour {
    public static Caeser instance;
    Quaternion def;
float t = 0.0f;
    // Use this for initialization
    void Start () {
        instance = this;
        def = transform.localRotation;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Talk(float speed){
        t+= Time.deltaTime * speed;
        transform.localRotation = def * Quaternion.AngleAxis(Mathf.Sin(t)*2f, Vector3.forward);
    }
}
