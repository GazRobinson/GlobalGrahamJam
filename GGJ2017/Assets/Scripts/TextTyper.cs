using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTyper : MonoBehaviour {

	public void SetText(string text){
        textTime = 0.0f;
        running = true;
        targetText = text + "\n\n";
    }
    public System.Action TextFinished;
    public float speed = 20.0f;
    private string targetText = "";
    private Text textRender;
    private int textCursor = 0;
    private float textTime = 0.0f;
    private bool running = false;
    // Use this for initialization
    void Start () {
        textRender = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		if(running){
            textTime += Time.deltaTime*speed;
			textRender.text = targetText.Substring(0, Mathf.Min(Mathf.CeilToInt(textTime), targetText.Length)) + ((Time.time*2)%2 < 1 ? "":"_");
            if(textTime > targetText.Length + 5){
                running = false;
                if(TextFinished!=null){
                    TextFinished();
                }
            }
        }
	}
}
