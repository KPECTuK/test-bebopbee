Shader "ThreeInLine/Selector"
{
	Properties
	{
		_BaseColor("_BaseColor", Color) = (0.5, 0.5, 0.5, 1.0)
		_SpecColor("_SpecColor", Color) = (1.0, 1.0, 1.0, 1.0)
		_Shine("_Shine", Range(0.01, 1.0)) = 0.78125
		_Size("_Size", Range(0.0, 1.0)) = 0.0
	}
	SubShader
	{
		Tags
		{
			"LightMode" = "Always"
			"Queue" = "Geometry"
			"RenderType" = "Opaque"
		}

		Pass
		{
			Tags
			{ 
				"LightMode" = "PrePassFinal"
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 normal : NORMAL;
			};

			struct v2f
			{
				float4 position : SV_POSITION;
			};

			float4 _BaseColor;
			float4 _SpecColor;
			float _Shine;
			float _Size;

			v2f vert (appdata v)
			{
				v2f o;
				o.position = mul(UNITY_MATRIX_MVP, float4(v.vertex.x * _Size, v.vertex.y, v.vertex.z * _Size, 1.0));
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				return _BaseColor;
			}
			ENDCG
		}
	}
}
