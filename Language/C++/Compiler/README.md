# C++ Compiler

C++ 컴파일러관련 내용 정리

## C++의 컴파일 과정

C++의 컴파일 과정은 크게 4단계로 나뉜다.

1. 전처리 단계: `#include`, `#define` 등의 전처리 지시자를 처리한다.
2. 컴파일 단계: 소스 파일들을 어셈블리 명령어로 변환한다.
3. 어셈블 단계: 어셈블리 코드들을 실제 기계어로 이루어진 목적 코드로 변환한다.
4. 링크 단계: 마지막으로 각각의 목적 코드를 하나의 실행 파일로 합친다.

![image](https://github.com/user-attachments/assets/c09eb13b-a2da-434e-b545-46ae1ef17d02)

### 전처리 단계 (Preprocessor)

**전처리 단계와 컴파일 단계 모두 컴파일러 안에서 수행된다.** [C++ 표준](https://eel.is/c++draft/lex.phases)에 따르면 이 두 단계는 8개의 세부 단계로 쪼개질 수 있다. 1부터 6까지가 전처리 과정으로 볼 수 있고, 나머지 세 개의 단계를 컴파일 과정으로 볼 수 있다.

#### Phase 1: 문자들 해석하기

가장 첫 단계로 문자들을 해석하는 것이다. C++ 코드에서는 총 96개의 문자들로 이루어진 Basic source character set을 통해 문자들을 해석한다.

```cpp
// 원본 소스 코드 (UTF-8로 저장되었다고 가정)
int main() {
    std::cout << "Hello, 세계!" << std::endl;
    return 0;
}
```

```cpp
cppCopyint main() {
    std::cout << "Hello, \u1138\u1112\u1100\uCE!" << std::endl;
    return 0;
}
```

#### phase 2: `\`문자 해석하기

`\`문자가 문장 맨 끝 부분에 위치한다면 해당 문장과 바로 다음에 오는 문장이 하나로 합쳐지고 개행 문자는 삭제된다.

```cpp
#define LONG_MACRO This is a very \
long macro definition that \
spans multiple lines
```

```cpp
#define LONG_MACRO This is a very long macro definition that spans multiple lines
```

#### Phase 3: 전처리 토큰들로 분리하기

이 단계에서는 소스 파일을 주석, 공백 문자, 전처리 토근들로 불리하는 단계이다. 전처리 토큰은 C++에서 가장 기본적인 문법 요소로 후에 컴파일러가 사용하는 토큰의 근간이 된다. 다음과 같은 것들이 전처리 토큰에 해당한다.

- 헤더 이름 (e.g. `#include <iostream>`)
- 식별자 (e.g. `main`, `std`, `cout`)
- 문자/문자 리터럴 (e.g. `'a'`, `"Hello, world!"`)
- 연산자들 (e.g. `+`, `<<`, `==`)

```cpp
#include <iostream>

int main() {
    // This is a comment
    std::cout << "Hello, world!" << std::endl;
    return 0;
}
```

- `#include <iostream>`은 전처리 지시자와 헤더 이름으로 인식됩니다.
- int, main, std, cout, endl, return은 식별자로 분류됩니다.
- "Hello, world!"는 문자열 리터럴로 인식됩니다.
- <<, ::는 연산자로 분류됩니다.
- (, ), {, }, ;는 구분자로 분류됩니다.
- 0은 숫자 리터럴로 분류됩니다.

#### Phase 4: 전처리 실행 단계

전처리 토큰을 분리했으니 이제 전처리기를 실행한다.

- `#include`에 지정된 파일의 내용을 복사한다.
- `#define`에 정의된 매크로를 사용해서 코드를 치환한다.
- `#if`, `#ifdef`와 같은 구문들을 실행해서 코드를 치환한다.
- `#program`와 같은 컴파일러 명령문을 해석한다.

```cpp
#include <iostream>
int main() {}
```

다음 코드는 실제로 다음과 같이 변환된다.

```cpp
namespace std
{
  typedef long unsigned int size_t;
  typedef long int ptrdiff_t;
  typedef decltype(nullptr) nullptr_t;
}
namespace std
{
  inline namespace __cxx11 __attribute__((__abi_tag__ ("cxx11"))) { }
}
namespace __gnu_cxx
{
  inline namespace __cxx11 __attribute__((__abi_tag__ ("cxx11"))) { }
}

// (생략) 약 27300줄의 코드

int main() {
```

따라서 헤더 가드를 사용하여 중복 포함을 방지하는 것이 중요하다. 또한 `#include`로 복사된 헤더 파일들은 다시 Phase 1부터 4까지의 단계를 거친다. 이 과정은 파일에 전처리기문이 없을 때 까지 지속된다.

but, `C++`20부터는 모듈이라는 개념이 도입되어 이러한 문제를 해결한다.

++ 해당 내용과 관련된 [Item 2: #define을 쓰려거든 const, enum, inline을 떠올리자](https://github.com/fkdl0048/BookReview/issues/278) 참고

#### Phase 5: 실행 문자 셋으로 변경하기

모든 문자들은 이전의 소스 코드 문자 셋에서 실행 문자 셋(Execution character set) 의 문자들로 변경된다.

#### Phase 6: 인접한 문자열 합치기

이 단계에서 인접한 문자열들이 하나로 합쳐진다.

```cpp
std::cout << "Hello, " "world!" << std::endl;
```

```cpp
std::cout << "Hello, world!" << std::endl;
```

**여기까지가 전처리기 과정이다.**

### 컴파일 단계 (Compiler)

전처리기 과정이 끝나고 나면 실제 컴파일 과정이 수행된다. 앞서 생성되었던 전처리기 토근들을 바탕으로 실제 컴파일 토근을 생성하여 분석한다.

#### Phase 7: 해석 유닛 생성 (Translation unit)

전처리기 토근들이 컴파일 토근으로 변환이 되고, 컴파일 토큰들은 컴파일러에 의해 해석되어서 해석 유닛(Translation unit, TU)이 생성된다. 해석 유닛은 컴파일러가 처리하는 가장 작은 단위로, 보통 하나의 소스 파일을 의미한다.

#### Phase 8: 인스턴스 유닛 생성 (Instantiation unit)

컴파일러는 TU를 분석해서 필요로 하는 템플릿 인스턴스를 확인한다. 템플릿들의 위치가 확인이 되면 해당 템플릿들의 인스턴스화가 진행되고 이를 통해서 인스턴스 유닛이 생성된다.

이 단계를 머치면 컴파일러는 목적 코드를 생성할 수 있게 되어 링킹을 위해 링커로 전달한다.

## 실제 어셈블리 과정

- [C++ 어셈블리어](https://github.com/fkdl0048/CodeReview/blob/main/Language/C%2B%2B/Assembler/README.md)

## 참조

- [씹어먹는 C++ - <20 - 1. 코드 부터 실행 파일 까지 - 전체적인 개요>](https://modoocode.com/319)
