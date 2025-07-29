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

- [Addressables 공식 문서](https://docs.unity3d.com/Manual/com.unity.addressables.html)
- [Unity Addressables 설명 문서](https://unity.com/kr/blog/engine-platform/addressables-planning-and-best-practices)

공식문서에서도 설명하듯이 어드레서블은 에셋 번들의 상위 레이어로 작동한다. 이를 효율적으로 관리할 수 있도록 UI를 제공하고 이에 따른 그룹과 라벨(키)을 통해 에셋을 관리한다.

> Addressables 그룹은 어드레서블 에셋에 대한 구성 구조를 제공하며, 이는 해당 에셋이 어떻게 AssetBundle에 구축될 것인지를 결정한다는 사실을 유념하세요. 따라서 가장 좋은 구성 전략은 게임 고유의 구조, 목표, 한계에 따라 AssetBundle을 가장 효율적으로 패킹, 로드, 언로드하는 것입니다.

위 내용과 같이 결국에는 이런 추상화는 제작하려는 게임의 구조와 로드맵 좀 더 유연하게 번역하자면 게임의 도메인과 개발 방향에 맞게 어드레서블을 구성해야 한다.

다음과 같은 질문에 답할 수 있어야 한다.

- 제작하는 게임의 구조와 로드맵
- 타게팅하는 게임 플랫폼의 강점과 한계점
- Addressables를 사용해 게임 성능을 최적화하여 달성하려는 최우선 목표

> 예를 들어, 플레이어가 수집할 수 있는 데코 아이템이 포함된 새로운 'Halloween 2023' 이벤트를 시작하려 한다고 가정해 보겠습니다. 'Halloween 2023 Outfits' 그룹에는 'Hats', 'Shoes', 'Masks' 등의 레이블이 지정된 에셋이 포함될 것입니다. 그러면 이 그룹에 있는 모든 에셋에 'Halloween 2023'이라는 레이블을 추가할 수 있습니다. 이 그룹에 'Pack Together By Label' 번들 모드를 사용하면 빌드 시 세 개의 AssetBundle이 생성됩니다.

*가장 이해가 잘 되는 예시*

또한, 모바일의 경우에는 상대적으로 작은 용량의 에셋을 자주 업데이트하는 것이 좋다.

잘 이해가 안된다면 이걸 사용해야 하는 이유를 생각해보면 된다. 실제 서비스중인 즉, 빌드된 애플리케이션에서 특정 어드레서블 데이터를 서버에서 받아와서 로드하는 경우가 많다. 이때 로컬에서 데이터를 수정하고 재빌드하지 않고 서버에 있는 데이터만 직접 수정하면 모든 사용자에게 적용된다.