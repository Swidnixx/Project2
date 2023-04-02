Shader "Lines/Colored Blended"
{
	SubShader
	{
		Pass 
		{
			Blend SrcAlpha OneMinusSrcAlpha
			//Blend Off
			ZWrite Off Cull Off Fog { Mode Off }
			BindChannels 
			{
				Bind "vertex", vertex Bind "color", color 
			}
		}
	} 
}
