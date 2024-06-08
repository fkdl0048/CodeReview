# C++ Const

C++에서는 const가 가능하다면 다 붙여주는 것이 좋다. 이것은 개발자 본인에게도 이점이 있지만, 컴파일러에게도 이점이 있다. 컴파일러는 const를 보고 최적화를 할 수 있기 때문이다.

## const

```cpp
class Cat
{
    Cat(std::string name) : mName{std::move(name)} {};
    void speak() const
    {
        std::cout << mName << " Meow" << std::endl;
    };

private:
    const std::string mName;
};

int main()
{
    const Cat cat("Kitty");
    cat.speak();

    return 0;
}
```

- const로 생성된 객체는 const 함수만 호출할 수 있다.
- const 함수는 멤버 변수를 변경할 수 없다.
- 변경하려면 mutable을 사용해야 한다.

## mutable

```cpp
class Cat
{
    Cat(std::string name) : mName{std::move(name)} {};
    void speak() const
    {
        std::cout << mName << " Meow" << std::endl;
        mCount++;
    };

private:
    const std::string mName;
    mutable int mCount = 0;
};

int main()
{
    const Cat cat("Kitty");
    cat.speak();

    return 0;
}
```

하지만 가능하다면 mutable을 사용하지 않는 것이 좋다. const로 선언된 객체는 const 함수만 호출할 수 있기 때문에, mutable을 사용하면 const 함수에서도 변경이 가능하다. 이는 const 함수의 의미를 퇴색시킬 수 있기 때문이다.

## explicit

explicit은 암시적 변환을 막아준다. 이는 생성자에만 사용할 수 있다.

```cpp
class Cat
{
    Cat(std::string name) : mName{std::move(name)} {};
    void speak() const
    {
        std::cout << mName << " Meow" << std::endl;
    };
    
    explicit Cat(int age) : mAge{age} {};

private:
    const std::string mName;
    int mAge;
};

int main()
{
    Cat cat(1); // OK
    Cat cat2 = 1; // Error

    return 0;
}
```

- C++에선 멤버변수가 하나라면 대부분 Explicit을 사용하는 것이 좋다.

## Encapsulation return

```cpp
class Cat
{
    Cat(std::string name) : mName{std::move(name)} {};

    // 기본적인
    std::string getName() const
    {
        return mName;
    };

    // Encapsulation return
    const std::string& getName() const
    {
        return mName;
    };

private:
    const std::string mName;
};

int main()
{
    Cat cat("Kitty");
    
    std::string name = cat.getName(); // 복사(deep copy)
    const std::string& name2 = cat.getName(); // 참조 (no deep copy)

    return 0;
}
```

기본적인 방식은 불필요한 복사가 일어나기 때문에 const로 리턴 받을 때는 해당 값을 조작할 수 없으니 Encapsulation return을 사용하는 것이 좋다.