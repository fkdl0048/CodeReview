# Smart Pointer

이전의 버전인 C++에선 동적 메모리를 해제하기 위해서 new/delete를 사용해 포인터 변수를 정의하여 사용하여야 했다. 이러한 포인터를 **원시 포인터**라고 한다.

C++에서 메모리는 프로그래머가 책임지고 delete하지 않는 이상 계속 남아있게 되어 메모리 누수가 발생할 수 있다. 이러한 문제를 해결하기 위해 스마트 포인터가 등장하게 되었다. (또는 적절한 타이밍에 delete해주지 못해서 [댕글링 포인터](../DanglingPointer/README.md)가 발생할 수 있다.)

C++11에 추가된 기능으로 스마트 포인터를 사용하면 객체가 더는 필요하지 않을 때 객체에 할당된 메모리가 자동으로 해제된다. (메모리 누수의 가능성이 없음)

레퍼런스 카운트를 통하여 제거 시점을 결정한다. (C#은 GC가 처리한다.)

## 원시 포인터와 차이점

- 스마트 포인터는 힙 영역의 메모리만 관리한다. (해당 메모리 주소만 저장할 수 있다.)
- 원시포인터의 증감 연산은 불가능하다.

**원천적으로 memory leak을 방지할 수 있다.**

이는 스택 영역에서 생성한 포인터 변수가 있고 해당 포인터를 delete하게 되면 스택 영역의 포인터 변수만 삭제되고 (해당 주소를 담고 있는 변수만) 실제 메모리에 있는 데이터는 삭제되지 않는다. 이것이 memory leak이다.

하지만, 스마트포인터는 해당 주소를 가지고 있는 변수가 삭제되면 해당 주소에 있는 데이터도 삭제된다. (자동으로)

### unique_ptr<T>

유니크 포인터는 한 객체에 대해 하나의 포인터만 가질 수 있다. (복사 불가능) 이를 **exclusive ownership**이라고 한다.

만약 unique_ptr을 복사하려고 하면 컴파일 에러가 발생한다. (컴파일 에러라는 것이 중요함)

```cpp
#include <memory>

int main()
{
    std::unique_ptr<int> p1(new int(10));
    std::unique_ptr<int> p2 = p1; // error
}
```

소유권을 옮기기 위해선 `std::move`를 사용한다.

```cpp
#include <memory>

int main()
{
    std::unique_ptr<int> p1(new int(10));
    std::unique_ptr<int> p2 = std::move(p1);
}
```

이렇게 되면 전 p1은 무효화된다.

즉, 객체에 대한 단일 소유권을 보장하기 위해선 `unique_ptr`를 사용한다.

`reset`을 통해 포인터를 초기화할 수 있다.

```cpp
#include <memory>

int main()
{
    std::unique_ptr<int> p1(new int(10));
    p1.reset();
}
```

- 원본은 소멸되고, ptr은 해제된다.

`release`를 통해 소유권을 반환할 수 있다.

```cpp
#include <memory>

int main()
{
    std::unique_ptr<int> p1(new int(10));
    int* p = p1.release();
}
```

### shared_ptr<T>

unique_ptr과 달리 여러 개의 포인터가 같은 객체를 가리킬 수 있다. 이를 shared ownership이라고 한다. unique_ptr은 레퍼런스 카운팅을 사용하여 관리한다. 이를 통해 레퍼런스 카운트가 0이 되면 자동으로 메모리를 해제한다.

- 자동으로 해제하는 기능은 RAII를 준수한다.

```cpp
#include <bits/stdc++.h>

using namespace std;

class cat {
public:
    cat() {
        cout << "cat created" << endl;
    }

    ~cat() {
        cout << "cat destroyed" << endl;
    }
};


int main() {
    shared_ptr<cat> c1 = make_shared<cat>();
    cout << "c1 use count: " << c1.use_count() << endl;
    shared_ptr<cat> c2 = c1;
    cout << "c1 use count: " << c1.use_count() << endl;

    return 0;
}
```

but 순환첨조 문제가 발생할 수 있다. 이 문제는 GC가 없는 C++에서는 피할 수 없다.

### weak_ptr<T>

`shared_ptr`과 함께 사용되며, `shared_ptr`의 순환 참조를 방지하기 위해 사용된다. `shared_ptr`의 레퍼런스 카운트를 증가시키지 않는다. 객체에 소멸에 영향을 주지 않는다.

```cpp
#include <memory>

int main()
{
    std::shared_ptr<int> p1(new int(10));
    std::weak_ptr<int> p2 = p1;
}
```

## 알아야 하는 정보

RAII(Resource Acquisition Is Initialization)를 지원한다. 이는 객체가 생성될 때 자원을 할당하고 소멸될 때 자원을 해제하는 것을 의미한다.

```
RAII 디자인 패턴은 자원의 생애주기를 객체의 생애에 결합시킨다는 것이다. 좀 더 풀어서 말하자면, 자원을 사용하고자 하는 상황에서, 생성자에서 자원 획득을, 소멸자에서 자원 해제를 하는 자원 관리용 클래스를 만드는 프로그래밍 패턴이다. 즉 객체의 시작과 끝은 컴퓨터가 알아서 해주는 이점을 활용하여, 자원 관리용 클래스를 만들어 생성자에서 자원 획득을, 소멸자에서 자원 해제를 하게한다면 자원 관리의 문제를 해결할 수 있게 된다.

이렇게 되면 자원의 생애 주기는 객체의 생애와 결합된다. 객체의 생애는 런타임이 알아서 잘 관리해준다고 하였다. 즉 그렇기에 앞으로는 객체와 바인딩된 자원은 런타임이 자동으로 관리할 수 있다
```

여기서 Resource는 메모리, 파일 핸들, 네트워크 소켓 등을 의미한다. *한정된 자원* Initlaization은 실제 코드의 변수를 초기화하는 것을 의미한다.

즉, 위에서 말한 것처럼 스마트 포인터는 객체가 생성될 때 메모리를 할당하고 소멸될 때 메모리를 해제한다.

이는 다르게 스코프 라이프 사이클이라고 한다. (스코프를 벗어나면 자동으로 메모리가 해제된다.) 즉, 지역 변수의 라이프 사이클과 같다. 좀 더 다르게 말하면 해당 스택에 들어 있는 변수가 pop이 되면서 메모리가 해제된다.

C++은 벡터에 담긴 포인터도 같은 방식으로 해제된다. (벡터가 소멸될 때 벡터에 담긴 포인터도 해제된다.)

## 참고

- [C++ Smart Pointer](https://www.youtube.com/watch?v=SQYPN8FVCAI)

## 연관 문서

- [Unreal Smart Pointer](../../../GameEngine/Unreal/UnrealSmartPointer/README.md)
- [Garbage Collection](../../CsharpProgramming/GarbageCollection/README.md)