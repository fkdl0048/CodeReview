# Recursion  

재귀함수를 설계할 때 가장 중여한 점은 탈출이 가능한지에 대한 로직이다.  

자칫 무한루프에 빠질 수 있는 구조는 매우 위험하다.  

1. 탈출이 가능한 구조
2. 역순으로 구조를 생각  

## Maze.cs  

재귀함수를 활용한 미로탈출 알고리즘  

1. 현재 위치한 곳이 탈출 곳 이거나
2. 이웃한 셀이 탈출 곳 이거나  

이 두가지 경우를 재귀로 돌려서 검사함  

```cs
public static Boolean findMazePath(int x, int y)
{
    if (x < 0 || y < 0 || x >= N || y >= N) // 셀을 벗어나는 예외
    {
        return false;
    }
    else if (maze[y, x] != (int)TileType.PATHWAY_COLOR) // 갈 수 있는 곳인지 판단
    {
        return false;
    }
    else if (x == N-1 && y == N-1) // 탈출구 이거나
    {
        maze[y, x] = (int)TileType.PATH_COLOR;
        return true;
    }
    else // 이웃한 셀이 탈출구 이거나
    {
        maze[y, x] = (int)TileType.PATH_COLOR;
        if (findMazePath(x, y - 1) || findMazePath(x + 1, y) || findMazePath(x , y + 1) || findMazePath(x - 1, y))
        {
            return true;
        }

        maze[y, x] = (int)TileType.BLOCKED_COLOR;

        return false;
    }
}
```

## Counting_Cells_in_a_Blob.cs  

바이너리 이미지파일의 연속되는 blob을 검출함  

![이미지](../Recursion/Img/Test.png)

* 입력:

 N*N 크기의 2차원 그리드(grid)  
 하나의 좌표 (x, y)

* 출력:  

픽셀 (x, y)가 포함된 blob의 크기,
(x, y)가 어떤 blob에도 속하지 않는 경우에는 0  

*알고리즘, 프로젝트 설계의 경우 먼저 글로 써보고 다음에 수도코드(그림으로 그리던지 UML)순서로 가자*

* Recursive Thinking  

```
현재 픽셀이 속한 blob의 크기를 카운트 하려면  
    현재 픽셀이 image Color가 아니라면  
        0을 반환한다.
    현재 픽셀이 image Color라면  
        먼저 픽셀을 카운트 한다.
        현재 픽셀이 중복 카운트 되는 것을 방지하기 위해 다른 색으로 칠한다.  
        현재 픽셀에 이웃한 모든 픽셀에 대해서  
            그 픽셀이 속한 blob의 크기를 카운트하여 카운트에 더해준다.  
        카운트를 반환한다.  
```

* 수도 코드  

```
Algorithm for countCells(x, y)

if the pixel (x, y) is outside the grid
    the result is 0;
else if pixel (x, y) is not an image pixel or already counted
    the result is 0;
else
    set the colour of the pixel (x, y) to a red colour;
    the result is 1 plus the number of cells in each piece of
        the blob that includes a nearest neighbour;
```

```cs
public int CountCells(int x, int y)
{
    if (x < 0 || y < 0 || x >= _size || y >= _size)
    {
        return 0;
    }
    else if (cells[y, x] != (int)TileType.IMAGE)
    {
        return 0;
    }
    else
    {
        cells[y, x] = (int)TileType.ALREADY;
        return 1 + CountCells(x, y - 1) + CountCells(x + 1, y - 1)
            + CountCells(x + 1, y) + CountCells(x + 1, y + 1)
            + CountCells(x, y + 1) + CountCells(x - 1, y + 1)
            + CountCells(x - 1, y) + CountCells(x - 1, y - 1);
    }
}
```

## N-Queens  

재귀에서 하노이탑만큼 유명한 문제인 N-Queens문제이다.  

N * N체스판에 퀸의 이동가능한 거리에 N개의 퀸을 놓는 문제이다.  

BackTracking으로 푸는 문제.  

* 상태공간트리 : 찾는 해를 포함하는 트리  

즉, 해가 존재한다면 그것은 반드시 이 트리의 어떤 한 노드에 해당함  
따라서 이 트리를 체계적으로 탐색하면 해를 구할 수 있음  

![이미지](../Recursion/Img/statusTree.png)  

backTracking은 상태공간트리의 모든 노드를 탐색할 필요가 없다.  

