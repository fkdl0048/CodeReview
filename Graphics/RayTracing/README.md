# Ray Tracing

기본적으로 레이 트레이싱이란, 현실에서 일어나는 빛의 반사, 굴절 등을 시뮬레이션 하는 기술이다. 실세계의 태양으로 부터 발산되는 빛의 반사는 컴퓨터로 시뮬레이션 하기에 너무 복잡하며 거의 불가능에 가깝다.

컴퓨터는 이를 해결하기 위해 시야로 부터 빛을 쏴서 역으로 추적하는 방식을 사용한다. 더욱 사실적인 묘사를 하기 위해선 레이 트레이싱의 차원이나 깊이를 늘려서 더욱 정교한 렌더링을 할 수 있다. *(ex 물의 굴절을 추가적으로 계산하거나 반사의 반사을 어디까지 할 것인가 등)*

- [도움이 되는 영상 Ray Tracing](https://www.youtube.com/watch?v=H5TB2l7zq6s)

## 

## Ray Tracing 코드

*실제 과정을 흉내낸 코드에 불과합니다.*

- [코드 전문](https://github.com/fkdl0048/Computer_Graphics/blob/main/Project/rayTracer_2/rayTracer_2/fPhong.glsl)

```glsl
#version 330

in vec3 N3; 
in vec3 L3; 
in vec3 V3; 
in vec3 wV;
in vec3 wP;
in vec3 wN;

out vec4 fColor;

struct Material {
	vec4  k_d;
	vec4  k_s;
	float n;
};

struct Sphere {
	vec4     center;
	float    radius;
	Material mtl;
};

struct Ray {
	vec3 pos;
	vec3 dir;
};

struct HitInfo {
	float    t;
	vec4     position;
	vec3     normal;
	Material mtl;
};

uniform mat4 uModelMat; 
uniform mat4 uViewMat; 
uniform mat4 uProjMat; 
uniform vec4 uLPos; 
uniform vec4 uLIntensity;
uniform vec4 uAmb; 
uniform vec4 uDif; 
uniform vec4 uSpc; 
uniform float uShininess; 
uniform samplerCube uCube;
uniform vec4 uEPos;
uniform int uNumSphere;
uniform Sphere uSpheres[20];
uniform int uBounceLimit;
uniform int uDrawingMode;

bool IntersectRay( inout HitInfo hit, Ray ray );

vec4 Shade( Material mtl, vec4 position, vec3 normal, vec3 view )
{
	HitInfo hit; 
	Ray r;

	vec4 color = vec4(0,0,0,1);
	r.pos = position.xyz;
	r.dir = (uLPos - position).xyz;

	if(IntersectRay(hit, r))
	{
		color += mtl.k_d * 0.01;
	}
	else
	{
		vec3 N = normalize(normal).xyz;
		vec3 L = normalize(uLPos - position).xyz;
		float NL = max(dot(N, L), 0);

		color += (mtl.k_d * NL) * uLIntensity;	// change this line
	}

	return color;
}

bool IntersectRay( inout HitInfo hit, Ray ray )
{
	hit.t = 1e30;
	bool foundHit = false;

	for ( int i=0; i<uNumSphere; ++i ) 
	{
		Sphere sphere = uSpheres[i];

		float a = dot(ray.dir, ray.dir);
		float b = dot(ray.pos - sphere.center.xyz, ray.dir);
		float c = dot(ray.pos - sphere.center.xyz, ray.pos - sphere.center.xyz) - sphere.radius * sphere.radius;
		float discriminant = b*b - a*c;
		if(discriminant < 0)
			continue ;
		
		float t = (-b - sqrt(discriminant)) / a;
		if(t < hit.t && t > 0)
		{
			foundHit = true;
			hit.t = t;
			hit.position = vec4((ray.pos + t*ray.dir), 1);
			hit.normal = normalize(hit.position - sphere.center).xyz;
			hit.mtl = sphere.mtl;
		}
	}

	return foundHit;
}

vec4 RayTracer( Ray ray )
{
	HitInfo hit;

	if ( IntersectRay( hit, ray ) ) 
	{
		vec3 view = normalize( ray.dir );
		vec4 clr = Shade( hit.mtl, hit.position, vec3(hit.normal), vec3(view));

		vec4 k_s = hit.mtl.k_s;
		for ( int bounce=0; bounce<uBounceLimit; bounce++ ) 
		{
			if ( hit.mtl.k_s.r + hit.mtl.k_s.g + hit.mtl.k_s.b <= 0.0 )
			{
				break;
			}

			Ray r;
			HitInfo h;

			r.pos = hit.position.xyz;
			r.dir = normalize(reflect(view, hit.normal)).xyz;
			
			if ( IntersectRay( h, r ) ) 
			{
				clr += k_s * Shade(h.mtl, h.position, vec3(h.normal), vec3(r.dir));
				hit = h;
				view = r.dir;
				k_s *= h.mtl.k_s;
			} 
			else 
			{
				clr += k_s * texture(uCube, vec3(1,-1,1)*r.dir);
				break;
			}
		}

		return clr;
	} 
	else 
	{
		return texture(uCube, vec3(1,-1,1)*ray.dir);
	}
}

void main()
{
	if(uDrawingMode == 0) 
	{
		vec3 N = normalize(N3); 
		vec3 L = normalize(L3); 
		vec3 V = normalize(V3); 
		vec3 H = normalize(V+L); 

		float NL = max(dot(N, L), 0); 
		float VR = pow(max(dot(H, N), 0), uShininess); 

		fColor = uAmb + uLIntensity*uDif*NL + uLIntensity*uSpc*VR; 
		fColor.w = 1; 

		vec3 viewDir = wP - wV;
		vec3 dir = reflect(viewDir, wN);

		fColor += uSpc*texture(uCube, vec3(1,-1,1)*dir);
	}
	else if(uDrawingMode == 1)
	{
		Ray r;
		r.pos = wV;
		r.dir = normalize(wP - wV);

		vec3 N = normalize(N3);
		vec3 L = normalize(L3);
		vec3 V = normalize(V3);
		vec3 H = normalize(V+L);
		float NL = max(dot(N, L), 0);
		float VR = pow(max(dot(H, N), 0), uShininess);
		fColor = uLIntensity * uSpc * VR;
		fColor += RayTracer (r);
	}
}
```

