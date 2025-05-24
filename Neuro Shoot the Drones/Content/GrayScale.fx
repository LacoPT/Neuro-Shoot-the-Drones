#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

sampler inputTexture : register(s0); // Input texture
float Intensity; // Intensity of grayscale (1.0 = full grayscale)

struct VertexShaderInput
{
	float4 Position : POSITION0;
	float4 Color : COLOR0;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
};

float4 MainPS(float4 position : SV_POSITION, float2 coords : TEXCOORD0) : SV_Target
{
    float4 color = tex2D(inputTexture, coords);
    
    // Convert RGB to grayscale using standard luminance formula
    float gray = dot(color.rgb, float3(0.299, 0.587, 0.114));
    float3 grayscaleColor = float3(gray, gray, gray);
    
    // Blend between original and grayscale using Intensity
    float3 mixedColor = lerp(color.rgb, grayscaleColor, Intensity);
    
    return float4(mixedColor, color.a);
}

technique BasicColorDrawing
{
	pass P0
	{
		//VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};