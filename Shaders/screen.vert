#version 450 core
layout(location = 0) in vec3 inPos;
layout(location = 1) in vec2 inUV;

out vec2 TexCoord;
uniform mat4 uFinalMvp; // We will send this from VB.NET

void main() {
    gl_Position = uFinalMvp * vec4(inPos, 1.0);
    TexCoord = inUV;
}