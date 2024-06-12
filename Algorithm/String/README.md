# string

문자열 관련 알고리즘

## 문자열 뒤집기

문자열을 뒤집는 방법은 여러가지가 있다. 여러 방법을 알아보자 (이런 문제는 대부분 개념적인 해결방법을 알아보기 위함으로, 스택, 포인터 등등의 방법을 알아보자)

### 양 끝에서 시작하여 서로 교환하기 (투 포인터)

```cpp
void reverseString(std::string& str) {
    int left = 0;
    int right = str.length() - 1;
    while (left < right) {
        std::swap(str[left], str[right]);
        left++;
        right--;
    }
}

int main() {
    std::string str = "Hello, World!";
    reverseString(str);
    std::cout << "Reversed string: " << str << std::endl;
    return 0;
}
```

### 스택 사용

```cpp
#include <iostream>
#include <string>
#include <stack>

std::string reverseString(const std::string& str) {
    std::stack<char> stack;
    for (char c : str) {
        stack.push(c);
    }
    std::string reversed;
    while (!stack.empty()) {
        reversed += stack.top();
        stack.pop();
    }
    return reversed;
}

int main() {
    std::string str = "Hello, World!";
    std::string reversed = reverseString(str);
    std::cout << "Reversed string: " << reversed << std::endl;
    return 0;
}
```

### 재귀함수 사용

```cpp
#include <iostream>
#include <string>

std::string reverseString(const std::string& str) {
    if (str.empty()) {
        return str;
    }
    return reverseString(str.substr(1)) + str[0];
}

int main() {
    std::string str = "Hello, World!";
    std::string reversed = reverseString(str);
    std::cout << "Reversed string: " << reversed << std::endl;
    return 0;
}
```

### STL 사용

```cpp
#include <iostream>
#include <string>
#include <algorithm>

int main() {
    std::string str = "Hello, World!";
    std::reverse(str.begin(), str.end());
    std::cout << "Reversed string: " << str << std::endl;
    return 0;
}
```

## KMP (string matching)

문자열매칭 알고리즘은 대부분 O(n)의 시간복잡도를 가질 수 있다. (Brute Force를 제외하고)

KMP 알고리즘은 문자열 매칭 알고리즘 중 가장 유명한 알고리즘이다. O(n)의 시간복잡도를 가진다. KMP 알고리즘은 문자열을 탐색하면서, 일치하지 않는 문자열이 나오면, 이전까지 일치한 문자열을 통해 다음 탐색 위치를 찾는다.

비슷한 알고리즘으로 Rabin-Karp 알고리즘이 있다. Rabin-Karp 알고리즘은 해시값을 이용하여 문자열을 탐색한다.

