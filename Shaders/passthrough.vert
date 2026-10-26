#version 450 core

layout(location = 0) in vec2 inPos;   // From VB.NET Vertices (X, Y)
layout(location = 1) in vec2 inUV;    // From VB.NET Vertices (U, V)

out vec2 TexCoord;

void main()
{
    // Directly use the NDC coordinates calculated in VB.NET
    gl_Position = vec4(inPos, 0.0, 1.0);
    TexCoord = inUV;
}