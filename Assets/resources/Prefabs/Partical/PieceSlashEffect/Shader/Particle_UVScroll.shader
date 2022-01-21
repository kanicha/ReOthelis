Shader "UVScroll"
{
    Properties
    {
        //Main
        _MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags{"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }

            Blend SrcAlpha One // 透過加算
			
								// 加算
								// Blend One One
								// 透過加算
								// Blend SrcAlpha One
								// アルファブレンド
								// Blend SrcAlpha OneMinusSrcAlpha
								// 乗算
								// Blend Zero SrcColor

        Pass
        {
            Cull BACK
            Lighting Off 
            ZWrite off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #include "UnityCG.cginc"

            struct appdata
            {
			float4 vertex : POSITION;
			half4 color : COLOR;
			half4 normal : NORMAL;
			float4 texcoords : TEXCOORD0;
			// float customData : TEXCOORD1; // float型のCustomDataをストリームに定義
			float2 customData1 : TEXCOORD1; // is UV2 (TEXCOORD0 = UV1) (customData1 = 変数名)
            };

            struct v2f // (struct) appdata で使用を宣言した頂点座標(vertex), UV2 (customData1) を Vertex/Fragment Shaderで使うための変数
            {
                float4 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                half4 color : COLOR;
            };

            //Main
            sampler2D _MainTex;
            float4 _MainTex_ST;
			float4 _Color;

            v2f vert (appdata v) // v2f で宣言した変数に (struct) appdata で取得した頂点情報を流す
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // 頂点座標 (struct) appdata の vertex を元に、画面での頂点位置を計算
                o.color = v.color;
				// o.uv.xy = TRANSFORM_TEX(v.texcoords.xy,_MainTex); // Default
				half2 mainCustomCoord = half2(v.customData1.x,v.customData1.y);
				o.uv.xy = TRANSFORM_TEX(v.texcoords.xy,_MainTex) + mainCustomCoord;

                return o;
            }
			
            fixed4 frag (v2f i) : SV_Target // frag（Fragment Shader） v2f で用意した変数を frag で使用　（v2f には i　でアクセス)
			{
				half4 col = tex2D(_MainTex, i.uv);
				col *= _Color;
				return col;
			}
            ENDCG
        }
    }
}