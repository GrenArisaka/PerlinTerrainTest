Shader "Unlit/waterShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct VertexInput
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
            };

            struct VertexOutput
            {
                
				half3 normal; TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

			VertexOutput vert (VertexInput v)
            {
				VertexOutput o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                
				o.normal = v.normal;
                return o;
            }

            fixed4 frag (VertexOutput i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                
                return col;
            }
            ENDCG
        }
    }
}
