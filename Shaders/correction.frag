#version 450 core

layout(location = 0) in vec2 TexCoord;
out vec4 FragColor;

// Explicitly define the slots (Bindings)
layout(binding = 0) uniform sampler2D image;      // Slot 0: The main photo
layout(binding = 1) uniform sampler3D lutTexture; // Slot 1: The 3D LUT (.cube)


// Uniforms
uniform float uExposure; 
uniform float uContrast;
uniform float uHighlights;
uniform float uShadowsLift;
uniform float uWhites;
uniform float uBlacks;
uniform float uTemp;
uniform float uTint;
uniform float uDynCon;
uniform float uClarity;
uniform float uStructX;
uniform float uTextureX;
uniform float uSharpen;
uniform float uDefringe;
uniform float uDenoise;
uniform float uNoiseLimit;
uniform float uNoiseChroma;
uniform float uNoiseLuma;
uniform float uSaturation;
uniform float uVibrance;
uniform float uRedMix;
uniform float uGreenMix;
uniform float uBlueMix;
uniform float uDeHaze;
uniform float uVignette;
uniform float uVignetteStd;
uniform float uFilmGrain;
uniform float uGrainSize;   // Size (Range 0.0 to 1.0)
uniform float uVivid;
uniform float uSepiaMix;
uniform float uNoirFactor;
uniform float uSplitToning;
uniform float uFade;
uniform float uFadeTint;
uniform float uCinematic;
uniform float uCineBalance;
uniform float uLutIntens;
uniform float uBarrelDistortion;

uniform vec3 uTargetRGB;      // The color to find (from txtHexa_Corect)
uniform vec3 uReplaceRGB;     // The color to use (from txtCorrectedHexa_Correct)
uniform float uRadius;        // Sensitivity (from sldHexaCorrect)
uniform float uSmoothness;    // Optional: for soft edges (0.01 to 0.1)
//uniform float uUseReplace;    // From cb_UseReplace.Checked (Pass as 1.0 or 0.0)
uniform vec2 uSelectionCenter; // The U, V position of the selection
uniform float uSelectionRange; // How large the selection area is
uniform float uOpacity; // New slider value (0.0 to 1.0)
uniform vec3 uRectangleSelColor;

uniform int useLut;
uniform bool uBypass;
uniform bool uShowShadows;
uniform bool uShowHighlights;
uniform float uTime;
uniform vec4 uCropRect; // x, y, width, height (all 0.0 to 1.0)
uniform bool uIsCropping;
uniform bool uIsCropApplied;
uniform int uFlipY; // Add this at the top with your other uniforms
uniform bool uLevelHorizon; // NEW: Flag to enable line-alignment correction
uniform mat4 uRotationMatrix; 
uniform float uAutoFillScale;


// Exposure function (Stays the same)
void applyExposure(inout vec3 color)
{
    if (abs(uExposure) < 0.001)
        return;

    float exposureScale = exp2(uExposure);

    if (uExposure > 0.0) {
        float luma = dot(color, vec3(0.299, 0.587, 0.114));
        
        float protection = smoothstep(0.0, 1.2, luma);
        
        float adaptiveScale = mix(exposureScale, 1.0, protection);
        
        color.rgb *= adaptiveScale;
    } else {
        color.rgb *= exposureScale;
    }

    color = clamp(color, 0.0, 1.0);
}



void applyContrast(inout vec3 color)
{
    float s = uContrast;
    if (abs(s) < 0.0001) return;

    // --- compute original luma and chroma ---
    float originalLuma = dot(color, vec3(0.299, 0.587, 0.114));
    vec3 chroma = color - vec3(originalLuma);

    float luma = originalLuma;

    float pivot = 0.5;
    float x = luma - pivot;

    // --- asymmetric strength ---
    float strengthPos = 0.6;
    float strengthNeg = 0.3;
    float strength = (s > 0.0) ? s * strengthPos : s * strengthNeg;

    // --- improved falloff ---
    float t = pow(abs(x) * 2.0, 0.8);
    float falloff = 1.0 - t * 0.7;

    float curved = x * (1.0 + strength * falloff);
    luma = curved + pivot;

    // --- highlight softening (right side) ---
    if (s > 0.0)
    {
        float h = smoothstep(0.7, 1.0, luma);
        luma -= h * 0.04 * s;
    }

    // --- anti-fog midtone boost (left side) ---
    if (s < 0.0)
    {
        float xm = (luma - 0.5) * 0.2;

        // tighter, bell-shaped mid mask
        float m = 1.0 - xm * xm;     // parabola
        m = clamp(m, 0.0, 1.0);

        // slightly stronger but more focused
        float bias = smoothstep(0.0, 0.5, luma); // favor darker mids
        luma += m * bias * (-s) * 0.09;

        // --- subtle left-side color boost ---
        float midMask = 1.0 - pow((luma - 0.5) * 2.0, 2.0); // parabola
        chroma += chroma * (-s) * 0.3 * midMask;           // tiny color boost
    }

    // --- subtle texture preservation ---
    luma = mix(luma, originalLuma, 0.1);

    // --- saturation ---
    float sat = 1.0 + max(s, 0.0) * 0.1; // only positive contrast boosts saturation
    color = vec3(luma) + chroma * sat;

    color = clamp(color, 0.0, 1.0);
}




void applyHighlights(inout vec3 color)
{
    float h = clamp(uHighlights, -2.0, 2.0); 
    if (abs(h) < 0.0001) return;

    const vec3 lumaWeights = vec3(0.2126, 0.7152, 0.0722);
    float luma = dot(color, lumaWeights);

    // --- adjustable recovery start ---
    // smaller startLuma = more travel to the left (affects lower highlights)
    float startLuma = 0.1; // adjust this to control "left slider travel"
    float endLuma   = 1.0;  // full recovery at top whites
    float mask = smoothstep(startLuma, endLuma, luma);
    mask = pow(mask, 1.5); // soft ramp  
    float strength = abs(h) * mask;

    if (h < 0.0)
    {
        // HIGHLIGHT RECOVERY
        float targetLuma = luma / (1.0 + strength * (1.0 - luma));
        float finalLuma = mix(luma, targetLuma, mask);
        float ratio = (luma > 1e-5) ? (finalLuma / luma) : 1.0;
        color *= ratio;

        // Slightly boost saturation in recovered areas
        float newLuma = dot(color, lumaWeights);
        color = mix(color, vec3(newLuma), -strength * 0.5); 
    }
    else
    {
        // EXPANSION (Brighten)
        vec3 boost = (1.0 - color) * strength * 0.4;
        color += boost * luma;
    }

    color = clamp(color, 0.0, 1.0);
}




