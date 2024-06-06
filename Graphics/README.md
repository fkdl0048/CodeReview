# Graphics

컴퓨터 그래픽스에 대한 공부 내용을 정리합니다.

## 요약

컴퓨터 그래픽스의 과정

- 컴퓨터로 그래픽을 나타내기 위해선 먼저 해당 그래픽을 표현해줄 수 있는 그래픽 API를 사용해야 한다. OpenGL, DirectX 등이 있다.
- 해당 API의 도움을 받아서 가상의 공간에 물체를 배치한다.
  - 해당 물체의 위치, 크기, 방향 등을 정의한다. (속성)
- 해당 물체가 그래픽으로 그려지기 위해선 해당 오브젝트, 빛, 카메라가 필요하다.
- 이후에 해당 정보를 가지고 렌더링을 하여 화면에 출력한다.
  - 렌더링은 fsh, vsh 파일을 통해 쉐이더를 사용하여 렌더링을 한다.
  - fs, vs 파일은 각각 프래그먼트 쉐이더, 버텍스 쉐이더를 의미한다.
  - 쉐이더는 GPU에서 동작하는 프로그램으로, 렌더링을 하기 위해 사용된다.
  - 자세한 내용은 렌더링 파이프라인을 참고하면 좋다.
- 렌더링을 통해 화면에 출력된다.
- 좀 더 사실적인 렌더링을 위해 Phong Shading, Ray Tracing 등을 사용한다.

## Index

- [OpenGL 함수 관련 정리](https://github.com/fkdl0048/Computer_Graphics/blob/main/Integration/Assignment.md)
- [그래픽스 개념 정리(OpenGL)](https://github.com/fkdl0048/Computer_Graphics/blob/main/Integration/LectrureNote.md)
- [Ray Tracing](./RayTracing/README.md)
- [Rendering Pipeline](./RenderingPipeline/README.md)
- Filed of View
- Shader
- Inverse Kinematics
- LOD
- Occlusion Culling

*수학적인 내용이 많을 수 있어서 [수학 폴더](../GameMath/README.md)를 참고하면 좋습니다.*