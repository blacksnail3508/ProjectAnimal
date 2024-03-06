Shader "Custom/RotateImageShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" { }
        _RotationAngle ("Rotation Angle", Range(0, 360)) = 45
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        fixed _RotationAngle;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Convert the UV coordinates to the center-based coordinates
            float2 centerUV = IN.uv_MainTex - 0.5;

            // Calculate the rotation matrix
            float sinTheta = sin(radians(_RotationAngle));
            float cosTheta = cos(radians(_RotationAngle));
            float2x2 rotationMatrix = float2x2(cosTheta, -sinTheta, sinTheta, cosTheta);

            // Apply the rotation to the UV coordinates
            float2 rotatedUV = mul(rotationMatrix, centerUV) + 0.5;

            // Sample the texture using the rotated UV coordinates
            fixed4 c = tex2D(_MainTex, rotatedUV);

            // Output the color
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }

    FallBack "Diffuse"
}