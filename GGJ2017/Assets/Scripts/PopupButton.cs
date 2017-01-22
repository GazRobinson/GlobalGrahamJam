using UnityEngine;
using UnityEngine.UI;
public class PopupButton : MonoBehaviour {
    private Button button;
    private Text text;
    public RectTransform rectTransform;
    // Use this for initialization
    void Awake () {
        rectTransform = GetComponent<RectTransform>();
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();
    }
	public float SetText(string txt){
        text.text = txt;
        float x = Mathf.Max(92.0f, text.preferredWidth + 16f);
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
		rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.sizeDelta = new Vector2(x, 36f);
       // rectTransform.anchorMin = new Vector2(0.0f, 0.0f);
	//	rectTransform.anchorMax = new Vector2(1f, 1f);
		return x;
    }
	
	public Vector2 GetSize(){
        return GetComponent<RectTransform>().sizeDelta;
    }
}
