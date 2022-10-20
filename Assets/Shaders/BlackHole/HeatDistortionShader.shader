// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "HeatDistortionShader"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Float0("Float 0", Float) = 0
		_Float1("Float 1", Float) = 0
		_Float2("Float 2", Float) = 0
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Float3("Float 3", Float) = 0
		_Float4("Float 4", Float) = 0
		_Float7("Float 7", Float) = 0.1
		_Color0("Color 0", Color) = (1,1,1,1)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		GrabPass{ }
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex);
		#else
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex)
		#endif
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float4 screenPos;
			float2 uv_texcoord;
		};

		ASE_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )
		uniform float _Float7;
		uniform float _Float3;
		uniform float _Float4;
		uniform float4 _Color0;
		uniform float _Float2;
		uniform float _Float1;
		uniform float _Float0;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform float _Cutoff = 0.5;


		inline float4 ASE_ComputeGrabScreenPos( float4 pos )
		{
			#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0;
			#else
			float scale = 1.0;
			#endif
			float4 o = pos;
			o.y = pos.w * 0.5f;
			o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
			return o;
		}


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( ase_screenPos );
			float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
			float2 temp_cast_0 = (_Float7).xx;
			float2 panner60 = ( 1.0 * _Time.y * temp_cast_0 + float2( 0,0 ));
			float2 uv_TexCoord47 = i.uv_texcoord * float2( 0.5,0.5 ) + panner60;
			float simplePerlin2D28 = snoise( uv_TexCoord47*_Float3 );
			simplePerlin2D28 = simplePerlin2D28*0.5 + 0.5;
			float4 screenColor2 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,( ase_grabScreenPosNorm + ( simplePerlin2D28 * _Float4 ) ).xy);
			o.Emission = ( screenColor2 * _Color0 ).rgb;
			o.Alpha = 1;
			float cos13 = cos( ( _Float2 * _Time.y ) );
			float sin13 = sin( ( _Float2 * _Time.y ) );
			float2 rotator13 = mul( i.uv_texcoord - float2( 0.5,0.5 ) , float2x2( cos13 , -sin13 , sin13 , cos13 )) + float2( 0.5,0.5 );
			float2 center45_g2 = float2( 0.5,0.5 );
			float2 delta6_g2 = ( rotator13 - center45_g2 );
			float angle10_g2 = ( length( delta6_g2 ) * _Float1 );
			float x23_g2 = ( ( cos( angle10_g2 ) * delta6_g2.x ) - ( sin( angle10_g2 ) * delta6_g2.y ) );
			float2 break40_g2 = center45_g2;
			float2 break41_g2 = float2( 0,0 );
			float y35_g2 = ( ( sin( angle10_g2 ) * delta6_g2.x ) + ( cos( angle10_g2 ) * delta6_g2.y ) );
			float2 appendResult44_g2 = (float2(( x23_g2 + break40_g2.x + break41_g2.x ) , ( break40_g2.y + break41_g2.y + y35_g2 )));
			float simplePerlin2D5 = snoise( appendResult44_g2*_Float0 );
			simplePerlin2D5 = simplePerlin2D5*0.5 + 0.5;
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			clip( ( simplePerlin2D5 * (float4( 0,0,0,0 ) + (tex2D( _TextureSample0, uv_TextureSample0 ) - float4( 0,0,0,0 )) * (float4( 1,1,1,1 ) - float4( 0,0,0,0 )) / (float4( 0.1981132,0.1981132,0.1981132,1 ) - float4( 0,0,0,0 ))) ).r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
1920;0;1920;1019;752.1075;-1038.648;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;62;-1773.688,1777.704;Inherit;False;Property;_Float7;Float 7;7;0;Create;True;0;0;0;False;0;False;0.1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;61;-1597.261,1587.528;Inherit;False;Constant;_Vector2;Vector 2;8;0;Create;True;0;0;0;False;0;False;0.5,0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;60;-1590.451,1764.711;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;47;-1292.36,1629.894;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;29;-1291.04,1912.47;Inherit;False;Property;_Float3;Float 3;5;0;Create;True;0;0;0;False;0;False;0;20;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;14;-1907.385,997.295;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-1891.671,906.4194;Inherit;False;Property;_Float2;Float 2;3;0;Create;True;0;0;0;False;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;28;-988.5813,1663.367;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;66;-891.5275,1901.48;Inherit;False;Property;_Float4;Float 4;6;0;Create;True;0;0;0;False;0;False;0;0.01;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-1644.671,949.3194;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;12;-1669.998,813.7019;Inherit;False;Constant;_Vector1;Vector 1;3;0;Create;True;0;0;0;False;0;False;0.5,0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;6;-1705.032,681.2958;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;-664.4274,1653.68;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GrabScreenPosition;63;-1033.688,1457.857;Inherit;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RotatorNode;13;-1429.356,830.8749;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-1337.283,1060.354;Inherit;False;Property;_Float1;Float 1;2;0;Create;True;0;0;0;False;0;False;0;20;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;18;-1048.61,1153.28;Inherit;True;Property;_TextureSample0;Texture Sample 0;4;0;Create;True;0;0;0;False;0;False;-1;None;8c4a7fca2884fab419769ccc0355c0c1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;8;-1051.764,803.5055;Inherit;True;Twirl;-1;;2;90936742ac32db8449cd21ab6dd337c8;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT;0;False;4;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;67;-308.5855,1654.194;Inherit;True;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-1018.401,1054.336;Inherit;False;Property;_Float0;Float 0;1;0;Create;True;0;0;0;False;0;False;0;7;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenColorNode;2;-34.92544,1637.79;Inherit;False;Global;_GrabScreen0;Grab Screen 0;0;0;Create;True;0;0;0;False;0;False;Object;-1;False;False;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;5;-770.401,886.3363;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;25;-715.6055,1147.319;Inherit;True;5;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0.1981132,0.1981132,0.1981132,1;False;3;COLOR;0,0,0,0;False;4;COLOR;1,1,1,1;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;73;166.2256,1778.829;Inherit;False;Property;_Color0;Color 0;8;0;Create;True;0;0;0;False;0;False;1,1,1,1;0.9622642,0.8397117,0.9425279,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-341.1047,942.5073;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;72;425.2256,1598.829;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;69;632.8183,1266.123;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;HeatDistortionShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Transparent;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;60;2;62;0
WireConnection;47;0;61;0
WireConnection;47;1;60;0
WireConnection;28;0;47;0
WireConnection;28;1;29;0
WireConnection;15;0;16;0
WireConnection;15;1;14;0
WireConnection;65;0;28;0
WireConnection;65;1;66;0
WireConnection;13;0;6;0
WireConnection;13;1;12;0
WireConnection;13;2;15;0
WireConnection;8;1;13;0
WireConnection;8;3;10;0
WireConnection;67;0;63;0
WireConnection;67;1;65;0
WireConnection;2;0;67;0
WireConnection;5;0;8;0
WireConnection;5;1;7;0
WireConnection;25;0;18;0
WireConnection;19;0;5;0
WireConnection;19;1;25;0
WireConnection;72;0;2;0
WireConnection;72;1;73;0
WireConnection;69;2;72;0
WireConnection;69;10;19;0
ASEEND*/
//CHKSM=CA84815B876FDD40592073025837BB1DAD3A120E