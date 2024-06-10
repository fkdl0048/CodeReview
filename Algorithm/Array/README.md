# Array

Array를 사용한 알고리즘은 다양하다. DP나 BP 등의 알고리즘에서도 Array를 사용하는 경우가 많다. Array를 사용한 알고리즘을 정리해보자.

- [sorting](../Sort/README.md)
- searching
- binary search

대부분 배열의 알고리즘은 정렬이나 탐색을 중심으로 한다. 이외에도 배열을 사용하는 다양한 알고리즘이 있다.

## binary search

배열이 정렬되어 있을 때, O(log n)의 시간복잡도로 원하는 값을 찾을 수 있다. 이진 탐색은 배열의 중간값을 기준으로 탐색을 진행한다. 중간값이 찾고자 하는 값보다 크면, 중간값의 왼쪽을 탐색하고, 중간값이 찾고자 하는 값보다 작으면 중간값의 오른쪽을 탐색한다.

```cpp
class Solution {
public:
    int search(vector<int>& nums, int target) {
        int left = 0;
        int right = nums.size() - 1;

        while(left <= right){
            int pivot = (left + right) / 2;

            if (nums[pivot] == target){
                return pivot;
            }

            if (nums[pivot] < target){
                left = pivot + 1;
            }
            else{
                right = pivot - 1;
            }
        }

        return -1;
    }
};
```

- left와 rigth를 초기화한다.
  - 배열의 시작지점과 끝지점을 가리킨다.
- 이후 pivot을 계산한다.
  - pivot은 left와 right의 중간값이다.
- 만약 피봇의 위치가 찾고자 하는 값과 같다면, 피봇의 위치를 반환한다.
- 찾고자 하는 값이 피봇의 값보다 크다면, left를 pivot + 1로 설정한다.
- 찾고자 하는 값이 피봇의 값보다 작다면, right를 pivot - 1로 설정한다.
- 이 과정을 반복한다.

생각해야 할 부분

- left와 right를 어떻게 설정할 것인가? right의 인덱스는 length - 1로 설정한다.
- 반복 조건을 어떻게 설정할 것인가? left가 right보다 작거나 같을 때까지 반복한다. 무한 루프이기 때문에 결국엔 넘을 것이다.
- pivot을 어떻게 설정할 것인가? left와 right의 중간값을 pivot으로 설정한다.

## move zeros

0을 배열의 끝으로 이동시키는 문제이다. 0을 제외한 나머지 숫자는 순서를 유지해야 한다. 배열 알고리즘에서 피봇을 두개 활용하는 좋은 예시이다.

```cpp
class Solution {
public:
    void moveZeroes(vector<int>& nums) {
        int lastNonZeroFoundAt = 0;

        for (int i = 0; i < nums.size(); i++){
            if (nums[i] != 0){
                nums[lastNonZeroFoundAt++] = nums[i];
            }
        }

        for (int i = lastNonZeroFoundAt; i < nums.size(); i++){
            nums[i] = 0;
        }
    }
};
```

- lastNonZeroFoundAt을 0으로 초기화한다.
  - 0을 제외한 나머지 숫자를 저장할 인덱스이다.
- 배열을 순회하면서 0이 아닌 숫자를 찾는다.
  - 0이 아닌 숫자를 찾으면, lastNonZeroFoundAt에 해당 숫자를 저장한다.
  - lastNonZeroFoundAt을 증가시킨다.
- 0이 아닌 숫자를 찾은 후, 나머지 배열을 0으로 초기화한다.

## Find pivot index

배열의 왼쪽과 오른쪽의 합이 같은 pivot을 찾는 문제이다. pivot을 찾는 문제는 피봇을 기준으로 왼쪽과 오른쪽을 탐색하는 문제이다. 이는 슬라이딩 기법을 학습하는데 좋은 문제이다.

```cpp
class Solution {
public:
    int pivotIndex(vector<int>& nums) {
        int sum = accumulation(nums);
        int leftSum = 0;
        int rightSum = sum;

        int pastPivotNum = 0;
        for (idx = 0; idx < nums.lenth; i++){
            int num = nums[idx];
            rightSum -= num;
            leftSum += pastPivotNum;

            if (rightSum == leftSum){
                return idx;
            }
            pastPivotNum = num;
        }

        return -1;   
    }
};
```

