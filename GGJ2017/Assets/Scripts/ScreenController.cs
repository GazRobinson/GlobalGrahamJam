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
    public InputWindow InputWindow;
    public RecycleBin bin;
    public DragHook file;
    public string Name = "Dr. Iver";
    public RectTransform cursor;
    private RectTransform rectTransform;
    private System.Action OnSelection;
    private QuestionData current;
    void Awake(){
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
        popup.WindowClosed += OnPopupClosed;
        InputWindow.OnStringSubmission += OnStringSubmission;
        bin.gameObject.SetActive(false);
        file.gameObject.SetActive(false);
    }
    void OnStringSubmission(string submission){
        Debug.Log("Controller got " + submission);
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
        if(current.Answers.Length>0 && current.Answers[0] == "<INPUT>"){
            if (current.Answers[1] == "<name>")
            {
                InputWindow.OnStringSubmission = DoName;
            }
                textWindow.OnFinishedText = delegate { Debug.Log("Finished Text"); GetInput(); };
        textWindow.Open();
        } else if(current.Answers.Length>0 && current.Answers[0] == "<FILE>"){

        bin.gameObject.SetActive(true);
        file.gameObject.SetActive(true);
            bin.OnFileTrash = delegate {
                Debug.Log("Feelings deleted");
                // textWindow.Close();
                // textWindow.ClosedDelegate += OnSelection;
                bin.gameObject.SetActive(false);
                current = questions[current.Referral[0]-1]; 
        GetTextBox(current.Question);   };
                
           // OnSelection();
           // textWindow.OnFinishedText = delegate { Debug.Log("Finished Text"); GetInput(); };
        } else
        {
            Debug.Log(current.ID);
            if (current.Answers.Length > 0)
            {
                textWindow.OnFinishedText = delegate { Debug.Log("Finished Text"); GetPopup(current.Answers, PopupDone); };
                
            }
            else
            {
                textWindow.OnFinishedText = delegate { Debug.Log("Finished Text"); GetPopup(new string[] { "OK" }, PopupDone); };
            }if(current.ID == 16){
                    textWindow.OnFinishedText += SirenRotationScript.Instance.Appear;
                } else if(current.ID == 38){
                NIGHTMODE.instance.ACTIVATENIGHTMODE();
            }
        textWindow.Open();
        }
    }
    public void GetInput(){
        InputWindow.Open();
    }
    public void GetPopup(string[] options, System.Action CompletionDelegate){
        Debug.Log("GET PPUP");
        OnSelection = CompletionDelegate;
        popup.Initialise(options);
    }
    private void DoName(string newName){
        if (newName.Length > 0)
        {
            Debug.Log("Name is " + newName);
            Name = newName;
        }
        textWindow.Close();
        textWindow.ClosedDelegate += OnSelection;
        GetNext(current.Referral[0]);
        //current = questions[current.Referral[0]-1];
    }
    void GetNext(int referral){
        if (referral > -1)
        {
            current = questions[referral - 1];
        } else{
            GameOver.Instance.GAMEOVER();
        }
    }
    public void InterruptWindow(){

        popup.WindowClosed = Interrupted;
        popup.Close();
        

    }
    void Interrupted(int pie){
        textWindow.Close();
        textWindow.ClosedDelegate += OnSelection;
        popup.WindowClosed = OnPopupClosed;
        GetNext(56);

    }
    void PopupDone(){
        Debug.Log("Done");
        GetTextBox(current.Question);
    }
    //Close the text window and move onto the next one
    void OnPopupClosed(int selection){
        textWindow.Close();
        textWindow.ClosedDelegate += OnSelection;
        GetNext(current.Referral[selection]);
       // current = questions[current.Referral[selection]-1];
    }

    // Use this for initialization
    void Start () {
        Cursor.visible = false;
        questions = CSVReader.Instance.GetData();
        StartStory();
    }
	
	// Update is called once per frame
	void Update () {
        Cursor.visible = false;
        Vector3 vpPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		cursor.anchoredPosition = 	new Vector2(vpPos.x * GetScreenSize().x, vpPos.y * GetScreenSize().y) - (GetScreenSize() * 0.5f);
        if(Input.GetKeyDown(KeyCode.Escape)){
            GameOver.Instance.GAMEOVER();
        }
    }
	public Vector2 GetScreenPos(Vector2 pos){
		Vector3 vpPos = Camera.main.ScreenToViewportPoint(pos);
		return	new Vector2(vpPos.x * GetScreenSize().x, vpPos.y * GetScreenSize().y) - (GetScreenSize() * 0.5f);
	}

}
