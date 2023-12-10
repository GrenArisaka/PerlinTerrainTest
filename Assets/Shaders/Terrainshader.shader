Shader "Unlit/Terrainshader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work


            #include "UnityCG.cginc"

            struct VertexInput
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
            };

            struct VertexOutput
            {
                float2 uv : TEXCOORD0;
				float3 normal : TEXCOORD1;
                float4 vertex : SV_POSITION;

            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            VertexOutput vert (VertexInput v)
            {
				VertexOutput o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.normal = v.normal;
                return o;
            }

            fixed4 frag (VertexOutput i) : SV_Target
            {
				float3 up = float3(0,1,0);
				float3 normal = i.normal;
				float angle = saturate(dot(up, normal));
				float4 col = float4(0.3, 0.5, 0.3, 0);

				angle = angle / 0.95;
				angle = saturate(pow(angle, 10));

				float4 brown = float4(0.75, 0.6, 0.4, 0);
				float4 green = float4(0.3, 0.5, 0.3, 0);
				
				col = float4(angle*green + (1 - angle)*brown);
				





                return col;
            }
            ENDCG
        }
    }
}
