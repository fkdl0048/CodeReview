# Virtual

C++의 또 다른 특징점인 가상함수(Virtual Function)에 대해 정리한다.

## 가상함수(Virtual Function)

```cpp
#include <iostream>
#include <string>

class Animal
{
public:
    virtual void speak() const
    {
        std::cout << "Animal" << std::endl;
    }
};

class Cat : public Animal
{
public:
    void speak() const override
    {
        std::cout << "Cat" << std::endl;
    }
};

int main()
{
    Animal *animal = new Cat();
    animal->speak(); // Cat

    return 0;
}
```

- `virtual` 키워드를 사용하면 해당 함수가 가상함수임을 명시한다.
- `override` 키워드를 사용하면 해당 함수가 부모 클래스의 가상함수를 오버라이딩한다는 것을 명시한다.
- `virtual` 함수는 해당 함수가 호출되는 객체의 타입을 따라가기 때문에, 위의 예제에서 `Animal *animal = new Cat();`에서 `animal->speak();`를 호출하면 `Cat`이 출력된다.
- `virtual` 함수는 가상함수 테이블(Virtual Function Table)을 통해 호출된다. 이는 객체의 메모리 구조에 대한 이해가 필요하다.
- 가상함수는 성능상의 이슈가 있기 때문에, 필요한 경우에만 사용하는 것이 좋다.
- 가상함수를 사용하지 않고, 함수 포인터를 사용하는 방법도 있다.
- 가상함수는 상속을 통해 다형성을 구현하는데 사용된다.

### 정리

- 대부분 Base 클래스에 `virtual` 키워드를 적용하고, 이를 상속받는 클래스에서 `override` 키워드를 사용한다.

## Virtual Table

```cpp
#include <iostream>
#include <string>

class Animal
{
public:
    void speak() const
    {
        std::cout << "Animal" << std::endl;
    }
private:
    double height;
};

class Cat : public Animal
{
public:
    void speak() const
    {
        std::cout << "Cat" << std::endl;
    }
private:
    double weight;
};

int main()
{
    // sizeof
    std::cout << sizeof(Animal) << std::endl; // 8 byte
    std::cout << sizeof(Cat) << std::endl; // 16 byte
}
```

위 예제는 멤버 변수의 크기에 맞게 8, 16바이트로 할당된다. 여기서 virtual 함수를 사용하면 어떻게 될까?

```cpp
#include <iostream>
#include <string>

class Animal
{
public:
    virtual void speak()
    {
        std::cout << "Animal" << std::endl;
    }
private:
    double height;
};

class Cat : public Animal
{
public:
    void speak() override
    {
        std::cout << "Cat" << std::endl;
    }
private:
    double weight;
};

int main()
{
    // sizeof
    std::cout << sizeof(Animal) << std::endl; // 16 byte
    std::cout << sizeof(Cat) << std::endl; // 24 byte
}
```

위 예제에서 `virtual` 키워드를 사용하면 `Animal` 클래스의 크기가 16, `Cat` 클래스의 크기가 24바이트로 할당된다. 이는 가상함수 테이블(Virtual Table)을 통해 가상함수를 호출하기 때문이다.

virtual 키워드를 붙임으로서 실제 메모리에는 포인터 변수(8 byte, 운영체제에 따라 다름) 하나가 추가되어 가상함수 테이블을 가리키게 된다. 이는 가상함수를 호출할 때, 해당 객체의 타입을 따라가기 위함이다.

**이 때문에 base클래스의 소멸자는 반드시 가상 소멸자로 선언해야 한다. 이는 상속받은 클래스의 소멸자가 호출될 수 있도록 하기 위함이다.** 만약 선언되어 있지 않다면 base클래스의 소멸자만 호출되고, 상속받은 클래스의 소멸자는 호출되지 않는다.

## 순수 가상함수(Pure Virtual Function)

```cpp
#include <iostream>
#include <string>

class Animal
{
public:
    virtual void speak() const = 0;
};

class Cat : public Animal
{
public:
    void speak() const override
    {
        std::cout << "Cat" << std::endl;
    }
};

int main()
{
    // Animal animal; // error
    Cat cat;
    cat.speak();

    return 0;
}
```

- 순수 가상함수는 `= 0`으로 선언한다.
- 순수 가상함수를 가지고 있는 클래스는 추상 클래스(abstract class)라고 한다.
- 추상 클래스는 객체를 생성할 수 없다.

여기서 인터페이스로 확장하기 위해선 내부에 순수 가상함수만을 가지고 있는 클래스를 만들어야 한다.

## virtual inheritance

```cpp
#include <iostream>
#include <string>

class Animal
{
public:
    virtual void speak() const
    {
        std::cout << "Animal" << std::endl;
    }
};

class Cat : public virtual Animal
{
public:
    void speak() const override
    {
        std::cout << "Cat" << std::endl;
    }
};

class Dog : public virtual Animal
{
public:
    void speak() const override
    {
        std::cout << "Dog" << std::endl;
    }
};

class CatDog : public Cat, public Dog
{
public:
    void speak() const override
    {
        std::cout << "CatDog" << std::endl;
    }
};

int main()
{
    CatDog catdog;
    catdog.speak(); // CatDog

    return 0;
}
```

다음의 상속 구조에서 `CatDog` 클래스는 `Cat`과 `Dog` 클래스를 다중 상속받는다. 이 때 `virtual` 키워드를 사용하면 `Animal` 클래스를 가상 상속받을 수 있다. *virtual이 없다면 Animal이 두 번 상속되어 다이아몬드 문제가 발생한다. 생성자가 두번 실행되어 버림*

**실제 메모리에도 VT가 추가되어서 8바이트가 더 할당된다.**, virtual inheritance의 특징이다.

### 추가적인

virtual 상속의 경우 어떤 부모를 가리키는지 모르기 때문에 일반적인 메모리 구조가 아닌 VT에서 해당 함수를 찾아가는 방식으로 동작한다. 실제로 내부에 offset이라는 값을 통해 해당 데이터를 참조하고 위치는 Tharker를 통해 찾아간다.