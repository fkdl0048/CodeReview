# Two Pointer

알고리즘 문제에서 자주 출제되는 형식으로 배열에서 포인터 2개를 조작함으로써 좀 더 효과적으로 풀이할 수 있는 알고리즘이다.

## [배열 합치기 예시](https://www.acmicpc.net/problem/11728)

가장 쉽게 접할 수 있는 문제로 1회차 때 무식하게 벡터나 배열로 푼다면 시간초과가 뜨는 문제이다.

이를 위해 투 포인터를 활용할 수 있다.

```cpp
#include <bits/stdc++.h>

using namespace std;

int main()
{
    ios_base::sync_with_stdio(0);
    cin.tie(0);
    cout.tie(0);

    int n, m;
    int aIndex = 0, bIndex = 0;

    cin >> n >> m;

    int a[n + 1], b[m + 1];

    for (int i = 0; i < n; i++)
        cin >> a[i];
    
    for (int i = 0; i < m; i++)
        cin >> b[i];

    while (aIndex < n && bIndex < m)
    {
        if (a[aIndex] <= b[bIndex])
            cout << a[aIndex++] << " ";
        else
            cout << b[bIndex++] << " ";
    }

    while (aIndex < n)
        cout << a[aIndex++] << " ";
    while (bIndex < m)
        cout << b[bIndex++] << " ";
}
```