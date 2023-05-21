namespace DataStructure
{
    class Graph
    {
        int [,] abj = new int[6, 6]
        {
            {-1, 15, -1, 35, -1, -1},
            {15, -1, 05, 10, -1, -1},
            {-1, 05, -1, -1, -1, -1},
            {35, 10, -1, -1, 05, -1},
            {-1, -1, -1, 05, -1, 05},
            {-1, -1, -1, -1, 05, -1}
        };

        public void Dijikstra(int start)
        {
            bool[] found = new bool[6];
            int[] distance = new int[6];

            Array.Fill(distance, Int32.MaxValue);

            distance[start] = 0;

            while (true)
            {
                // 제일 좋은 후보를 찾는다.(가장 가까이에 있는)  

                // 가장 유력한 후보의 거리와 번호를 저장한다.
                int closest = Int32.MaxValue;
                int now = -1;

                for (int i = 0; i < 6; i++)
                {
                    // 이미 방문한 정점은 스킵
                    if (visited[i])
                        continue;
                    // 아직 발견된적 없거나 기존 후보보다 멀리 있으면 스킵 
                    if (distance[i] == Int32.MaxValue || distance[i] >= closest)
                        continue;
                    // 여태껏 발견한 가장 후보라는 의미니까, 정보를 갱신
                    closest = distance[i];
                    now = i;
                }

                // 다음 후보가 없다. -> 종료
                if (now == -1)
                    break;
                
                // 제일 좋은 후보를 찾았으니까 방문한다.
                visited[now] = true;

                // 방문한 정점과 인접한 정점을 조사해서, 상황에 따라 최단거리를 갱신한다.  
                for (int next = 0; next < 6; next++)
                {
                    if (abj[now, next] == -1)
                        continue;
                    if (visited[next])
                        continue;
                    
                    int nextDist = distance[now] + abj[now, next];
                    if (nextDist < distance[next])
                    {
                        distance[next] = nextDist;
                    }
                }
            }
        }

        List<int>[] abj2 = new List<int>[]
        {
            new List<int>() { 1, 3 },
            new List<int>() { 0, 2, 3 },
            new List<int>() { 1 },
            new List<int>() { 0, 1, 4 },
            new List<int>() { 3, 5 },
            new List<int>() { 4 },
        };

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
    }
}