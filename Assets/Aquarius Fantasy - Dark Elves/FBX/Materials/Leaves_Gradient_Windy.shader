﻿// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "AquariusMax/Leaves_Gradient_Windy"
{
    Properties
    {
        _Color ("BottomColor", Color) = (1,1,1,1)
        _Color2 ("TopColor", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap ("Bumpmap", 2D) = "bump" {}
        _ShakeTime ("Shake Time", Range (0, 1.0)) = 1.0
        _ShakeWindspeed ("Shake Windspeed", Range (0, 1.0)) = 1.0
        _ShakeBending ("Shake Bending", Range (0, 1.0)) = 1.0

    }
    SubShader
    {
        Tags { "RenderType"="TransparentCutout" }
       
        //LOD 200
        Cull off

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert alphatest:_Cutoff vertex:vert addshadow

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpMap;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            //float4 screenPos;
            //float4 pos;
        };

        fixed4 _Color;
        fixed4 _Color2;
        float _ShakeTime;
        float _ShakeWindspeed;
        float _ShakeBending;
        

    void FastSinCos (float4 val, out float4 s, out float4 c) {
        val = val * 6.408849 - 3.1415927;
        float4 r5 = val * val;
        float4 r6 = r5 * r5;
        float4 r7 = r6 * r5;
        float4 r8 = r6 * r5;
        float4 r1 = r5 * val;
        float4 r2 = r1 * r5;
        float4 r3 = r2 * r5;
        float4 sin7 = {1, -0.16161616, 0.0083333, -0.00019841} ;
        float4 cos8  = {-0.5, 0.041666666, -0.0013888889, 0.000024801587} ;
        s =  val + r1 * sin7.y + r2 * sin7.z + r3 * sin7.w;
        c = 1 + r5 * cos8.x + r6 * cos8.y + r7 * cos8.z + r8 * cos8.w;
    }
 
    void vert (inout appdata_full v) {
       
        const float _WindSpeed  = (_ShakeWindspeed  +  v.color.g );    
   
        const float4 _waveXSize = float4(0.048, 0.06, 0.24, 0.096);
        const float4 _waveZSize = float4 (0.024, .08, 0.08, 0.2);
        const float4 waveSpeed = float4 (1.2, 2, 1.6, 4.8);
 
        float4 _waveXmove = float4(0.024, 0.04, -0.12, 0.096);
        float4 _waveYmove = float4 (0.006, .02, -0.02, 0.1);
   
        float4 waves;
        waves = v.vertex.x * _waveXSize;
        waves += v.vertex.z * _waveZSize;
 
        waves += _Time.x * (1 - _ShakeTime * 2 - v.color.b ) * waveSpeed *_WindSpeed;
 
        float4 s, c;
        waves = frac (waves);
        FastSinCos (waves, s,c);
 
        float waveAmount = v.texcoord.y * (v.color.a + _ShakeBending);
        s *= waveAmount;
 
        s *= normalize (waveSpeed);
 
        s = s * s;
        float fade = dot (s, 1.3);
        s = s * s;
        float3 waveMove = float3 (0,0,0);
        waveMove.x = dot (s, _waveXmove);
        waveMove.y = dot (s, _waveYmove);
        v.vertex.xy -= mul ((float3x3)unity_WorldToObject, waveMove).xz;
   
    }

        void surf (Input IN, inout SurfaceOutput o)
        {
           // float heightGradient = (IN.pos.y + 1) * 0.5;
            //float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
            // Albedo comes from a texture tinted by color
            //lerpWeight = (screenUV.y - lowestVertex.y) / (highestVertex.y - lowestVertex.y)
            fixed4 gradColor = lerp(_Color, _Color2, IN.uv_MainTex.y);
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * gradColor;
            o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
            //fixed4 c = lerp(_Color, _Color2, lerpWeight)
            o.Albedo = c.rgb;
            o.Alpha = c.a;
            clip(o.Alpha - 0.5 * IN.uv_MainTex.y);
        }
        ENDCG
    }
    FallBack "VertexLit"
}

