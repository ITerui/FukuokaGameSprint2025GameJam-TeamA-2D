Shader "Custom/Psychedelic"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _Speed ("Color Speed", Range(0,10)) = 2
        _Emission ("Emission", Range(0,5)) = 2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;
        sampler2D _BumpMap;
        half _Speed;
        half _Emission;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
        };

        fixed3 HueShift(fixed3 color, float shift)
        {
            float3 k = float3(0.57735, 0.57735, 0.57735);
            float cosA = cos(shift);
            float sinA = sin(shift);
            return color * cosA + cross(k, color) * sinA + k * dot(k, color) * (1 - cosA);
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);

            fixed3 normal = tex2D(_BumpMap, IN.uv_BumpMap).rgb;
            normal = normal * 2 - 1; // unpack
            if(length(normal) == 0) normal = float3(0,0,1);

            float brightness = dot(c.rgb, float3(0.299,0.587,0.114));
            float hue = (_Time.y * _Speed + brightness * 6.28);
            fixed3 shifted = HueShift(c.rgb, hue);

            o.Albedo = shifted;
            o.Normal = normal;
            o.Emission = shifted * _Emission * brightness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
