shader "Minimalist Free/Simple" {
	Properties{
		_MainTexture ("Main Texture", 2D) = "white" {} //Locked
		_MainTexturePower ("Main Texture Power", Range(0, 10.0)) = 1 //Locked

		_Color_F("Forward Color",  Color) = (1, 1, 0, 1)
		_Color_B("Backward Color", Color) = (0, 1, 1, 1)
		_Color_L("Left Color",     Color) = (1, 0, 1, 1)
		_Color_R("Right Color",    Color) = (1, 1, 1, 1)
		_Color_T ("Top Color",     Color) = (0, 0, 1, 1)
		_Color_D ("Bottom Color",  Color) = (1, 0, 0, 1)

		_AmbientColor("Ambient Color",Color) = (1, 1, 1, 1) //Locked
		_AmbientPower("Ambient Power", Range(0, 2.0)) = 0 //Locked

		[MaterialToggle] _DontMix ("Don't Mix Color", Float ) = 0
		[MaterialToggle] _Fog ("Fog", Float ) = 0 //Locked
		[MaterialToggle] _RealtimeShadow ("RealTime Shadow", Float ) = 0 //Locked

		[MaterialToggle] _LM ("Enable Lightmap", Float ) = 0
		_LMColor ("LightMap Color", Color) = (1, 1, 1, 1) //Locked
		_LMPower ("LightMap Power", Range(0, 5.0)) = 0 //Locked
	}

	SubShader{
		Tags { "QUEUE"="Geometry" "RenderType"="Opaque" "LIGHTMODE"="ForwardBase"}

		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase_fullshadows

			#include "UnityCG.cginc"

			uniform half3 _Color_F;
			uniform half3 _Color_B;
			uniform half3 _Color_L;
			uniform half3 _Color_R;
			uniform half3 _Color_T;
			uniform half3 _Color_D;

			uniform fixed _DontMix;

			uniform fixed _LM;

			static const half3 FrontDir = half3(0, 0, 1);
			static const half3 BackDir = half3(0, 0, -1);
			static const half3 LeftDir = half3(1, 0, 0);
			static const half3 RightDir = half3(-1, 0, 0);
			static const half3 TopDir = half3(0, 1, 0);
			static const half3 BottomDir = half3(0, -1, 0);
			static const half3 whiteColor = half3(1, 1, 1);

			struct vertexInput{
				float4 vertex : POSITION;
				half3 normal : NORMAL;
				float4 uv0 : TEXCOORD0;
				float4 uv1 : TEXCOORD1;
			};

			struct vertexOutput{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
				float2 lightmapUV : TEXCOORD1;
				float3 color : TEXCOORD2;
			};

			vertexOutput vert(vertexInput v)
			{
				vertexOutput o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv0;
				half3 normal = normalize(mul(unity_ObjectToWorld, half4(v.normal, 0))).xyz;

				fixed dirFront  = max(dot(normal, FrontDir),  0.0);
				fixed dirBack   = max(dot(normal, BackDir),   0.0);
				fixed dirLeft   = max(dot(normal, LeftDir),   0.0);
				fixed dirRight  = max(dot(normal, RightDir),  0.0);
				fixed dirTop    = max(dot(normal, TopDir),    0.0);
				fixed dirBottom = max(dot(normal, BottomDir), 0.0);

				fixed3 AdditiveColor = _Color_F * dirFront + _Color_B * dirBack + _Color_L * dirLeft + _Color_R * dirRight + _Color_T * dirTop + _Color_D * dirBottom;
				fixed3 MultipliedColor = lerp(_Color_F, whiteColor, 1-dirFront) * lerp(_Color_B, whiteColor, 1-dirBack) * lerp(_Color_L, whiteColor, 1-dirLeft) * lerp(_Color_R, whiteColor, 1-dirRight) * lerp(_Color_T, whiteColor, 1-dirTop) * lerp(_Color_D, whiteColor, 1-dirBottom);

				fixed3 Maincolor = lerp(MultipliedColor, AdditiveColor, _DontMix);
				o.color = Maincolor;

				//Lightmap
				o.lightmapUV = v.uv1.xy * unity_LightmapST.xy + unity_LightmapST.zw;

				return o;
			}

			fixed4 frag(vertexOutput i) : COLOR
			{
				 fixed4 mainColor = fixed4(i.color, 1);

				 half4 lmColor = UNITY_SAMPLE_TEX2D(unity_Lightmap, i.lightmapUV);
                 half4 lmPower = lerp(fixed4(1,1,1,1), half4(DecodeLightmap(lmColor), 0), 1);
                 fixed4 LightData = lerp(fixed4(0,0,0,0), fixed4(1,1,1,1) , lmPower);

                 fixed4 finalColor = mainColor * ((LightData * _LM) + ( 1 -_LM ));

				 return  finalColor;
			}

			ENDCG
		}
	}
	FallBack "Standard"
    CustomEditor "MinimalistFreeSimpleMat"
}