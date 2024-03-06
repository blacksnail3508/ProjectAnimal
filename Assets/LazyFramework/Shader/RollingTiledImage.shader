Shader"Custom/RollingTiledImage"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _Tiling ("Tiling", Vector) = (1, 1)
        _Speed ("Roll Speed", Float) = 1
        _RotationAngle ("Rotation Angle", Float) = 0
    }

    SubShader
    {
        Tags { "Queue" = "Overlay" }
LOD100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma exclude_renderers gles xbox360 ps3
            #pragma fragment frag
#include "UnityCG.cginc"

struct appdata
{
    float4 vertex : POSITION;
    float3 normal : NORMAL;
};

struct v2f
{
    float4 pos : POSITION;
    float2 uv : TEXCOORD0;
};

sampler2D _MainTex;
float2 _Tiling;
float _Speed;
float _RotationAngle;

v2f vert(appdata v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.uv = v.vertex.xz * _Tiling;
    return o;
}

fixed4 frag(v2f i) : COLOR
{
    float angle = radians(_RotationAngle);
    float cosTheta = cos(angle);
    float sinTheta = sin(angle);

    float2 rotatedUV = float2(i.uv.x * cosTheta - i.uv.y * sinTheta, i.uv.x * sinTheta + i.uv.y * cosTheta);

    float t = frac(rotatedUV.y - _Time.y * _Speed);
    fixed4 col = tex2D(_MainTex, rotatedUV);
    col.a *= smoothstep(0, 0.01, 0.5 - abs(t - 0.5));

    return col;
}
            ENDCG
        }
    }
}
