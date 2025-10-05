Shader "Unlit/PsychedelicWave"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed ("Color Speed", Range(0,10)) = 2
        _WaveSpeed ("Wave Speed", Range(0,10)) = 1
        _Amplitude ("Wave Amplitude", Range(0,0.2)) = 0.05
        _Frequency ("Wave Frequency", Range(1,20)) = 10

        _Brightness ("Brightness", Range(0,1)) = 0.7     // 明度調整用（0で真っ黒、1で元の明るさ）
        _Saturation ("Saturation", Range(0,2)) = 1       // 彩度調整用（0で白黒、1で元の彩度）
        _HueStrength ("Hue Shift Strength", Range(0,6.28)) = 6.28  // 色相変化の強さ（0で変化なし）
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float _Speed;
            float _WaveSpeed;
            float _Amplitude;
            float _Frequency;
            float _Brightness;
            float _Saturation;
            float _HueStrength;

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

            // 色相変化（HueShift）
            fixed3 HueShift(fixed3 color, float shift)
            {
                float3 k = float3(0.57735,0.57735,0.57735);
                float cosA = cos(shift);
                float sinA = sin(shift);
                return color * cosA + cross(k,color)*sinA + k*dot(k,color)*(1-cosA);
            }

            // 彩度調整（色の鮮やかさを変更）
            fixed3 AdjustSaturation(fixed3 color, float saturation)
            {
                float gray = dot(color, float3(0.299, 0.587, 0.114));
                return lerp(float3(gray, gray, gray), color, saturation);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;

                // 波打つUV歪み
                uv.x += sin(uv.y * _Frequency + _Time.y * _WaveSpeed) * _Amplitude;
                uv.y += cos(uv.x * _Frequency + _Time.y * _WaveSpeed) * _Amplitude;

                // テクスチャ取得
                fixed4 c = tex2D(_MainTex, uv);

                // 明度取得
                float brightness = dot(c.rgb, float3(0.299,0.587,0.114));

                // 色相シフトの計算に _HueStrength を掛けて強さ調整
                float hue = _Time.y * _Speed + brightness * _HueStrength;
                fixed3 shifted = HueShift(c.rgb, hue);

                // 彩度調整
                shifted = AdjustSaturation(shifted, _Saturation);

                // 明度調整（全体の輝度を抑える）
                shifted *= _Brightness;

                return fixed4(shifted,1.0);
            }
            ENDCG
        }
    }
}
