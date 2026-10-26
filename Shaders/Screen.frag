#version 450 core
out vec4 FragColor;
in vec2 TexCoord;

// These names MUST match the GL.GetUniformLocation calls in VB.NET
uniform sampler2D photoTex; // Bound to TextureUnit.Texture0
uniform sampler2D textTex;  // Bound to TextureUnit.Texture1

void main() {
    vec4 photo = texture(photoTex, TexCoord);
    
    // Flip the Y coordinate for the text texture only
    vec2 flippedUV = vec2(TexCoord.x, 1.0 - TexCoord.y);
    vec4 text = texture(textTex, flippedUV);
    
    FragColor = mix(photo, text, text.a);
}