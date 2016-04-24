Shader "Unlit/gradient_alpha"
{
	

 Properties {
    _Color ("Main Color", Color) = (1,1,1,1)
    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
}
 
SubShader {
Lighting Off
    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}

 
CGPROGRAM
#pragma surface surf Lambert alpha
//#pragma surface surf NoLighting noforwardadd
//  Lighting Off

sampler2D _MainTex;
fixed4 _Color;
 
struct Input {
    float2 uv_MainTex;
};
 
void surf (Input IN, inout SurfaceOutput o) {
    fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
    o.Emission = c.rgb;

    float2 center = (0.5, 0.5);
    float rad = length(IN.uv_MainTex - center);

    if (rad <= 0.4){o.Alpha = 1;} 
    if (rad > 0.4) {o.Alpha = 1.0 - (rad - 0.4)/0.09;}
    if (rad > 0.49){o.Alpha = 0;}
}
ENDCG
}


Fallback off
 
}
