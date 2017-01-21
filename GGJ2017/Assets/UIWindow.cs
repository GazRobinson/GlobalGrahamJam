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
	protected WindowState state = WindowState.Closed;
	
	public void Open(){
//		if(WindowState)
	}
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
