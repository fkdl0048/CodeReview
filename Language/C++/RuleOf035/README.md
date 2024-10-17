# C++ Rule(0/3/5)

- [Effective C++ Item 5](https://github.com/fkdl0048/BookReview/issues/281)에서 나온 C++가 자동으로 생성하는 함수들에 해당되는 개념이다. 해당 책은 C++ 11이전의 개념이라 현재 추가된 이동 함수의 개념까지 정리하려고 한다.
- [공식문서 개념](https://en.cppreference.com/w/cpp/language/rule_of_three)

## Rule of 5

C++에서 자원을 관리하는 클래스(동적 메모리나 파일 핸들 등)는 복사나 이동 시에 자원의 정확한 관리를 위해 특별한 멤버 함수를 필요로 한다. 이러한 함수들은 객체의 생명주기 동안 자원의 소유권과 관리 권한을 제어하는데 이를 정리한 것이 Rule of 5이다.

### 소멸자 (Destructor)

객체가 소멸될 때 호출되며, 동적 메모리나 다른 자원을 해제하는 데 사용한다.

```cpp
class Cat {
public:
    ~Cat();
};
```

직접 구현하지 않으면 컴파일러가 자동으로 생성한다. but, 포인터를 사용하는 경우 직접 구현해야 한다. 이유는 메모리 누수가 발생할 수 있거나 얕은 복사에 해당하는 문제가 발생할 수 있기 때문이다.

요즘은 RAII나 Smart Pointer, STL을 사용하긴 하지만, 필요에 따라 직접 구현해야 한다.

추가로 다형성을 가진 기본 클래스에서는 소멸자를 반드시 가상으로 선언해야 한다. 좀 더 자세한 내용은 [Effective C++ Item 7](https://github.com/fkdl0048/BookReview/issues/284)을 참고하자.

### 복사 생성자 (Copy Constructor)

객체가 다른 객체로부터 복사되어 생성될 때 호출되며, **깊은 복사**를 수행하여 자원의 복제를 처리한다.

실제로 발생하는 경우는 다음과 같다.

- 객체를 함수에 값으로 전달할 때
- 함수에서 객체를 값으로 반환할 때
- 객체를 초기화할 때(다른 객체를 사용하여)

*추가적으로 함수나 반환에 의해서 처리될 때 RVO(Return Value Optimization)에 의해서 처리될 때는 복사 생성자가 호출되지 않는다. 또는 Copy elision*

```cpp
class Cat {
public:
    Cat(const Cat& other);
};
```

직접 구현하지 않으면 컴파일러가 자동으로 생성한다.

만약 포인터나 동적 할당된 메모리를 사용하는 경우는 단순히 주소를 복사하는 것이 아닌 실제 데이터를 새로 할당하고 복사해야 한다. 일반적으로 const 참조를 받아 원복 객체의 수정을 방지한다.

만약 복사 대임 연산자와 같이 자기 자신을 복사하는 경우를 처리할 수 있어야 한다.

- 추가적인 깊은 개념은 [Effective C++ Item 12](https://github.com/fkdl0048/BookReview/issues/294)를 참고하자.

```cpp
class DynamicArray {
private:
    int* data;
    size_t size;

public:
    DynamicArray(const DynamicArray& other) 
        : size(other.size), data(new int[other.size]) 
    {
        std::copy(other.data, other.data + size, data);
    }
    
    // 다른 멤버 함수들...
};
```

이렇게 깊은 복사를 수행하여 독립적인 메모리 구조를 가질 수 있도록 해야 댕글링포인터나 이중 해제 등의 문제를 방지할 수 있다.

### 복사 대입 연산자 (Copy Assignment Operator)

이미 존재하는 객체에 다른 객체를 복사 대입할 때 호출되며, 기존 자원을 적절히 해제하고 새로운 자원을 복제한다.

직접 구현하지 않으면 컴파일러가 자동으로 생성한다. 하지만 포인터나 동적 할당된 메모리를 사용하는 경우에는 마찬가지로 직접 구현해야 한다.

복사 대입 연산자는 다음과 같은 상황에서 호출된다

- 이미 존재하는 객체에 다른 객체를 **대입**할 때
- 객체 배열에서 요소를 **대입**할 때

복사 대입 연산자를 구현할 때는 자기 대입 검사를 철저하게 해야 한다..

- 추가적인 깊은 개념은 [Effective C++ operator=에서는 자기대입에 대한 처리가 빠지지 않도록 하자](https://github.com/fkdl0048/BookReview/issues/293)를 참고하자.

또한 새로운 자원을 할당하기 전에 기존 자원을 적절하게 해제해야 한다. 깊은 복사도 마찬가지로 지원해야 하기에 위 복사 생성자의 개념도 마찬가지로 적용된다.

```cpp
class DynamicArray {
private:
    int* data;
    size_t size;

public:
    DynamicArray& operator=(const DynamicArray& other) {
        if (this != &other) {  // 자기 대입 검사
            DynamicArray temp(other);  // 복사 생성자를 이용한 임시 객체 생성
            std::swap(data, temp.data);  // 데이터 교환
            std::swap(size, temp.size);
        }
        return *this;
    }
    
    // 다른 멤버 함수들...
};
```

복사 대입 연산자를 구현할 때 `Copy and Swap` 개념을 사용할 수 있다. 이 개념을 사용한다면 자기 대입 검사가 필요하지 않다..!

```cpp
class DynamicArray {
public:
    DynamicArray& operator=(DynamicArray other) {
        std::swap(data, other.data);
        std::swap(size, other.size);
        return *this;
    }
};

```

### 이동 생성자 (Move Constructor)

객체가 이동되어 생성될 때 호출되며, 자원의 소유권을 이전하여 복사 비용을 절감한다. 이는 C++11 이후에 추가된 개념이다.

```cpp
class Cat {
public:
    Cat(Cat&& other) noexcept;
};
```

*우측값에 대한 이해가 필요한 부분이라 Lvalue, Rvalue에 대한 개념을 이해해야 한다. 해당 개념은 [PointerVsReference](https://github.com/fkdl0048/CodeReview/blob/main/Language/C%2B%2B/PointerVsReference/README.md#lvalue-rvalue)에서 확인할 수 있다.*

직접 구현하지 않으면 컴파일러가 자동으로 생성한다. 하지만 포인터나 동적 할당된 메모리를 사용하는 경우에는 역시.. 직접 구현하는 것이 좋다.

이동 생성자는 다음과 같은 상황에서 호출된다:

- 임시 객체를 생성할 때
- std::move()를 사용하여 객체를 명시적으로 이동할 때
- 함수에서 객체를 반환할 때 (RVO가 적용되지 않는 경우)

이동 생성자를 구현할 때 주의해야 할 점들은 다음과 같다.

noexcept 지정자 사용하여 이동 연산은 예외를 발생시키지 않아야 한다. 이는 성능에 영향과 예외 안전성을 보장한다. 또한 이동 생성자가 필요한 이유를 좀 크게 고민해보고.. 자원의 이전이 필요한 이유를 좀 더 크게 고민해보면 좋다.

사용자 정의로 구현한다면 원본 객체의 포인터를 무효화하고 자원을 이전해야 한다.

```cpp
class DynamicArray {
private:
    int* data;
    size_t size;

public:
    DynamicArray(DynamicArray&& other) noexcept
        : data(other.data), size(other.size)
    {
        other.data = nullptr;
        other.size = 0;
    }
    
    // 다른 멤버 함수들...
};
```

### 이동 대입 연산자 (Move Assignment Operator)

이동 대입 연산자는 이미 존재하는 객체에 다른 객체를 이동 대입할 때 호출한다. 이동 생성자와 다른 점은 이미 존재하는 객체에 대한 작업을 수행한다는 점이다.

```cpp
class Cat {
public:
    Cat& operator=(Cat&& other) noexcept;
};
```

이동 대입 연산자는 이미 존재하는 객체에 임시 객체나 **std::move()**를 사용하여 이동된 객체를 대입할 때 호출한다. 즉, 사용자가 이동을 명시적으로 요청하는 경우에 호출된다.

이를 구현할 대는 마찬가지로 noexcept 지정자를 사용해 예외를 발생시키지 않도록 해야하고, 자기 대입 검사를 수행해야 한다. 또한, 기존 자원을 해제한 후 새로운 자원의 소유권을 이전해야 한다.

```cpp
class DynamicArray {
private:
    int* data;
    size_t size;

public:
    DynamicArray& operator=(DynamicArray&& other) noexcept {
        if (this != &other) {  // 자기 대입 검사
            delete[] data;  // 기존 자원 해제
            
            // 자원 이동
            data = other.data;
            size = other.size;
            
            // 원본 객체 무효화
            other.data = nullptr;
            other.size = 0;
        }
        return *this;
    }
    
    // 다른 멤버 함수들...
};
```

### 필요한 이유

C++은 애초에 메모리를 조심스럽게 다뤄야 하는 영역이기 때문에 최적화나 성능의 관리가 매우 중요하다. 여기에 가장 밀접하게 연관되어 있는 복사, 이동, 생성의 영역이기에 Cpp자체에서 규칙을 정해놓았다.

중간에 언급했지만 기본 제공되는 함수들은 얕은 복사를 수행하기에 자원 누수나 이중 해제 문제가 생길 수 있다..

이 외에도 소멸자를 사용자 정의로 구현하면 이동 생성자와 이동 대입 연산자는 자동으로 생성되지 않는다.

```cpp
class MyClass {
private:
    int* data;
public:
    // 1. 소멸자
    ~MyClass() {
        delete data;
    }

    // 2. 복사 생성자
    MyClass(const MyClass& other) {
        data = new int(*other.data);
    }

    // 3. 복사 대입 연산자
    MyClass& operator=(const MyClass& other) {
        if (this != &other) {
            delete data;
            data = new int(*other.data);
        }
        return *this;
    }
    // 추가적으로 copy and swap 개념을 사용할 수 있는데 해당 내용은 위에서 참고

    // 4. 이동 생성자
    MyClass(MyClass&& other) noexcept {
        data = other.data;
        other.data = nullptr;
    }

    // 5. 이동 대입 연산자
    MyClass& operator=(MyClass&& other) noexcept {
        if (this != &other) {
            delete data;
            data = other.data;
            other.data = nullptr;
        }
        return *this;
    }
};
```

논외이긴 하지만 객체를 하나 만들었을 때 생성되는 함수는 총 6개이다. 기본 생성자는 룰에 해당하지 않는다.

## Rule of 3

C++11 이전에는 Rule of 3이라는 개념이 있었다. 이는 복사 생성자, 복사 대입 연산자, 소멸자 이렇게 세 가지만 정의하면 된다는 개념이다.

## Rule of 0

현대 C++에서는 Rule of Zero라는 개념이 있다. **이는 리소스 관리를 스마트 포인터나 표준 라이브러리의 컨테이너를 통해 자동화하면 특별한 멤버 함수를 직접 구현할 필요가 없다는 것**으로, RAII개념과 맞닿아 있다. 이렇게 하면 클래스가 복사나 이동 시에도 안전하게 동작하며, 코드의 복잡성을 줄일 수 있다.

이는 스마터 포인터의 개념과 STL 컨테이너의 개념을 좀 더 깊게 곰부하면 된다. 그래도 위의 내용을 알아야 하는 이유는 실제로 개발할 때는 Rule of Zero를 활용하겠지만, 이에 대한 배경지식과 엔진쪽이나 과거의 라이브러리, 내부에선 어떻게 돌아가는지 알아야 문제점을 찾아내거나 버그를 찾아낼 수 있기 때문이다.

- [스마트 포인터 개념 정리](https://github.com/fkdl0048/CodeReview/blob/main/Language/C%2B%2B/SmartPointer/README.md)
