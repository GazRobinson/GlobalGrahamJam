Shader "Hidden/VHSPostProcessEffect" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_VHSTex ("Base (RGB)", 2D) = "white" {}
		_NoiseTex ("Base (RGB)", 2D) = "white" {}
		_maskSize ("_Strength", Float) = 1
	}

	SubShader {
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
					
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform sampler2D _VHSTex;
			uniform sampler2D _NoiseTex;
			
			float _yScanline;
			float _xScanline;
			float _Strength;

			float rand(float2 co)
			{
				return frac(sin( dot(co.xy ,float3(12.9898,78.233,45.5432) )) * 43758.5453);
			}
			float ramp(float y, float start, float end)
			{
				float inside = step(start,y) - step(end,y);
				float fact = (y-start)/(end-start)*inside;
				return (1.-fact) * inside;
				
			}
			float noise(float2 p)
			{
				//float sample = texture2D(_NoiseTex, float2(1.0,2.0*cos(_Time.y))*_Time.y*8.0 + p*1.0).x;
				float sample = tex2D(_NoiseTex, float2(1.0,2.0*cos(_Time.y*70))*_Time.y*40 + p*1.0).x;
				sample *= sample;
				return sample;
			}
			float stripes(float2 uv)
			{
				
				float noi = noise(uv*float2(0.5,1.) + float2(1.,3.));
				return ramp(fmod(uv.y*4. + _Time.y*12+sin(_Time.y + sin(_Time.y*20)),.9),0.1,0.12)*noi*0.5;
			}

float onOff(float a, float b, float c)
{
	return step(c, sin(_Time.y + a*cos(_Time.y*b)));
}

			fixed4 frag (v2f_img i) : COLOR{	
				fixed4 vhs = tex2D (_VHSTex, i.uv);

				
				float dx = 1-abs(distance(i.uv.y, _xScanline));
				float dy = 1-abs(distance(i.uv.y, _yScanline));

//DISTORTION
				float2 uv = i.uv;
				i.uv -= float2(.5,.5);
				i.uv = i.uv*1.2*(1./1.2+2.*i.uv.x*i.uv.x*i.uv.y*i.uv.y);
				i.uv += float2(.5,.5);
	/*
				i.uv.x += dy * 0.025;
				
				float white = (vhs.r+vhs.g+vhs.b)/3;
				i.uv.y += step(0.99, dx) * white * dx;
				
				if(dx > 0.99)
					i.uv.y = _xScanline;
				i.uv.y = step(0.99, dy) * (_yScanline) + step(dy, 0.99) * i.uv.y;
			
*/
				float2 look = i.uv;
				float window = 1./(1.+20.*(look.y-fmod(_Time.y/4.,1.))*(look.y-fmod(_Time.y/4.,1.)));
				look.x = look.x + sin(look.y*10. + _Time.y)/50.*onOff(4.,4.,.3)*(1.+cos(_Time.y*80.))*window;
				float vShift = 0.4*onOff(2.,3.,.9)*(sin(_Time.y)*sin(_Time.y*20.) + 
													(0.5 + 0.1*sin(_Time.y*200.)*cos(_Time.y)));
				look.y = fmod(look.y + vShift, 1.);
				float4 video = tex2D(_MainTex,look);


				//return c + vhs;

				// Flip Y Axis
				//uv.y = -uv.y;
				
/*
				float magnitude = 0.0009;
				
				
				// Set up offset
				float2 offsetRedUV = uv;
				offsetRedUV.x = uv.x + rand(float2(_Time.y*0.03,uv.y*0.42)) * 0.001;
				offsetRedUV.x += sin(rand(float2(_Time.y*0.2, uv.y)))*magnitude;
				
				float2 offsetGreenUV = uv;
				offsetGreenUV.x = uv.x + rand(float2(_Time.y*0.004,uv.y*0.002)) * 0.004;
				offsetGreenUV.x += sin(_Time.y*9.0)*magnitude;
				
				float2 offsetBlueUV = uv;
				offsetBlueUV.x = uv.y;
				offsetBlueUV.x += rand(float2(cos(_Time.y*0.01),sin(uv.y)));
				
				// Load Texture
				float r = tex2D(_MainTex, offsetRedUV).r;
				float g = tex2D(_MainTex, offsetGreenUV).g;
				float b = tex2D(_MainTex, uv).b;*/
				//float vigAmt = _Strength;

				float vigAmt = 1.5;
				float4 final = video;// + vhs;
				final += stripes(uv);
				final *= (12.+fmod(uv.y*30.+_Time,1.))/13.;
				float vignette = (1.-vigAmt*(uv.y-.5)*(uv.y-.5))*(1.-vigAmt*(uv.x-.5)*(uv.x-.5)) > 0.5? 1:0;
				final += noise(uv*1.4)*0.05;
				final *= vignette;
				return final;
			}
			ENDCG
		}
	}
Fallback off
}
