Shader "Custom/Blended 6 Sided Skybox"
{
    Properties
    {
        [Header(Day Skybox)]
        [NoScaleOffset] _DayFront ("Day Front", 2D) = "white" {}
        [NoScaleOffset] _DayBack ("Day Back", 2D) = "white" {}
        [NoScaleOffset] _DayLeft ("Day Left", 2D) = "white" {}
        [NoScaleOffset] _DayRight ("Day Right", 2D) = "white" {}
        [NoScaleOffset] _DayUp ("Day Up", 2D) = "white" {}
        [NoScaleOffset] _DayDown ("Day Down", 2D) = "white" {}

        [Header(Night Skybox)]
        [NoScaleOffset] _NightFront ("Night Front", 2D) = "white" {}
        [NoScaleOffset] _NightBack ("Night Back", 2D) = "white" {}
        [NoScaleOffset] _NightLeft ("Night Left", 2D) = "white" {}
        [NoScaleOffset] _NightRight ("Night Right", 2D) = "white" {}
        [NoScaleOffset] _NightUp ("Night Up", 2D) = "white" {}
        [NoScaleOffset] _NightDown ("Night Down", 2D) = "white" {}

        _Blend ("Blend", Range(0, 1)) = 0
        _Exposure ("Exposure", Float) = 1
    }

    SubShader
    {
        Tags
        {
            "Queue"="Background"
            "RenderType"="Background"
            "PreviewType"="Skybox"
        }

        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _DayFront;
            sampler2D _DayBack;
            sampler2D _DayLeft;
            sampler2D _DayRight;
            sampler2D _DayUp;
            sampler2D _DayDown;

            sampler2D _NightFront;
            sampler2D _NightBack;
            sampler2D _NightLeft;
            sampler2D _NightRight;
            sampler2D _NightUp;
            sampler2D _NightDown;

            float _Blend;
            float _Exposure;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 position : SV_POSITION;
                float3 direction : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
                o.direction = v.vertex.xyz;
                return o;
            }

            fixed4 SampleSixSided(
                float3 dir,
                sampler2D frontTex,
                sampler2D backTex,
                sampler2D leftTex,
                sampler2D rightTex,
                sampler2D upTex,
                sampler2D downTex
            )
            {
                float3 absDir = abs(dir);
                float2 uv;
                fixed4 col;

                // Front / Back
                if (absDir.z >= absDir.x && absDir.z >= absDir.y)
                {
                    if (dir.z > 0)
                    {
                        uv = float2(dir.x, dir.y) / absDir.z;
                        col = tex2D(frontTex, uv * 0.5 + 0.5);
                    }
                    else
                    {
                        uv = float2(-dir.x, dir.y) / absDir.z;
                        col = tex2D(backTex, uv * 0.5 + 0.5);
                    }
                }
                // Left / Right
                else if (absDir.x >= absDir.y)
                {
                    if (dir.x > 0)
                    {
                        uv = float2(-dir.z, dir.y) / absDir.x;
                        col = tex2D(rightTex, uv * 0.5 + 0.5);
                    }
                    else
                    {
                        uv = float2(dir.z, dir.y) / absDir.x;
                        col = tex2D(leftTex, uv * 0.5 + 0.5);
                    }
                }
                // Up / Down
                else
                {
                    if (dir.y > 0)
                    {
                        uv = float2(dir.x, -dir.z) / absDir.y;
                        col = tex2D(upTex, uv * 0.5 + 0.5);
                    }
                    else
                    {
                        uv = float2(dir.x, dir.z) / absDir.y;
                        col = tex2D(downTex, uv * 0.5 + 0.5);
                    }
                }

                return col;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 dir = normalize(i.direction);

                fixed4 dayColor = SampleSixSided(
                    dir,
                    _DayFront,
                    _DayBack,
                    _DayLeft,
                    _DayRight,
                    _DayUp,
                    _DayDown
                );

                fixed4 nightColor = SampleSixSided(
                    dir,
                    _NightFront,
                    _NightBack,
                    _NightLeft,
                    _NightRight,
                    _NightUp,
                    _NightDown
                );

                fixed4 finalColor = lerp(dayColor, nightColor, _Blend);
                finalColor.rgb *= _Exposure;

                return finalColor;
            }

            ENDCG
        }
    }
}