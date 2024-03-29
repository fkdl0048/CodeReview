# 게임 디자인 워크숍

현재 진행중인 UNSEEN이라는 프로그램에서 언리얼을 이용한 프로젝트를 기획해야 하는데, 혼자 러프하게 작성한 기획서를 이득우 교수님이 진행하는 게임 디자인 워크숍을 통해 보완한다.

2주동안 금요일에 약 6시간씩 진행되었으며, 개인사정으로 1번 밖에 참여하지 못했다.

내가 듣고 정리한 내용을 토대로 [1차 기획서](https://github.com/futurelabunseen/B-JeonganLee/blob/main/Document/ProjectPlan/1stPlan.md)를 2차 기획서(세부적인)로 올리면서 같이 회고를 진행하려고 한다.
목차는 다음과 같다.

- 게임의 디커플링
- 코어 미캐닉 다이어 그램
- GAS 설계
- 내 게임에 적용하기

*모든 내용은 기억을 토대로 작성하며, 결코 정답이 아님을 밝힌다.*

## 게임의 디커플링

워크숍 도중에 가장 인상깊었던, 부분은 게임 디자인도 하나의 디커플링이라는 점이였다. 사실 코드적인 설계과정에서만 디커플링을 생각했었는데, 게임 디자인도 설계과정이며 매우 많은 요소들을 생각하고 구체화 하는 과정이기에 추상화가 필요하고 추상화를 통해 설계적인 부분과 다른 부분들을 단절시켜야 한다는 것을 알게 되었다.

아키텍처에서도 언급되는 말이며, 프로그래머의 뇌와 같이 생각의 수를 낮춘다던가, Task를 더 세분화한다던가 하는 것과 같은 맥락으로 이해된다. 결국은 크게 다를 것 없이 설계과정에서 레이턴시를 줄이기 위해서 디커플링을 생각해야 한다는 것이다.

따라서 뒤에서 기획하는 내용인 게임 미캐닉과 실제 도메인 설계 및 GAS 시스템 설계에서는 이러한 디커플링을 핵심적으로 생각하며 기획을 진행하려고 한다.

내가 알고 있던 개념들과 교수님의 생각을 비교해보니.. 확실히 생각의 깊이가 다르다는 것도 느끼고, 프로그래밍도 결국은 하나의 사고방식이기에 설계라는 관점에서의 복잡성을 줄이는 행위에 대해서 다시한번 생각할 수 있었다. (청킹)

최근 [게임 아키텍처에 대한 생각을 정리한 글](https://fkdl0048.github.io/game/gameArchitecture/)이 있는데, 아마 이 워크숍의 영향도 많이 받은 것 같다.

## 코어 미캐닉 다이어 그램

![image](https://github.com/fkdl0048/ToDo/assets/84510455/25c89425-25cf-4740-9c15-f56440f42781)

게임 설계나 게임의 분해로 많이 사용할 수 있을 것 같은 내용으로, 결국 게임은 미캐닉을 통해 구성되어 있으며, 게임의 복잡함을 빼고 좀 더 단순하게 봐야할 필요성을 느낀 내용이다.

코어 미캐닉은 해당 게임 플레이에서 가장 핵심이 되는 요소로 대부분 행동을 지정하며, 슈터 게임에서는 총을 쏘는 것이 코어 미캐닉이 될 수 있다.

2차 미캐닉은 코어 미캐닉 다음으로 가장 많이 사용되는 행동으로 코어 미캐닉과 조합하여 게임 플레이의 큰 틀을 구성한다고 한다. 실제 게임에서는 더 많은 조합이 나올 수 있지만 현재 게임에서 핵심이 되는 행동의 조합으로 본다.

2차 미캐닉과 코어 미캐닉의 조합은 실제 게임들에서 뜯어본다면 몇 가지 사례로 볼 수 있다. 만약 게임이 한 가지 코어 미캐닉만 가진다면 그것은 숙련이 매우 힘들어지고 게임의 재미도가 떨어질 수 있다. (대부분 조합되어 있음)

게임플레이 진행은 플레이어와 NPC 및 배경 물체오 상호작용을 통한 게임 플레이 진행이다. 스테이지 전개나 레벨 디자인을 의미하며 게임의 흐름을 결정한다.

게임의 서사는 게임 시스템을 사용해 게임 콘텐츠를 완성시켜주는 최상단 기제로, 게임 스토리나 세계관을 의미한다.

대부분의 게임이 이런 형태로 분해가 된다고 했지만, 게임이라는 매우 넓은 범주에서는 힘들 수 있다고 생각하기도 한다. 교수님도 이런 부분을 강조하시기도 하셨으며 이렇게 해보는 이유에 대해서 집중하는 것이 좋다고 하셨다. 또한 사람마다 같은 게임이라도 다른 미캐닉이나 시스템이 나올 것 같다는 생각도 들었다.

*이렇게 게임을 추상적으로 바라보는 훈련.*

따라서 이런 방법으로 게임을 디자인하기보다 중요한 특징을 정리하는 용도로 적합하다. 나의 게임의 핵심적인 부분을 다시 바라볼 수 있다는 점

## GAS 설계

- [GAS 설명](https://github.com/futurelabunseen/B-JeonganLee/blob/main/Document/StudyNote/UnrealFunction/GameplayAbilitySystem.md)

Game Ability System이라고 하며 쉽게 말해 언리얼에서 제공해주는 게임제작 아키텍처이다. FSM이나 BT의 한계를 극복하기 위해 만들어진 시스템이다. 각 애트리뷰트, 이펙트, 어빌리티가 순환구조를 가지며 이를 게임플레이 태그를 통해 쉽게 참조하여 구성한다.

더 자세한 내용은 영상이나 [잘 정리된 문서](https://github.com/tranek/GASDocumentation)를 참고하면 된다.

## 내 게임에 적용하기

### 게임의 디커플링 적용

게임의 디커플링을 적용하기 위해서는 게임의 핵심적인 부분을 뽑아내야 한다. 현재 내가 기획한 게임은 슈터 게임이며, 총을 쏘는 것이 코어 미캐닉이 될 것 같다. 따라서 총을 쏘는 것을 기반으로 게임을 디자인해야 한다.

좋은 점은 설계의 복잡성이 생각보다 적다는 것이다. 아마 레퍼런스가 확실한 게임이라 그런 것 같다.

### 코어 미캐닉 다이어그램 적용

현재 1차 기획서를 토대로 코어 미캐닉 다이어그램을 생각해본다면 다음과 같다.

- 코어 미캐닉: 총을 쏘는 것
- 2차 미캐닉: 스킬을 사용하는 것
- 게임플레이 진행: 스테이지 전개
- 게임의 서사: x (에셋에 맞춰서)

이렇게 생각해볼 수 있을 것 같다.

### GAS 설계 적용

게임 어빌리티 시스템은 현재 내가 기획한 게임에는 적합하다고 판단된다.

### 결과물

- [2차 기획서](https://github.com/futurelabunseen/B-JeonganLee/blob/main/Document/ProjectPlan/2ndPlan.md)
