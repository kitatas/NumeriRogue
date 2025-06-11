Shader "Custom/Background/Top"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #define PI 3.141592

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float wave(float2 st) {
                return step(sin(st.x * PI + _Time.y * 1.5) * 0.05 + 0.5, st.y);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return lerp(
                    fixed4(0.03, 0.09, 0.14, 1.0),
                    fixed4(0.03, 0.22, 0.30, 1.0),
                    wave(i.uv));
            }
            ENDCG
        }
    }
}