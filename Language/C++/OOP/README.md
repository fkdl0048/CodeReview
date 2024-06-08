# C++ OOP

C++에서의 특징정인 OOP의 개념을 정리한다. OOP에 관한 문서는 따로 작성된 문서를 참고하고, C++에서의 OOP에 대한 내용을 정리한다. (low-level의 C++에서 동작하는 OOP)

- [기본 OOP의 개념](../../../OOP/README.md)

## C++에서 OOP

OOP가 주가 되어선 안된다. 읽기 쉽고, 유지보수가 좋고, 퍼포먼스를 위해 OOP를 사용하는 것이지, OOP의 개념에 입각하여 완벽한 객체지향을 짜는 것이 아니다.

**명심할 것, 가치 있는 제품, 소프트웨어을 창출하는 것이지 OOP가 완벽하게 준수된 프로그램을 짜는 것이 아니다.**

c++에서는 OOP와 퍼포먼스를 잡기 위함인데, 퍼포먼스가 중요한 영역에서는 OOP를 준수하지 못하는 경우가 종종 있다.

ex) 한 객체가 들고 있는 데이터가 많을 때, 해당 객체마다 데이터를 접근 순회하며 처리하기 보다 필요한 데이터 중심으로 처리하는 것이 더 효율적일 수 있다.

## 클래스

*구조체도 똑같이 적용된다.*

```cpp
class Cat
{
public:
    void speak();
private:
    int age;
};

int main()
{
    std::cout << sizeof(Cat) << std::endl; // 4

    Cat *cat = new Cat(); 
    // 힙에 실제로 할당되는 객체는 4바이트이다.
    // 스택의 포인터변수는 8바이트이다. 운영체제에 따라 다름

    return 0;
}
```

메모리 영역에 Cat클래스는 4바이트로 할당된다. 이는 멤버 변수 age가 4바이트이기 때문이다. 포인터도 영역의 차이이지 실제 할당되는 객체의 크기는 4바이트이다. static변수도 마찬가지로 힙 아래 데이터 영역에 4바이트로 할당된다.

여기서 멤버 변수의 순서나 바이트수의 차이에 따라 달라지는 것을 [바이트 패딩](../BytePadding/README.md) 이라고 한다. false sharing을 방지하기 위해 패딩을 사용할 수 있으니 해당 문서를 확인할 것.

중요한 점은 실제로 내가 생각한 객체의 크기가 메모리상에서 달라질 수 있으며 (자동적으로 효율적인 접근을 위해) 이를 인지하고 있어야 한다는 점이다..!

### Function Overloading

```cpp
void Function(int a)
{
    std::cout << "int" << std::endl;
}

void Function(float a)
{
    std::cout << "float" << std::endl;
}

int main()
{
    Function(1); // int
    Function(1.0f); // float

    return 0;
}
```

맹글링을 통해 함수의 이름이 변경되어 컴파일된다. 이는 함수의 이름이 같더라도 매개변수의 타입이나 개수에 따라 다른 함수로 인식되어 컴파일된다. 실제 어셈블리에서는 함수 이름 뒤에 매개변수의 타입이 붙어있는 것을 확인할 수 있다. `_Z8Functionf`와 같이.

function overloading을 다른말로 **static polymorphism**이라고도 한다. 이는 컴파일 시점에 어떤 함수와 바인딩될지 결정되기 때문이다. 이와 반대되는 것이 **dynamic polymorphism**이다. *virtual을 통해 구현된다.*

### Operator Overloading

```cpp
class Cat
{
public:
    Cat(int age) : age(age) {}
    Cat operator+(const Cat &cat) const
    {
        return Cat(this->age + cat.age);
    }
    int GetAge() const { return age; }
private:
    int age;
};

int main()
{
    Cat cat1(1);
    Cat cat2(2);

    Cat cat3 = cat1 + cat2;
    std::cout << cat3.GetAge() << std::endl; // 3

    return 0;
}
```

## Inheritance (상속)

상속을 사용하는 대표적인 이유에는 몇 가지가 있다.

- 클래스간의 관계
  - is-a 관계를 나타내기 위해 사용된다.
  - but, has-a의 관계나 인터페이스를 사용하는 것이 더 좋을 수 있다.
