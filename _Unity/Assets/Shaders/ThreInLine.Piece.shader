Shader "ThreeInLine/Piece"
{
	Properties
	{
		_Palyer1Color("_BaseColor", Color) = (0.5, 0.5, 0.5, 1.0) // EC5300FF
		_Player2Color("_BaseColor", Color) = (0.5, 0.5, 0.5, 1.0) // EC5300FF
		_SpecColor("_SpecColor", Color) = (1.0, 1.0, 1.0, 1.0)
		_Shine("_Shine", Range(0.01, 1.0)) = 0.78125
		_State("_State", Range(-0.5, 0.5)) = 0
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

			float4 _Palyer1Color;
			float4 _Player2Color;
			float4 _SpecColor;
			float _Shine;
			float _State;

			v2f vert (appdata v)
			{
				v2f o;
				o.position = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				if(floor(_State))
					return _Palyer1Color;
				if(floor(-_State))
					return _Player2Color;

				discard;
				return fixed4(0, 0, 0, 0);
			}
			ENDCG
		}
	}
}
