using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputWindow : UIWindow {

    public System.Action<string> OnStringSubmission;
    private string subString = "";
    private InputField field;
    // Use this for initialization
    void Start () {
        field = GetComponentInChildren<InputField>();
        field.onEndEdit.AddListener(SubmitString);
    }

	void SubmitString(string submissionString){
		Debug.Log(submissionString);
        subString = submissionString;
        Close();
    }
	
	protected override void OnFinishedClose(){
		if(OnStringSubmission != null){
            OnStringSubmission(subString);
        }
	}
}
