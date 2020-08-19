#version 430
#extension GL_ARB_bindless_texture : require

in vec2 UV;
layout(rgba8, bindless_image) uniform image2D prev;
out vec4 color;
uniform ivec2 screenSize;

void main() 
{
    color = vec4(imageLoad(prev, ivec2((UV.x+1)*screenSize.x/2, (UV.y+1)*screenSize.y/2)));
}