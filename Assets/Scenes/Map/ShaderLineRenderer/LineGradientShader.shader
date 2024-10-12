Shader "Custom/LineGradientShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color1 ("Color 1", Color) = (1, 0.6, 0, 1) // Orange clair
        _Color2 ("Color 2", Color) = (0, 0, 0, 1)   // Noir
        _Width ("Line Width", Range(0.1, 1.0)) = 0.15 // Ajuster la plage
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            fixed4 _Color1;
            fixed4 _Color2;
            float _Width;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Ajuster les UV pour le dégradé
                o.uv = v.uv; // Passer les coordonnées UV directement
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Calculer le facteur de dégradé basé sur la largeur
                float distance = abs(i.uv.x - 0.5) * (1.0 / _Width); // Utiliser 0.5 pour centrer
                float factor = smoothstep(0.0, 1.0, 1.0 - distance); // Créer un dégradé

                return lerp(_Color2, _Color1, factor); // Lerp entre noir et orange
            }
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}
