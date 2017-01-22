using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour {
public AudioSource aud;
public CanvasGroup group;
private RectTransform rectt;
    public RectTransform cursor;
    
    // Use this for initialization
    void Start () {
        rectt = GetComponent<RectTransform>();
        Cursor.visible = false;
    }

	public Vector2 GetScreenSize(){
        return rectt.sizeDelta;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 vpPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		cursor.anchoredPosition = 	new Vector2(vpPos.x * GetScreenSize().x, vpPos.y * GetScreenSize().y) - (GetScreenSize() * 0.5f);
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
