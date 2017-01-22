using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirenRotationScript : MonoBehaviour {
    public static SirenRotationScript Instance;
	void Start(){
        Instance = this;
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update () 
	{
		transform.Rotate (new Vector3 (0f, Time.deltaTime * 400, 0f));
	}
	public void Appear(){
        gameObject.SetActive(true);
	}
}
