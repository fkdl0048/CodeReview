# Graph

그래프 관련 알고리즘

## DFS

깊이 우선 탐색(DFS)은 그래프의 모든 정점을 한 번씩 방문하는 알고리즘입니다. DFS는 스택이나 재귀함수를 이용하여 구현할 수 있습니다.

### 재귀함수를 이용한 DFS

```cpp
#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

vector<int> graph[1001];
bool visited[1001];

void dfs(int node){
    visited[node] = true;
    cout << node << " ";

    for (int i = 0; i < graph[node].size(); i++){
        int next = graph[node][i];
        if (!visited[next]){
            dfs(next);
        }
    }
}

int main(){
    int n, m, start;
    cin >> n >> m >> start;

    for (int i = 0; i < m; i++){
        int u, v;
        cin >> u >> v;
        graph[u].push_back(v);
        graph[v].push_back(u);
    }

    for (int i = 1; i <= n; i++){
        sort(graph[i].begin(), graph[i].end());
    }

    dfs(start);
    cout << endl;

    return 0;
}
```

- `graph`는 인접 리스트를 표현합니다.
- `visited`는 방문한 노드를 표시합니다.
- `dfs`는 재귀함수로 구현되어 있습니다.
  - 방문한 노드를 표시합니다.
  - 현재 노드를 출력합니다.
  - 현재 노드와 연결된 노드를 탐색합니다.
    - 방문하지 않은 노드라면, 재귀함수를 호출합니다.
    - 방문한 노드라면, 다음 노드를 탐색합니다.
  - 모든 노드를 탐색하면 종료합니다.

### 스택을 이용한 DFS

```cpp
#include <iostream>
#include <vector>
#include <stack>
#include <algorithm>

using namespace std;

vector<int> graph[1001];
bool visited[1001];

void dfs(int start){
    stack<int> s;
    s.push(start);
    visited[start] = true;
    cout << start << " ";

    while(!s.empty()){
        int node = s.top();
        s.pop();

        for (int i = 0; i < graph[node].size(); i++){
            int next = graph[node][i];
            if (!visited[next]){
                s.push(node);
                s.push(next);
                visited[next] = true;
                cout << next << " ";
                break;
            }
        }
    }
}
```

## BFS

너비 우선 탐색(BFS)은 그래프의 모든 정점을 한 번씩 방문하는 알고리즘입니다. BFS는 큐를 이용하여 구현할 수 있습니다.

```cpp
#include <iostream>
#include <vector>
#include <queue>

using namespace std;

vector<int> graph[1001];
bool visited[1001];

void bfs(int start){
    queue<int> q;
    q.push(start);
    visited[start] = true;

    while(!q.empty()){
        int node = q.front();
        q.pop();
        cout << node << " ";

        for (int i = 0; i < graph[node].size(); i++){
            int next = graph[node][i];
            if (!visited[next]){
                q.push(next);
                visited[next] = true;
            }
        }
    }
}
```

- `graph`는 인접 리스트를 표현합니다.
- `visited`는 방문한 노드를 표시합니다.
