// sphere_vol_fog.shader
//
// By: Ismael Cortez
// Date: 06-02-2020
// CMPM 163 Final Project: Foggy Forest
//
// Adapted from:
//	https://forum.unity.com/threads/spherical-fog-shader-shared-project.269771/
//
Shader "Custom/Sphere Volume Fog"
{
    Properties
    {
        // base color of the fog
        _FogBaseColor ("Fog Base Color", Color) = (0,1,1,1)
        // color as fog grows denser
        _FogDenseColor ("Fog Dense Color", Color) = (1,1,1,1)
        // ratio between the two colors
        _InnerRatio ("Inner Ratio", Range (0.0, 0.9999)) = 0.5
        // fog density
        _Density ("Density", Range (0.0, 10.0)) = 10.0
        // lighting fade over distance?
        _ColorFalloff ("Color Falloff", Range (0.0, 50.0)) = 16.0
    }
    SubShader
    {
        // culling and depth testing
		// controls which sides of polygons should be culled (not drawn)
		Cull Off
		// controls whether pixels from the object are written to the depth buffer
		// set off for semitransparent effects
		ZWrite Off

		// use blending to make transparent objects
		// alpha blending for traditional transparency
		Blend SrcAlpha OneMinusSrcAlpha

		// set to off so that color is taken from the color command
		Lighting Off
		
		// depth testing performance is off
		ZTest Always

        // Tags tell when and how to be rendered
        Tags{"Queue"="Transparent+99" "IgnoreProjector"="True" "RenderType"="Transparent" "ForceNoShadowCasting"="true"}

        Pass
        {
            CGPROGRAM
            // DX11 shader model 4.0
            #pragma target 4.0
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            inline float calc(float3 center, float radius, float i_ratio, float density, float3 cam_pos, float3 view_dir, float max_dist)
            {
                // render the fog using the technique of volume ray casting
                // cast a ray from the viewing position to the vertex we want to fog
                // if this ray intersects the fog volume then calculate
                // the depth of the volume penetrated by the ray

                // is the cam position in local space of the sphere
                float3 view_pos = cam_pos - center;

                // calculate ray-sphere intersection points
                // use the standard quadratic equation ax^2 + bx + c = 0
                float a = dot(view_dir, view_dir);
                float b = 2 * dot(view_dir, view_pos);
                float c = dot(view_pos, view_pos) - radius * radius;

                // discriminant of quadratic formula
                float d = b * b - 4 * a * c;

                // if the discriminant of this equation is less than or equal
                // to zero the ray does not pass through the sphere
                if(d <= 0.0f) return 0;

                // 1/(2a)
                float recp_two_a = 0.5 / a;
                float sqrt_d = sqrt(d);

                // distance from camera to front of shpere (0 if inside sphere)
                // this is where the sampling should begin
                float dist_a = max((-b - sqrt_d) * recp_two_a, 0);

                // distance from camera to back of sphere (0 if inside sphere)
                float dist_b = max((-b + sqrt_d) * recp_two_a, 0);

                // sampling ends at min distance of back of sphere or solid surface hit
                // the lower value is the closest intersection point
                float back_depth = min(max_dist, dist_b);

                float sample = dist_a;
                // caldulate the distance between samples
                float step_dist = (back_depth - dist_a) / 10;

                // step_contribution is a value where 0 means no reduction in clarity
                // on the other hand, a value of 1 means full reduction in clarity
                // step_contribution approaches 1 as step_dist increases
                float step_contribution = (1 - 1 / pow(2, step_dist)) * density;

                // calculate the center value needed to make the value 1 at
                // the desired inner ratio
                // this high value does not actually produce high density in
                // the center since it's clamped to 1
                float center_val = 1 / (1 - i_ratio);

                // init clarity to 1 which is full clarity and no fog
                float clarity = 1;
                for(int i = 0; i < 10; ++i)
                {
                    float3 pos = view_pos + view_dir * sample;
                    float val = saturate(center_val * (1 - length(pos) / radius));
                    float sample_amount = saturate(val * step_contribution);
                    clarity *= (1 - sample_amount);
                    sample += step_dist;
                }
                return (1 - clarity);
            }
            
            fixed4 _FogBaseColor;
            fixed4 _FogDenseColor;
            float _InnerRatio;
            float _Density;
            float _ColorFalloff;
            sampler2D _CameraDepthTexture;
            uniform float4 FogParam;

            struct v2f
            {
                // vertex shader output semantic
                float4 pos : SV_POSITION;
                // first UV coordinate
                float3 view : TEXCOORD0;
                // second UV coordinate
                float4 projPos : TEXCOORD1;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                float4 wPos = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex);
                o.view = wPos.xyz - _WorldSpaceCameraPos;
                o.projPos = ComputeScreenPos(o.pos);

                // move projected z to near plane if point is behind near plane
                float inFrontOf = (o.pos.z / o.pos.w) > 0;
                o.pos.z *= inFrontOf;
                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                half4 color = half4(1,1,1,1);
                float depth = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos))));
                float3 view_dir = normalize(i.view);

                // calculate fog density
                // currently scaled by a factor of 1000 to avoid precision
                // errors for large volumes
                float fog = calc(FogParam.z * 0.001, FogParam.w * 0.001, _InnerRatio, _Density * 10, _WorldSpaceCameraPos * 0.001, view_dir, depth * 0.001);

                // calculate ratio of dense color
                float dense_color_ratio = pow(fog, _ColorFalloff);

                // set color based on denseness and alpha based on raw calculated fog density
                color.rgb = lerp(_FogBaseColor.rgb, _FogDenseColor.rgb, dense_color_ratio);
                color.a = fog * lerp(_FogBaseColor.a, _FogDenseColor.a, dense_color_ratio);
                return color;
            }
            ENDCG
        }
    }
    Fallback "VertexLit" // optional fallback
}

