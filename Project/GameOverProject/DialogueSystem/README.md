# Task: Unity Dialogue System

- https://github.com/GG-Studio-990001/GameOver/issues/125

작업을 상세히 기록하여 정리 및 블로그 글 형태로 다듬을 것

요구사항을 정리하여 기록

프로젝트에 전반적으로 사용될 대화 시스템을 설계

## 설계

설계를 하기 위해서 가장 먼저 글로 다 적어보려고 한다.

가장 먼저 요구사항을 분석한다.

### 요구사항

- Dialouge는 각 챕터마다 다른 방식으로 요구될 수 있다.
- 상호작용, 타임라인(컷신) 등의 형태에서 언제든지 호출하여 사용할 수 있어야한다.
- 대화는 해당 상태, 대상에 따라 대사가 매번 달라져야 한다.
- 대화문은 쉽게 편집가능해야 하고 쉽게 읽을 수 있어야 함 (기획입장에서)
- 프레임에 들어갈 정보는 이름, 대화문, 토글, +a가 있어야 함 (수정 가능해야함)

### 구조

현재 구조에서 좀 더 유연한 구조를 위해 yarnSpinner를 사용할 것이다.

따라서 DialogueSystem은 크게 3가지로 구성된다.

- 해당 정보 즉, 대화 진행에 필요한 정보(NPC 정보, 이름, 상태 등등)를 가지고 있는 DialogueData
- 해당 정보를 가지고 yarnSpinner를 통해 대화를 진행하는 DialogueRunner
- 대화 창을 관리하는 DialogueUI

## yarnSpinner

구조 설명

시작하기 위해선 "GameObject > YarnSpinner > Dialogue Runner"를 추가해야 한다.

- [구문 설명](https://docs.yarnspinner.dev/beginners-guide/syntax-basics) 
  - 기획용
- [테스트 용](https://try.yarnspinner.dev/)
  - 직접 넣어서 테스트 가능
- [사용 가이드](https://docs.yarnspinner.dev/beginners-guide/making-a-game/yarn-spinner-for-unity)

### DialogueRunner

대화를 진행하는 클래스로 가장 부모 격이다.

앞서 말한 생성된 프리팹에 부착되어 있다.

가장 최종의 관리자로 DialogueView를 통해 대화를 진행한다. (모든 대화는 이 클래스를 토대로 진행된다.)

### DialogueView

DialogueRunner를 통해 대화를 진행하는 클래스로 대화를 진행하는 클래스를 상속받아 구현한다.

