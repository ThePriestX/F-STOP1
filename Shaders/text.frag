#version 450 core

// Must match 'out vec2 outTexCoord' in your new Text.vert
in vec2 outTexCoord;

out vec4 color;

uniform sampler2D uTextTex;
uniform float uTextOpacity;

void main()
{
    // 1. Grab the raw pixel from the text texture
    vec4 texel = texture(uTextTex, outTexCoord);
    
    // 2. Apply the opacity slider
    vec3 rgb = texel.rgb;
    float alpha = texel.a * uTextOpacity;

    // 3. THE MATCH-FIX: Gamma Correction (1.0 / 2.2)
    // This prevents the "10% looking like 20%" issue on export.
    // It lightens the mid-tones so the shade matches the Viewport.
    rgb = pow(rgb, vec3(1.0 / 2.2));

    color = vec4(rgb, alpha);
}