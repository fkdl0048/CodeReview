# AStart

다양한 길 찾기 알고리즘 중 가장 대중적인 길찾기 알고리즘

*요즘 강곽 받는 JPS가 있다.*

- 특징
  - A* 알고리즘은 시작 노드만을 지정해 다른 모든 노드에 대한 최단 경로를 파악하는 다익스트라 알고리즘과 다르게 **시작 노드와 목적지 노드**를 분명하게 지정해 이 두 노드 간의 최단 경로를 파악할 수 있습니다.
  - 휴리스틱 추정값을 통해 알고리즘을 개선 (값에 따라 속도가 결정)

[가장 이해가 잘 된 블로그 글](http://www.gisdeveloper.co.kr/?p=3897)

## 중요 포인트

- 다익스트라 알고리즘의 원리를 차용한 것
- A*가 사용되는 이유는 다익스트라는 현실에 적용하기 매우 부담되기에
- A* 알고리즘을 너비 우선 탐색의 한 예

## 구현 포인트

- A* 알고리즘은 현재 상태의 비용을 g(x)
- 현재 상태에서 다음 상태로 이동할 때의 휴리스틱 함수를 h(x)
- g(x)와 h(x)를 더한 f(x)를 최소가 되는 지점으로 우선 탐색한다.
- f(x)가 작은 값부터 탐색하는 특성상 우선순위 큐가 사용된다.
- 휴리스틱 함수(추정거리 계산)은 유클리드 거리, 맨하탄 거리, 코사인 유사도 등이 있다.
-

## 수도 코드

- f(x)를 오름차순 우선순위 큐에 노드로 삽입한다. (자동 정렬)
- 우선순위 큐에서 최우선 노드를 pop한다.
- 해당 노드에서 이동할 수 있는 노드를 찾는다. (열린 구간)
- 그 노드들의 f(x)를 구한다.
- 그 노드들을 우선순위 큐에 삽입한다. (열린 구간 추가)
- 목표 노드에 도달할 때따지 반복한다.

```c++
PQ.push(start_node, g(start_node) + h(start_node))       //우선순위 큐에 시작 노드를 삽입한다.

while PQ is not empty       //우선순위 큐가 비어있지 않은 동안
    node = PQ.pop       //우선순위 큐를 pop한다.

    if node == goal_node       //만일 해당 노드가 목표 노드이면 반복문을 빠져나온다. 기저사례
        break

    for next_node in (next_node_begin...next_node_end)       //해당 노드에서 이동할 수 있는 다음 노드들을 보는 동안
        PQ.push(next_node, g(node) + cost + h(next_node))       //우선순위 큐에 다음 노드를 삽입한다.

print goal_node_dist       //시작 노드에서 목표 노드까지의 거리를 출력한다.
```

## 실제 코드

```c++
using ii = pair<int, int>;

priority_queue<ii, vector<ii>, greater<ii> > pq;
/// N_VER은 그래프의 정점의 개수를 의미한다.
int dist[N_VER], cost[N_VER][N_VER]; /// dist[i]는 i번째 정점까지 가는 최단 거리를 의미한다.
vector<ii> edges[N_VER]; /// edges[i]는 i와 연결된 정점과 가중치를 묶어 저장하는 벡터이다.

int minDist(int src, int dst) {
    pq.emplace(0, src);
    bool success = false;
    while (!pq.empty()) {
        int v = pq.top(); pq.pop();
        if (v == dst) {
            success = true;
            break;
        }
        for (ii& adj : edges[v]) {
            if (dist[adj.first] < g(v) + adj.second + h(adj.first)) {
                dist[adj.first] = g(v) + adj.second + h(adj.first); // 이완 (relaxation)
                pq.emplace(dist[adj], adj); // 다음 정점 후보에 탐색하고 있는 정점을 넣는다.
            }
        }
    }
    if (!success) return -1;
    else return dist[dst];
}
```

## 생각

휴리스틱에 따라 값이 달라진다면 아마 가장 최악은 f(x)와 g(x)의 값이 같아지는 경우가 아닐까 싶다.

그렇다면 노드들이 일렬로 늘어선 경우에는 최악의 성능을 보일 것이다.

어느정도 방사형으로 뻗어나가는 경우에는 최적의 성능을 보일 것이다. (휴리스틱에 따라)

스타크래프트도 A*를 사용한다고 한다.
