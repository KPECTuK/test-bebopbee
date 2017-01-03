Shader "ThreeInLine/Test"
{
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal: NORMAL;
			};

			struct v2f
			{
				float4 position : SV_POSITION;
				float3 wNormal : TEXCOORD0;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.position = mul(UNITY_MATRIX_MVP, v.vertex);
				o.wNormal = mul((float3x3)_Object2World, v.normal);
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				float3 norm = (i.wNormal * 0.5) + 0.5;
				return float4(norm, 0.5);
			}
			ENDCG
		}

		//Pass
		//{
		//	Tags { "LightMode"="PrePassBase" }
		//	CGPROGRAM
		//	#pragma vertex vert
		//	#pragma fragment frag
		//	
		//	struct appdata
		//	{
		//		float4 vertex : POSITION;
		//		float3 normal: NORMAL;
		//	};

		//	struct v2f
		//	{
		//		float4 pos : SV_POSITION;
		//		float3 wNorm : TEXCOORD0;
		//	};

		//	v2f vert (appdata v)
		//	{
		//		v2f o;
		//		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		//		o.wNorm = mul((float3x3)_Object2World, v.normal);
		//		return o;
		//	}
		//	
		//	fixed4 frag (v2f i) : COLOR
		//	{
		//		float3 norm = (i.wNorm * 0.5) + 0.5;
		//		return float4(norm, 0.5);
		//	}
		//	ENDCG
		//}
		//Pass
		//{
		//	Tags { "LightMode"="PrePassFinal" }
		//	ZWrite off
		//	CGPROGRAM
		//	#pragma vertex vert
		//	#pragma fragment frag
		//	
		//	#include "UnityCG.cginc"

		//	struct appdata
		//	{
		//		float4 vertex : POSITION;
		//	};

		//	struct v2f
		//	{
		//		float4 pos : SV_POSITION;
		//		float4 uvProj : TEXTCOORD0;
		//	};

		//	v2f vert (appdata v)
		//	{
		//		v2f o;
		//		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

		//		float4 posHalf = o.pos * 0.5;
		//		posHalf.y *= _ProjectionParams.x;
		//		o.uvProj.xy = posHalf.xy + float2(posHalf.w, posHalf.w);
		//		o.uvProj.zw = o.pos.zw;
		//		return o;
		//	}
		//	
		//	float4 frag (v2f i) : COLOR
		//	{
		//		float4 light = tex2Dproj(_LightBuffer, i.uvProj);
		//		float4 logLight = -(log2(max(light, float4(0.001,0.001,0.001,0.001))));
		//		float4 texCol = tex2D(_MainTex, i.uv);
		//		return float4((texCol.xyz * light.xyz) + float3(_SpecColor.xyz) * light.w, texCol.w);
		//	}
		//	ENDCG
		//}
	}
}