void applyShadowsLift(inout vec3 color)
{
    float s = uShadowsLift; 
    if (abs(s) < 0.0001) return;

    const vec3 lumaWeights = vec3(0.2126, 0.7152, 0.0722);
    float luma = dot(color, lumaWeights);

    float strength = s; // Use raw s to preserve direction
    float targetLuma;

    if (s > 0.0) {
        // --- THE SURGICAL MASK ---
        // (1.0 - luma)^3.0 creates a very steep drop-off.
        // It's 1.0 at black, 0.12 at middle-gray, and 0.0 at highlights.
        // This is what protects the sky and light bulbs.
        float shadowMask = pow(1.0 - luma, 3.0);
        
        // We apply the lift only within that mask
        float lift = strength * luma * shadowMask;
        targetLuma = luma + (lift * 1.5); // 1.5 for that extra travel you wanted
    } else {
        // --- THE DEEP CRUNCH ---
        // We use the same mask logic to ensure we only deepen darks
        float deepenMask = pow(1.0 - luma, 2.0);
        targetLuma = luma - (abs(strength) * luma * deepenMask * 0.5);
    }

    // Ratio-based scaling to maintain "On1" color depth
    float ratio = (luma > 0.0001) ? (targetLuma / luma) : 1.0;
    vec3 colorRatio = vec3(ratio);
    
    // Your Red-channel stability fix
    colorRatio.r = mix(1.0, ratio, 0.85); 

    color *= colorRatio;

    // PIN THE BLACKS: Prevent the "fog" by keeping absolute 0 at 0.
    color = mix(vec3(0.0), color, smoothstep(0.0, 0.02, luma));

    color = clamp(color, 0.0, 1.0);
}



void applyWhites(inout vec3 color)
{
    // Adjusted to handle your -2.0 to 2.0 range
    float w = clamp(uWhites, -2.0, 2.0);
    
    if (abs(w) < 0.0001)
        return;

    float luma = dot(color, vec3(0.2126, 0.7152, 0.0722));

    float exponent;

    if (w > 0.0)
        // Expand whites: s=2.0 maps to exponent 0.5 (aggressive bright lift)
        exponent = mix(1.0, 0.5, w / 2.0); 
    else
        // Compress whites: s=-2.0 maps to exponent 2.0 (aggressive highlight compression)
        exponent = mix(1.0, 2.0, -w / 2.0); 

    float newLuma = pow(max(luma, 0.0), exponent);

    float ratio = newLuma / max(luma, 1e-5);
    color *= ratio;
    
    color = clamp(color, 0.0, 1.0);
}


void applyBlacks(inout vec3 color)
{
    // Adjusted to handle your -2.0 to 2.0 range
    float b = clamp(uBlacks, -2.0, 2.0);
    
    if (abs(b) < 0.0001)
        return;

    float luma = dot(color, vec3(0.2126, 0.7152, 0.0722));
    float safeLuma = clamp(luma, 0.0, 1.0);

    // Exponent curve pivoting at white (1.0)
    float exponent;
    
    if (b > 0.0)
        // Lift blacks: b=2.0 maps to exponent ~2.2 (strong shadow lift)
        exponent = mix(1.0, 2.2, b / 2.0); 
    else
        // Deepen blacks: b=-2.0 maps to exponent ~0.4 (strong black crush)
        exponent = mix(1.0, 0.4, -b / 2.0);

    float newLuma = pow(safeLuma, exponent);

    // Ratio calculation with safety guards
    float ratio = newLuma / max(safeLuma, 1e-4);
    ratio = clamp(ratio, 0.0, 10.0);

    color *= ratio;
    color = clamp(color, 0.0, 1.0);
}


void applyTemp(inout vec3 color)
{
    // Adjusted to handle your -2.0 to 2.0 range
    float t = clamp(uTemp, -2.0, 2.0);
    
    if (abs(t) < 0.0001)
        return;

    // Non-linear response for stronger feel
    // With t=2.0, strength becomes ~2.3
    float strength = sign(t) * pow(abs(t), 1.2);

    // Multipliers for Red and Blue
    // We use 0.25 to keep the color shift manageable at +/- 2.0
    float rMul = clamp(1.0 + strength * 0.25, 0.1, 3.0);
    float bMul = clamp(1.0 - strength * 0.25, 0.1, 3.0);

    color.r *= rMul;
    color.b *= bMul;

    // Optional mild green compensation (prevents magenta/green cast)
    // Scaled by 0.5 to account for the -2 to 2 range
    color.g *= mix(1.0, 0.96, abs(strength) * 0.5);
    
    color = clamp(color, 0.0, 1.0);
}


void applyTint(inout vec3 color)
{
    // Adjusted to handle your -2.0 to 2.0 range
    float t = clamp(uTint, -2.0, 2.0);
    
    if (abs(t) < 0.0001)
        return;

    // Non-linear response (feels more natural on slider)
    float strength = sign(t) * pow(abs(t), 1.2);

    // Green/Magenta shift
    // Scaled by 0.25 to prevent completely killing the green channel at +2.0
    float gMul = clamp(1.0 + strength * 0.25, 0.1, 3.0);

    color.g *= gMul;

    // Small compensation for Red and Blue to maintain overall luminance
    float rbComp = clamp(1.0 - strength * 0.06, 0.5, 1.5);
    color.r *= rbComp;
    color.b *= rbComp;
    
    color = clamp(color, 0.0, 1.0);
}


void applyDynCon(inout vec3 color)
{
    // Adjusted to handle your -2.0 to 2.0 range
    float c = clamp(uDynCon, -2.0, 2.0);
    
    if (abs(c) < 0.0001)
        return;

    float luma = dot(color, vec3(0.2126, 0.7152, 0.0722));
    float safeLuma = clamp(luma, 0.0, 1.0);

    // Midtone mask (bell-shaped curve: peaks at 0.5, zero at 0.0 and 1.0)
    float midMask = 4.0 * safeLuma * (1.0 - safeLuma);

    // Apply strength based on the mask
    // We scale by 0.5 to keep the shift manageable even at c = 2.0
    float contrastStrength = c * midMask * 0.5;

    // Pivot around 0.5 (middle gray)
    float newLuma = safeLuma + contrastStrength * (safeLuma - 0.5);

    // Calculate ratio and apply to RGB channels
    float ratio = newLuma / max(safeLuma, 1e-4);
    ratio = clamp(ratio, 0.0, 5.0); // Clamped to 5.0 for stability

    color *= ratio;
    color = clamp(color, 0.0, 1.0);
}


