# 운동학

물체의 위치, 속도, 가속도: 물체의 움직임을 기술하고 예측하는 방법을 말한다.

## 등속운동

물체의 속도가 일정한 상태로 움직이는 것을 말한다.

기본 수식: `v = v0 + at`

```cpp
#include <iostream>

struct Vector2 {
    float x;
    float y;

    Vector2 operator+(const Vector2& other) const {
        return {x + other.x, y + other.y};
    }

    Vector2 operator*(float scalar) const {
        return {x * scalar, y * scalar};
    }
};

class GameObject {
public:
    Vector2 position;
    Vector2 velocity;

    void update(float deltaTime) {
        // 등속 운동: 속도에 시간(deltaTime)을 곱하여 위치를 업데이트
        position = position + (velocity * deltaTime);
    }

    void printPosition() const {
        std::cout << "Position: (" << position.x << ", " << position.y << ")\n";
    }
};

int main() {
    GameObject object;
    object.position = {0.0f, 0.0f};
    object.velocity = {1.0f, 0.5f}; // x 방향으로 1.0 유닛/초, y 방향으로 0.5 유닛/초

    const float deltaTime = 1.0f; // 1초마다 업데이트

    for (int i = 0; i < 10; ++i) {
        object.update(deltaTime);
        object.printPosition();
    }

    return 0;
}
```