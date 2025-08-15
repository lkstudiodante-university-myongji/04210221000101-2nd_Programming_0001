Shader "Example_21/E01SurfaceShader_21_10" {
	Properties {
		_Color("Color", Color) = (1, 1, 1, 1)
		_MainTex("Main Texture", 2D) = "white" { }
	}

	SubShader {
		Tags {
			"Queue" = "Geometry+1"
			"RenderType" = "Opaque"
		}

		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Standard

		float4 _Color;
		sampler2D _MainTex;

		/** 입력 */
		struct Input {
			float4 color;
			float2 uv_MainTex;
		};

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, inout SurfaceOutputStandard a_stOutput) {
			float4 stColor = tex2D(_MainTex, a_stInput.uv_MainTex + float2(_Time.y, 0.0));

			a_stOutput.Alpha = stColor.a * _Color.a;
			a_stOutput.Albedo = stColor.rgb * _Color.rgb;
		}
		ENDCG
	}
}
