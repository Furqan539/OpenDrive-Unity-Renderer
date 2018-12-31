// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:1,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1928,x:33523,y:32578,varname:node_1928,prsc:2|diff-1071-OUT,spec-8856-R,gloss-8856-A,normal-1573-OUT;n:type:ShaderForge.SFN_Tex2d,id:4852,x:32323,y:32783,ptovrint:False,ptlb:Lines Albedo,ptin:_LinesAlbedo,varname:node_4852,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f425d33645a8e2c45ae53fcae2216f96,ntxv:0,isnm:False|UVIN-90-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:7892,x:32323,y:33032,ptovrint:False,ptlb:Lines Normal,ptin:_LinesNormal,varname:node_7892,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3f9a9bdcec4801e4d8c86a9d88725256,ntxv:3,isnm:False|UVIN-90-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:7426,x:32139,y:32410,ptovrint:False,ptlb:Road Albedo,ptin:_RoadAlbedo,varname:node_7426,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b3a3bc5d48a23f54dacbe2a560d9a2f5,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8856,x:33144,y:32627,ptovrint:False,ptlb:Road MetalRough,ptin:_RoadMetalRough,varname:node_8856,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:461b023723068ef4ab7d5abfd0f3e8ae,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5924,x:32506,y:32015,ptovrint:False,ptlb:Detail Diffuse,ptin:_DetailDiffuse,varname:node_5924,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:270699af628e91c42b940d8f25289d89,ntxv:0,isnm:False|UVIN-3954-OUT;n:type:ShaderForge.SFN_Tex2d,id:5246,x:32506,y:32209,ptovrint:False,ptlb:Detail Normal,ptin:_DetailNormal,varname:node_5246,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7164312e9ec8e0448a0f2bafa966a9ed,ntxv:3,isnm:True|UVIN-3954-OUT;n:type:ShaderForge.SFN_TexCoord,id:90,x:31986,y:32917,varname:node_90,prsc:2,uv:1;n:type:ShaderForge.SFN_Lerp,id:6859,x:32726,y:32541,varname:node_6859,prsc:2|A-7426-RGB,B-4852-RGB,T-4852-A;n:type:ShaderForge.SFN_TexCoord,id:8943,x:31908,y:31983,varname:node_8943,prsc:2,uv:0;n:type:ShaderForge.SFN_ValueProperty,id:847,x:31931,y:32262,ptovrint:False,ptlb:Detail Scale,ptin:_DetailScale,varname:node_847,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-2;n:type:ShaderForge.SFN_Multiply,id:160,x:32108,y:31993,varname:node_160,prsc:2|A-8943-U,B-847-OUT;n:type:ShaderForge.SFN_Multiply,id:7139,x:32091,y:32156,varname:node_7139,prsc:2|A-8943-V,B-847-OUT;n:type:ShaderForge.SFN_Append,id:3954,x:32266,y:32087,varname:node_3954,prsc:2|A-160-OUT,B-7139-OUT;n:type:ShaderForge.SFN_Blend,id:8401,x:32897,y:32339,varname:node_8401,prsc:2,blmd:1,clmp:True|SRC-5924-RGB,DST-6859-OUT;n:type:ShaderForge.SFN_Lerp,id:1071,x:33144,y:32485,varname:node_1071,prsc:2|A-8401-OUT,B-6859-OUT,T-4852-A;n:type:ShaderForge.SFN_Lerp,id:436,x:33144,y:32784,varname:node_436,prsc:2|A-5246-RGB,B-7892-RGB,T-4852-A;n:type:ShaderForge.SFN_Blend,id:1573,x:33144,y:32937,varname:node_1573,prsc:2,blmd:10,clmp:True|SRC-5246-RGB,DST-7892-RGB;proporder:7426-8856-7892-4852-5924-847-5246;pass:END;sub:END;*/

