// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:Skybox/Cubemap,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:False,nrmq:1,nrsp:0,vomd:1,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:False,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32909,y:32711,varname:node_3138,prsc:2|emission-6440-OUT;n:type:ShaderForge.SFN_Cubemap,id:5486,x:32233,y:32704,ptovrint:False,ptlb:Cubemap1,ptin:_Cubemap1,varname:_Cubemap1,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,cube:27fc329fbfc94fd4986b6f90faa9c429,pvfc:1|DIR-1569-XYZ;n:type:ShaderForge.SFN_Cubemap,id:1803,x:32233,y:32883,ptovrint:False,ptlb:Cubemap2,ptin:_Cubemap2,varname:_Cubemap2,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,cube:d0fed46e80de1d64a9ea8972c857e08d,pvfc:-1|DIR-3109-XYZ;n:type:ShaderForge.SFN_Slider,id:2091,x:32460,y:32936,ptovrint:False,ptlb:Slider,ptin:_Slider,varname:_Slider,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Lerp,id:6440,x:32604,y:32722,varname:node_6440,prsc:2|A-5486-RGB,B-1803-RGB,T-2091-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:1569,x:31987,y:32653,varname:node_1569,prsc:2;n:type:ShaderForge.SFN_FragmentPosition,id:3109,x:31987,y:32864,varname:node_3109,prsc:2;proporder:5486-2091-1803;pass:END;sub:END;*/

Shader "Shader Forge/skyunlit" {
    Properties {
        _Cubemap1 ("Cubemap1", Cube) = "_Skybox" {}
        _Slider ("Slider", Range(0, 1)) = 1
        _Cubemap2 ("Cubemap2", Cube) = "_Skybox" {}
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
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform samplerCUBE _Cubemap1;
            uniform samplerCUBE _Cubemap2;
            uniform float _Slider;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float3 emissive = lerp(texCUBE(_Cubemap1,i.posWorld.rgb).rgb,texCUBE(_Cubemap2,i.posWorld.rgb).rgb,_Slider);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Skybox/Cubemap"
    CustomEditor "ShaderForgeMaterialInspector"
}