void applyClarity(inout vec3 color, vec2 uv)
{
    float c = clamp(uClarity, -2.0, 2.0);
    if (abs(c) < 0.01) return;

    vec2 texel = 1.0 / textureSize(image, 0);
    vec3 centerOrig = texture(image, uv).rgb;
    
    vec3 avg = vec3(0.0);
    float spread = 3.0;
    
    for(float x = -1.0; x <= 1.0; x++) {
        for(float y = -1.0; y <= 1.0; y++) {
            avg += texture(image, uv + (vec2(x, y) * spread * texel)).rgb;
        }
    }
    avg /= 9.0;

    float lumaOrig = dot(centerOrig, vec3(0.299, 0.587, 0.114));
    float blurLuma = dot(avg,        vec3(0.299, 0.587, 0.114));
    
    float highPass = lumaOrig - blurLuma;

    float lumaProcessed = dot(color.rgb, vec3(0.299, 0.587, 0.114));
    
    float softMask = lumaProcessed * (1.0 - lumaProcessed);
    softMask = clamp(softMask * 4.0, 0.0, 1.0);
    
    float clarityEffect = highPass * c * 0.6;
    
    color.rgb += clarityEffect * softMask;

    color.rgb = mix(color.rgb, clamp(color.rgb, 0.0, 1.0), 0.5);
    color = clamp(color, 0.0, 1.0);
}



void applyStructX(inout vec3 color, vec2 uv)
{
    float amount = clamp(uStructX, 0.0, 2.0);

    if (amount <= 0.0001)
        return;

    vec2 texel = 1.0 / textureSize(image, 0);
    vec3 centerOrig = texture(image, uv).rgb;
    
    vec3 blur = vec3(0.0);
    
    float adaptiveRadius = 2.5 + (amount * 1.5);
    
    blur += texture(image, uv + texel * vec2(-adaptiveRadius, -adaptiveRadius)).rgb;
    blur += texture(image, uv + texel * vec2( adaptiveRadius, -adaptiveRadius)).rgb;
    blur += texture(image, uv + texel * vec2(-adaptiveRadius,  adaptiveRadius)).rgb;
    blur += texture(image, uv + texel * vec2( adaptiveRadius,  adaptiveRadius)).rgb;
    blur += texture(image, uv + texel * vec2(-adaptiveRadius, 0.0)).rgb;
    blur += texture(image, uv + texel * vec2( adaptiveRadius, 0.0)).rgb;
    blur += texture(image, uv + texel * vec2(0.0, -adaptiveRadius)).rgb;
    blur += texture(image, uv + texel * vec2(0.0,  adaptiveRadius)).rgb;
    blur /= 8.0;

    float lumCenter = dot(centerOrig, vec3(0.299, 0.587, 0.114));
    float lumBlur   = dot(blur,       vec3(0.299, 0.587, 0.114));

    float midFreq = lumCenter - lumBlur;
    
    float boost = amount * 1.2;
    color.rgb += midFreq * boost;

    color = clamp(color, 0.0, 1.0);
}



void applySharpen(inout vec3 color, vec2 uv)
{
    float amount = clamp(uSharpen, 0.0, 2.0);

    if (amount <= 0.0001)
        return;

    vec2 texel = 1.0 / textureSize(image, 0);

    vec3 blur = vec3(0.0);
    blur += texture(image, uv + texel * vec2(-1.5, -1.5)).rgb;
    blur += texture(image, uv + texel * vec2( 1.5, -1.5)).rgb;
    blur += texture(image, uv + texel * vec2(-1.5,  1.5)).rgb;
    blur += texture(image, uv + texel * vec2( 1.5,  1.5)).rgb;
    blur += texture(image, uv + texel * vec2( 0.0, -1.5)).rgb;
    blur += texture(image, uv + texel * vec2( 0.0,  1.5)).rgb;
    blur += texture(image, uv + texel * vec2(-1.5,  0.0)).rgb;
    blur += texture(image, uv + texel * vec2( 1.5,  0.0)).rgb;
    blur /= 8.0;

    vec3 centerOrig = texture(image, uv).rgb;

    float lumCenter = dot(centerOrig, vec3(0.299, 0.587, 0.114));
    float lumBlur   = dot(blur,       vec3(0.299, 0.587, 0.114));

    float highFreq = lumCenter - lumBlur;

    float softBoost = amount * 1.5;
    
    color += highFreq * softBoost;

    color = clamp(color, 0.0, 1.0);
}


void applyDefringe(inout vec3 color)
{
    // Range is 0.0 to 2.0
    float d = clamp(uDefringe, 0.0, 2.0);

    // Early exit for performance
    if (d <= 0.001)
        return;

    float luma = dot(color, vec3(0.2126, 0.7152, 0.0722));

    // Mask for light areas (fringing usually happens in bright highlights)
    float lightMask = smoothstep(0.05, 0.5, luma);

    // Detect purple/magenta: high Red + Blue relative to Green
    float purpleAmount = max(0.0, color.r + color.b - 2.0 * color.g);

    // Edge detection using hardware derivatives
    float edge = abs(dFdx(luma)) + abs(dFdy(luma));
    float edgeMask = smoothstep(0.005, 0.05, edge);

    // Saturation mask: only defringe pixels that actually have color
    float maxC = max(color.r, max(color.g, color.b));
    float minC = min(color.r, min(color.g, color.b));
    float sat = maxC - minC;
    float satMask = smoothstep(0.02, 0.25, sat);

    // Combine all masks to isolate the fringe
    float fringeMask = purpleAmount * edgeMask * satMask * lightMask;

    // Desaturate the fringe areas
    vec3 gray = vec3(luma);
    
    // Applying the 0.0 - 2.0 amount
    color = mix(color, gray, clamp(fringeMask * d * 2.0, 0.0, 1.0));

    color = clamp(color, 0.0, 1.0);
}






// denoise complex

