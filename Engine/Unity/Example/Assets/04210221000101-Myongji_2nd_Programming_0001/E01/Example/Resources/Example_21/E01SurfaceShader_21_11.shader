Shader "Example_21/E01SurfaceShader_21_11" {
	Properties {
		_Color("Color", Color) = (1, 1, 1, 1)
		_MainTex("Main Texture", 2D) = "white" { }
	}

	SubShader {
		Tags {
			"Queue" = "Geometry+1"
			"RenderType" = "Opaque"
		}

		Pass {
			CGPROGRAM
			#pragma target 3.0
			#pragma vertex VSMain
			#pragma fragment PSMain

			/*
			 * UnityCG.cginc 파일에는 CG 언어로 작성 된 여러 편의성 기능을 포함하고 있다. (즉, 해당 파일을 추가함으로서
			 * 미리 제작 되어있는 기능을 가져와서 재활용하는 것이 가능하다.)
			 */
			#include "UnityCG.cginc"

			float4 _Color;
			sampler2D _MainTex;

			/*
			 * Surface Shader 와 달리 Vertex & Fragment Shader 방식을 활용 할 때는 반드시 입력 or 출력 될 데이터의 용도를
			 * 명시 할 필요가 있으며 이를 시멘틱이라고 한다.
			 */
			/** 출력 */
			struct STOutput {
				float4 m_stPos: SV_POSITION;
				float2 m_stUV: TEXCOORD;
			};

			/*
			 * appdata_base 구조체는 Unity 에서 제공하는 구조체로서 정점 위치를 포함한 법선과 UV 정보를 포함하고 있다.
			 * 
			 * (즉, 해당 구조체 이외에도 Unity 는 다양한 구조체를 제공하며 이를 활용하면 여러 가지 정보를 입력 받는 것이
			 * 가능하다.)
			 */
			/** 정점 쉐이더 */
			STOutput VSMain(appdata_base a_stInput) {
				STOutput stOutput = (STOutput)0;
				stOutput.m_stPos = UnityObjectToClipPos(a_stInput.vertex);
				stOutput.m_stUV = a_stInput.texcoord;

				return stOutput;
			}

			/** 픽셀 쉐이더 */
			float4 PSMain(STOutput a_stInput) : SV_TARGET {
				float4 stColor = tex2D(_MainTex, a_stInput.m_stUV);
				return stColor * _Color;
			}
			ENDCG
		}
	}
}
