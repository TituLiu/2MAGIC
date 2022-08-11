// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Tornado"
{
	Properties
	{
		_Vector0("Vector 0", Vector) = (3,8,0,0)
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Float0("Float 0", Float) = 10.41
		_Vector1("Vector 1", Vector) = (-0.3,-0.3,0,0)
		_Float1("Float 1", Float) = 15.01
		_Color3("Color 3", Color) = (1,0.918384,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float2 _Vector0;
		uniform float _Float1;
		uniform float2 _Vector1;
		uniform float _Float0;
		uniform float4 _Color3;
		uniform float _Cutoff = 0.5;


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


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float mulTime99 = _Time.y * _Float1;
			float2 panner102 = ( mulTime99 * _Vector1 + i.uv_texcoord);
			float2 uv_TexCoord103 = i.uv_texcoord * _Vector0 + panner102;
			float simplePerlin2D83 = snoise( uv_TexCoord103*_Float0 );
			simplePerlin2D83 = simplePerlin2D83*0.5 + 0.5;
			float4 myVarName81 = ( step( simplePerlin2D83 , 0.4 ) * _Color3 );
			o.Albedo = myVarName81.rgb;
			o.Emission = myVarName81.rgb;
			o.Alpha = 1;
			clip( myVarName81.r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
940;73;515;632;644.8782;-2492.323;1;False;False
Node;AmplifyShaderEditor.RangedFloatNode;97;-1431.8,3208.232;Inherit;False;Property;_Float1;Float 1;4;0;Create;True;0;0;0;False;0;False;15.01;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;99;-1267.8,3214.232;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;100;-1333.384,3040.396;Inherit;False;Property;_Vector1;Vector 1;3;0;Create;True;0;0;0;False;0;False;-0.3,-0.3;0.5,-0.2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;98;-1365.384,2898.6;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;101;-932.6869,2756.414;Inherit;False;Property;_Vector0;Vector 0;0;0;Create;True;0;0;0;False;0;False;3,8;-0.2,16.45;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;102;-1023.385,2917.94;Inherit;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;104;-623.6905,2888.737;Inherit;False;Property;_Float0;Float 0;2;0;Create;True;0;0;0;False;0;False;10.41;1.39;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;103;-701.3118,2761.913;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;83;-303.941,2765.319;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;75;11.04014,2767.716;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0.4;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;79;72.99202,2943.243;Inherit;False;Property;_Color3;Color 3;5;0;Create;True;0;0;0;False;0;False;1,0.918384,0,0;1,0.918384,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;76;373.3719,2730.049;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;77;373.4761,2069.593;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;78;63.67605,2185.392;Inherit;False;Constant;_Color2;Color 2;11;0;Create;True;0;0;0;False;0;False;1,0.649691,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;81;623.8837,2720.331;Inherit;True;myVarName;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;93;800.7265,2162.174;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;74;70.20988,2077.494;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0.18;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1012.502,2629.048;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Tornado;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;TransparentCutout;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;99;0;97;0
WireConnection;102;0;98;0
WireConnection;102;2;100;0
WireConnection;102;1;99;0
WireConnection;103;0;101;0
WireConnection;103;1;102;0
WireConnection;83;0;103;0
WireConnection;83;1;104;0
WireConnection;75;0;83;0
WireConnection;76;0;75;0
WireConnection;76;1;79;0
WireConnection;77;0;74;0
WireConnection;77;1;78;0
WireConnection;81;0;76;0
WireConnection;93;0;77;0
WireConnection;0;0;81;0
WireConnection;0;2;81;0
WireConnection;0;10;81;0
ASEEND*/
//CHKSM=88CA9CCE4079D87DE40714C49E3CE100D7722784