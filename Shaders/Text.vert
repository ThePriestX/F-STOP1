#version 450 core

layout(location = 0) in vec3 inPos;
layout(location = 1) in vec2 inUV;

// We use 'outTexCoord' as a unique name to avoid any confusion
out vec2 outTexCoord;

uniform mat4 uFinalMvp;

void main() {
    gl_Position = uFinalMvp * vec4(inPos, 1.0);
    outTexCoord = inUV;
}