# FSM

가장 잘 정리된 [유튜브 영상](https://www.youtube.com/watch?v=lPAhyrudbVU) 참고

유한 상태 기계로 게임 알고리즘 중 가장 유명하다.

- 개발이 쉽고, 빠르게 구현할 수 있다.
- 구성요소
  - State: 상태
  - Transition: 상태 전이
  - Action: 상태 전이 시 수행할 동작

## 구현

다양한 방법으로 가능 (if-else, enum & switch-case, **State 패턴**)

대부분 State 패턴을 사용한다.

```c++
class FSMState
{
  virtual void onEnter();
  virtual void onUpdate();
  virtual void onExit();
  list<FSMTransition> transitions;
}
```

인터페이스 (추상클래스)로 각 State패턴에 맞게 동작할 행위, 전이 조건이 담긴 list를 가진다.

```c++
class FSMTransition
{
  virtual bool isVaild();
  virtual FSMState* getNextState();
  virtual void onTransition();
}
```

전환 가능한지 여부, 다음 상태, 전이 시 수행할 동작을 가진다.

```c++
class FiniteStateMachine
{
  void update();
  list<FSMState> states;
  FSMState* initialState;
  FSMState* activeState;
}
```

Update를 통해 상태를 업데이트하고, 상태들을 가진다.

- Update에서 현재 상태의 전이 조건에서 isVaild()인지 체크한다.(전이가 불가능하다면 현재 상태 Update)
- Vaild하다면 현재 상태의 onExit()를 호출하고, Transition.getNexState()를 호출하여 다음 상태로 전이한다.
- 현재 상태의 onEnter()를 호출한다.
- 이후 다시 Update를 호출한다. (반복)

## 단점

- 복잡한 상태를 표현하기 어렵다. (2~5개는 적당하지만, 30개라면..?)
  - 전이의 단계가 기하급수로 증가할 가능성이 있음
- 상태의 추가뿐만 아니라 error의 가능성이 높아짐
- 이전 상태로 돌아가는 것이 어려움 (추가 구현이 필요)
  - 해결방법으로 Hierarchy State Machine이 있다. (state machine을 계층적으로 나눔)
  - 쉽게 state machine자체를 래퍼로 감싸서 한번더 추상화하는 방법
  - H라는 History State를 사용하는 방법 (stack을 사용)

## 정리

많은 장점과 단점이 같이 존재

규모가 크지 않다면 사용하는걸 권장하지만 상태가 많아짐에 따라 엔트로피 증가

HFSM으로 대부분 커버가 가능하다.

## 생각

과거 체리플젝에서 보스, 몬스터, 플레이어의 상태를 FSM으로 관리했는데, 플레이어와 몬스터는 FSM형태로 작성되었지만, 코드 간의 의존성은 좋지 못한 상태로 관리되었다.

보스에 가서는 좀 더 좋은 코드를 짜고 싶어서 Attack State를 한번 래핑하여 Pattern으로 구분하여 페이즈, 패턴에 맞게 호출되도록 짰는데 이게 HFSM인지 몰랐다.

좋은 경험이였고, 좀 더 넓은 이해가 된 것 같다.
