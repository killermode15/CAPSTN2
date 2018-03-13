Shader "Toon/ToonlitDissolve" 
{
    Properties 
	{
        _Color ("Main Color", Color) = (0.5,0.5,0.5,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
		//_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {}
		_DissolveNoiseTex ("Dissolve Noise", 2D) = "white" {}     
		_SecondDissolveTex ("2nd Dissolve Noise", 2D) = "white" {}     

		[Toggle]_UseLocalPosition ("Use Local Position", Float) = 0
		_DissolveStartPosition ("Dissolve Start Position", Vector) = (0, 0, 0, 0)
		_DissolvePosition ("Dissolve Position", Vector) = (0,0,0,0)
		_DissolveSize ("Dissolve Size", Float) = 0
		_NoiseScale ("Noise Scale", Float) = 0

	}
 
    SubShader 
	{
        Tags { "RenderType"="Opaque" }
        LOD 200
       
		CGPROGRAM
		#pragma surface surf ToonRamp
 
		sampler2D _Ramp;
 
		// custom lighting function that uses a texture ramp based
		// on angle between light direction and normal
		#pragma lighting ToonRamp exclude_path:prepass

		inline half4 LightingToonRamp (SurfaceOutput s, half3 lightDir, half atten)
		{
			#ifndef USING_DIRECTIONAL_LIGHT
			lightDir = normalize(lightDir);
			#endif
	   
			half d = dot (s.Normal, lightDir)*0.5 + 0.5;
			half3 ramp = tex2D (_Ramp, float2(d,d)).rgb;
	   
			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2);
			c.a = 0;
			return c;
		}
	 
	 
		sampler2D _MainTex;
		sampler2D _DissolveNoiseTex;
		sampler2D _SecondDissolveTex;
		float3 _DissolveStartPosition;
		float3 _DissolvePosition;
		float4 _Color;
		float _NoiseScale;
		float _DissolveSize;
		float _UseLocalPosition;
	 
		struct Input 
		{
			float4 vertex : POSITION;
			float3 worldPos;// : TEXCOORD1;
			float2 uv_MainTex : TEXCOORD0;
		};
	 
		void surf (Input IN, inout SurfaceOutput o) 
		{
			half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			float3 transition;
			if(_UseLocalPosition == 0)
			{
				transition = _DissolvePosition - IN.worldPos;
			}
			if(_UseLocalPosition == 1)
			{
				float3 localPos = IN.worldPos - mul(unity_ObjectToWorld, float4(0,0,0,1)).xyz;
				transition = _DissolvePosition - localPos;
			}
			clip(_DissolveStartPosition + transition + ((tex2D(_DissolveNoiseTex, IN.uv_MainTex) + tex2D(_SecondDissolveTex, IN.uv_MainTex)) * _DissolveSize));

			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
    Fallback "Diffuse"
}