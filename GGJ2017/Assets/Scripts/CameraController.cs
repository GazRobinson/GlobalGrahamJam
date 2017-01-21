using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
public Transform target;
public Vector3 targetOffset;
public float distance = 10.0f;
public float HorizontalRot = 0.0f;
public float VerticalRot = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 offset =  -target.forward * distance;
		transform.position =  target.TransformPoint( targetOffset.normalized*distance );//+ (offset);
		transform.LookAt(target);
		//transform.position = target.position - transform.forward * distance;
	}
}
