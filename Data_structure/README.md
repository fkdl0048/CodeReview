# 자료구조  


## 선형 자료구조  

선형 자료구조란 데이터가 일렬로 나열된 형태

배열, 연결리스트, 동적 배열, 스택, 큐

### 스택  

LIFO (후입선출 Last In First Out)

### 큐

FIFO (선입선출 First In First Out)

## 비선형자료구조  

트리, 그래프  

### 그래프  

현실세계의 사물이나 추상적인 개념 간의 연결 관계를 표현  

* 정점(Vertex): 데이터를 표현(사물, 개념 등)
* 간선(Edge): 정점들을 연결하는데 사용  

구현방법에는 대표적으로 두가지 방식이 있다.  

```cs
int [,] abj = new int[6, 6]
{
    {0, 1, 0, 1, 0, 0},
    {1, 0, 1, 1, 0, 0},
    {0, 1, 0, 0, 0, 0},
    {1, 1, 0, 0, 1, 0},
    {0, 0, 0, 1, 0, 1},
    {0, 0, 0, 0, 1, 0}
};
```

```cs
List<int>[] abj2 = new List<int>[]
{
    new List<int>() { 1, 3 },
    new List<int>() { 0, 2, 3 },
    new List<int>() { 1 },
    new List<int>() { 0, 1 },
    new List<int>() { 5 },
    new List<int>() { 4 },
};
```

행렬을 사용하여 간선을 표현하거나 list를 사용하여 간선을 표현..  

#### 그래프 순회 방법

* DFS(Depth First Search) 깊이 우선 탐색  
* BFS(Breadth First Search) 너비 우선 탐색  

##### DFS  

* 1번 행렬 그래프 DFS 구현  

```cs
bool[] visited = new bool[6];
// 1. now부터 방문하고,
// 2. now와 연결된 정점들을 하나씩 확인해서, [아직 미방문한 상태라면] 방문한다.
public void DFS(int now)
{
    System.Console.WriteLine(now);
    visited[now] = true;

    for (int next = 0; next < 6; next++)
    {
        if (abj[now, next] == 0)
        {
            continue;
        }
        if (visited[next])
        {
            continue;
        }
        DFS(next);
    }
}
```

연결이 되어 있지 않거나 방문 했다면 다음 간선을 검사하고 연결되어 있다면 재귀  

* 2번 리스트 그래프 DFS 구현  

```cs
public void DFS2(int now)
{
    System.Console.WriteLine(now);
    visited[now] = true;

    foreach (int next in abj2[now])
    {
        if (visited[next])
        {
            continue;
        }
        DFS2(next);
    }
}
```

애초에 리스트형태이기 때문에 연결되어 있지 않은 것을 검사할 필요가 없음  

```cs
public void SearchAll()
{
    visited = new bool[6];
    for (int now = 0; now < 6; now++)
    {
        if (visited[now] == false)
        {
            DFS2(now);
        }
    }
}
```

SearchAll()은 연결이 끊어진 그래프라도 전체 출력


##### BFS  

```cs
public void BFS(int start)
{
    bool[] found = new bool[6];

    Queue<int> q = new Queue<int>();
    q.Enqueue(start);
    found[start] = true;

    while(q.Count > 0)
    {
        int now = q.Dequeue();
        System.Console.WriteLine(now);

        for (int next = 0; next < 6; next++)
        {
            if (abj[now, next] == 0)
            {
                continue;
            }
            if (found[next])
            {
                continue;
            }
            q.Enqueue(next); 
            found[next] = true;
        }
    }
}
```

예약기능을 queue로 사용하여 구현  


#### 다익스트라 알고리즘  



### 트리  

노드와 간선으로 구성   

#### 이진 검색 트리  

### 우선 순위 큐  

