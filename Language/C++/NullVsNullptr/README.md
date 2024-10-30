# null vs nullptr

C++에서 사용되는 null과 nullptr의 차이점을 알아보자. *(사용 방법은 대략적으로 알고 있지만 실제 차이점을 명확히 모르는 경우)*

## null

`null`은 기존 C언어에서 사용된 관습적인 **매크로**로, 보통 `#define NULL 0` 또는 `#define NULL ((void*)0)`으로 정의된다. *cstddef 헤더 파일에 정의되어 있으며, `0` 또는 `NULL`로 사용된다.*

배경과 기원이 이러해서 C++에서는 `NULL`이 단순히 정수 `0`으로 취급되며 포인터와 정수 간의 모호함을 유발한다. 예를 들어, `NULL`은 `int`와 `char*` 모두에 대입할 수 있어서 컴파일러가 암묵적으로 형변환을 수행한다.

```cpp
int* p = NULL; // int* p = 0;
char* q = NULL; // char* q = 0; => 컴파일러가 암묵적으로 형변환
```

이러한 코드는 코드 작성자에게 혼란을 줄 수 있다..! *실제 경험* 따라서 C++11에서는 `nullptr`를 도입하여 이러한 문제를 해결하였다.

## nullptr

C++11에서 등장한 `nullptr`은 `nullptr_t` 타입의 리터럴이다. `nullptr`는 `nullptr_t` 타입의 유일한 값이며, `nullptr`는 모든 포인터 타입으로 암묵적으로 변환되지 않는다. 따라서 위에서 말한 혼란을 방지할 수 있다.

```cpp
void foo(int i);
void foo(char* p);

foo(nullptr); // foo(char* p); => nullptr는 모든 포인터 타입으로 암묵적으로 변환되지 않는다.
```

헷갈릴 수 있는 부분인 `nullptr`은 `nullptr_t` 타입의 리터럴이라는 용어로 이는 특별한 리터럴 상수로 nullptr이 메모리의 특정 위치를 차지하지 않으며 컴파일 타임에 처리되는 리터럴 상수라고 이해할 수 있다. nullptr_t가 타입이니 nullptr은 변수이고 전역적으로 사용되니 static인가? -> 이 흐름이 아닌 리터럴 상수라는 점을 기억하자.
