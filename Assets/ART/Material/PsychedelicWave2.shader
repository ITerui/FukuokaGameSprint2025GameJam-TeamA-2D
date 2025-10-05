Shader "Unlit/PsychedelicWave2"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed ("Color Speed", Range(0,10)) = 2
        _WaveSpeed ("Wave Speed", Range(0,10)) = 1
        _Amplitude ("Wave Amplitude", Range(0,0.2)) = 0.05
        _Frequency ("Wave Frequency", Range(1,20)) = 10
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off
        Lighting Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Speed;
            float _WaveSpeed;
            float _Amplitude;
            float _Frequency;

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // HueShift
            fixed3 HueShift(fixed3 color, float shift)
            {
                float3 k = float3(0.57735,0.57735,0.57735);
                float cosA = cos(shift);
                float sinA = sin(shift);
                return color * cosA + cross(k,color)*sinA + k*dot(k,color)*(1-cosA);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;

                // 波打つUV歪み
                uv.x += sin(uv.y*_Frequency + _Time.y*_WaveSpeed)*_Amplitude;
                uv.y += cos(uv.x*_Frequency + _Time.y*_WaveSpeed)*_Amplitude;

                fixed4 c = tex2D(_MainTex, uv);

                // アルファを保持
                float alpha = c.a;

                // 完全透明ならそのまま返す
                if(alpha < 0.01)
                    return fixed4(0,0,0,0);

                // 明度取得
                float brightness = dot(c.rgb, float3(0.299,0.587,0.114));

                // 時間＋明度で色相変化
                float hue = _Time.y*_Speed + brightness*6.28;
                fixed3 shifted = HueShift(c.rgb, hue);

                return fixed4(shifted, alpha);
            }
            ENDCG
        }
    }
}
