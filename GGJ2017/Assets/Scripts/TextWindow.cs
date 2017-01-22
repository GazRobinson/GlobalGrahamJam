using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextWindow : UIWindow {
    public string testText = "";
    public TextTyper textBody;

    public System.Action OnFinishedText;
    public System.Action OpenedDelegate, ClosedDelegate;

    void Start(){
        textBody.TextFinished = delegate { if (OnFinishedText != null) { OnFinishedText(); } };
    }

    protected override void OnFinishedOpen(){
textBody.SetText(testText);
    }
    protected override void OnFinishedClose(){
        textBody.Clear();
        if(ClosedDelegate!=null){
            ClosedDelegate();
        }
    }
}
