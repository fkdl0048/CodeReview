# Vector

[영상 참고](https://www.youtube.com/watch?v=va6eKs7db4s&t=99s)

벡터는 게임 개발에서 중요한 개념으로 사용된다.

- 크기와 방향으로 정의되는 값
- 방향/거리/속도, 위치를 나타내기 위한 수학적 도구

*스칼라는 크기*

## 표현

- 시점, 종점으로 표현
- 크기가 존재, 방향이 존재

## 성질

- 크기와 방향이 동일하면 동일한 벡터로 취급
- 임의의 벡터 공간에서, 한 벡터와 동일한 벡터는 무수히 많다.

## 상대좌표, 절대좌표

- 상대좌표: 원점을 기준으로 한 좌표
- 절대좌표: 절대적인 좌표
  - 벡터 공간내에서 모든 벡터의 시점을 일치시킨다면, 임의의 벡터는 존재하지 않는다. (종점과 일대일 대응된다. 위치벡터)

## 벡터의 덧셈

각 피연산자들의 성분끼리 더한다.

$$ \vec{a} + \vec{b} = \vec{c} $$
$$ \vec{a}(1,4) + \vec{b}(3,2) = \vec{c}(4,6) $$

![image](https://github.com/fkdl0048/CodeReview/assets/84510455/57b9cce0-694d-49e5-89e8-e4be88239e14)

## 벡터의 뺄셈

각 피연산자들의 성분끼리 뺀다.

$$ \vec{a} - \vec{b} = \vec{c} $$
$$ \vec{a}(1,4) - \vec{b}(3,2) = \vec{c}(-2,2) $$

벡터의 뺄셈은 벡터 더하기 마이너스 벡터로 표현할 수 있다.

## 벡터의 스칼라 곱

*벡터의 실수배*

각 성분에 스칼라를 곱한다.

$$ k\vec{a} = \vec{c} $$
$$ 3\vec{a}(1,4) = \vec{c}(3,12) $$

## 방향벡터

벡터를 통해 방향과 거리를 포현하는 경우 상당히 비직관저임

따라서 방향*거리로 쪼개어 표현하는 것이 직관적임

방향을 표현하는 벡터를 방향벡터라고 한다. (크기가 1인 벡터)

방향 x 크기 = 벡터

방향벡터를 구하는 방법

- 벡터를 크기로 나눈다.
- 단위벡터를 구한다.

![image](https://github.com/fkdl0048/CodeReview/assets/84510455/c125d95e-61dd-43ee-a10c-66b73343ad89)

크기를 구하는 방법

- 벡터의 각 성분을 제곱하여 더한 후 제곱근을 취한다.

![image](https://github.com/fkdl0048/CodeReview/assets/84510455/d9a168e9-7e2a-45aa-b68d-dfd18a0105f0)

(3,-4, 0)

$$ \sqrt{3^2 + (-4)^2 + 0^2} = 5 $$
$$ \frac{1}{5}(3,-4,0) = (0.6, -0.8, 0) $$

## 벡터의 내적

두 벡터가 이루는 각을 구하려면 내적을 사용한다.

벡터의 내적은 한 벡터를 다른 벡터에 투영시켜 그 크기를 곱하는 연산

$$ \vec{a} \cdot \vec{b} = |\vec{a}||\vec{b}|cos\theta $$
$$ \vec{a} \cdot \vec{b} = a_xb_x + a_yb_y + a_zb_z $$
$$ \frac{\vec{a} \cdot \vec{b}}{|\vec{a}||\vec{b}|} = cos\theta $$

- 성질
  - 두 벡터가 서로 수직이면 내적의 결과는 0이다.
  - 내적의 결과는 스칼라이다.
  - 교환/분배가 성립한다.

![image](https://github.com/fkdl0048/CodeReview/assets/84510455/84627079-3c70-48cf-a6e6-5b4cb48a56b6)

cosangle구하는 법

내적은 벡터의 곱셉 하지만 값은 스칼라

내적의 값이 0이라면 두 벡터는 수직이다.

내적의 값이 양수라면 0도와 90도 사이에 있다. (1)
즉, 두 벡터가 같은 방향을 가지고 있다.

내적의 값이 음수라면 90도와 180도 사이에 있다. (-1)
즉, 두 벡터가 반대 방향을 가지고 있다.

![image](https://github.com/fkdl0048/CodeReview/assets/84510455/8c6078ca-b5a4-404b-9e7c-9d758215ac84)

총알의 진행 방향과, 현재 뱡향의 내적이 0 초과라면 데미지를 입는다. 즉 같은 방향을 가진다면 데미지를 입는다.

![image](https://github.com/fkdl0048/CodeReview/assets/84510455/9d7c04a2-add0-43f4-913d-59e687a57cda)

![image](https://github.com/fkdl0048/CodeReview/assets/84510455/6a98199e-3b99-443e-97dc-57c34cb3555d)

에너지 총량의 법칙 때문에 빛이 닿는 면적의 밝기는 코사인 세타이다.

45도는 약 0.7정도로 나옴(코사인 45도)

## 벡터의 외적

- 벡터의 외적은 두 벡터를 모두 수직으로 통과하는 벡터를 구하는 연산
- 벡터곱 또는 교차연산으로 부르기도 한다.

$$ \vec{a} \times \vec{b} = |\vec{a}||\vec{b}|sin\theta $$
$$ \vec{a} \times \vec{b} = (a_yb_z - a_zb_y, a_zb_x - a_xb_z, a_xb_y - a_yb_x) $$

- 성질
  - 두 벡터를 외적한 벡터의 크기는 두 벡터가 이루는 평행사변형의 넓이와 같다.
  - 외적의 결과는 벡터이다.

![image](https://github.com/fkdl0048/CodeReview/assets/84510455/7ffece0a-e493-42c6-b492-806e49d803bf)

frontDirection은 현재 면의 앞을 가리키는 벡터이다.

sinagngle은 두 벡터가 이루는 각도이다.

![image](https://github.com/fkdl0048/CodeReview/assets/84510455/e1a99eef-9fa2-49c0-8ffe-032f409fe256)

![image](https://github.com/fkdl0048/CodeReview/assets/84510455/3fd58e41-75c8-43f4-a92b-e4adb4fd818b)

![image](https://github.com/fkdl0048/CodeReview/assets/84510455/62ce505a-247d-4df2-829b-0980b04f8a47)