float getLocalVariance(vec2 uv, vec2 texel)
{
    float lumaC = dot(texture(image, uv).rgb, vec3(0.299, 0.587, 0.114));

    float lumaL = dot(texture(image, uv - vec2(texel.x, 0)).rgb, vec3(0.299, 0.587, 0.114));
    float lumaR = dot(texture(image, uv + vec2(texel.x, 0)).rgb, vec3(0.299, 0.587, 0.114));
    float lumaU = dot(texture(image, uv + vec2(0, texel.y)).rgb, vec3(0.299, 0.587, 0.114));
    float lumaD = dot(texture(image, uv - vec2(0, texel.y)).rgb, vec3(0.299, 0.587, 0.114));

    float dx = lumaR - lumaL;
    float dy = lumaU - lumaD;

    return dx * dx + dy * dy;
}

vec3 denoiseChroma(vec3 color, vec2 uv, float d, float mask)
{
    vec2 res = textureSize(image, 0);
    vec2 texel = 1.0 / res;
    float luma = dot(color, vec3(0.299, 0.587, 0.114));
    vec3 chroma = color - vec3(luma);

    float variance = getLocalVariance(uv, texel);
    float edgeMask = smoothstep(0.0005, 0.01, variance);
    float flatMask = 1.0 - edgeMask;

    vec3 sum = vec3(0.0);
    float wsum = 0.0;

    float sigma = 1.8 + d * 0.6;
    float spatialMult = -0.5 / (sigma * sigma);
    
    // Adaptive Radius: mare in zone plate, mic pe margini
    float radius = mix(1.2, 1.0 + d * 3.5, flatMask); 

    const int K = 5;
    
    for (int x = -K; x <= K; x++) {
        for (int y = -K; y <= K; y++) {
    
            vec2 offset = vec2(float(x), float(y)) * texel * (radius / float(K));
    
            vec3 sampleCol = texture(image, uv + offset).rgb;
    
            float sLuma = dot(sampleCol, vec3(0.299, 0.587, 0.114));
            vec3 sChroma = sampleCol - vec3(sLuma);
    
            float norm = float(x*x + y*y) / float(K*K);
            float spatial = exp(norm * spatialMult);
    
            float diff = length(sChroma - chroma);
    
            // THIS is where adaptation should happen
            float chromaRangeK = mix(30.0, 18.0, flatMask);
            float range = exp(-diff * diff * chromaRangeK);
    
            float w = spatial * range;
    
            sum += sChroma * w;
            wsum += w;
        }
    }



    vec3 cleanChroma = sum / max(wsum, 0.0001);
    return vec3(luma) + mix(chroma, cleanChroma, mask * uNoiseLuma);
}

vec3 denoiseLuma(vec3 color, vec2 uv, float d, float mask)
{
    vec2 res = textureSize(image, 0);
    vec2 texel = 1.0 / res;
    float luma = dot(color, vec3(0.299, 0.587, 0.114));
    vec3 chroma = color - vec3(luma);

    float variance = getLocalVariance(uv, texel);
    float edgeMask = smoothstep(0.0005, 0.01, variance);
    float flatMask = 1.0 - edgeMask;

    float sum = 0.0;
    float wsum = 0.0;

    float sigma = 1.4 + d * 0.4;
    float spatialMult = -0.5 / (sigma * sigma);
    
    // Adaptive Radius Luma
    float radius = mix(1.1, 1.0 + d * 2.8, flatMask);

    for (int x = -3; x <= 3; x++) {
        for (int y = -3; y <= 3; y++) {
            vec2 offset = vec2(float(x), float(y)) * texel * radius;
            vec3 sampleCol = texture(image, uv + offset).rgb;

            float sLuma = dot(sampleCol, vec3(0.299, 0.587, 0.114));
            float d2 = float(x*x + y*y);
            float spatial = exp(d2 * spatialMult);

            float diff = abs(sLuma - luma);
            
            // Adaptive Range Luma: Protejam detaliile fine (65) vs blur puternic in flat (32)
            float lumaRangeK = mix(65.0, 32.0, flatMask * (0.5 + mask * 0.5));
            float range = exp(-diff * diff * lumaRangeK);

            float w = spatial * range;
            sum += sLuma * w;
            wsum += w;
        }
    }

    float cleanLuma = sum / max(wsum, 0.0001);
    return vec3(mix(luma, cleanLuma, mask * uNoiseLuma)) + chroma;
}

void applyDenoise(inout vec3 color, vec2 uv)
{
    float d = clamp(uDenoise, 0.0, 2.0);
    if (d <= 0.001) return;

    vec2 res = textureSize(image, 0);
    vec2 texel = 1.0 / res;
    float luma = dot(color, vec3(0.299, 0.587, 0.114));

    // Detectam FlatMask global pentru Detail Restore
    float variance = getLocalVariance(uv, texel);
    float flatMask = 1.0 - smoothstep(0.0005, 0.01, variance);

    float darkGate = uNoiseLimit * 0.5; 
    float lightGate = 1.0 - (uNoiseChroma * 0.5);
    float mDark = 1.0 - smoothstep(darkGate - 0.25, darkGate + 0.1, luma);
    float mLight = smoothstep(lightGate - 0.1, lightGate + 0.25, luma);
    float finalMask = clamp(mDark + mLight, 0.0, 1.0);

    vec3 stage1 = denoiseChroma(color, uv, d, finalMask);
    vec3 stage2 = denoiseLuma(stage1, uv, d, finalMask);

    // Adaptive Detail Restore
    vec3 hf = color - stage2; 
    float hfLen = length(hf);
    float detailMask = smoothstep(0.02, 0.1, hfLen);
    
    // Magic Fix: reducem restaurarea in zonele plate pentru a nu reintroduce zgomot
    detailMask *= (1.0 - flatMask * 0.70);
    
    float detailAmount = 0.12 + d * 0.06;
    vec3 final = stage2 + hf * detailMask * detailAmount;

    color = clamp(final, 0.0, 1.0);
}






void applySaturation(inout vec3 color)
{
    // Range is 0.0 to 2.0 (1.0 is neutral)
    float s = clamp(uSaturation, 0.0, 2.0);
    
    // If it's effectively 1.0, do nothing
    if (abs(s - 1.0) < 0.001)
        return;

    float luma = dot(color, vec3(0.299, 0.587, 0.114));
    vec3 gray = vec3(luma);

    // Protect highlights from oversaturation
    float highlightProtect = smoothstep(0.7, 1.0, luma);
    float strength = mix(s, 1.0, highlightProtect * 0.4);

    color = mix(gray, color, strength);
}

