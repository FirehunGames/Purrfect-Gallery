Shader "Custom/GradientAlphaShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _LeftAlpha("Left Alpha", Range(0, 1)) = 0.0
        _RightAlpha("Right Alpha", Range(0, 1)) = 0.0
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

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
                };

                sampler2D _MainTex;
                float _LeftAlpha;
                float _RightAlpha;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    float2 uv = i.uv;
                    fixed4 texColor = tex2D(_MainTex, uv);

                    // Calculate alpha gradient from left to right
                    float alpha = lerp(_LeftAlpha, _RightAlpha, uv.x);
                    texColor.a *= alpha;

                    return texColor;
                }
                ENDCG
            }
        }
}
