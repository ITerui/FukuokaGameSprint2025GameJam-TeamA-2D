Shader "Custom/IridescentWave"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _IridescenceFactor ("Iridescence Factor", Range(0, 1)) = 0.5
        _WaveSpeed ("Wave Speed", Range(0, 10)) = 1
        _DistortionAmount ("Distortion Amount", Range(0, 1)) = 0.1
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

            // �錾
            uniform float _IridescenceFactor;
            uniform float _WaveSpeed;
            uniform float _DistortionAmount;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 normal : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            sampler2D _MainTex;

            // ���_�V�F�[�_�[
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                o.uv = v.uv;

                // ���ԂɊ�Â��Ē��_�̈ʒu������񂮂��Ɠ�����
                float time = _Time.y;
                float wave = sin(v.vertex.x * 10.0 + time * _WaveSpeed);
                float distortion = wave * _DistortionAmount;
                o.pos.xyz += v.normal * distortion;  // �@�������ɓ��������ƂŘc�܂���

                return o;
            }

            // �t���O�����g�V�F�[�_�[
            half4 frag(v2f i) : SV_Target
            {
                // ���Ԃ̎擾
                float time = _Time.y;

                // ���_�����Ɩ@������Fresnel���ʂ��v�Z
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.pos.xyz);
                float fresnel = pow(1.0 - dot(i.normal, viewDir), 3.0);

                // �g�̓����Ɠ��F�̐F�̑J�ڂ����
                float waveX = sin(i.uv.x * 10.0 + time * _WaveSpeed);
                float waveY = cos(i.uv.y * 10.0 + time * _WaveSpeed);
                float iridescence = sin(waveX + waveY) * 0.5 + 0.5;
                iridescence = lerp(0.5, 1.0, iridescence * _IridescenceFactor);

                // �F����F�ɑJ�ڂ�����
                half4 col = tex2D(_MainTex, i.uv);
                col.rgb *= iridescence; // �g�̉e����F�ɓK�p

                // �F�ɓ��F���ʂ�������
                float3 rainbow = sin(i.uv.xyx + time * 0.5) * 0.5 + 0.5;
                col.rgb = col.rgb * rainbow;

                return col;
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}