void applyVibrance(inout vec3 color)
{
    // Range 0.0 to 2.0 (But logic expects 0.0 as neutral)
    // We will treat the slider 0-2 as a boost/cut of -1.0 to 1.0
    float v = clamp(uVibrance - 1.0, -1.0, 1.0);

    if (abs(v) < 0.001)
        return;

    float luma = dot(color, vec3(0.299, 0.587, 0.114));

    float maxC = max(color.r, max(color.g, color.b));
    float minC = min(color.r, min(color.g, color.b));
    float saturation = maxC - minC;

    // Protect already saturated colors
    float satMask = 1.0 - saturation;

    // Protect skin tones (orange range bias)
    float skinMask = smoothstep(0.0, 0.4, abs(color.r - color.g));

    float strength = v * satMask * (1.0 - skinMask * 0.5);

    vec3 gray = vec3(luma);
    color = mix(gray, color, 1.0 + strength);
    color = clamp(color, 0.0, 1.0);
}


void applyChannelMix(inout vec3 color)
{
    // We use your 0.0 to 2.0 range (1.0 is neutral)
    vec3 mixScale = vec3(
        clamp(uRedMix,   0.0, 2.0),
        clamp(uGreenMix, 0.0, 2.0),
        clamp(uBlueMix,  0.0, 2.0)
    );

    // If all are 1.0, early exit
    if (all(equal(mixScale, vec3(1.0))))
        return;

    // We apply the multiplier to each channel separately
    color.r *= mixScale.r;
    color.g *= mixScale.g;
    color.b *= mixScale.b;

    // After multiplying, we must clamp to ensure no "illegal" colors (> 1.0)
    color = clamp(color, 0.0, 1.0);
}


void applyDeHaze(in vec2 uv, inout vec3 color)
{
    // Standard 0.0 to 2.0 range
    float d = clamp(uDeHaze, 0.0, 2.0);

    if (d <= 0.001) return;

    // Airlight: The color of the fog. 
    // In your examples, the fog is bright, so 0.85 is a good baseline.
    const vec3 A = vec3(0.85); 

    // Dark Channel: Estimating the haze thickness
    float dark = min(color.r, min(color.g, color.b));

    // Transmission: Recovering what is hidden
    // We scale the intensity by (d * 0.5) to keep it manageable
    float t = 1.0 - (d * 0.5) * dark;
    t = max(t, 0.3); // Safety floor to prevent division by zero artifacts

    // Recover Radiance: This pulls the "hidden" image out of the fog
    color = (color - A) / t + A;

    // Post-Dehaze contrast to clean up the remaining 'wash'
    float lum = dot(color, vec3(0.299, 0.587, 0.114));
    float contrast = 1.0 + (d * 0.15);
    color = (color - 0.5) * contrast + 0.5;

    // Saturation boost: Haze naturally kills color, so we add some back
    color = mix(vec3(lum), color, 1.0 + (d * 0.2));

    color = clamp(color, 0.0, 1.0);
}



void applyVignette(in vec2 uv, inout vec3 color)
{
    // 0.0 is Off, 1.0 is Medium, 2.0 is Strong
    float v = clamp(uVignette, 0.0, 2.0);
    
    if (v <= 0.001) return;

    /* Centered UV: -1 to 1 */
    vec2 pos = uv * 2.0 - 1.0;

    /* ---- VERTICAL DOMINANT FALLOFF ---- */
    // This creates the 'letterbox' style cinematic feel
    float vertical = abs(pos.y);          
    float lateral  = abs(pos.x) * 0.35;   

    /* Combined distance (weighted toward top/bottom) */
    float dist = vertical + lateral;

    /* Slider mapping (0.0 to 2.0) */
    // We normalize v to a 0-1 range for the mix functions
    float factor = v * 0.5; 
    
    float strength = factor * 0.65;
    float radius   = mix(1.1, 0.55, factor);
    float softness = mix(0.4, 0.9, factor);

    /* Smooth falloff mask */
    float mask = smoothstep(
        radius,
        radius - softness,
        dist
    );

    /* Highlight protection (pro look) */
    // Prevents the vignette from 'dirtying' bright skies or lights
    float luma = dot(color, vec3(0.2126, 0.7152, 0.0722));
    float protect = smoothstep(0.6, 1.0, luma);

    // Calculate vignette influence
    float vig = mix(1.0, mask, strength);
    
    // Apply with highlight protection
    color *= mix(vig, 1.0, protect);
}



void applyVignetteStd(vec2 uv, inout vec3 color) {
    // If the slider is 0, don't do any math (Total Bypass)
    if (uVignetteStd <= 0.0) return;

    float dist = distance(uv, vec2(0.5));
    
    // We subtract a "Safe Zone" (0.2) so the center 40% of the image is untouched.
    // Then we clamp it so it doesn't go negative.
    float radius = max(dist - 0.2, 0.0);
    
    // Use uVignetteStd to control the intensity of the falloff
    float vignette = smoothstep(0.8, 0.5 - (uVignetteStd * 0.5), radius);
    
    color *= vignette;
}



float hash12(vec2 p) {
    vec3 p3 = fract(vec3(p.xyx) * 0.1031);
    p3 += dot(p3, p3.yzx + 33.33);
    return fract((p3.x + p3.y) * p3.z);
}

float smoothNoise(vec2 uv) {
    vec2 i = floor(uv);
    vec2 f = fract(uv);
    f = f * f * (3.0 - 2.0 * f); 
    float a = hash12(i);
    float b = hash12(i + vec2(1.0, 0.0));
    float c = hash12(i + vec2(0.0, 1.0));
    float d = hash12(i + vec2(1.0, 1.0));
    return mix(mix(a, b, f.x), mix(c, d, f.x), f.y);
}

void applyFilmGrain(inout vec3 color, vec2 uv) {
    if (uFilmGrain <= 0.001) return;

    // We map uGrainSize (0.0 to 1.0) to a Density range.
    // 2500.0 = Very Fine, 400.0 = Very Chunky.
    float density = mix(2500.0, 400.0, uGrainSize);
    
    float n1 = smoothNoise(uv * density);
    float n2 = smoothNoise(uv * density * 1.8);
    float finalNoise = mix(n1, n2, 0.6);

    float lum = dot(color, vec3(0.2126, 0.7152, 0.0722));
    float weight = sin(lum * 3.14159); 

    // Intensity remains controlled by the original slider
    float amount = uFilmGrain * 0.25 * weight;

    color += (finalNoise - 0.5) * amount;
    color = clamp(color, 0.0, 1.0);
}



