using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextWindow : UIWindow {
    public string testText = "";
    public TextTyper textBody;
    protected override void OnFinishedOpen(){
textBody.SetText(testText);
    }
}
