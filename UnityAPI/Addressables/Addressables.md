# Task: Addressable

리소스 -> 에셋번들 -> 어드레서블

기존 에셋관리 시스템은 해당 에섯의 위치를 중심으로 작동 ex) Resource.Load

기존의 에셋번들을 더 편하게 사용할 수 있는 툴이라고 생각하면 된다.

사용법은 key를 가진 group을 만들어서 쉽게 사용가능하나 이후에 반환을 위해 핸들에 캐싱하여 사용하는 것이 중요

두 가지 방법

```c#
Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/AddressablePrefab.prefab").Completed += OnLoadDone;
```

직접 주소를 참조하여 비동기로 불러오기가 완료된다면 이벤트를 등록하여 호출

```c#
[SerializeField] AssetReference prefabReference;

prefabReference.InstantiateAsync(this.transform); // 좀 더 간단
```

AssetReference를 통해 인스펙터에서 지정한 레퍼런스를 불러오는 방식
