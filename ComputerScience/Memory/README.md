# Memory

*메모리 관련*

- [실제 어셈블리 동작 과정](https://github.com/fkdl0048/CodeReview/issues/58)

![image](https://github.com/fkdl0048/CodeReview/assets/84510455/c716c73a-f9c2-431b-8638-ac6db1da3296)

프로그램이 실행되려면 먼저 프로그램을 메모리에 로드해야 한다.

프로그램 코드가 담고 있는 명령어와 프로그램 내에서 사용될 변수 등을 위해서 메모리가 필요하기 때문이다. 이때, 프로그램이 메모리에 탑재되어 실제로 실행되고 있는 것을 가리켜 프로세스라 부른다.

힙 영역은 동적할당되는 영역, 스택은 지역 변수등의 영역

스택은 함수를 호출할 때 할당되고 함수가 반환될 때 소멸된다.

메모리 스택은 그림 영역과 같이 아래로 할당되고 힙은 위로 할당된다.

지정된 메모리 할당을 넘어 영역을 넘어가게 되면 오버플로우가 발생한다.

`malloc(), calloc(), realloc()`으로 할당

`free()로 해제`

```c++
#include <stdio.h>

int a = 10; // 데이터 영역

int main() {
    int b = 20; // 스택 영역
    int *c = (int *)malloc(sizeof(int)); // 힙 영역
    *c = 30;
    printf("%d %d %d\n", a, b, *c);
    free(c);
    return 0;
}
```

위 전체 코드가 코드 영역에 해당

스택은 힙보다 빠르다. (힙은 메모리를 할당하고 해제하는 과정이 필요하기 때문)

힙은 메모리를 할당하고 해제하는 과정이 필요하기 때문에 느리다.

delete로 해제

## 개인적인 생각

오버플로우가 나는 상황은 대부분 재귀를 잘 못 돌리거나 `C#`이 아닌 `C/C++`에서 메모리 관리를 하지 못하여 일어난다. (전자는 스택, 후자는 힙)

메모리영역이 다른 언어와 `C/C++`이 가지는 유일한 차이점이자 장점 그리고 단점이다.

개발속도에 영향을 주기 때문에 단점이 될 수 있지만, 게임의 최적화/속도에 가장 직접적인 영향을 주기 때문에 이를 관리한다면 장점이 될 수 있다.

이러한 차이점을 이해하고 언어의 선택을 하는 것이 중요하다.
