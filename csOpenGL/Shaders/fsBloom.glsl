#version 430
#extension GL_ARB_bindless_texture : require

in vec2 UV;
layout(rgba32f, bindless_image) uniform image2D prev;
uniform ivec2 screenSize;

void main() 
{
    ivec2 pos = ivec2((UV.x+1)*screenSize.x/2, (UV.y+1)*screenSize.y/2);
	vec3 colour = vec3(0, 0, 0);
	int bloomX = screenSize.x/480;
	int bloomY = screenSize.y/270;
	vec4 baseColour = imageLoad(prev, pos);
	int bxb = bloomX*bloomY;
	for(int i=-bloomX;i<bloomX+1;i++){
		for(int j=-bloomY;j<bloomY+1;j++){
			vec4 c = imageLoad(prev, pos + ivec2(i, j)) - vec4(1, 1, 1, 0);
			if(c.x > 0 || c.y > 0 || c.z > 0) {
				c = max(c, vec4(0, 0, 0, 0)) * (bxb - abs(i*j))/(bxb);
 				colour += c.xyz*c.w;
			}
		}
	}
	gl_FragColor = baseColour + vec4(colour/bxb, 0);
}