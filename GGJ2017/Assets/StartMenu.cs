using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour {
public AudioSource aud;
public CanvasGroup group;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		

	}
	public void StartGame(){
        StartCoroutine(Loadd());
    }
	IEnumerator Loadd(){
        float t = 0.0f;
		while (t<1f){
            t = Mathf.Min(1f, t + Time.deltaTime);
aud.volume = 1 - t;
group.alpha = t;
            yield return null;
        }
        Application.LoadLevel(1);

    }
}
