# MutexSemaphore

뮤축스와 세마포어에 대해 정리합니다.

## 경쟁 상태

- 두 개 이상의 프로세스나 스레드가 공유 자원에 동시에 접근할 때 발생하는 문제
- 의도치 않은 결과를 초래할 수 있음

따라서, 동시에 접근하는 것을 막기 위해 뮤텍스와 세마포어를 사용한다.

### 임계 영역

- 공유 자원에 접근하는 코드 영역
- 임계 영역에는 한 번에 하나의 프로세스나 스레드만 접근할 수 있어야 함
  - 이를 상호 배제(Mutual Exclusion)라고 함

상호배제를 동기하 기법으로 구현한 것이 바로 뮤텍스와 세마포어이다.

## 뮤텍스(Mutex)

- Mutual Exclusion의 줄임말
- 여러개의 프로세스나 스레드가 공유 자원에 접근하는 것을 막기 위한 동기화 기법 (락)

### 락과 언락(Lock and Unlock)

- 락(Lock): 뮤텍스를 사용하여 자원을 잠급니다. 다른 스레드가 이 자원에 접근하려고 할 때, 락이 걸려 있으면 대기 상태에 들어갑니다.
- 언락(Unlock): 자원의 사용을 마친 후 뮤텍스를 해제합니다. 다른 대기 중인 스레드가 자원에 접근할 수 있게 됩니다.

락이 걸려있다면 접근하려는 쓰레드는 대기 큐에 들어가게 되고, 언락이 되면 대기 큐에 있는 쓰레드 중 하나가 락을 획득하게 된다.

대기 방식에는 다음과 같은 방식이 있다.

- Busy Waiting: 락을 획득할 때까지 계속 루프를 돌면서 확인하는 방식
- Non Busy Waiting: 락을 획득하지 못하면 대기 큐에 들어가서 대기하는 방식

### C++에서의 뮤텍스

```cpp
#include <iostream>
#include <thread>
#include <mutex>

std::mutex mtx; // 전역 뮤텍스 객체

void print_thread_id(int id) {
    mtx.lock(); // 뮤텍스 잠금
    // 임계 구역 시작
    std::cout << "Thread " << id << " is running\n";
    // 임계 구역 종료
    mtx.unlock(); // 뮤텍스 해제
}

int main() {
    std::thread threads[10];

    // 10개의 스레드 생성
    for (int i = 0; i < 10; ++i) {
        threads[i] = std::thread(print_thread_id, i + 1);
    }

    // 10개의 스레드 조인
    for (auto& th : threads) {
        th.join();
    }

    return 0;
}
```

## 세마포어(Semaphore)

여러개의 프로세스/스레드가 공유 자원에 동시에 접근하는 것을 제한하기 위한 정수

```
세마포어(Semaphore)는 다중 프로그래밍 환경에서 공유 자원의 접근을 제어하기 위한 동기화 도구입니다. 세마포어는 카운터 값을 사용하여 여러 스레드가 동시에 특정 자원에 접근할 수 있는 최대 수를 조절합니다. 이는 뮤텍스와 유사하지만, 뮤텍스는 한 번에 하나의 스레드만 접근할 수 있도록 하는 반면, 세마포어는 여러 스레드가 동시에 접근할 수 있게 합니다.
```

### 세마포어의 종류

- 이진 세마포어(Binary Semaphore): 0 또는 1의 값을 가지는 세마포어
- 카운팅 세마포어(Counting Semaphore): 0 이상의 값을 가지는 세마포어

### 세마포어의 연산

- P(Operation): 세마포어 값을 감소시킴
- V(Operation): 세마포어 값을 증가시킴
- P, V 연산은 원자적으로 수행되어야 함

### C++에서의 세마포어

```cpp
#include <iostream>
#include <thread>
#include <semaphore.h>
#include <vector>

// 세마포어 객체
std::counting_semaphore<10> sem(3); // 최대 3개의 스레드가 동시에 접근 가능

void worker(int id) {
    std::cout << "Thread " << id << " waiting to enter\n";
    sem.acquire(); // P 연산: 세마포어 값 감소 또는 대기
    std::cout << "Thread " << id << " entered\n";

    // 임계 구역
    std::this_thread::sleep_for(std::chrono::seconds(1)); // 자원 사용 시뮬레이션

    std::cout << "Thread " << id << " exiting\n";
    sem.release(); // V 연산: 세마포어 값 증가
}

int main() {
    std::vector<std::thread> threads;

    // 10개의 스레드 생성
    for (int i = 0; i < 10; ++i) {
        threads.emplace_back(worker, i + 1);
    }

    // 10개의 스레드 조인
    for (auto& th : threads) {
        th.join();
    }

    return 0;
}
```
