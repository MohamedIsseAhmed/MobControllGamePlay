Shader "Custom/SpecularLighting"
{
    Properties
    {
        _SpecColor ("_SpecColor", Color) = (1,1,1,1)
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Gloss ("_Glossiness", Range(0,1)) = 0.5
        _Spec ("_Spec", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
      //  #pragma surface surf Standard fullforwardshadows
        #pragma surface surf BlinnPhong

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Gloss;
        half _Metallic;
        fixed4 _Color;
        half _Spec;
     
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Albedo=_Color.rgb;
            o.Specular=_Spec;
            o.Gloss=_Gloss;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
