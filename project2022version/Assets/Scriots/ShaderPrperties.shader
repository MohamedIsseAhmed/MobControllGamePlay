Shader "Custom/ShaderPrperties"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap ("Bump Map", 2D) = "Bump" {}
        _MyCube("Cube Map", CUBE) =""{}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _BumpMapSlider ("BumpMapSlider", Range(0,10)) = 1
        _TextureMapScaleSlider ("TextureMapScaleSlider", Range(0,10)) = 1
        _Metallic ("Metallic", Range(0,1)) = 0.0
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
        sampler2D _BumpMap;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float3 worldRefl;
        };
        half _BumpMapSlider;
        half _TextureMapScaleSlider;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        samplerCUBE _MyCube;
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            //o.Albedo = IN.worldRefl;
            //o.Emission=texCUBE(_MyCube,IN.worldRefl).rgb;
             //Metallic and smoothness come from slider variables
            //o.Metallic = _Metallic;
            //o.Smoothness = _Glossiness;
            c.x*=_TextureMapScaleSlider;
            c.y*=_TextureMapScaleSlider;
            c.z*=_TextureMapScaleSlider;
            o.Albedo = c.rgb;
            half3 myNormalMap=UnpackNormal( tex2D (_BumpMap, IN.uv_BumpMap));
            myNormalMap.x*=_BumpMapSlider;
            myNormalMap.y*=_BumpMapSlider;
            o.Normal=myNormalMap;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
