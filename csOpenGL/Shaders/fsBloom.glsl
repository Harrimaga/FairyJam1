#version 430
#extension GL_ARB_bindless_texture : require

in vec2 UV;
layout(rgba32f, bindless_image) uniform image2D prev;
uniform ivec2 screenSize;

void main() 
{
    ivec2 pos = ivec2((UV.x+1)*screenSize.x/2, (UV.y+1)*screenSize.y/2);
	vec3 colour = vec3(0, 0, 0);
	float num = 0;
	int bloomX = screenSize.x/480;
	int bloomY = screenSize.x/270;
	vec4 baseColour = imageLoad(prev, pos);
	for(int i=-bloomX;i<bloomX+1;i++){
		for(int j=-bloomY;j<bloomY+1;j++){
			ivec2 posi = pos + ivec2(i, j);
			if(!(posi.x < 0 || posi.y < 0 || posi.x >= screenSize.x || posi.y >= screenSize.y)) 
			{
				vec4 c = imageLoad(prev, pos + ivec2(i, j)) - vec4(1, 1, 1, 0);
				c = clamp(c, vec4(0, 0, 0, 0), vec4(999, 999, 999, 999));
 				colour += c.xyz*c.w;
			}
			num++;
		}
	}
	if(num > 0) 
	{
		gl_FragColor = baseColour + vec4(colour/num, 0);
	}
	else {
		gl_FragColor = vec4(0, 0, 0, 0);
	}
}