Shader "Shader Forge/Roads" {
    Properties {
        _RoadAlbedo ("Road Albedo", 2D) = "white" {}
        _RoadMetalRough ("Road MetalRough", 2D) = "white" {}
        _LinesNormal ("Lines Normal", 2D) = "bump" {}
        _LinesAlbedo ("Lines Albedo", 2D) = "white" {}
        _DetailDiffuse ("Detail Diffuse", 2D) = "white" {}
        _DetailScale ("Detail Scale", Float ) = -2
        _DetailNormal ("Detail Normal", 2D) = "bump" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _LinesAlbedo; uniform float4 _LinesAlbedo_ST;
            uniform sampler2D _LinesNormal; uniform float4 _LinesNormal_ST;
            uniform sampler2D _RoadAlbedo; uniform float4 _RoadAlbedo_ST;
            uniform sampler2D _RoadMetalRough; uniform float4 _RoadMetalRough_ST;
            uniform sampler2D _DetailDiffuse; uniform float4 _DetailDiffuse_ST;
            uniform sampler2D _DetailNormal; uniform float4 _DetailNormal_ST;
            uniform float _DetailScale;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float2 node_3954 = float2((i.uv0.r*_DetailScale),(i.uv0.g*_DetailScale));
                float3 _DetailNormal_var = UnpackNormal(tex2D(_DetailNormal,TRANSFORM_TEX(node_3954, _DetailNormal)));
                float4 _LinesNormal_var = tex2D(_LinesNormal,TRANSFORM_TEX(i.uv1, _LinesNormal));
                float3 normalLocal = saturate(( _LinesNormal_var.rgb > 0.5 ? (1.0-(1.0-2.0*(_LinesNormal_var.rgb-0.5))*(1.0-_DetailNormal_var.rgb)) : (2.0*_LinesNormal_var.rgb*_DetailNormal_var.rgb) ));
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _RoadMetalRough_var = tex2D(_RoadMetalRough,TRANSFORM_TEX(i.uv0, _RoadMetalRough));
                float gloss = 1.0 - _RoadMetalRough_var.a; // Convert roughness to gloss
                float specPow = exp2( gloss * 10.0+1.0);
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                d.boxMax[0] = unity_SpecCube0_BoxMax;
                d.boxMin[0] = unity_SpecCube0_BoxMin;
                d.probePosition[0] = unity_SpecCube0_ProbePosition;
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.boxMax[1] = unity_SpecCube1_BoxMax;
                d.boxMin[1] = unity_SpecCube1_BoxMin;
                d.probePosition[1] = unity_SpecCube1_ProbePosition;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float4 _DetailDiffuse_var = tex2D(_DetailDiffuse,TRANSFORM_TEX(node_3954, _DetailDiffuse));
                float4 _RoadAlbedo_var = tex2D(_RoadAlbedo,TRANSFORM_TEX(i.uv0, _RoadAlbedo));
                float4 _LinesAlbedo_var = tex2D(_LinesAlbedo,TRANSFORM_TEX(i.uv1, _LinesAlbedo));
                float3 node_6859 = lerp(_RoadAlbedo_var.rgb,_LinesAlbedo_var.rgb,_LinesAlbedo_var.a);
                float3 diffuseColor = lerp(saturate((_DetailDiffuse_var.rgb*node_6859)),node_6859,_LinesAlbedo_var.a); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, _RoadMetalRough_var.r, specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * (UNITY_PI / 4) );
                float3 directSpecular = 1 * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _LinesAlbedo; uniform float4 _LinesAlbedo_ST;
            uniform sampler2D _LinesNormal; uniform float4 _LinesNormal_ST;
            uniform sampler2D _RoadAlbedo; uniform float4 _RoadAlbedo_ST;
            uniform sampler2D _RoadMetalRough; uniform float4 _RoadMetalRough_ST;
            uniform sampler2D _DetailDiffuse; uniform float4 _DetailDiffuse_ST;
            uniform sampler2D _DetailNormal; uniform float4 _DetailNormal_ST;
            uniform float _DetailScale;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float2 node_3954 = float2((i.uv0.r*_DetailScale),(i.uv0.g*_DetailScale));
                float3 _DetailNormal_var = UnpackNormal(tex2D(_DetailNormal,TRANSFORM_TEX(node_3954, _DetailNormal)));
                float4 _LinesNormal_var = tex2D(_LinesNormal,TRANSFORM_TEX(i.uv1, _LinesNormal));
                float3 normalLocal = saturate(( _LinesNormal_var.rgb > 0.5 ? (1.0-(1.0-2.0*(_LinesNormal_var.rgb-0.5))*(1.0-_DetailNormal_var.rgb)) : (2.0*_LinesNormal_var.rgb*_DetailNormal_var.rgb) ));
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _RoadMetalRough_var = tex2D(_RoadMetalRough,TRANSFORM_TEX(i.uv0, _RoadMetalRough));
                float gloss = 1.0 - _RoadMetalRough_var.a; // Convert roughness to gloss
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float4 _DetailDiffuse_var = tex2D(_DetailDiffuse,TRANSFORM_TEX(node_3954, _DetailDiffuse));
                float4 _RoadAlbedo_var = tex2D(_RoadAlbedo,TRANSFORM_TEX(i.uv0, _RoadAlbedo));
                float4 _LinesAlbedo_var = tex2D(_LinesAlbedo,TRANSFORM_TEX(i.uv1, _LinesAlbedo));
                float3 node_6859 = lerp(_RoadAlbedo_var.rgb,_LinesAlbedo_var.rgb,_LinesAlbedo_var.a);
                float3 diffuseColor = lerp(saturate((_DetailDiffuse_var.rgb*node_6859)),node_6859,_LinesAlbedo_var.a); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, _RoadMetalRough_var.r, specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * (UNITY_PI / 4) );
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _LinesAlbedo; uniform float4 _LinesAlbedo_ST;
            uniform sampler2D _RoadAlbedo; uniform float4 _RoadAlbedo_ST;
            uniform sampler2D _RoadMetalRough; uniform float4 _RoadMetalRough_ST;
            uniform sampler2D _DetailDiffuse; uniform float4 _DetailDiffuse_ST;
            uniform float _DetailScale;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                o.Emission = 0;
                
                float2 node_3954 = float2((i.uv0.r*_DetailScale),(i.uv0.g*_DetailScale));
                float4 _DetailDiffuse_var = tex2D(_DetailDiffuse,TRANSFORM_TEX(node_3954, _DetailDiffuse));
                float4 _RoadAlbedo_var = tex2D(_RoadAlbedo,TRANSFORM_TEX(i.uv0, _RoadAlbedo));
                float4 _LinesAlbedo_var = tex2D(_LinesAlbedo,TRANSFORM_TEX(i.uv1, _LinesAlbedo));
                float3 node_6859 = lerp(_RoadAlbedo_var.rgb,_LinesAlbedo_var.rgb,_LinesAlbedo_var.a);
                float3 diffColor = lerp(saturate((_DetailDiffuse_var.rgb*node_6859)),node_6859,_LinesAlbedo_var.a);
                float specularMonochrome;
                float3 specColor;
                float4 _RoadMetalRough_var = tex2D(_RoadMetalRough,TRANSFORM_TEX(i.uv0, _RoadMetalRough));
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, _RoadMetalRough_var.r, specColor, specularMonochrome );
                float roughness = _RoadMetalRough_var.a;
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
