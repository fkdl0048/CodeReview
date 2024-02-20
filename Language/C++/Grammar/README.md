# C++의 문법

C의 문법을 대부분 따라가기 때문에 특징적인 부분을 위주로 학습

대부분 C와 비슷하지만 특징적인 부분을 위주로 C#과 비교하여 작성

[참고 문서](https://modoocode.com/141)

## auto

- [공식문서](https://learn.microsoft.com/ko-kr/cpp/cpp/auto-cpp?view=msvc-170)

`auto`는 C++11부터 지원하는 키워드로, 변수의 타입을 자동으로 추론하는 기능을 제공한다.

```c++
auto a = 10; // int
auto b = 10.0; // double
auto c = "Hello"; // const char*
```

`auto`는 초기화를 통해 타입을 추론하기 때문에 초기화가 없으면 사용할 수 없다.

```c++
auto a; // error
```

`auto`는 포인터, 참조, const, volatile, const volatile를 모두 지원한다.

```c++
int a = 10;
auto b = &a; // int*
auto& c = a; // int&
auto d = const_cast<const int&>(a); // const int
auto e = const_cast<volatile int&>(a); // volatile int
auto f = const_cast<const volatile int&>(a); // const volatile int
```

`auto`는 함수의 반환 타입을 추론할 때도 사용할 수 있다.

```c++
auto add(int a, int b) -> int {
    return a + b;
}
```

`auto`는 반복자를 사용할 때도 사용할 수 있다.

```c++
std::vector<int> v = {1, 2, 3, 4, 5};
for (auto it = v.begin(); it != v.end(); ++it) {
    std::cout << *it << std::endl;
}
```

`auto`는 람다식을 사용할 때도 사용할 수 있다.

```c++
auto f = [](int a, int b) -> int {
    return a + b;
};
```

`auto`는 템플릿을 사용할 때도 사용할 수 있다.

```c++
template <typename T, typename U>
auto add(T a, U b) -> decltype(a + b) {
    return a + b;
}
```

`C#`의 `var`와 차이점은 `auto`는 초기화를 통해 타입을 추론하기 때문에 초기화가 없으면 사용할 수 없다는 것이다.

`C#`의 `var`는 초기화가 없어도 사용할 수 있지만, 초기화가 없으면 `object`로 추론된다.

따라서 C#의 var는 컴파일러에게 타입 추론을 요청하는 반면, C++의 auto는 컴파일러가 타입을 추론하여 변수를 선언할 때 사용됩니다.

C++ 11에 추가

## const auto&

`const auto&`는 `const`로 선언된 변수를 참조하는 것이다.

```c++
const auto& a = 10;
```

`const auto&`는 `const`로 선언된 변수를 참조하기 때문에 `const`로 선언된 변수와 동일한 특성을 가진다.

```c++
const auto& a = 10;
a = 20; // error
```

## typedef

- [공식문서](https://learn.microsoft.com/ko-kr/cpp/cpp/aliases-and-typedefs-cpp?view=msvc-170)

별칭 선언을 사용하여 이전에 선언된 형식의 동의어로 사용할 이름을 선언할 수 있습니다. (이 메커니즘은 비공식적으로 형식 별칭이라고 도 함). 이 메커니즘을 사용하여 사용자 지정 할당자에 유용할 수 있는 별칭 템플릿을 만들 수도 있습니다.

```c++
using identifier = type;
```

`C#`의 `using`과 비슷하다.

- 구문
  - C++의 typedef: typedef 키워드를 사용하여 새로운 타입을 선언합니다. 예를 들어, typedef int MyInt;는 MyInt를 int의 별칭으로 정의합니다.
  - C#의 using 문: using 키워드를 사용하여 다른 네임스페이스를 별칭으로 지정합니다. 예를 들어, using MyInt = System.Int32;는 MyInt를 System.Int32의 별칭으로 지정합니다.
- 범위
  - C++의 typedef: typedef는 특정 타입에 대한 별칭을 현재 스코프에서만 유효합니다. 다른 스코프에서는 별칭이 적용되지 않습니다.
  - C#의 using 문: using 문은 네임스페이스 레벨에서 사용되며, 해당 네임스페이스 내의 모든 범위에서 적용됩니다. 다른 네임스페이스에서는 별칭이 유효하지 않습니다.
- 적용 대상
  - C++의 typedef: 주로 기본 데이터 타입이나 사용자 정의 타입에 대한 별칭을 지정하는 데 사용됩니다.
  - C#의 using 문: 주로 네임스페이스의 별칭을 지정하는 데 사용됩니다. 따라서 주로 외부 라이브러리의 특정 타입에 대한 별칭을 지정하는 데 사용됩니다.
- 확장성
  - C++의 typedef: 한 번 선언된 별칭은 변경할 수 없으며, 새로운 별칭을 추가할 때마다 새로운 선언이 필요합니다.
  - C#의 using 문: 여러 개의 using 문을 사용하여 여러 개의 별칭을 한 번에 지정할 수 있으며, 기존의 using 문을 수정하여 새로운 별칭을 추가하거나 제거할 수 있습니다.

## constexpr

- [공식문서](https://learn.microsoft.com/ko-kr/cpp/cpp/constexpr-cpp?view=msvc-170)

constexpr는 C++11부터 도입된 키워드로, 컴파일 시간에 평가되고 실행 시간에 상수로 사용될 수 있는 함수나 변수를 선언하는 데 사용됩니다. 즉, constexpr를 사용하면 컴파일러가 컴파일 시간에 계산할 수 있는 값을 미리 계산하여 상수로 사용할 수 있습니다.

```c++
constexpr int square(int x) {
    return x * x;
}

int main() {
    constexpr int result = square(5); // 컴파일 시간에 square(5)가 25로 계산됩니다.
    return 0;
}
```

- 컴파일 시간 평가
  - constexpr로 선언된 함수는 컴파일 시간에 호출될 수 있으며, 함수가 반환하는 값을 컴파일 시간에 계산할 수 있습니다. 이를 통해 실행 시간의 성능을 향상시킬 수 있습니다.
- 상수 표현식
  - constexpr로 선언된 변수는 상수 표현식으로 사용될 수 있습니다. 다른 constexpr 변수나 함수의 매개변수 또는 리턴 값으로 사용될 수 있습니다.
- 컴파일 타임 최적화
  - constexpr로 선언된 함수는 컴파일 시간에 실행되므로, 컴파일러는 이러한 함수를 최적화할 수 있습니다. 이를 통해 실행 시간의 오버헤드를 최소화할 수 있습니다.

## iterator

- [공식문서](https://learn.microsoft.com/ko-kr/cpp/standard-library/iterator?view=msvc-170)

- 헤더: `<iterator>`
- 네임스페이스: `std`

`C#`의 `IEnumerator`와 다르지만, 비슷하다.

C++ 프로그램이 서로 다른 데이터 구조로 균일한 방식으로 작업할 수 있도록 하는 포인터의 일반화입니다.

- 입력 반복자(Input Iterator): 한 번에 한 요소를 읽을 수 있는 반복자입니다. 읽기 전용으로 사용되며, 요소를 읽은 후에는 다음 요소로 이동하여 순차적으로 이동할 수 있습니다.
- 출력 반복자(Output Iterator): 한 번에 한 요소를 쓸 수 있는 반복자입니다. 읽기 전용이 아니며, 요소를 쓴 후에는 다음 요소로 이동하여 순차적으로 이동할 수 있습니다.
- 양방향 반복자(Bidirectional Iterator): 한 번에 한 요소를 읽거나 쓸 수 있는 반복자이며, 요소를 앞뒤로 이동할 수 있습니다. 리스트와 같은 양방향 순회가 가능한 컨테이너에서 사용됩니다.
- 임의 접근 반복자(Random Access Iterator): 어떤 요소든 한 번에 접근하고 읽거나 쓸 수 있는 반복자입니다. 또한 임의의 위치로 이동할 수 있으며, 산술 연산(덧셈, 뺄셈)을 사용하여 특정 위치로 바로 이동할 수 있습니다. 벡터와 같은 배열 기반의 컨테이너에서 사용됩니다.

- 반복자는 일반적으로 다음과 같은 연산을 지원합니다
  - *it: 반복자가 가리키는 요소에 대한 참조 또는 값에 접근합니다.
  - it++, it--: 다음 또는 이전 요소로 이동합니다.
  - it + n, it - n, it += n, it -= n: n개의 요소를 앞뒤로 이동합니다.
  - it1 == it2, it1 != it2: 두 개의 반복자가 같은지 여부를 확인합니다.

STL(Standard Template Library)에서는 각 컨테이너에 대해 적절한 종류의 반복자를 제공하여 일관된 방식으로 컨테이너를 순회할 수 있도록 지원합니다. 종종 반복자는 범위 기반(for-range) 루프와 함께 사용되어 컨테이너를 편리하게 순회할 수 있습니다.

C#에선 IEnumerable을 사용하여 컨테이너를 순회할 수 있지만, C++에선 반복자를 사용하여 컨테이너를 순회할 수 있다.

인터페이스를 통해 이터레이터 패턴을 구현하여 사용하는 것

실제로 컨테이너 함수에 이터레이터 함수를 사용하여 컨테이너를 순회할 수 있다.

```c++
#include <array>
#include <iostream>

typedef std::array<int, 4> MyArray;

int main()
{
    MyArray c0 = { 0, 1, 2, 3 };

    // display contents " 0 1 2 3"
    std::cout << "it1:";
    for (MyArray::const_iterator it1 = c0.begin();
        it1 != c0.end();
        ++it1) {
        std::cout << " " << *it1;
    }
    std::cout << std::endl;

    // display first element " 0"
    MyArray::const_iterator it2 = c0.begin();
    std::cout << "it2:";
    std::cout << " " << *it2;
    std::cout << std::endl;

    return (0);
}
```

## 연산자 관련

연산자는 전위로 쓰는게 좋다.