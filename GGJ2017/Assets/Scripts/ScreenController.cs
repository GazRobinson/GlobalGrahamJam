using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour {

    public static ScreenController Instance = null;
    public List<UIWindow> OpenWindows = new List<UIWindow>();
public RectTransform cursor;
    private RectTransform rectTransform;
    void Awake(){
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
    }
	public static Vector2 GetScreenSize(){
        return Instance.rectTransform.sizeDelta;
    }

    // Use this for initialization
    void Start () {
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 vpPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		cursor.anchoredPosition = 	new Vector2(vpPos.x * GetScreenSize().x, vpPos.y * GetScreenSize().y) - (GetScreenSize() * 0.5f);
    }
	public Vector2 GetScreenPos(Vector2 pos){
		Vector3 vpPos = Camera.main.ScreenToViewportPoint(pos);
		return	new Vector2(vpPos.x * GetScreenSize().x, vpPos.y * GetScreenSize().y) - (GetScreenSize() * 0.5f);
	}

	private void GetInput(){
	//	if(Input.GetKeyDown)
	}
}