void applyVivid(inout vec3 color)
{
    if (uVivid <= 0.001)
        return;

    float lum = dot(color, vec3(0.299, 0.587, 0.114));

    // --- 1. Contrast boost around midtones ---
    vec3 pivot = vec3(0.5);
    // uVivid goes to 2.0, so contrastStrength goes to 2.2
    float contrastStrength = 1.0 + uVivid * 0.6;
    color = mix(color, pivot + (color - pivot) * contrastStrength, uVivid * 0.5);

    // --- 2. Adaptive saturation boost ---
    float satBoost = 1.0 + uVivid * 0.7;

    // Protect highlights from clipping
    float highlightProtect = 1.0 - smoothstep(0.7, 1.0, lum);

    float l = dot(color, vec3(0.299, 0.587, 0.114));
    vec3 saturated = mix(vec3(l), color, satBoost);

    // Scale back uVivid influence to 0-1 range for the mix factor
    color = mix(color, saturated, highlightProtect * (uVivid * 0.5));

    color = clamp(color, 0.0, 1.0);
}



void applySepia(inout vec3 color)
{
    if (uSepiaMix <= 0.001)
        return;

    // 1. Get the Grayscale (Luminance) of the current pixel
    float gray = dot(color, vec3(0.299, 0.587, 0.114));

    // 2. Create the Sepia Tone based on that Gray value
    // This gives you the "Photo" look without breaking the math
    vec3 sepiaTone = vec3(gray * 1.2, gray * 1.0, gray * 0.8);
    
    // Optional: Add a tiny bit of red-shift to the shadows for "warmth"
    sepiaTone.r += 0.05 * (1.0 - gray); 

    // 3. Blend based on your slider
    // Using uSepiaMix 0.0 to 1.0 (or even up to 2.0 is safe now)
    float strength = clamp(uSepiaMix, 0.0, 1.0);
    color = mix(color, sepiaTone, strength);

    color = clamp(color, 0.0, 1.0);
}


void applyNoir(inout vec3 color)
{
    // Use uNoirFactor to match your VB.NET mapping
    if (uNoirFactor <= 0.001)
        return;

    float lum = dot(color, vec3(0.299, 0.587, 0.114));

    // --- 1. Convert to monochrome ---
    vec3 bw = vec3(lum);

    // --- 2. Strong contrast curve ---
    // At uNoirFactor = 1.0, contrast is 2.2. At 2.0, it's very aggressive.
    float contrast = 1.0 + uNoirFactor * 1.2;
    bw = vec3(0.5) + (bw - vec3(0.5)) * contrast;

    // --- 3. Deepen shadows slightly ---
    bw -= (1.0 - lum) * 0.25 * uNoirFactor;

    // --- 4. Slight highlight compression ---
    float highlightCompress = smoothstep(0.7, 1.0, lum);
    bw = mix(bw, vec3(lum * 0.9), highlightCompress * uNoirFactor * 0.5);

    // --- 5. Blend with original ---
    color = mix(color, bw, clamp(uNoirFactor, 0.0, 1.0));

    color = clamp(color, 0.0, 1.0);
}



void applySplitToning(inout vec3 color)
{
    if (uSplitToning <= 0.001) return;

    float luma = dot(color, vec3(0.2126, 0.7152, 0.0722));


    float shadowMask   = smoothstep(0.50, 0.15, luma);         
    float highlightMask = smoothstep(0.60, 0.95, luma);        


    vec3 shadowTint   = vec3(0.78, 0.92, 1.18);   
    vec3 highlightTint = vec3(1.22, 1.08, 0.88);  

    float strength = clamp(uSplitToning * 0.6, 0.0, 0.8);   


    vec3 shadowed    = color * mix(vec3(1.0), shadowTint,   shadowMask   * strength);
    vec3 highlighted = shadowed * mix(vec3(1.0), highlightTint, highlightMask * strength);

    float brightClip = smoothstep(0.92, 1.0, luma);
    highlighted = mix(highlighted, color, brightClip * 0.7);


    float finalLuma = dot(highlighted, vec3(0.2126, 0.7152, 0.0722));
    color = mix(vec3(finalLuma), highlighted, 1.0 - strength * 0.2);

    color = clamp(color, 0.0, 1.0);
}


void applyFade(inout vec3 color, float uFade, float uFadeTint)
{
    // 1. Input Amplification (Double intensity)
    float fadeInput = clamp(uFade * 2.0, 0.0, 2.0);
    if (fadeInput <= 0.001) return;

    float amount = fadeInput * 0.22;

    // 2. Map uFadeTint [-1, 1] to [0, 1]
    float tintPos = clamp((uFadeTint + 1.0) / 2.0, 0.0, 1.0);

    // 3. Shadow Lift
    vec3 fadeTarget = vec3(0.08, 0.09, 0.10);         
    float avg = dot(color, vec3(0.333));
    float liftMask = pow(1.0 - clamp(avg, 0.0, 1.0), 3.2);  
    vec3 lifted = mix(color, fadeTarget, amount * liftMask * 1.6);

    // 4. Desaturation
    float luma = dot(lifted, vec3(0.299, 0.587, 0.114));
    vec3 faded = mix(lifted, vec3(luma), amount * 0.4);  

    // 5. Normalized Color Tinting
    // We use balanced vectors to avoid pushing luma past 1.0
    // Cold: leaning Blue/Cyan | Warm: leaning Orange/Yellow
    vec3 coldTint = vec3(0.85, 0.95, 1.15); 
    vec3 warmTint = vec3(1.15, 1.05, 0.85);
    vec3 finalTint = mix(coldTint, warmTint, tintPos);
    
    // Apply tint using multiplication but keep it energy-neutral
    faded *= mix(vec3(1.0), finalTint, amount * 0.7);    

    // 6. Final Matte Pass & Histogram Protection
    // Instead of raw multiplication, we use a soft-knee compression
    // This keeps the histogram from "falling" by preventing hard clipping
    faded = mix(faded, faded * 0.95 + 0.025, amount * 0.8);

    color = clamp(faded, 0.0, 1.0);
}


