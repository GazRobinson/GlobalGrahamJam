using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
    public static GameOver Instance;
    private CanvasGroup group;
    private CanvasGroup time;
Image img;
float a = 0.0f;
    // Use this for initialization
    void Start () {
        Instance = this;
        img = GetComponent<Image>();
        group = transform.GetChild(0).GetComponent<CanvasGroup>();
        time = transform.GetChild(1).GetComponent<CanvasGroup>();
        a = 0.0f;

		group.alpha = a;
        time.alpha = a;
        StartCoroutine(FadeOut());
    }
	public void GAMEOVER(){
        a = 0.0f;
        StartCoroutine(FadeIn());
    }IEnumerator FadeOut(){
        a = 1.0f;
        yield return new WaitForSeconds(2.0f);
        while (a > 0.0f)
        {
            a = Mathf.Max(0.0f, a - Time.deltaTime);
            Color col = img.color;
            col.a = a;
            img.color = col;
            yield return null;
        }
    }
	IEnumerator FadeIn(){
        while (a < 1.0f)
        {
            a = Mathf.Min(1.0f, a + Time.deltaTime);
            Color col = img.color;
            col.a = a;
            img.color = col;
            yield return null;
        }
        StartCoroutine(ShowText());
    }
	IEnumerator ShowText(){
        a = 0.0f;
        while (a < 1.0f)
        {
            a = Mathf.Min(1.0f, a + Time.deltaTime);
            group.alpha = a;
            yield return null;
        }
        StartCoroutine(ShowTime());
    }

	IEnumerator ShowTime(){
        a = 0.0f;
        while (a < 1.0f)
        {
            a = Mathf.Min(1.0f, a + Time.deltaTime);
            time.alpha = a;
            yield return null;
        }
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(HideText());
    }

	IEnumerator HideText(){
        a = 1.0f;
        while (a > 0.0f)
        {
            a = Mathf.Max(0.0f, a - Time.deltaTime);
            group.alpha = a;
            time.alpha = a;
            yield return null;
        }
        End();
    }
	void End(){
        Application.LoadLevel(0);
    }
}
