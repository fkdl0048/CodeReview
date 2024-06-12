# Behavior Tree

가장 잘 설명된 [영상](https://www.youtube.com/watch?v=nTtnKYNEGFE) 참고

10년전부터 사용되고 있으며, 언리얼의 의사결정 시스템에도 BT가 사용되고 있다.

이제는 대부분의 회사가 BT로 의사결정 시스템을 만든다.

행동트리라고 부름

FSM(finite state machine)은 State중심으로 결정했다면, BT는 행동중심으로 결정한다.

## 구성

- 루트노드에서 시작, NPC의 개별 행동을 정의하는 behavior로 구성
- 각각의 Behavior는 자식의 Behavior를 가질 수 있다. (tree구조)
- Behavior는 다음을 정의
  - Precondition: agent가 이 Behavior를 실행할 수 있는지 여부 (조건)
  - Action: Behavior가 실행될 때 agent가 취할 행동
- 알고리즘
  - 루트노드에서 시작
  - Behavior의 Precondition을 순서대로 체크 (왼쪽에서 오른쪽으로 DFS)
    - 하나의 레벨에서 하나의 Behavior만 실행
  - 선택된 Behavior의 자식 노드의 검사 (반복)
  - 전체 순회를 마쳤을 때 선택된 노드 중 가장 우선순위가 높은 노드부터 순차적 실행

## 알고리즘

- 루트 노드를 현재 노드로 설정
- 현재 노드가 존재하는 동안 반복
  - 현재 노드의 Precondition(조건)을 체크
  - 만약 참이라면
    - 그 노드를 실행 리스트에 추가
    - 이후 그 노드에 자식 노드가 존재한다면 그 노드를 현재 노드로 설정 (반복)
  - 거짓이라면
    - 형제 노드로 이동

## 특징

- 장점
  - Simplicity: 특유의 단순함으로 구현이 쉽다.
  - Stateless: 상태의 전환이 없으므로 전 상태에 대한 기억이 필요없다.
  - Unware of each other: 각 Behavior는 서로에 대해 알 필요가 없다. (디커플링, 독립적)
    - BT에서 Behavior를 추가하거나 삭제해도 다른 노드에 영향이 없다.
    - FSM의 경우엔 다른 상태에 전환을 위해 다른 상태에 대한 정보를 알아야 한다.
  - Extensibliity: 단순한 base알고리즘에서 시작해서 기능을 붙여나가면 된다.
    - 부모 동작은 실행할 자식 중 하나를 선택하는 대신, 자식을 각각 차례대로 실행해야 하거나(sequence), 자식 중 하나를 임의로 선택하여 실행(select)하도록 지정가능
      - Utility System-type selector 사용가능
      - 게임 상황에 따른 event를 Precondition에 적용 -> fiexibility 제공
  - 기타: 노드의 재사용, 시각화 툴 제작에 유리
- 단점
  - 매번 루트로부터 탐색: 실행시간이 FSM보다 길다.
  - 단순 구현은 거대한 조건문을 양산하게 된다. (느려짐)
  - 모든 Behavior에 대한 평가라는 구조 -> 하드웨어 성능 고려
  - State가 없음으로 생길 수 있는 문제
    - 루프가 발생할 수 있음
  - 기타
    - 행동 트리의 탐색 순서가 행동에 영향을 미친다. (우선순위-> 왼쪽에서 오른쪽으로 탐색)
    - Selector의 자식 노드가 많을수록 AI의 의사결정이 지연될 수 있다.

## C#으로 구현

```csharp
using ETC;

namespace Interface
{
    public interface IBTNode
    {
        public BTNodeState Evaluate();
    }
}
```

```csharp
using System;
using ETC;
using Interface;

namespace BT
{
    public class BTActionNode : IBTNode
    {
        readonly Func<BTNodeState> _action;
        
        public BTActionNode(Func<BTNodeState> action)
        {
            _action = action;
        }
        
        public BTNodeState Evaluate() => _action?.Invoke() ?? BTNodeState.Failure;
    }
}
```

```csharp
using System.Collections.Generic;
using ETC;
using Interface;

namespace BT
{
    public class BTSelectorNode : IBTNode
    {
        private readonly List<IBTNode> _childNode;
        
        public BTSelectorNode(List<IBTNode> childNode)
        {
            _childNode = childNode;
        }
        
        public BTNodeState Evaluate()
        {
            if (_childNode == null || _childNode.Count == 0)
            {
                return BTNodeState.Failure;
            }
            
            foreach (var node in _childNode)
            {
                switch (node.Evaluate())
                {
                    case BTNodeState.Running:
                        return BTNodeState.Running;
                    case BTNodeState.Success:
                        return BTNodeState.Success;
                }
            }
            
            return BTNodeState.Failure;
        }
    }
}
```

```csharp
using System.Collections.Generic;
using ETC;
using Interface;

namespace BT
{
    public class SequenceNode : IBTNode
    {
        private readonly List<IBTNode> _childNode;
        
        public SequenceNode(List<IBTNode> childNode)
        {
            _childNode = childNode;
        }
        
        public BTNodeState Evaluate()
        {
            if (_childNode == null || _childNode.Count == 0)
            {
                return BTNodeState.Failure;
            }
            
            foreach (var node in _childNode)
            {
                switch (node.Evaluate())
                {
                    case BTNodeState.Running:
                        return BTNodeState.Running;
                    case BTNodeState.Success:
                        continue;
                    case BTNodeState.Failure:
                        return BTNodeState.Failure;
                }
            }
            
            return BTNodeState.Success;
        }
    }
}
```


```csharp
using Interface;
using UnityEngine;

namespace BT
{
    public class BTRunner
    {
        private IBTNode _rootNode;
        
        public BTRunner(IBTNode rootNode)
        {
            _rootNode = rootNode;
        }
        
        public void Execute()
        {
            _rootNode.Evaluate();
        }
    }
}
```