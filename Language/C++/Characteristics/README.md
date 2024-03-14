# C++의 특징

*C#과 달리 C++에 대한 이해가 부족하여 차이점을 위주로 이해하고자 한다.*

## C++ 특징

- C언어와의 호환성: 기존 C언어 코드를 그대로 사용할 수 있도록 C언어의 문법을 그대로 가져온다.
- 객체지향: 소프트웨어를 재사용하여 생산성을 높이기 위해 객제지향 개념을 도입
- 타입 체크: 타입 체크를 엄격히 하여 실행 시간 오류의 가능성을 줄이고 디버깅을 도움
- 효율성 저하 최소화: 성능에 영향을 주지 않는 선에서 기능을 추가한다. 예를 들어 함수의 인라인을 통해 함수 호출로 인한 시간을 최소화한다. (속도)
- C에서 C++로 오면서 많은 특징이 추가되었다. 가장 대표적인 것은 객체지향 프로그래밍이 가능해진 점이고, 그 이외에도 bool 자료형, 참조자 추가, 메서드 오버로딩, 연산자 오버로딩이 생기면서 C언어와는 완전히 새로운 언어가 나타났다고 볼 수 있다.

## C++과 C#의 차이점

- C++은 가상머신을 사용하지 않고 운영체제에 접근하기 때문에 빠름, 메모리 관리가 가능함 (포인터)
- `C#`은 메모리 관리를 .NetFramework가 관리하여 (가비지 콜렉터) 작동함 CLR
- `C#`은 모든 것을 객체로 취급하기 때문에 컴포넌트 지향 프로그래밍 언어
- C++은 다중상속을 지원하고, 인터페이스가 없다(추상클래스로 흉내냄)
- C#은 런타임 도중 수집된 정보를 사용하여 객체를 인스턴스화하고, 메서드를 호출할 수 있는 ‘리플렉션’이라는 기능을 가지고 있습니다.
- 컴파일하는 도중에는 메서드 호출이 불가능하지만, 런타임 시에는 함수명을 사용해서 메서드를 호출할 수 있습니다.
- 반면, C++은 바로 컴파일되기 때문에 그 구조상 리플렉션을 가질 수 없습니다.
- 그 대신 C++에는 런타임 유형 정보(run-time type information)라는 기능이 있습니다.