// Effect uses a scrolling displacement texture to offset the position of the main
// texture. Depending on the contents of the displacement texture, this can give a
// wide range of refraction, rippling, warping, and swirling type effects.

float2 DisplacementScroll;
float2 angle;

sampler TextureSampler : register(s0);
sampler DisplacementSampler : register(s1);

float2x2 RotationMatrix(float rotation)
{
	float c = cos(rotation);
	float s = sin(rotation);

	return float2x2(c, -s, s, c);
}

float4 main(float4 color : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
{
	float2 rotated_texcoord = texCoord;
	rotated_texcoord -= float2(0.25, 0.25);
	rotated_texcoord = mul(rotated_texcoord, RotationMatrix(angle));
	rotated_texcoord += float2(0.25, 0.25);

	float2 DispScroll = DisplacementScroll;

		// Look up the displacement amount.
		float2 displacement = tex2D(DisplacementSampler, DispScroll + texCoord / 3);

		// Offset the main texture coordinates.
		texCoord += displacement * 0.2 - 0.15;

	// Look up into the main texture.
	return tex2D(TextureSampler, texCoord) * color;
}


technique Refraction
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 main();
	}
}