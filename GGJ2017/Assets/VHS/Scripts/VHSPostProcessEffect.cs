using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]
public class VHSPostProcessEffect :  UnityStandardAssets.ImageEffects.PostEffectsBase{
	Material m;
	public Shader shader;
	public MovieTexture VHS;
	public Texture2D distortionTex;
	

	float yScanline, xScanline;

public float strength = 1.0f;
	public new void Start() {
		m = new Material(shader);
		m.SetTexture("_VHSTex", VHS);
		m.SetTexture("_NoiseTex", distortionTex);
		VHS.loop = true;
		//VHS.Play();
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination){
		yScanline += Time.deltaTime * 0.1f;
		xScanline -= Time.deltaTime * 0.1f;

		if(yScanline >= 1){
			yScanline = Random.value;
		}
		if(xScanline <= 0 || Random.value < 0.05){
			xScanline = Random.value;
		}
		m.SetFloat("_Strength", strength);
		m.SetFloat("_yScanline", yScanline);
		m.SetFloat("_xScanline", xScanline);
		Graphics.Blit(source, destination, m);
	}
}