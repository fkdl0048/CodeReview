# C++ Static

C++에서의 `static` 키워드에 대해 정리한다.

크게 3가지로 나눌 수 있다.

- Static Member Function
- Static Member Variable
- Static Variable in Function

## Static Member Function

멤버 함수에 `static` 키워드를 붙이면, 해당 함수는 객체가 아닌 클래스에 속해있는 함수가 된다.

```cpp
class Example {
public:
    static void print() {
        std::cout << "Hello, World!" << std::endl;
    }
};

int main() {
    Example::print(); // Hello, World!
    return 0;
}
```

즉, 객체를 생성하지 않고도 클래스 이름으로 호출할 수 있다. 이것이 가능한 이유는 static member function은 객체와 연관성이 없기 때문이다.

이는 객체의 `this`라는 참조가 바인딩이 되어 있지 않다. 이는 [C++ 메모리 구조](../MemoryStructure/README.md)에서 나온 **스택 프레임**과 연관성이 있다. 콜스택에 객체의 주소인 this로 접근하는 반면, static member function은 객체와 연관성이 없기 때문에 this가 없다.

*멤버변수로 접근하게 되면 에러가 발생한다.*

## Static Member Variable

멤버 변수에 `static` 키워드를 붙이면, 해당 변수는 클래스에 속해있는 변수가 된다.

```cpp
class Example {
public:
    static int count;
};

int Example::count = 0;

int main() {
    Example::count = 1;
    std::cout << Example::count << std::endl; // 1
    return 0;
}
```

이는 필수적으로 클래스 외부에서 초기화를 해주어야 한다.

exe파일을 실행함으로써 프로세스에 메모리 구조를 생각하면서 이해하면 힙 아래 데이터 영역에서 생성된 Static 변수를 확인할 수 있다. 실제 객체들은 count라는 변수를 들고 있지 않고 공유하고 있다.

## Static Variable in Function

함수 내부에 `static` 키워드를 붙이면, 해당 변수는 함수 내부에서만 사용되는 변수가 된다.

```cpp
class Example {
public:
    static void print() {
        static int count = 0;
        count++;
        std::cout << count << std::endl;
    }
};

int main() {
    Example::print(); // 1
    Example::print(); // 2
    Example::print(); // 3
    return 0;
}
```

앞서 다룬 static member variable는 main에서도 접근이 가능하기에 오용의 위험이 있지만, static variable in function은 함수 내부에서만 사용되기에 안전하며 공유 가능하다.

코드 상으로는 초기화구문 처럼 보이지만, 실제로 프로세스에는 데이터 영역에 컴파일 시점에 초기화된 변수가 생성된다. 이후 실제 어셈블리 코드를 보면 초기화 구문이 없다.