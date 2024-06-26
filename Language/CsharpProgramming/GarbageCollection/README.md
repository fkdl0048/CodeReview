# Garbage Collection

메모리 C언어를 이용하면 Free사용하여 메모리를 해제해야 한다. 하지만 C#은 Garbage Collection을 사용하여 메모리를 자동으로 해제한다. (Java, Unreal C++도 마찬가지)

가비지 콜렉터는 주기적으로 실행되며, 사용하지 않는 메모리를 해제한다. (사용하지 않는 메모리를 찾아내는 방법은 다양하다.)

- [Garbage Collection 위키](https://namu.wiki/w/%EC%93%B0%EB%A0%88%EA%B8%B0%20%EC%88%98%EC%A7%91)

## Mark and Sweep

가장 기본적인 가비지 콜렉션 알고리즘으로, 두 단계로 이루어진다.

1. Mark: 모든 객체를 순회하며, 사용되는 객체를 표시한다.
2. Sweep: 표시되지 않은 객체를 해제한다.

간단하지만, 레퍼런스 카운트 방식이라면 순환 참조 문제가 발생할 수 있다. 이 문제는 GC가 없는 C++에서는 피할 수 없다.