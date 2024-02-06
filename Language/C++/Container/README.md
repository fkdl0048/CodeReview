# C++ 컨테이너 정리

## 자료구조: 주요 컨테이너(map, unordered_map, list, vector, array) 동작원리

이 컨테이너도 `C#`과의 차이점을 비교하여 이해하고자 한다.

`C++` 특성상 STL라이브러리에서 다양한 컨테이너를 제공한다.

c++문법도 같이 학습해서 내용이 조금 깊은 부분도 있지만, 아마도 각 컨테이너의 장단점, 비교부분이 제일 중요할 것이다.

### 기본 배열

*실제 컨테이너 구현은 이 기본 배열로 이뤄져 있기 때문에 이해하고 넘어간다.*

실제 [공식문서](https://learn.microsoft.com/ko-kr/cpp/cpp/arrays-cpp?view=msvc-170)를 참고하는 게 좋다.

기존 C스타일 대신 C++의 스타일을 사용하는 것이 좋다고 한다.

값을 지정하지 않으면 기본 0이 할당된다.

#### 스택선언

스택 선언은 다음과 같다.

```c++
constexpr size_t size = 1000;

    // Declare an array of doubles to be allocated on the stack
    double numbers[size] {0};

    // Assign a new value to the first element
    numbers[0] = 1;

    // Assign a value to each subsequent element
    // (numbers[1] is the second element in the array.)
    for (size_t i = 1; i < size; i++)
    {
        numbers[i] = numbers[i-1] * 1.1;
    }

    // Access each element
    for (size_t i = 0; i < size; i++)
    {
        std::cout << numbers[i] << " ";
    }
  ```

스택 기반 배열은 힙 기반 배열보다 더 빠르게 할당하고 액세스할 수 있습니다

#### 힙선언

힙선언은 다음과 같다.

```c++
void do_something(size_t size)
{
    // Declare an array of doubles to be allocated on the heap
    double* numbers = new double[size]{ 0 };

    // Assign a new value to the first element
    numbers[0] = 1;

    // Assign a value to each subsequent element
    // (numbers[1] is the second element in the array.)
    for (size_t i = 1; i < size; i++)
    {
        numbers[i] = numbers[i - 1] * 1.1;
    }

    // Access each element with subscript operator
    for (size_t i = 0; i < size; i++)
    {
        std::cout << numbers[i] << " ";
    }

    // Access each element with pointer arithmetic
    // Use a copy of the pointer for iterating
    double* p = numbers;

    for (size_t i = 0; i < size; i++)
    {
        // Dereference the pointer, then increment it
        std::cout << *p++ << " ";
    }

    // Alternate method:
    // Reset p to numbers[0]:
    p = numbers;

    // Use address of pointer to compute bounds.
    // The compiler computes size as the number
    // of elements * (bytes per element).
    while (p < (numbers + size))
    {
        // Dereference the pointer, then increment it
        std::cout << *p++ << " ";
    }

    delete[] numbers; // don't forget to do this!

}
int main()
{
    do_something(108);
}
```

스택에 할당하기에는 너무 크거나 컴파일 시간에 크기를 알 수 없는 배열이 필요할 수 있습니다.

연산자는 첫 번째 요소에 대한 포인터를 반환합니다. (포인터로 배열접근, *p++)

제대로 사용하려면 다음을 보장해야 한다. (사용자가)

- 항상 원래 포인터 주소의 복사본을 유지하므로 배열이 더 이상 필요하지 않을 때 메모리를 삭제할 수 있습니다.
- 배열 범위를 지나 포인터 주소를 증가하거나 감소하지 않습니다.

코드를 보면 배열로 순회하는 법, 포인터로 사이즈만큼 반복하여 순회하는 법, 포인터로 실제 메모리 크기만큼 반복하는 법을 알 수 있다.

규칙에 나와 있듯이 3가지 방법다 배열의 범위를 지날 수 없는 기저사례들로 반복하고, 원본 포인터를 유지하여 마지막에 delete[]로 메모리를 해제해야 한다.

#### 배열 초기화

다음 두 내용은 동일하다.

```c++
    int a[10];
    for (int i = 0; i < 10; ++i)
    {
        a[i] = i + 1;
    }

    int b[10]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
```

#### 함수에 배열 전달

배열이 함수에 전달되면 스택 기반 배열이든 힙 기반 배열이든 관계없이 **첫 번째 요소에 대한 포인터**로 전달됩니다.

배열을 함수에 전달할 때는 항상 별도의 매개 변수의 요소 수를 지정해야 합니다. 또한 이 동작은 배열이 함수에 전달될 때 배열 요소가 복사되지 않음을 의미합니다. 함수가 요소를 수정하지 못하도록 하려면 매개 변수를 요소에 대한 포인터 const 로 지정합니다.

즉, 길이를 인자로 같이 전달하는데 오류를 막기 위해 불변으로 지정하는 것이다.

```c++
void process(double *p, const size_t len)
{
    std::cout << "process:\n";
    for (size_t i = 0; i < len; ++i)
    {
        // do something with p[i]
    }
}
```

포인터로 전달하기 때문에 복사본이 아닌 원본을 전달한다.

```c++
// Unsized array
void process(const double p[], const size_t len);

// Fixed-size array. Length must still be specified explicitly.
void process(const double p[1000], const size_t len);
```

같음.

#### 배열 초기화

클래스 생성자가 있는 개체의 배열은 생성자에 의해 초기화됩니다

*C#과 똑같이 동작, 인자가 있는 생성자 구현 시 기본 생성자를 구현해야 함.*

```c++
// initializing_arrays1.cpp
class Point
{
public:
   Point()   // Default constructor.
   {
   }
   Point( int, int )   // Construct from two ints
   {
   }
};

// An array of Point objects can be declared as follows:
Point aPoint[3] = {
   Point( 3, 3 )     // Use int, int constructor.
};

int main()
{
}
```

여기서 aPoint객체 배열의 첫 번째 인자는 (int, int)생성자를 통해 초기화되고, 나머지는 기본 생성자를 통해 초기화된다.

#### 배열 요소 접근

```c++
// using_arrays.cpp
int main() {
   char chArray[10];
   char *pch = chArray;   // Evaluates to a pointer to the first element.
   char   ch = chArray[0];   // Evaluates to the value of the first element.
   ch = chArray[3];   // Evaluates to the value of the fourth element.
}
```

C언어와 동일하게 생각하면 된다.

### array

- [공식 문서](https://learn.microsoft.com/ko-kr/cpp/standard-library/array-class-stl?view=msvc-170)

길이가 N인 Ty 형식의 요소 시퀀스를 제어하는 개체를 설명합니다. 시퀀스는 array<Ty, N> 개체에 포함된 Ty의 배열로 저장됩니다.

- 배열은 기본적으로 연속된 메모리 공간에 할당된 데이터를 가리킨다.
- 일정한 메모리 크기 단위로 할당하기 때문에 동일한 자료형만 담을 수 있다.
- 배열은 연속된 메모리 공간에서 첫 주소를 담는 포인터와 같다.
- C언어 배열(int arr[SIZE])은 배열의 이름(여기서는 arr)이 배열의 주소를 가리킨다. 다만, 표준 라이브러리에서 제공하는
- std::array는 구조체이기 때문에 배열의 이름이 포인터로 자동으로 변환되지는 않는다.
- 메모리를 할당할 때 크기가 고정되므로 원소를 더 추가하거나 삭제할 수 없다.
- 인덱스를 통해 원소에 임의 접근(Random access)이 가능하다(EX: int[3] = 5;).

```c++
template <class Ty, std::size_t N>
class array;
```

형식에 기본 생성자 array()와 기본 대입 연산자 operator=가 있고 aggregate에 대한 요구 사항을 충족합니다

- [aggregate: 집성체에 대한 설명](https://jacking75.github.io/cpp_aggregat/)

```c++
array<int, 4> ai = { 1, 2, 3 };
```

초기화 과정, 4번째 요소는 0으로 초기화된다.

- 요구사항
  - 헤더: `<array>`
  - 네임스페이스: `std`

`C#`의 Array와 동일하다.

#### array::array

```c++
array();

array(const array& right);
```

기본 생성자와 복사 생성자를 제공한다.

```c++
#include <array>
#include <iostream>

typedef std::array<int, 4> Myarray;
int main()
{
    Myarray c0 = { 0, 1, 2, 3 };

    // display contents " 0 1 2 3"
    for (const auto& it : c0)
    {
        std::cout << " " << it;
    }
    std::cout << std::endl;

    Myarray c1(c0);

    // display contents " 0 1 2 3"
    for (const auto& it : c1)
    {
        std::cout << " " << it;
    }
    std::cout << std::endl;

    return (0);
}
```

- [auto 설명](#auto)
- [const auto& 설명](#const-auto)
- [typedef 설명](#typedef)

#### array::at

```c++
#include <array>
#include <iostream>

typedef std::array<int, 4> Myarray;
int main()
{
    Myarray c0 = { 0, 1, 2, 3 };

    // display contents " 0 1 2 3"
    for (const auto& it : c0)
    {
        std::cout << " " << it;
    }
    std::cout << std::endl;

    // display odd elements " 1 3"
    std::cout << " " << c0.at(1);
    std::cout << " " << c0.at(3);
    std::cout << std::endl;

    return (0);
}
```

멤버 함수는 위치 off에서 제어되는 시퀀스의 요소에 대한 참조를 반환합니다. 해당 위치가 잘못된 경우 함수는 out_of_range 클래스의 개체를 throw합니다.

#### array::back

```c++
reference back();

constexpr const_reference back() const;
```

마지막 요소에 액세스합니다.

- [constexpr 설명](#constexpr)

```c++
#include <array>
#include <iostream>

typedef std::array<int, 4> Myarray;
int main()
{
    Myarray c0 = { 0, 1, 2, 3 };

    // display contents " 0 1 2 3"
    for (const auto& it : c0)
    {
        std::cout << " " << it;
    }
    std::cout << std::endl;

    // display last element " 3"
    std::cout << " " << c0.back();
    std::cout << std::endl;

    return (0);
}
```

#### array::begin

```c++
iterator begin() noexcept;
const_iterator begin() const noexcept;
```

- [iterator 설명](#iterator)

```c++
#include <array>
#include <iostream>

typedef std::array<int, 4> Myarray;
int main()
{
    Myarray c0 = { 0, 1, 2, 3 };

    // display contents " 0 1 2 3"
    for (const auto& it : c0)
    {
        std::cout << " " << it;
    }
    std::cout << std::endl;

    // display first element " 0"
    Myarray::iterator it2 = c0.begin();
    std::cout << " " << *it2;
    std::cout << std::endl;

    return (0);
}
```

#### array::empty

```c++
constexpr bool empty() const;
```

멤버 함수는 N == 0인 경우에만 true를 반환합니다.

```c++
#include <array>
#include <iostream>

typedef std::array<int, 4> Myarray;
int main()
{
    Myarray c0 = { 0, 1, 2, 3 };

    // display contents " 0 1 2 3"
    for (const auto& it : c0)
    {
        std::cout << " " << it;
    }
    std::cout << std::endl;

    // display whether c0 is empty " false"
    std::cout << std::boolalpha << " " << c0.empty();
    std::cout << std::endl;

    std::array<int, 0> c1;

    // display whether c1 is empty " true"
    std::cout << std::boolalpha << " " << c1.empty();
    std::cout << std::endl;

    return (0);
}

// 0 1 2 3
// false
// true
```

#### array::iterator

```c++
#include <array>
#include <iostream>

typedef std::array<int, 4> MyArray;

int main()
{
    MyArray c0 = { 0, 1, 2, 3 };

    // display contents " 0 1 2 3"
    std::cout << "it1:";
    for (MyArray::iterator it1 = c0.begin();
        it1 != c0.end();
        ++it1) {
        std::cout << " " << *it1;
    }
    std::cout << std::endl;

    // display first element " 0"
    MyArray::iterator it2 = c0.begin();
    std::cout << "it2:";
    std::cout << " " << *it2;
    std::cout << std::endl;

    return (0);
}
```

이터레이터 반환

#### array::max_size

- size와 동일하다.

```c++
#include <array>
#include <iostream>

typedef std::array<int, 4> Myarray;
int main()
{
    Myarray c0 = { 0, 1, 2, 3 };

    // display contents " 0 1 2 3"
    for (const auto& it : c0)
    {
        std::cout << " " << it;
    }
    std::cout << std::endl;

    // display (maximum) size " 4"
    std::cout << " " << c0.max_size();
    std::cout << std::endl;

    return (0);
}
```

멤버 함수는 N를 반환합니다.

#### array::operator[]

```c++
reference operator[](size_type off);

constexpr const_reference operator[](size_type off) const;
```

```c++
#include <array>
#include <iostream>

typedef std::array<int, 4> Myarray;
int main()
{
    Myarray c0 = { 0, 1, 2, 3 };

    // display contents " 0 1 2 3"
    for (const auto& it : c0)
    {
        std::cout << " " << it;
    }
    std::cout << std::endl;

    // display odd elements " 1 3"
    std::cout << " " << c0[1];
    std::cout << " " << c0[3];
    std::cout << std::endl;

    return (0);
}
```

멤버 함수는 위치 off에서 제어되는 시퀀스의 요소에 대한 참조를 반환합니다. 해당 위치가 유효하지 않을 경우 동작이 정의되지 않습니다.

또한 멤버 get 가 아닌 함수를 사용하여 요소에 대한 참조를 가져올 수 있습니다 array.

#### array::pointer

```c++
#include <array>
#include <iostream>

typedef std::array<int, 4> Myarray;
int main()
{
    Myarray c0 = { 0, 1, 2, 3 };

    // display contents " 0 1 2 3"
    for (const auto& it : c0)
    {
        std::cout << " " << it;
    }
    std::cout << std::endl;

    // display first element " 0"
    Myarray::pointer ptr = &*c0.begin();
    std::cout << " " << *ptr;
    std::cout << std::endl;

    return (0);
}
```

`Myarray::pointer ptr = &*c0.begin();`

해당 코드는 `c0.begin()`의 반환값을 `*`로 역참조하여 `&`로 주소를 가져온다. (없어도 동일하게 동작)

### vector

- [공식문서](https://learn.microsoft.com/ko-kr/cpp/standard-library/vector-class?view=msvc-170)

`C#`의 `List`와 동일하다. (동적 배열)

```c++
template <class Type, class Allocator = allocator<Type>>
class vector
```

- `type`은 저장할 데이터 타입이다.
- `Allocator`는 메모리 할당자이다.

C++ 표준 라이브러리 벡터 클래스는 시퀀스 컨테이너에 대한 클래스 템플릿입니다. 벡터는 지정된 형식의 요소를 선형 배열에 저장하고 모든 요소에 대한 빠른 임의 액세스를 허용합니다.

- 언어 및 라이브러리
  - std::vector: C++의 표준 라이브러리(STL)에 속하는 컨테이너 클래스입니다.
  - List<T>: C#의 표준 라이브러리인 .NET Framework 또는 .NET Core에 속하는 제네릭 컬렉션 클래스입니다.
- 메모리 관리
  - std::vector: 요소를 포인터 배열로 저장하며, 동적으로 크기가 조정될 때 메모리를 다시 할당합니다.
  - List<T>: 요소를 노드로 연결된 방식으로 저장하며, 동적으로 크기가 조정될 때 요소를 복사하여 새로운 배열을 할당합니다.
- 메모리 구조
  - std::vector: 요소는 연속적인 메모리 블록에 저장됩니다.
  - List<T>: 요소는 각각의 노드에 저장되며, 노드들은 메모리에서 서로 불연속적으로 할당됩니다.
- 성능
  - std::vector: 요소의 추가나 삭제가 배열의 끝에서 빠르게 수행됩니다. 요소의 삽입 및 삭제가 중간에서 발생할 경우 다수의 요소를 이동해야 할 수 있습니다.
  - List<T>: 요소의 삽입 및 삭제가 배열의 중간에서 빠르게 수행됩니다. 그러나 인덱스로 직접 접근할 때에는 선형적인 시간이 소요됩니다.
- 공통점
  - 동적 크기 조정: 두 클래스 모두 요소의 동적 크기 조정을 지원하여 요소의 추가 및 삭제가 자유롭게 가능합니다.
  - 접근자 및 반복자: 두 클래스 모두 요소에 접근할 수 있는 방법을 제공하며, 반복자를 통해 요소를 순회할 수 있습니다.
  - 제네릭: 두 클래스 모두 제네릭(Generic)으로 구현되어 다양한 데이터 유형을 저장할 수 있습니다.
  - 원소 삽입 및 삭제: 배열의 중간에서의 원소 삽입 및 삭제가 가능합니다. 하지만 std::vector는 배열의 끝에서의 삽입 및 삭제가 더 효율적입니다.
  - 메모리 할당 및 해제: 요소가 추가되거나 삭제될 때 내부적으로 메모리 할당 및 해제 작업이 수행됩니다

벡터를 사용하면 시퀀스 끝에서 상수 시간 삽입 및 삭제할 수 있습니다. 벡터 중간에 요소를 삽입하거나 삭제하려면 선형 시간이 필요합니다.  (배열기반이기 때문)

자세한 내용은 동적배열에 관한 문서 참고

벡터는 재할당 과정에서 상당한 자원이 소모된다. 따라서 reserve 메서드를 통해 원하는 크기만큼 새로운 공간을 재할당할 수도 있다.

#### push_back

```c++
#include <vector>
#include <iostream>

int main()
{
    std::vector<int> v1;

    v1.push_back(10);
    v1.push_back(20);

    std::cout << "The first element is " << v1[0] << std::endl;
    std::cout << "The second element is " << v1[1] << std::endl;
}
```

벡터의 끝에 요소를 추가합니다.

#### pop_back

```c++
#include <vector>
#include <iostream>

int main()
{
    std::vector<int> v1;

    v1.push_back(10);
    v1.push_back(20);

    std::cout << "The first element is " << v1[0] << std::endl;
    std::cout << "The second element is " << v1[1] << std::endl;

    v1.pop_back();

    std::cout << "The first element is " << v1[0] << std::endl;
}
```

벡터의 끝에서 요소를 제거합니다.

#### insert

```c++
#include <vector>
#include <iostream>

int main()
{
    std::vector<int> v1;

    v1.push_back(10);
    v1.push_back(20);

    std::cout << "The first element is " << v1[0] << std::endl;
    std::cout << "The second element is " << v1[1] << std::endl;

    v1.insert(v1.begin() + 1, 15);

    std::cout << "The first element is " << v1[0] << std::endl;
    std::cout << "The second element is " << v1[1] << std::endl;
    std::cout << "The third element is " << v1[2] << std::endl;
}
```

벡터의 지정된 위치에 요소를 삽입합니다.

#### at

벡터의 지정된 위치에 있는 요소에 대한 참조를 반환합니다.

```c++
// vector_at.cpp
// compile with: /EHsc
#include <vector>
#include <iostream>

int main( )
{
   using namespace std;
   vector <int> v1;

   v1.push_back( 10 );
   v1.push_back( 20 );

   const int &i = v1.at( 0 );
   int &j = v1.at( 1 );
   cout << "The first element is " << i << endl;
   cout << "The second element is " << j << endl;
}
```

주소값을 반환하기 때문에 const가 아니라면 값을 변경할 수 있다.(원본의 값을 변경)

#### back

벡터의 마지막 요소에 대한 참조를 반환합니다.

```c++
#include <vector>
#include <iostream>

int main() {
   using namespace std;
   vector <int> v1;

   v1.push_back( 10 );
   v1.push_back( 11 );

   int& i = v1.back( );
   const int& ii = v1.front( );

   cout << "The last integer of v1 is " << i << endl;
   i--;
   cout << "The next-to-last integer of v1 is "<< ii << endl;
}
```

#### begin

벡터의 첫 번째 요소에 대한 임의 액세스 반복기를 반환합니다.

```c++
const_iterator begin() const;

iterator begin();
```

```c++
#include <vector>
#include <iostream>

int main()
{
    using namespace std;
    vector<int> c1;
    vector<int>::iterator c1_Iter;
    vector<int>::const_iterator c1_cIter;

    c1.push_back(1);
    c1.push_back(2);

    cout << "The vector c1 contains elements:";
    c1_Iter = c1.begin();
    for (; c1_Iter != c1.end(); c1_Iter++)
    {
        cout << " " << *c1_Iter;
    }
    cout << endl;

    cout << "The vector c1 now contains elements:";
    c1_Iter = c1.begin();
    *c1_Iter = 20;
    for (; c1_Iter != c1.end(); c1_Iter++)
    {
        cout << " " << *c1_Iter;
    }
    cout << endl;

    // The following line would be an error because iterator is const
    // *c1_cIter = 200;
}
```

#### capacity

가용 공간의 길이를 반환합니다.

```c++
#include <vector>
#include <iostream>

int main( )
{
   using namespace std;
   vector <int> v1;

   v1.push_back( 1 );
   cout << "The length of storage allocated is "
        << v1.capacity( ) << "." << endl;

   v1.push_back( 2 );
   cout << "The length of storage allocated is now "
        << v1.capacity( ) << "." << endl;
}
```

2배씩 늘어난다. (동적 배열의 기본)

- 1 2 4 8 16 ...

#### clear

배열을 비운다.

```c++
// vector_clear.cpp
// compile with: /EHsc
#include <vector>
#include <iostream>

int main( )
{
   using namespace std;
   vector <int> v1;

   v1.push_back( 10 );
   v1.push_back( 20 );
   v1.push_back( 30 );

   cout << "The size of v1 is " << v1.size( ) << endl;
   v1.clear( );
   cout << "The size of v1 after clearing is " << v1.size( ) << endl;
}
```

### list

- [공식문서](https://learn.microsoft.com/ko-kr/cpp/standard-library/list-class?view=msvc-170)

C++ 표준 라이브러리 목록 클래스는 해당 요소를 선형 배열로 기본 시퀀스 내의 모든 위치에서 효율적인 삽입 및 삭제를 허용하는 시퀀스 컨테이너의 클래스 템플릿입니다. 시퀀스는 각각 일부 형식 Type의 멤버를 포함하는 양방향 연결된 요소 목록으로 저장됩니다.

`C#`의 `LinkedList`와 동일하다.

차이점

- 언어 및 라이브러리
  - std::list: C++의 표준 라이브러리(STL)에 포함된 컨테이너 클래스입니다.
  - LinkedList<T>: C#의 표준 라이브러리인 .NET Framework 또는 .NET Core에 속하는 제네릭 컬렉션 클래스입니다.
- 구현 방식
  - std::list: C++의 std::list는 포인터로 연결된 노드로 구성되어 있습니다. 각 노드는 다음 노드와 이전 노드에 대한 포인터를 가지고 있습니다.
  - LinkedList<T>: C#의 LinkedList<T>도 포인터로 연결된 노드로 구성되어 있습니다. 각 노드는 다음 노드와 이전 노드에 대한 참조를 가지고 있습니다.
- 메모리 할당 및 해제
  - std::list: std::list의 요소는 C++의 메모리 관리 기법에 따라 동적으로 할당되고 해제됩니다.
  - LinkedList<T>: LinkedList<T>의 요소는 .NET Framework 또는 .NET Core의 가비지 컬렉션에 의해 자동으로 관리되며, 메모리 할당 및 해제는 CLR(Common Language Runtime)에서 처리됩니다.

공통점

- 동적 크기 조정
  - 둘 다 동적으로 크기가 조정되는 이중 연결 리스트를 구현하고 있으므로 요소의 삽입 및 삭제가 자유롭게 가능합니다.
- 요소에 대한 순차 접근
  - 둘 다 반복자(iterator)를 통해 리스트의 요소에 접근하고 순회할 수 있습니다.
- 삽입 및 삭제 연산의 효율성
  - 삽입 및 삭제 연산은 상수 시간(O(1))에 수행됩니다. 이는 리스트의 헤드 또는 테일에 요소를 추가하는 경우에도 마찬가지입니다.
- 선형 시간 요소 검색
  - 요소를 검색하는 데에는 선형 시간(O(n))이 소요됩니다. 이는 요소의 위치를 찾기 위해 순차적으로 리스트를 탐색해야 하기 때문입니다.
- 이전 및 다음 요소에 대한 참조 유지
  - 각 요소는 다음 요소와 이전 요소에 대한 참조를 유지하고 있어 이중 연결 리스트의 특성을 유지합니다.

연결리스트에 기반을 두므로 데이터가 메모리 상에 저장되는 위치가 불연속적이다.

데이터 검색은 vector가 유리하고, 데이터 삽입/삭제는 list가 유리하다. 정리하면, 데이터 검색은 배열 기반의 vector가 유리하고, 데이터 삽입/삭제는 연결리스트 기반의 list가 유리하다.

임의접근이 가능하지 않기 때문에 검색이 느리다. (순차적으로 탐색해야 하기 때문)

std::list는 상수 시간에 원소를 삽입하고 삭제할 수 있는 컨테이너이다.

일반적으로 자료구조에서 말하는 이중 연결리스트 혹은 양방향 연결리스트(doubly-linked list)로 구성된다. 연결리스트는 기본적으로 노드 기반으로 데이터를 저장하는 자료구조를 말하는데, 각 노드는 데이터 필드와 다른 노드를 가리키는 링크 필드로 나뉜다.

```c++
// list_assign.cpp
// compile with: /EHsc
#include <list>
#include <iostream>

int main()
{
    using namespace std;
    list<int> c1, c2;
    list<int>::const_iterator cIter;

    c1.push_back(10);
    c1.push_back(20);
    c1.push_back(30);
    c2.push_back(40);
    c2.push_back(50);
    c2.push_back(60);

    cout << "c1 =";
    for (auto c : c1)
        cout << " " << c;
    cout << endl;

    c1.assign(++c2.begin(), c2.end());
    cout << "c1 =";
    for (auto c : c1)
        cout << " " << c;
    cout << endl;

    c1.assign(7, 4);
    cout << "c1 =";
    for (auto c : c1)
        cout << " " << c;
    cout << endl;

    c1.assign({ 10, 20, 30, 40 });
    cout << "c1 =";
    for (auto c : c1)
        cout << " " << c;
    cout << endl;
}
```

- assign: 목록에서 요소를 삭제하고 대상 목록에서 요소의 새 집합을 복사합니다.

나머지 함수들은 동일해서 생략

새로운 데이터를 삽입하는 함수에는 push_front(), push_back(), insert()가 있다.

push_front: 시작 지점에 원소를 삽입
push_back: 마지막에 원소를 삽입
insert: 현재 반복자가 가리키고 있는 위치에 원소를 삽입
상수 시간에 데이터를 삽입하고 삭제할 수 있다는 특징 덕분에 세 함수 모두 O(1)의 시간 복잡도로 수행된다.

삭제의 경우 pop_front(), pop_back(), erase, remove, remove_if 등이 있다.

pop_front()는 첫 번째 원소를, pop_back()은 마지막 원소를 삭제한다.
erase는 인자로 넘긴 반복자가 가리키는 노드를 제거한다.
remove와 remove_if는 주어진 조건을 만족하는 원소를 모두 삭제한다. 연결리스트는 현재 가리키는 원소만을 삭제할 수 있으므로, 두 함수는 처음부터 끝까지 주어진 std::list를 순회하며 조건에 맞는 원소를 지워나간다.

앞의 세 개는 시간 복잡도가 O(1)이지만 remove, remove_if는 모든 노드를 순회해야 하므로 시간 복잡도가 O(n)이다.

장단점

요소의 삽입, 삭제는 상수 시간 O(1)이 걸린다.

std::list는 연결리스트에 기반을 두는데, 덕분에 어디에서든 데이터를 삽입하고 삭제할 때 속도가 빠르다. 삽입할 때, 삭제할 때 모두 두 노드를 연결하는 링크만을 변경해 주면 되기 때문이다.

다만, 실제로 원하는 요소를 찾아 지우려면 탐색이 느리기 때문에 O(n)의 시간이 걸린다.

반면 벡터와 달리 임의로 접근하는 것이 불가능하므로, 원하는 노드를 탐색할 때 최대 N번의 탐색이 필요하다.

그리고 선형탐색할 때에도 캐시 효과로 인해 불연속적인 메모리의 선형탐색이 연속적인 메모리에서보다 느리기 때문에, 탐색도 느린 편이다. 그 때문에 벡터를 쓰는 게 더 빠른 경우가 많다.

시간 복잡도를 정리하면 다음과 같다.

요소의 삽입/삭제: 어디든지 상수 시간 O(1)
요소의 탐색: O(N)

### map

- [공식 문서](https://learn.microsoft.com/ko-kr/cpp/standard-library/map-class?view=msvc-170)

std::map (C++)
C++의 표준 라이브러리(STL)에 포함되어 있습니다.
키가 정렬된 순서로 유지됩니다.
이진 검색 트리(Binary Search Tree, BST)를 사용하여 구현되어 있어 검색 및 삽입 연산이 빠릅니다.
키에 대한 범위 검색이 가능합니다.
std::map<Key, T> 형태로 사용됩니다.

맵(std::map)은 키(Key)와 값(value)의 쌍들을 저장하는 이진탐색트리 기반의 컨테이너이다.

이때, 키는 중복될 수 없다.

맵의 각 원소는 std::pair<key,value>로 저장된다.

pair는 순서쌍을 가리키는데 여기서는 pair.first에 key가, pair.second에 value가 저장된다.

C++에서 맵은 Key를 기준으로 자동 정렬되는데, 내부적으로는 레드블랙트리로 구현된다. (균형트리)

std::map은 균형 이진 탐색 트리의 일종인 레드블랙트리를 사용하여 삽입, 삭제, 탐색이 빠르다(O(logN)).

다만 자동으로 정렬되는 것이 필요하지 않을 때는 굳이 map을 사용할 이유가 없다. map은 레드블랙트리 기반이기 때문에 삽입할 때마다 트리의 균형을 맞춰야 한다. 게다가 map은 해시맵, 해시테이블과 달리 상수 시간 O(1)에 데이터를 처리하지 못한다.

만약 해시맵 기반의 자료구조가 필요하면 unordered_map을 사용해야 한다.

삽입, 삭제, 탐색: 시간 복잡도 O(logN)

```c++
template <class Key,
    class Type,
    class Traits = less<Key>,
    class Allocator=allocator<pair <const Key, Type>>>
class map;
```

C++ 표준 라이브러리 map 클래스의 특징은 다음과 같습니다.

연결된 키 값을 기반으로 요소 값을 효율적으로 검색하는 다양한 크기의 컨테이너

이는 해당 요소에 액세스할 수 있는 양방향 반복기를 제공하기 때문에 되돌릴 수 있습니다.

요소가 지정된 비교 함수에 따른 키 값으로 정렬되므로 정렬되어 있습니다.

고유합니다. 각 요소에 고유 키가 있어야 하기 때문입니다.

요소의 데이터 값은 키 값과 구별되기 때문에 쌍 연관 컨테이너입니다.

클래스 템플릿은 제공하는 기능이 제네릭이며 요소 또는 키 형식과 독립적이기 때문입니다. 요소와 키에 사용되는 데이터 형식은 비교 함수 및 할당자와 함께 클래스 템플릿에서 매개 변수로 지정됩니다.

```c++
#include <map>
#include <iostream>

typedef std::map<char, int> Mymap;
int main()
    {
    Mymap c1;

    c1.insert(Mymap::value_type('a', 1));
    c1.insert(Mymap::value_type('b', 2));
    c1.insert(Mymap::value_type('c', 3));

// find and show elements
    std::cout << "c1.at('a') == " << c1.at('a') << std::endl;
    std::cout << "c1.at('b') == " << c1.at('b') << std::endl;
    std::cout << "c1.at('c') == " << c1.at('c') << std::endl;

    return (0);
    }
```

```c++
#include <map>
#include <iostream>

int main( )
{
   using namespace std;
   map <int, int> m1;

   map <int, int> :: iterator m1_Iter;
   map <int, int> :: const_iterator m1_cIter;
   typedef pair <int, int> Int_Pair;

   m1.insert ( Int_Pair ( 0, 0 ) );
   m1.insert ( Int_Pair ( 1, 1 ) );
   m1.insert ( Int_Pair ( 2, 4 ) );

   m1_cIter = m1.begin ( );
   cout << "The first element of m1 is " << m1_cIter -> first << endl;

   m1_Iter = m1.begin ( );
   m1.erase ( m1_Iter );

   // The following 2 lines would err because the iterator is const
   // m1_cIter = m1.begin ( );
   // m1.erase ( m1_cIter );

   m1_cIter = m1.begin( );
   cout << "The first element of m1 is now " << m1_cIter -> first << endl;
}
```

### unordered_map

std::unordered_map은 기존 std::map의 문제를 해결하기 위해 나온 컨테이너로 C++ 11부터 적용된다.

기존 std::map은 O(logN)의 시간 복잡도를 갖으므로 요소의 삽입/삭제가 빈번하면 성능 저하가 있다.

반면 std::unordered_map은 이름처럼 정렬을 수행하지 않기 때문에 시간 복잡도가 O(1)이다.

```c++
#include <iostream>
#include <string>
#include <unordered_map>

int main()
{
    std::unordered_map<std::string, int> unordered;

    unordered["Alice"] = 50;
    unordered["Bob"] = 60;
    unordered["Sam"] = 70;

    for (auto it = unordered.begin(); it != unordered.end(); it++)
        std::cout << it->first << ": " << it->second << std::endl;

    return 0;
}
```

이렇게 입력하면, 결과가 정렬되지 않고 랜덤하게 나타난다.

std::unordered_map은 map과 마찬가지로 키와 값의 쌍(pair)을 저장하며 키도 중복 불가이다. 하지만 Hash Map 기반으로 만들어진 컨테이너이기 때문에 정렬을 수행하지 않는다. 이 컨테이너는 삽입, 삭제에 걸리는 시간 복잡도가 O(1)이다.

해시 맵 사용

std::unordered_map에서 사용되는 자료구조는 해시맵 또는 해시테이블로, 여러 개의 버킷을 두고 해시 함수(Hash function)를 통해 색인(index)한다.

해시 함수는 키 값을 정수로 변환하는 역할을 한다. 동일한 키 값이 주어지면 동일한 정수로 변환해 준다. 하지만 정수 값을 통해 키 값을 구하는 것은 불가능하다.

해시 함수를 이용하면 함수 한 번으로 데이터가 들어갈 인덱스를 구할 수 있어 탐색이 훨씬 빠르다.

### 정리

각각의 자료구조인 `std::list`, `std::vector`, `std::array`에는 각각의 장단점이 있습니다. 다음은 각각의 자료구조의 장단점을 설명한 것입니다:

#### `std::list`

**장점**:

1. 요소의 삽입 및 삭제가 효율적입니다.
2. 삽입 및 삭제 연산이 상수 시간(O(1))에 수행됩니다.
3. 메모리의 조각화가 발생하지 않습니다.
4. 중간에 요소를 삽입하거나 삭제하는 경우에 유용합니다.

**단점**:

1. 인덱스로 직접 요소에 접근하는 것이 불가능합니다.
2. 요소의 검색에 선형 시간(O(n))이 소요됩니다.
3. 요소에 연속적으로 접근하는 경우에는 효율적이지 않습니다.

#### `std::vector`

**장점**:

1. 요소에 대한 랜덤 접근이 가능합니다.
2. 요소의 삽입 및 삭제가 배열의 끝에서 상수 시간(O(1))에 수행됩니다.
3. 메모리 효율성이 좋습니다.

**단점**:

1. 요소의 삽입 및 삭제가 배열의 중간에서 발생하는 경우에는 선형 시간(O(n))이 소요됩니다.
2. 메모리 재할당이 발생할 때마다 요소들을 복사해야 합니다.
3. 메모리를 연속적으로 할당하기 때문에 중간에 요소를 삽입하거나 삭제하는 경우에 비효율적입니다.

#### `std::array`

**장점**:

1. 요소에 대한 랜덤 접근이 가능합니다.
2. 크기가 고정되어 있으므로 메모리를 효율적으로 사용할 수 있습니다.
3. 요소의 삽입 및 삭제가 발생하지 않기 때문에 삽입 및 삭제 연산의 오버헤드가 없습니다.

**단점**:

1. 크기가 고정되어 있기 때문에 동적으로 크기를 조정할 수 없습니다.
2. 배열의 크기가 컴파일 시간에 정의되어야 합니다.
3. 요소의 삽입 및 삭제가 발생하지 않기 때문에 삽입 및 삭제 연산이 필요한 경우에는 적합하지 않습니다.

따라서 각각의 자료구조는 자신만의 특징과 장단점을 가지고 있으며, 사용하고자 하는 상황과 요구 사항에 따라 적절한 자료구조를 선택해야 합니다.

`std::map`과 `std::unordered_map`은 둘 다 키-값 쌍을 저장하는 연관 컨테이너로서, 각각의 장단점이 있습니다. 다음은 각각의 자료구조의 특징과 장단점을 설명한 것입니다:

#### `std::map`

**장점**:

1. 키가 정렬되어 저장됩니다.
2. 이진 검색 트리(Binary Search Tree, BST)를 사용하여 구현되어 있어 효율적인 검색이 가능합니다.
3. 키에 대한 범위 검색이 가능합니다.

**단점**:

1. 삽입 및 삭제 연산이 느립니다. BST의 균형을 유지하기 위해 재배열 과정이 필요하기 때문입니다.
2. 해시 함수를 사용하지 않기 때문에 `std::unordered_map`에 비해 검색이 느릴 수 있습니다.

#### `std::unordered_map`

**장점**:

1. 해시 테이블을 사용하여 구현되어 있어 검색, 삽입 및 삭제 연산이 상수 시간(O(1))에 수행됩니다.
2. 키의 순서가 유지되지 않습니다.
3. 해시 충돌을 최소화하기 위한 좋은 해시 함수를 사용한다면 매우 효율적입니다.

**단점**:

1. 해시 충돌이 발생할 경우 성능이 저하될 수 있습니다.
2. 해시 함수의 선택이 중요하며, 나쁜 해시 함수를 사용할 경우 성능이 저하될 수 있습니다.
3. 키에 대한 범위 검색이 불가능합니다.

따라서 `std::map`은 정렬된 키가 필요하거나 범위 검색이 필요한 경우에 유용하며, `std::unordered_map`은 검색 및 삽입 연산이 빈번하게 발생하고 정렬된 키가 필요하지 않은 경우에 유용합니다. 선택하는 것은 사용하고자 하는 요구 사항에 따라 다르며, 각각의 자료구조는 자신만의 장단점을 가지고 있습니다.
