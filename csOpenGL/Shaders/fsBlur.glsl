#version 430
#extension GL_ARB_bindless_texture : require

in vec2 UV;
layout(rgba8, bindless_image) uniform image2D prev;
out vec4 color;
uniform ivec2 screenSize;

void main() 
{
    ivec2 pos = ivec2((UV.x+1)*screenSize.x/2, (UV.y+1)*screenSize.y/2);
	vec3 colour = vec3(0, 0, 0);
	float num = 0;
	for(int i=-1;i<2;i++){
		for(int j=-1;j<2;j++){
			ivec2 posi = pos + ivec2(i, j);
			if(!(posi.x < 0 || posi.y < 0 || posi.x >= screenSize.x || posi.y >= screenSize.y)) 
			{
				vec4 c = imageLoad(prev, pos + ivec2(i, j));
 				colour += c.xyz*c.w;
				num++;
			}
		}
	}
	if(num > 0) 
	{
		color = vec4(colour/num, 1);
	}
	else {
		color = vec4(0 ,0, 0, 0);
	}
}