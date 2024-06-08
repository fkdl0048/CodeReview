# C++ 포인터와 레퍼런스

C++에서는 포인터와 레퍼런스를 사용할 수 있다. 포인터와 레퍼런스는 메모리 주소를 저장하는 변수이다. 포인터는 메모리 주소를 직접 저장하고, 레퍼런스는 메모리 주소를 간접적으로 저장한다.

![image](https://github.com/fkdl0048/CodeReview/assets/84510455/ab84af96-cdcf-4fff-8a71-208ab2de65f0)

## 포인터

주소값을 저장할수 있는 타입의 변수로 실체가 없이 NULL이 저장 가능하다.

과거 42서울에서 사용한 C와 C++의 차이점에서 중요했던 포인트는 동적할당이 다르다는 점이다. C에서는 malloc, free를 사용했지만 C++에서는 new, delete를 사용한다.

- Malloc: 단순한 메모리 할당, 할당 시 메모리의 사이즈를 입력해서 할당받음 `malloc(sizeof(int) * 10)`
- New: 할당과 동시에 초기화 가능, 객체를 생성할 때 사용, 객체 생성 시 생성자를 호출함 `new *cpp_style = new int[10]`
  - 오버로딩 가능

## 레퍼런스

실체가 있어야 하며 선언 즉시 할당되어야한다. 즉 NULL, nullptr로 할당 불가능하다.

```cpp
int a = 10;
int &b = a;
```

## 추가적인 정리 (Lvalue, Rvalue)

C++에서 값을 넘겨주기 위한 방법으로 value, pointer, reference가 있다.

### Value

값을 복사해서 넘겨주는 방법이다. 함수 내에서 값을 변경해도 원본 값은 변하지 않는다.

```cpp
void foo(int a) {
    a = 10;
}

int main() {
    int a = 5;
    foo(a);
    cout << a << endl; // 5
}
```

이는 [스택프레임](../MemoryStructure/README.md/#스택프레임)에 새로운 메모리 공간이 할당되어 값을 복사하기 때문이다.

따라서 크기가 큰 배열이나 객체를 매개변수로 잡겨 되면 스택영역에 그대로 복사하기 때문에 비효율적이다.

### Pointer

포인터는 메모리 주소를 넘겨주는 방법이다. 함수 내에서 값을 변경하면 원본 값도 변경된다.

```cpp
void foo(int *a) {
    *a = 10;
}

int main() {
    int a = 5;
    foo(&a);
    cout << a << endl; // 10
}
```

마찬가지로 스택에서 동작하지만, 포인터는 해당 주소값을 넘겨주기 때문에 원본 값이 변경된다.

### Reference

사실상 포인터와 같은 역할을 한다. 포인터와 다른 점은 선언 시 주소값을 넘겨주어야 한다. 실제 어셈블리어로 봐도 같은 코드로 동작한다.

따라서 이는 포인터와 같은 역할을 하지만 제한을 두기에 가능하다면 안전한 레퍼런스를 사용하는 것이 좋다. 포인터는 주소값이 그대로 드러나기 때문에..

추가적으로 const를 붙여서 더욱 더 안전하게 사용할 수 있다.

```cpp
void foo(const int &a) {
    int b = a;
    b = 10; // error
}

int main() {
    int a = 5;
    foo(a);
    cout << a << endl; // 5
}
```

#### Lvalue, Rvalue

추가적으로 Reference를 사용할 때 Lvalue, Rvalue를 알아야 한다.

가장 간단하게 `int a = 10;`에서 `a`는 Lvalue이고 `10`은 Rvalue이다. Lvalue는 주소값을 가지고 있는 변수이고 Rvalue는 주소값을 가지고 있지 않는 변수이다. 추가적으로 `int b = a`에서 `a`는 Lvalue이고 `b`는 Lvalue이다.

정리하자면, 한번 부르고 다시한번 부를 수 있는 것이 Lvalue이고 한번 부르고 다시 부를 수 없는 것이 Rvalue이다.

##### std::move

Lvalue를 Rvalue로 변환하는 방법이다. Move를 통해 복사가 아닌 이동을 할 수 있다. string의 예로 데이터를 넣을 때, Lvlaue를 넣으면 복사가 일어나 비효율적이기 때문에 Move를 사용한다.

- Lvalue를 Rvalue로 바꿈으로서 Resource Ownership을 이전할 수 있다.

```cpp
#include <iostream>
#include <vector>

class MyClass {
public:
    std::vector<int> data;
    
    MyClass(std::vector<int> d) : data(std::move(d)) {
        // R-value 참조를 사용하여 벡터의 데이터를 이동
    }
};

int main() {
    std::vector<int> vec = {1, 2, 3, 4, 5};
    MyClass obj(std::move(vec)); // vec의 내용을 이동
    std::cout << "vec size: " << vec.size() << std::endl; // vec는 비어 있음
}
```

##### 사용해야 하는 이유

쉽게 복사가 아닌 이동이 필요한 이유를 되게 작은 예제로 다뤄보면 다음과 같다.

```cpp
class Cat{
  public:
    void setName(std::string name){
      name_ = std::move(name);
    }
  private:
    std::string name_;
};

int main(){
  Cat cat;
  std::string name = "Kitty";
  cat.setName(name); // 1번의 복사가 일어남
  cat.setName("Kitty"); // 0번의 복사가 일어남
}
```

- 가장 먼저 Lvalue에 담아서 이름을 설정하는 경우엔 move가 없다면 2번의 복사가 일어난다.
  - 매개 변수로 전달하는 과정에서 1번 복사
  - setName 함수 내에서 name_에 복사하는 과정에서 1번 복사
  - 총 2번 복사
- 하지만, value로 받아서 내부에서 Move를 통해 이동하는 경우에는 1번의 복사만 일어난다.
  - 매개변수로 전달하는 과정에서 1번 복사 name에서 name으로
- 그냥 리터럴, Rvalue로 전달하는 과정은 copy elision이 일어나기 때문에 복사가 일어나지 않는다.
  - 컴파일러가 최적화를 통해 복사를 생략하는 것

이런 최적화가 필요한 이유는 작게보면 크게 성능에는 영향이 없지만, 실제 동작하는 소프트웨어에서는 이런 최적화가 매우 중요하다.

##### RVO (Return Value Optimization)

위에서 다룬 내용도 사실이지만, 사실 대부분은 RVO를 통해 최적화가 이루어진다. RVO는 함수의 반환값을 최적화하는 것이다.

```cpp
#include <iostream>
#include <vector>

std::vector<int> foo() {
    std::vector<int> vec = {1, 2, 3, 4, 5};
    return vec;
}

int main() {
    std::vector<int> vec = foo();
    std::cout << "vec size: " << vec.size() << std::endl;
}
```

위의 코드에서 `foo` 함수에서 `vec`를 반환하면서 복사가 일어나지만, RVO를 통해 최적화가 이루어진다. 따라서 복사가 일어나지 않는다. move또한 이뤄지지 않는다.

즉, 0copy, 0move가 이루어진다.

이러한 개념을 잘 활용하고 이해하고 있어야 한다. 자신이 사용하는 함수가 pass by value인지, return by value인지 잘 확인하고 최적화를 잘 활용하자.

## 참고

- [Lvalue, Rvalue](https://www.youtube.com/watch?v=6buEm6R980o&list=PLDV-cCQnUlIa5K5UYxaXsH78Ao0pltrlq&index=4)
- [Copy Elision](https://en.cppreference.com/w/cpp/language/copy_elision)