- sum을 계산한다.
  - 배열의 모든 값을 더한 값을 저장한다.
- leftSum과 rightSum을 초기화한다.
  - leftSum은 0, rightSum은 sum으로 초기화한다.
- pastPivotNum을 초기화한다.
- 배열을 순회하면서 leftSum과 rightSum을 계산한다.
  - rightSum은 num을 빼고, leftSum은 pastPivotNum을 더한다.
  - 만약 leftSum과 rightSum이 같다면, 해당 인덱스를 반환한다.
  - pastPivotNum을 num으로 업데이트한다.
- 만약 pivot을 찾지 못했다면, -1을 반환한다.

## sort colors

- leetcode: [75. Sort Colors](https://leetcode.com/problems/sort-colors/)

이 문제는 0, 1, 2 세가지 색깔을 정렬하는 문제이다. 0은 배열의 앞쪽에, 2는 배열의 뒷쪽에 위치하도록 정렬해야 한다. 이 문제는 피봇을 3개 사용하는 문제이다.

쉽게 생각하면 정렬로 풀 수 있는 문제이다. 하지만 N의 시간복잡도로 풀 수 있는 방법이 있는지 생각해보자. 공간을 3개를 써서 개수를 세고, 배열을 다시 채우는 방법이 있다. 하지만 swap을 사용하면 공간을 사용하지 않고 풀 수 있다.

```cpp
class Solution {
public:
    void sortColors(vector<int>& nums) {
        int left = 0;
        int right = nums.size() - 1;
        int idx = 0;

        while (idx <= right){
            if (nums[idx] == 0){
                swap(nums[idx++], nums[left++]);
            }
            else if (nums[idx] == 2){
                swap(nums[idx], nums[right--]);
            }
            else{
                idx++;
            }
        }
    }
};
```

## Merge Sorted Array

- leetcode: [88. Merge Sorted Array](https://leetcode.com/problems/merge-sorted-array/)

두 배열을 합치는 문제이다. 두 배열은 정렬되어 있다. 두 배열을 합친 후 정렬된 상태로 유지해야 한다. 이 문제는 두 배열을 합칠 때, 뒤에서부터 합치는 방법을 사용하면 쉽게 풀 수 있다.

```cpp
class Solution {
public:
    void merge(vector<int>& nums1, int m, vector<int>& nums2, int n) {
        int idx1 = m - 1;
        int idx2 = n - 1;
        int idx = m + n - 1;

        while (idx1 >= 0 && idx2 >= 0){
            if (nums1[idx1] > nums2[idx2]){
                nums1[idx--] = nums1[idx1--];
            }
            else{
                nums1[idx--] = nums2[idx2--];
            }
        }

        while (idx2 >= 0){
            nums1[idx--] = nums2[idx2--];
        }
    }
};
```

- idx1과 idx2를 초기화한다.
  - idx1은 nums1의 마지막 인덱스, idx2는 nums2의 마지막 인덱스로 초기화한다.
  - idx는 nums1과 nums2의 합친 배열의 마지막 인덱스로 초기화한다.
- idx1과 idx2가 0보다 크거나 같을 때까지 반복한다.
  - nums1[idx1]과 nums2[idx2]를 비교한다.
  - nums1[idx1]이 크다면, nums1[idx]에 nums1[idx1]을 저장하고 idx1을 감소시킨다.
  - nums2[idx2]가 크다면, nums1[idx]에 nums2[idx2]을 저장하고 idx2을 감소시킨다.
- idx2가 0보다 크거나 같을 때까지 반복한다.
  - nums1[idx]에 nums2[idx2]를 저장하고 idx2을 감소시킨다.
- 이 과정을 반복한다.

## Find peak element

- leetcode: [162. Find Peak Element](https://leetcode.com/problems/find-peak-element/)

peak element를 찾는 문제이다. peak element는 양쪽의 값보다 큰 값을 가지는 값이다. 이 문제는 이진 탐색을 사용하여 풀 수 있다.

```cpp
class Solution {
public:
    int findPeakElement(vector<int>& nums) {
        int left = 0;
        int right = nums.size() - 1;

        while (left < right){
            int pivot = (left + right) / 2;

            if (nums[pivot] > nums[pivot + 1]){
                right = pivot;
            }
            else{
                left = pivot + 1;
            }
        }

        return left;
    }
};
```
