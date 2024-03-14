Shader"Custom/BlackWhiteImageEffectShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" }
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
                UNITY_FOG_COORDS(1)
    float4 vertex : SV_POSITION;
};

sampler2D _MainTex;

v2f vert(appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv;
    return o;
}

half4 frag(v2f i) : SV_Target
{
    half4 color = tex2D(_MainTex, i.uv);

                // If alpha is 0, return original color
    if (color.a <= 0)
    {
        return color;
    }

                // Apply black and white effect
    float gray = dot(color.rgb, float3(0.299, 0.587, 0.114));
    return half4(gray, gray, gray, color.a);
}
            ENDCG
        }
    }
}
