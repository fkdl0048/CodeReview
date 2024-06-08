# C++ 생성자와 소멸자

C++에서는 C#과 다르게 소멸자를 사용한다. C#에서는 가비지 컬렉터가 메모리를 관리해주기 때문에 소멸자를 사용하지 않아도 된다. 하지만 C++에서는 메모리를 직접 관리하기 때문에 소멸자를 사용해야 한다.

*but 언리얼에서는 가비지 컬렉터를 사용하기 때문에 소멸자를 사용하지 않아도 된다.*

## 예제

```cpp
#include <iostream>

class Example {
public:
    Example() {
        std::cout << "생성자 호출" << std::endl;
    }

    ~Example() {
        std::cout << "소멸자 호출" << std::endl;
    }
};

int main() {
    Example example;
    return 0;
}
```

실제 결과는 다음과 같다.

```
생성자 호출
소멸자 호출
```

상속을 사용하여 좀 더 복잡한 예제를 살펴보자.

```cpp
#include <iostream>

class Base {
public:
    Base() {
        std::cout << "Base 생성자 호출" << std::endl;
    }

    ~Base() {
        std::cout << "Base 소멸자 호출" << std::endl;
    }
};

class Derived : public Base {
public:
    Derived() {
        std::cout << "Derived 생성자 호출" << std::endl;
    }

    ~Derived() {
        std::cout << "Derived 소멸자 호출" << std::endl;
    }
};

int main() {
    Derived derived;
    return 0;
}
```

실제 결과는 다음과 같다.

```
Base 생성자 호출
Derived 생성자 호출
Derived 소멸자 호출
Base 소멸자 호출
```

## 생성자 초기화 리스트

생성자에서 멤버 변수에 값을 넣어줄 때, C#스타일을 사용하면 불필요한 메모리 할당이 발생한다. 이를 방지하기 위해 생성자 초기화 리스트를 사용한다.

```cpp
#include <iostream>

class Example {
public:
    Example(int value) : m_Value(value) {
        std::cout << "생성자 호출" << std::endl;
    }

    ~Example() {
        std::cout << "소멸자 호출" << std::endl;
    }

private:
    int m_Value;
};

int main() {
    Example example(10);
    return 0;
}
```

## Copy Move Constructor

```cpp
class Cat {
public:
    void print()
    {
        std::cout << "Cat" << std::endl;
    }
private:
    int m_Age;
};
```

이런 코드가 있을 때, 컴파일러는 다음과 같은 메서드를 생성한다.

- Constructor
- Destructor
- Copy/Move Constructor
- Copy/Move Assignment

대부분 생성자는 사용자가 만들지만, 소멸자를 포함한 나머지는 컴파일러가 자동으로 생성하는 경우가 많다. 하지만 멤버변수로 포인터를 사용할 때는 사용자가 직접 만들어주는 것이 좋다.

이는 C++ the rule of three라고 불린다.

### Copy Constructor

이는 객체를 복사할 때 호출된다. 다른 말로 깊은 복사(deep copy)라고도 한다.

```cpp
Cat(const Cat& other)
{
    m_Age = other.m_Age;
}

//better
Cat(const Cat& other) : m_Age{other.m_Age}
{
}

int main()
{
    Cat cat1;
    Cat cat3{cat1}; // Copy Constructor
    Cat cat2 = cat1; // Copy Constructor 자동으로 호출
}
```

위의 cat2의 과정은 새로운 오브젝트를 생성하고, cat1을 복사하는 과정을 거친다. 이는 깊은 복사이다.

만약 포인터를 사용했다면 `memcpy`를 사용하여 복사했을 것

### Move Constructor

이는 객체의 이동을 할 때 호출한다. 객체를 복사하는 것이 아니라, 이동하는 것이다.

```cpp
Cat(Cat&& other) : m_Age{other.m_Age} {}
// string이라면 std::move를 사용한다.
// Cat(Cat&& other) : m_Age{std::move(other.m_Age)} {}
// 포인터라면 포인터를 옮기고 nullptr로 초기화한다.
```

### Copy Assignment

Assignment는 `=` 연산자를 사용할 때 호출된다.

```cpp
Cat& operator=(const Cat& other)
{
    if (this == &other)
        return *this;
    m_Age = other.m_Age;
    return *this;
}

int main()
{
    Cat cat1;
    Cat cat2;
    cat2 = cat1; // Copy Assignment
}
```

여기서 중요하게 생각해여 할 점은 `if (this == &other)`이다. 이는 자기 자신을 대입하는 것을 방지하기 위한 코드이다.

### Move Assignment

```cpp
Cat& operator=(Cat&& other)
{
    if (this == &other)
        return *this;
    m_Age = other.m_Age;
    // 포인터 동작 생각 즉, move에 대한 로직 매개변수가 rvalue
    return *this;
}
```

### Advanced

- Destructor, Move Constructor, Move Assignment는 noexcept로 선언하는 것이 좋다.
  - 이는 예외가 던져지지 않기 때문이다.
- 위에서 만든 예제에서 만약 포인터를 사용하지 않았다면 생성자를 제외한 나머지는 구현할 필요가 없다. 컴파일러가 자동으로 생성해준다.
  - but, 포인터를 사용했다면 직접 구현해야 한다.
- 따라서 생산성이 좋은 C++을 짜기 위해선 포인터를 최대한 피해야 한다.
- `delete`를 통해 copy, move를 막을 수 있다.
  - `Cat(const Cat& other) = delete;`
  - `Cat(Cat&& other) = delete;`
  - `Cat& operator=(const Cat& other) = delete;`
  - `Cat& operator=(Cat&& other) = delete;`
  - 이를 활용해 싱글톤 패턴의 특성 또는 static의 특성을 활용할 수 있다
  - ++ C++11이전에는 private 내부에 생성자를 넣어서 같은 기능을 구현했다.
