shader_type canvas_item;

uniform sampler2D layer_r : repeat_enable, filter_linear_mipmap_anisotropic; 
uniform sampler2D layer_g : repeat_enable, filter_linear_mipmap_anisotropic;
uniform sampler2D layer_b : repeat_enable, filter_linear_mipmap_anisotropic;
uniform sampler2D noise_img : repeat_enable, filter_linear_mipmap_anisotropic;

uniform float uv_scale = 10.0;

vec2 rotate(vec2 uv, float degrees) {
    float radians = degrees * 0.0174533; 
    float s = sin(radians);
    float c = cos(radians);
    
    uv -= 0.5;
    uv = vec2(uv.x * c - uv.y * s, uv.x * s + uv.y * c);
    uv += 0.5;
    return uv;
}

void fragment() {
    vec2 base_uv = UV * uv_scale;
    float noise = texture(noise_img, UV).r; 
    vec4 weights = texture(TEXTURE, UV);
    
    // Подготовка UV (как у вас)
    vec2 uv_r = rotate(base_uv, 10.0) + vec2(0.123, 0.456);
    vec2 uv_g = base_uv + vec2(0.789, 0.012);
    vec2 uv_b = rotate(base_uv, -100.0) + vec2(0.345, 0.678);
    
    vec4 tex_r = texture(layer_r, uv_r);
    vec4 tex_g = texture(layer_g, uv_g);
    vec4 tex_b = texture(layer_b, uv_b);
    
    float rg_sum = max(weights.r + weights.g, 0.001);
    vec4 mix_rg = mix(tex_g, tex_r, weights.r / rg_sum);
    
    float total_sum = max(rg_sum + weights.b, 0.001);
    vec4 final_color = mix(tex_b, mix_rg, rg_sum / total_sum);
                       
    final_color.rgb *= (0.8 + noise * 0.3);
   
    COLOR = vec4(final_color.rgb, weights.a);
}