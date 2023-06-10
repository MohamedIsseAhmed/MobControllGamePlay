Shader "Custom/RimLighjtingShader"
{
    Properties
    {
        _RimColor ("Color", Color) = (1,1,1,1)
        _RimExponet ("_RimExponet", Range(0,3)) = 1
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        //_Glossiness ("Smoothness", Range(0,1)) = 0.5
        //_Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
            float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        half _RimExponet;
        fixed4 _RimColor;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            half rimAmount=1-saturate(dot(normalize(IN.viewDir),o.Normal));
           // o.Emission=rimAmount>0.5?float3(1,0,0):rimAmount>0.3?float3(0,1,0):0;
            o.Emission=frac(IN.worldPos.y*10*0.3)>0.4?float3(0,1,0)*rimAmount:float3(1,0,0)*rimAmount;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
