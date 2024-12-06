Shader "Custom/RadialGradient"
{
    Properties
    {
        _Color1 ("Center Color", Color) = (0.11, 0.77, 0.60, 1) // #1cc49a
        _Color2 ("Outer Color", Color) = (0.03, 0.44, 0.67, 1) // #0870aa
        _Radius ("Radius", Float) = 0.88
        _Center ("Center Position", Vector) = (0.5, 0.62, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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

            float4 _Center;
            float4 _Color1;
            float4 _Color2;
            float _Radius;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float2 center = float2(_Center.x, _Center.y);
                float dist = distance(uv, center);
                float t = saturate(dist / _Radius);
                return lerp(_Color1, _Color2, t);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