void applyTextureX(inout vec3 color, vec2 uv)
{
    float amount = clamp(uTextureX, 0.0, 2.0);
    if (amount <= 0.0001) return;

    vec2 texel = 1.0 / textureSize(image, 0);
    vec3 centerOrig = texture(image, uv).rgb;
    
    vec3 neighbors = vec3(0.0);
    neighbors += texture(image, uv + vec2(texel.x, 0.0)).rgb;
    neighbors += texture(image, uv + vec2(-texel.x, 0.0)).rgb;
    neighbors += texture(image, uv + vec2(0.0, texel.y)).rgb;
    neighbors += texture(image, uv + vec2(0.0, -texel.y)).rgb;
    neighbors /= 4.0;

    float lumCenter = dot(centerOrig, vec3(0.299, 0.587, 0.114));
    float lumNeighbors = dot(neighbors, vec3(0.299, 0.587, 0.114));
    
    float microFreq = lumCenter - lumNeighbors;

    float luma = dot(centerOrig, vec3(0.2126, 0.7152, 0.0722));
    float midMask = 4.0 * luma * (1.0 - luma);
    midMask = pow(midMask, 0.5);

    float powerBoost = amount * 10.0;
    
    color += microFreq * powerBoost * midMask;

    color = clamp(color, 0.0, 1.0);
}



void applyCAS(inout vec3 color, vec2 uv) {
    float sharpness = 0.5;
    vec2 res = textureSize(image, 0);
    
    vec3 e_orig = texture(image, uv).rgb; 
    vec3 d = texture(image, uv + vec2(-1.0, 0.0) / res).rgb;
    vec3 f = texture(image, uv + vec2(1.0, 0.0) / res).rgb;
    vec3 b = texture(image, uv + vec2(0.0, -1.0) / res).rgb;
    vec3 h = texture(image, uv + vec2(0.0, 1.0) / res).rgb;

    float min_g = min(min(e_orig.g, d.g), min(f.g, min(b.g, h.g)));
    float max_g = max(max(e_orig.g, d.g), max(f.g, max(b.g, h.g)));
    
    max_g = max(max_g, 0.0001); 

    float weight = clamp(sqrt((1.0 - max_g) / max_g), 0.0, sharpness);
    
    vec3 detail = e_orig - (d + f + b + h) * 0.25;
    
    color += detail * weight;

    color = clamp(color, 0.0, 1.0);
}



void applyLutIntens(inout vec3 colorValue)
{
    if(useLut != 0) // <-- changed
    {
        vec3 lookup = clamp(colorValue,0.0,1.0);

        vec3 lutResult = texture(lutTexture, lookup).rgb;

        lutResult = clamp(lutResult,0.0,1.0);

        colorValue = mix(colorValue, lutResult, uLutIntens);
    }
}



void applyCinematic(inout vec3 color, float strength, float balance) 
{
    if (strength <= 0.001) return;


    float luma = dot(color, vec3(0.2126, 0.7152, 0.0722));
    

    float pivot = 0.5 + (balance * 0.2); 
    float shadowMask = smoothstep(pivot + 0.2, pivot - 0.2, luma);
    float highlightMask = smoothstep(pivot - 0.2, pivot + 0.3, luma);

    vec3 tealShadows = vec3(0.82, 1.06, 1.18); 
    vec3 orangeHighlights = vec3(1.22, 1.04, 0.85);

    vec3 shadowed = mix(color, color * tealShadows, shadowMask * strength);
    vec3 graded = mix(shadowed, shadowed * orangeHighlights, highlightMask * strength);

    float shadowCrush = smoothstep(0.15, 0.0, luma) * strength * 0.12;
    graded -= shadowCrush;

    float skinMask = smoothstep(0.0, 0.12, abs(color.r - color.g)) * smoothstep(0.45, 0.15, abs(color.r - color.b));
    
    float midMask = 1.0 - clamp(shadowMask + highlightMask, 0.0, 1.0);
    vec3 final = mix(graded, color, midMask * (0.35 + skinMask * 0.45));

    color = clamp(final, 0.0, 1.0);
}



// Asymmetric Distortion: Auto-zoom only for Barrel (k > 0)
vec2 getDistortedUV(vec2 uv, float k) {
    float strength = k * 0.05; // Using your stable reference multiplier
    
    // Normalize coordinates to [-1, 1]
    vec2 pos = uv * 2.0 - 1.0;
    
    // Calculate squared distance from center
    float r2 = dot(pos, pos);
    
    // Determine scaling factor
    float scale = 1.0;
    
    if (k > 0.0) {
        // --- BARREL SIDE (Right on slider) ---
        // Apply auto-zoom to hide rounded black corners
        // We calculate the expansion at the corners to compensate
        float f = 1.0 + strength * 2.0; 
        scale = 1.0 / f; 
    } else {
        // --- PINCUSHION SIDE (Left on slider) ---
        // Keep it as it was (no auto-zoom)
        scale = 1.0;
    }
    
    // Apply distortion and the conditional scale
    vec2 distortedPos = pos * (1.0 + strength * r2) * scale;
    
    // Map back to [0, 1]
    return (distortedPos + 1.0) / 2.0;
}






