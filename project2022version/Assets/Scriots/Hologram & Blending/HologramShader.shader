Shader "Custom/HologramShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _RimPower("RimPower",Range(0,10))=1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
      
        Pass
        {
            ZWrite On
            ColorMask 0
        }

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert aplha:fade 

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
        };

        half _Glossiness;
        half _Metallic;
        half _RimPower;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
          half rim=1-saturate(dot(normalize(IN.viewDir),o.Normal));
         o.Emission=_Color.rgb*pow(rim,_RimPower)*10;
          //o.Albedo=_Color.rgb*pow(rim,_RimPower);
          o.Alpha=pow(rim,_RimPower);
          //if(o.Alpha<0.2)
          //{
          //    descard;
          //}
        }
        ENDCG
    }
    FallBack "Diffuse"
}
