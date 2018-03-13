Shader "Unlit/DissolveShader"
{
	Properties
	{
		_Tint ("Tint", Color) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0


		_DissolveTexture ("DissolveTexture", 2D) = "white" {}
		_SecondDissolveTexture ("2nd Dissolve Texture", 2D) = "white" {}
		_ThirdDissolveTexture ("3rd Dissolve Texture", 2D) = "white" {}
		
		[Toggle] _ShowXDissolve ("Show X Dissolve", Float) = 0
		[Toggle] _ShowYDissolve ("Show Y Dissolve", Float) = 0
		[Toggle] _ShowZDissolve ("Show Z Dissolve", Float) = 0

		_DissolvePositionX ("DissolvePositionX", Float) = 0
		_DissolvePositionY ("DissolvePositionY", Float) = 0
		_DissolvePositionZ ("DissolvePositionZ", Float) = 0


		_DissolveSize("Dissolve Size", Float) = 0

		
		_StartingX("Start X", Float) = 0
		_StartingY("Start Y", Float) = 0
		_StartingZ("Start Z", Float) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200

		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 worldPos : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Tint;
			sampler2D _DissolveTexture;
			sampler2D _SecondDissolveTexture;
			sampler2D _ThirdDissolveTexture;

			float _DissolvePositionX;
			float _DissolvePositionY;
			float _DissolvePositionZ;

			float _ShowXDissolve;
			float _ShowYDissolve;
			float _ShowZDissolve;

			float _DissolveSize;

			float _StartingX;
			float _StartingY;
			float _StartingZ;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldPos = mul (unity_ObjectToWorld, v.vertex).xyz;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float transition = _DissolvePositionX - i.worldPos.x;

				if(_ShowXDissolve == 1)
				{
					clip(_StartingX + (transition + ((tex2D(_DissolveTexture, i.uv)) + tex2D(_SecondDissolveTexture, i.uv) + tex2D(_ThirdDissolveTexture, i.uv)) * _DissolveSize));
				};

				transition = _DissolvePositionY - i.worldPos.y;
				
				if(_ShowYDissolve == 1)
				{
					clip(_StartingY + (transition + ((tex2D(_DissolveTexture, i.uv)) + tex2D(_SecondDissolveTexture, i.uv) + tex2D(_ThirdDissolveTexture, i.uv)) * _DissolveSize));
				};
				
				transition = _DissolvePositionZ - i.worldPos.z;
				
				if(_ShowZDissolve == 1)
				{
					clip(_StartingZ + (transition + ((tex2D(_DissolveTexture, i.uv)) + tex2D(_SecondDissolveTexture, i.uv) + tex2D(_ThirdDissolveTexture, i.uv)) * _DissolveSize));
				};
				fixed4 col = tex2D(_MainTex, i.uv);// * _Tint;
				return col;
			}
			ENDCG
		}
			CGPROGRAM
			#pragma surface surf Standard fullforwardshadows

			#pragma target 3.0
			
			sampler2D _MainTex;

			struct Input {
				float2 uv_MainTex;
			};

			half _Glossiness;
			half _Metallic;
			fixed4 _Tint;

			void surf (Input IN, inout SurfaceOutputStandard o)
			{
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Tint;
				o.Albedo = c.rgb;

				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = c.a;
			}

			ENDCG
		}
		//FallBack "Diffuse"

}
