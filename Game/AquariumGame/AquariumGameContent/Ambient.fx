sampler s0;



float4 PixelShaderFunction1(float2 coords: TEXCOORD0) : COLOR0
{
	float4 color = tex2D(s0, coords);
	color.gb = color.r;
	return color;
}

float4 PixelShaderFunction2(float2 coords: TEXCOORD0) : COLOR0
{
	float4 color = tex2D(s0, coords);
	return color;
}



technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 PixelShaderFunction1();
	}
	pass Pass2
	{
		PixelShader = compile ps_2_0 PixelShaderFunction2();
	}
}