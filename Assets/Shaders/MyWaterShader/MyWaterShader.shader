Shader "Leksay/WaterShader"
{
    Properties
    {
        [PerRendererData]
        _MainTex ("Main Texture", 2D) = "white" {}
        _Color ("Color" , Color) = (1,1,1,1)
        _Displacement("Displacement Power", Float) = 0
        _DisplacementTexture("Displacement Texture",2D) = "white"{}
    }

    SubShader
    {

        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha
        
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
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                float4 grabPos:TEXCOORD1;
            };

            fixed4 _Color;
            sampler2D _MainTex;            
            sampler2D _DisplacementTexture;
            uniform float _Displacement;
            v2f vert (appdata v)
            {
                v2f o;
                o.uv = v.uv;
                o.color = v.color;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.grabPos = ComputeGrabScreenPos(o.vertex);
                o.grabPos /= o.grabPos.w;
                return o;
            }
            sampler2D _GrabTexture;
            fixed4 frag (v2f i) : SV_Target
            {                           
                fixed4 displacementCoords = tex2D(_DisplacementTexture, i.uv);
                float2 offset =_Time.y*displacementCoords.xy  * _Displacement;
                // fixed4 grabColor = tex2D(_GrabTexture,i.grabPos.xy + offset);
                 fixed4 texColor = tex2D(_MainTex, i.uv + float2(1,0) * _Time.x + _SinTime.x * offset)*i.color;
                 texColor.w = 1;
                // float s = step(grabColor, 0.5);
                // fixed4 color = s * (2*grabColor * texColor) + (1-s) * (1-2*(1-texColor)*(1-grabColor));                   
                //color = lerp(grabColor,color,texColor.a);             
                return texColor;
            }
            ENDCG
        }

    }
}