using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour {
    public Question[] tempQuestions;
    public List<QuestionData> questions;
    public static ScreenController Instance = null;
    public List<UIWindow> OpenWindows = new List<UIWindow>();

    public ButtonWindow popup;
    public TextWindow textWindow;

    public RectTransform cursor;
    private RectTransform rectTransform;
    private System.Action OnSelection;
    private QuestionData current;
    void Awake(){
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
        popup.WindowClosed += OnPopupClosed;
    }
    void StartStory(){
        current = questions[0];
        GetTextBox(current.Question);
    }
	public static Vector2 GetScreenSize(){
        return Instance.rectTransform.sizeDelta;
    }
    public void GetTextBox(string text){
        textWindow.testText = null;
        textWindow.testText = text;
        textWindow.OnFinishedText = null;
        if (current.Answers.Length > 0)
        {
            textWindow.OnFinishedText = delegate { Debug.Log("Finished Text"); GetPopup(current.Answers, PopupDone); };
        } else{
            OnSelection = PopupDone;
            textWindow.OnFinishedText = delegate { OnPopupClosed(0); };
        }
        textWindow.Open();
    }
    public void GetPopup(string[] options, System.Action CompletionDelegate){
        Debug.Log("GET PPUP");
        OnSelection = CompletionDelegate;
        popup.Initialise(options);
    }
    
    void PopupDone(){
        Debug.Log("Done");
        GetTextBox(current.Question);
    }
    //Close the text window and move onto the next one
    void OnPopupClosed(int selection){
        textWindow.Close();
        textWindow.ClosedDelegate += OnSelection;
        current = questions[current.Referral[selection]-1];
    }

    // Use this for initialization
    void Start () {
        Cursor.visible = false;
        questions = CSVReader.Instance.GetData();
        StartStory();
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