- 코드 재사용
  - 중복되는 코드를 줄이기 위해 사용된다.
- 일관적인 클래스 인터페이스
  - 상속을 통해 일관적인 인터페이스를 제공할 수 있다.
- 동적 바인딩
  - 가상함수를 통해 동적 바인딩을 사용할 수 있다.

가상함수 관련은 다음 문서를 참고할 것. [가상함수](../Virtual/README.md)

C++에서 발생하는 다중상속의 문제점을 해결하기 위해 인터페이스를 사용한다. 다중상속은 다이아몬드 문제를 발생시킬 수 있기 때문이다. 또는 상속에 `virtual` 키워드를 사용하여 다중상속의 문제를 해결할 수 있다.

### Object Slicing

오브젝트 슬라이싱은 파생 클래스의 객체를 기본 클래스의 객체로 변환할 때 발생한다. 파생 클래스의 객체를 기본 클래스의 객체로 변환하면 파생 클래스의 멤버 변수가 잘려나가는 현상이 발생한다.

```cpp
class Animal
{
public:
    Animal(std::string name) : name(name) {}
    std::string GetName() const { return name; }
private:
    std::string name;
};

class Cat : public Animal
{
public:
    Cat(std::string name, std::string color) : Animal(name), color(color) {}
    std::string GetColor() const { return color; }
private:
    std::string color;
};

int main()
{
    Cat cat("Kitty", "White");
    Animal animal = cat;

    std::cout << animal.GetName() << std::endl; // Kitty
    std::cout << animal.GetColor() << std::endl; // Error

    return 0;
}
```

위의 예제에서 `Animal animal = cat;`에서 cat 객체를 animal 객체로 변환하면서 오브젝트 슬라이싱이 발생한다. cat 객체의 color 멤버 변수가 잘려나가는 현상이 발생한다.

따라서 파생 클래스의 객체를 기본 클래스의 객체로 변환할 때는 포인터나 레퍼런스를 사용해야 한다.

```cpp
int main()
{
    Cat cat("Kitty", "White");
    Animal &animal = cat;

    std::cout << animal.GetName() << std::endl; // Kitty
    std::cout << dynamic_cast<Cat&>(animal).GetColor() << std::endl; // White

    return 0;
}
```

가장많이 발생하는 경우는 함수의 매개변수로 객체를 넘길 때 발생한다. 이때는 레퍼런스나 포인터를 사용해야 한다.

```cpp
void PrintAnimal(Animal &animal) // value로 넘기면 슬라이싱 발생 (복사하기 때문)
{
    std::cout << animal.GetName() << std::endl;
}
```

또는 copy constructor를 제한함으로써 오브젝트 슬라이싱을 방지할 수 있다.

```cpp
class Animal
{
public:
    Animal(std::string name) : name(name) {}
    Animal(const Animal &animal) = delete;
    std::string GetName() const { return name; }
}
```

### RTTI (Run-Time Type Information)

*기본적으로 사용하지 않는 것이 좋다.*

C++에서 업 캐스팅은 크게 문제가 되지 않지만, 다운 캐스팅은 문제가 될 수 있다. 이때 RTTI를 사용하여 객체의 타입을 확인할 수 있다.

또한, 업캐스팅의 경우 실제 메모리의 변화가 없기 때문에 Cat -> Animal로의 변환을 하더라도 Cat의 메서드를 호출한다. 다형성을 하고 싶다면 `virtual` 키워드를 사용해야 한다.

꼭 다운 캐스팅이 필요하다면 `dynamic_cast`를 사용한다. 이는 RTTI를 사용하여 객체의 타입을 확인하고, 타입이 맞다면 다운 캐스팅을 수행한다. 아니라면 `nullptr`을 반환한다.

```cpp
int main()
{
    Cat cat("Kitty", "White");
    Animal &animal = cat;

    if (Cat *cat = dynamic_cast<Cat*>(&animal))
    {
        std::cout << cat->GetColor() << std::endl;
    }

    return 0;
}
```

이러한 기능은 `typeid`를 통해 구현된다. `typeid`는 `type_info` 클래스를 반환한다. 실제로 VT에는 해당 타입 정보에 대한 포인터가 들어있다. 이를 확인하고 동적으로 Casting을 수행한다.

```cpp