void main()
{
    // 1. Start with raw texture coordinate input space
    vec2 baseCoords = TexCoord;
   
    // 2. Process Barrel Distortion first if active
    if (abs(uBarrelDistortion) > 0.0001) {
        baseCoords = getDistortedUV(baseCoords, uBarrelDistortion);
    }
   
    // 3. Bypass / Before view processing (Pristine and untouched)
    if (uBypass) { 
        vec2 flippedCoords = vec2(baseCoords.x, 1.0 - baseCoords.y);  
        if (baseCoords.x < 0.0 || baseCoords.x > 1.0 || baseCoords.y < 0.0 || baseCoords.y > 1.0) {
            FragColor = vec4(0.0, 0.0, 0.0, 1.0);
        } else {
            FragColor = vec4(texture(image, flippedCoords).rgb, 1.0);
        }
        return; 
    }

    // 4. APPLY THE AUTO-CROP TO FILL LAYER FIRST
    vec2 finalCoords = baseCoords;
    if (uIsCropApplied) {
        finalCoords.x = uCropRect.x + (baseCoords.x * uCropRect.z);
        finalCoords.y = uCropRect.y + (baseCoords.y * uCropRect.w);
    }
   
    // 5. APPLY THE LEVEL HORIZON ROTATION SECOND (Rotates inside the crop frame)
    if (uLevelHorizon) {
        // Shift spatial plane origin to center point (0.5, 0.5) so it spins on its central axis
        vec2 centeredUV = finalCoords - vec2(0.5);
        
        // 1. Transform the coordinate space via the rotation matrix
        vec4 transformedUV = uRotationMatrix * vec4(centeredUV, 0.0, 1.0);
        
        // 2.  Divide instead of multiply to zoom INWARD and clip the black gaps
        centeredUV = transformedUV.xy / uAutoFillScale;
        
        // Map space layout safely back to standard scale bounds [0..1]
        finalCoords = centeredUV + vec2(0.5);
    }

    // 6. Check borders using the complete final position matrices
    if (finalCoords.x < 0.0 || finalCoords.x > 1.0 || finalCoords.y < 0.0 || finalCoords.y > 1.0) {
        FragColor = vec4(0.0, 0.0, 0.0, 1.0);
        return;
    }

    // 7. Extract base texture mappings and bind to processing data channels
    vec3 rawColor = texture(image, finalCoords).rgb;
    vec3 color = rawColor;

    // --- All your remaining original features continue below untouched ---
    if (uRadius > 0.0) {
        vec2 flippedTexCoord = vec2(TexCoord.x, 1.0 - TexCoord.y);
        ivec2 texSize = textureSize(image, 0);
        float aspect = float(texSize.x) / float(texSize.y);
        
        vec2 aspectCoord = vec2(flippedTexCoord.x * aspect, flippedTexCoord.y);
        vec2 aspectCenter = vec2(uSelectionCenter.x * aspect, uSelectionCenter.y);
        float distFromSelection = distance(aspectCoord, aspectCenter);

        if (distFromSelection < uSelectionRange) {
            float areaMask = smoothstep(uSelectionRange, uSelectionRange * 0.8, distFromSelection);
            float d = distance(rawColor, uTargetRGB);
            
            if (d < uRadius) {
                float colorMask = smoothstep(uRadius, uRadius - uSmoothness, d);            
                float originalLuma = dot(rawColor, vec3(0.299, 0.587, 0.114));           
                vec3 finalReplace = uReplaceRGB * (originalLuma * 1.5);      
                float finalBlendWeight = colorMask * areaMask * (1.0 - uOpacity);
                color = mix(color, finalReplace, finalBlendWeight);
            }
        }
    }

    applyDenoise(color, finalCoords);
    applyDefringe(color);
    applyDeHaze(finalCoords, color);

    applyExposure(color);
    applyContrast(color);
    applyHighlights(color);
    applyShadowsLift(color);
    applyWhites(color);
    applyBlacks(color);
    applyDynCon(color);

    applyTemp(color);
    applyTint(color);
    applySaturation(color);
    applyVibrance(color);
    applyVivid(color);
    applyChannelMix(color);
    applySplitToning(color);
    applyLutIntens(color);
    applyCinematic(color, uCinematic, uCineBalance);

    applySepia(color);
    applyNoir(color); 
    applyFade(color, uFade, uFadeTint);
    applyVignette(finalCoords, color);
    applyVignetteStd(finalCoords, color);

    applyClarity(color, finalCoords);
    applyTextureX(color, finalCoords);
    applyStructX(color, finalCoords);
    applyCAS(color, finalCoords); 
    applySharpen(color, finalCoords);

    applyFilmGrain(color, finalCoords);

    float blink = step(0.0, sin(uTime * 5.0));
    if (uShowHighlights && (color.r >= 1.0 || color.g >= 1.0 || color.b >= 1.0)) {
        color = mix(color, vec3(0.99, 0.0, 0.0), blink);
    }
    if (uShowShadows && (color.r <= 0.0 && color.g <= 0.0 && color.b <= 0.0)) {
        color = mix(color, vec3(0.0, 0.0, 0.99), blink); 
    }

   if (uIsCropping) {
        float edgeThickness = 0.001;
        float ruleOfThirdsThickness = 0.0005; // Slightly thinner than edges
        float handleBoxSize = 0.006;
        float dashLength = 0.015;
        float dashGap = 0.01;

        float xMin = uCropRect.x; 
        float xMax = uCropRect.x + uCropRect.z;
        float yMin = uCropRect.y; 
        float yMax = uCropRect.y + uCropRect.w;
        
        // Calculate Rule of Thirds positions
        float xThird1 = xMin + (uCropRect.z / 3.0);
        float xThird2 = xMin + (2.0 * uCropRect.z / 3.0);
        float yThird1 = yMin + (uCropRect.w / 3.0);
        float yThird2 = yMin + (2.0 * uCropRect.w / 3.0);

        float xMid = xMin + (uCropRect.z / 2.0); 
        float yMid = yMin + (uCropRect.w / 2.0);

        // Standard Edges
        bool onVerticalEdge = (abs(TexCoord.x - xMin) < edgeThickness || abs(TexCoord.x - xMax) < edgeThickness) && (TexCoord.y >= yMin && TexCoord.y <= yMax);
        bool onHorizontalEdge = (abs(TexCoord.y - yMin) < edgeThickness || abs(TexCoord.y - yMax) < edgeThickness) && (TexCoord.x >= xMin && TexCoord.x <= xMax);

        // NEW: Rule of Thirds Vertical Lines
        bool onVerticalThird = (abs(TexCoord.x - xThird1) < ruleOfThirdsThickness || abs(TexCoord.x - xThird2) < ruleOfThirdsThickness) && (TexCoord.y >= yMin && TexCoord.y <= yMax);
        
        // NEW: Rule of Thirds Horizontal Lines
        bool onHorizontalThird = (abs(TexCoord.y - yThird1) < ruleOfThirdsThickness || abs(TexCoord.y - yThird2) < ruleOfThirdsThickness) && (TexCoord.x >= xMin && TexCoord.x <= xMax);

        // DASH LOGIC
        bool isDash = false;
        if (onVerticalEdge && mod(TexCoord.y, dashLength + dashGap) < dashLength) isDash = true;
        if (onHorizontalEdge && mod(TexCoord.x, dashLength + dashGap) < dashLength) isDash = true;

        // Combine logic
        vec2 handles[8] = vec2[](vec2(xMin, yMin), vec2(xMid, yMin), vec2(xMax, yMin), vec2(xMax, yMid), vec2(xMax, yMax), vec2(xMid, yMax), vec2(xMin, yMax), vec2(xMin, yMid));
        bool isHandle = false;
        for(int i = 0; i < 8; i++) { if(abs(TexCoord.x - handles[i].x) < handleBoxSize && abs(TexCoord.y - handles[i].y) < handleBoxSize) { isHandle = true; break; } }

        // Final color assignment including the Rule of Thirds lines
        if (isHandle || isDash || onVerticalThird || onHorizontalThird) color = uRectangleSelColor.rgb; 
    }

    FragColor = vec4(clamp(color, 0.0, 1.0), 1.0);
}