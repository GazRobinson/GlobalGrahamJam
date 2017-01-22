using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NIGHTMODE : MonoBehaviour {
public static NIGHTMODE instance = null;


    float t = 0.0f;
    public float fadeTime = 3.0f;
    public Material skybox;
    public Color pink = new Color(1.0f, 0.0f, 164f / 255f);
    public Color night = new Color(1.0f, 0.0f, 164f / 255f);
    public Material car;
	public Material wheel;
    public Light lightt;
    public GameObject lightsss;
    // Use this for initialization
    void Start () {
        instance = this;
        RenderSettings.skybox = skybox;

        Init();
        //  skybox = RenderSettings.skybox;
    }
	[ContextMenu("NIGHT")]
	public void ACTIVATENIGHTMODE(){
StartCoroutine(ToNight());
    }
	IEnumerator ToNight(){
        lightsss.gameObject.SetActive(true);
        t = 0.0f;
		while(t<1f){
            t = Mathf.Min(1.0f, t + Time.deltaTime / fadeTime);
			RenderSettings.skybox.SetColor("_SkyTint", Color.Lerp(pink, night, t));
        car.SetFloat("_Blend", t);
        wheel.SetFloat("_Blend", t);
            lightt.intensity = Mathf.Lerp(1f, 0.4f, t);
			RenderSettings.ambientIntensity = Mathf.Lerp(1f, 0.4f, t);
            yield return null;
        }
    }
	void Init(){

		RenderSettings.skybox.SetColor("_SkyTint", pink);
        car.SetFloat("_Blend", 0.0f);
        wheel.SetFloat("_Blend", 0.0f);
	}
	void OnDestroy(){
        Init();
    }
}
