# Utility System

효용 경제학의 용어

대부분의 AI Logic은 Boolean questions으로 이뤄져있다. (양극화)

나는 적을 볼 수 있는가? yes or no

```c++
if (canSeeEnemy()) {
  attack();
}
```

하지만 실세계의 의사결정은 그렇지 않다. (복잡함, 수많은 가능성)

고려해야 할 요소들이 매우 많음 (적이 얼마나 남았고, 탄약이 얼마나 있고, 내 상태는?)

이러한 상황을 if case로 잡을 수 없음, 결과들이 단순히 할까, 말까로 결정되지 않음

따라서 어떠한 행동을 할 가능성으로 형태가 적합 (확률)

Utility System은 잠재적인 행동의 선호도를 결정한다. 고려사항에 대한 측정, 가중치, 결합, 비율, 우선순위, 정렬 등을 수행

보통은 AI아키텍처의 전환논리가 필요할 때 사용, 전체 의사결정 엔진으로 구축할 수 있음

- 수 많은 행동이 있는 게임
- 한 가지 옳은 답이 없는 경우
- 수 많은 경쟁적인 입력에 기반한 선호하는 행동의 선택

이것은 "네가 해야할 유일한 액션"으로 귀결되지 않음

취할 수 있는 가능한 옵션을 제공하는 형태

대표적으로 심즈
