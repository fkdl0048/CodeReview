# C++ String

기본적으로 C++을 사용함에 있어서 C스타일의 문자열을 사용할 수 있어야 한다.

char의 연속된 배열, 포인터로 사용하는 법, readonly등의 특징을 가지고 있다.

## C++에서 다른 점

C++에서는 string이라는 클래스를 제공한다. 여러 메서드를 제공한다. 또한 스택이 아닌 힙에 할당되어 런타임에 자유롭게 크기를 조절할 수 있다.

퍼포먼스가 민감한 부분에서는 C스타일의 문자열을 사용하는 것이 좋다. 하지만 대부분의 경우 string 클래스를 사용하는 것이 편리하다.

## string view

C++17부터 string_view라는 클래스가 추가되었다. span과 같이 여러 문자열을 다루는 데 유용하다.

- char [] (스택 메모리에 할당)
- const char * (프로세스 영역에 readonly로 할당)
- std::string (힙 메모리에 할당)

함수에서 이를 string으로 받아서 사용해도 크게 문제는 없지만, 퍼포먼스 저하가 일어나기 때문에 string_view를 사용하는 것이 좋다.

*string은 복사가 일어나서 성능이 떨어지지만, string_view는 복사가 일어나지 않아 성능이 좋다.*

```cpp
#include <iostream>
#include <string>
#include <string_view>

void printString(std::string_view str) {
    std::cout << str << std::endl;
}

int main() {
    std::string str = "Hello, World!";
    printString(str);
    return 0;
}
```

마찬가지로 레퍼런스로 동작하며, 시작 주소와 길이를 가지고 있다. (span과 비슷한 개념이다.) 단점도 마찬가지로 원본의 수정이 발생하면 문제가 발생할 수 있다.
