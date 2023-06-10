Shader "Unlit/ColoringUnlit"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScaleUVX("SclaUVX",Range(1,15))=1
        _ScaleUVY("SclaUVY",Range(1,15))=1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
      
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
          
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
                float4 color : COLOR0;
            };
            float _ScaleUVX;
            float _ScaleUVY;
            sampler2D _MainTex;
            //float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv=v.uv;//TRANSFORM_TEX(v.uv,_MainTex);
                o.uv.x=sin(o.uv.x*_ScaleUVX+_Time.y);
                //o.color.r = v.vertex.y;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
               
                fixed4 col = tex2D(_MainTex, i.uv);
                //float4 col = i.color;
               
                return col;
            }
            ENDCG
        }
    }
}
