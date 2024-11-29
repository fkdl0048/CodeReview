# CoreSystem

기본 프로젝트를 시작할 때 대부분 기반작업 또는 기본적인 게임 시스템을 구현하는 경우 코어 시스템을 설계한다고 한다.

최근 진행한 프로젝트인 [merchants-journey](https://github.com/fkdl0048/merchants-journey)에서 제작한 코어 시스템에 대한 리뷰와 설계 방법에 대하여 정리한다.

## 도메인 모델링

우선 Unity로 PC게임을 제작하며, 동아리에서 진행한 프로젝트라 기획이 명확하게 정해진 상태에서 시작한 프로젝트가 아니다.

따라서 기획이 불안정하거나 변경이 잦은 경우에 대비하여 도메인 모델링을 통해 설계를 진행하였다. 과거 객체지향 설계 관련 책을 읽고 실습을 해본 경험이 있어서 이를 토대로 설계해봤다.

- [해당 이슈](https://github.com/BRIDGE-DEV/BRIDGE_BookClub/issues/129)

이번 게임은 유니티라는 게임엔진 특성에 맞게 설계하였다. 기존에 다른 프로그램을 설계할 때 도움 받았던 다음의 룰을 적용하여 생각한다.

- 나는 내가 이해할 수 있는 언어로 쉽게 풀어쓰고, 협력 과정을 의사코드로 나타냈다.
- 가장 먼저 도메인을 크게 역할에 맞게 덩어리로 잡는다.
- 과정을 요약하여 글로 적는다.
- 각 역할에 대한 책임을 구체화하여 적는다.
- 코드로 나타내본다. (행위 중심으로)

*기본적으로 게임의 기획이 자세하게 나오지 않아, 변경 가능성을 생각하여 기본적인 게임 구조를 설정함*

---

- 게임은 기본적으로 다른 PC게임과 비슷한 형태로 진행된다.
- 즉, 타이틀, 인게임, 크레딧 등의 상태를 가진다.
- 인게임에서는 유닛 배치, 웨이브, 스테이지 클리어, 스테이지 실패 등의 상태를 가지지만 이는 추후 변경될 수 있다. (변경 가능성이 큼)
- 게임에서 사용되는 유틸적인 부분과 프레젠테이션, 비즈니스 로직을 분리하여 설계한다.

이에 맞게 구체화를 진행

- 게임 상태를 나타내는 GameState와 InGameState로 구분한다.
  - GameState는 전역적으로 관리가 되어야 함 -> GameManager 각각 덩어리가 크기 때문에 씬으로 관리하고 이를 전역적으로 관리하는 GameManager를 두어 관리함
  - GameManager는 싱글톤의 형태를 가진다. 유니티 특성 상 오브젝트 마다 독립적인 라이프 사이클, 객체지향의 성격을 가져가기 어렵기 때문에 싱글톤으로 관리하게 됨
  - 싱글톤은 데이터를 사용하지 않음 (들고 있으면 안됨) 불변성을 보장하고 매니저에 맞는 책임만 할당할 것 -> 이 구조가 깨지는 순간 스파게티 코드가 됨
  - InGameState는 씬에서 관리되어야 함 -> InGameSceneController로 관리하고 이를 통해 상태를 변경함
  - 각각 상태는 모듈화를 위해 State패턴을 사용
  - 앞서 말한 상태가 추가되고 삭제될 수 있는 부분이 대부분 인게임에서 일어나기 때문이 이 부분을 가장 구체화하여 설계함
- 게임에서 사용되는 유틸적인 부분은 Util로 관리함
  - Enum, Struct, Class 등의 데이터를 관리함
  - Manager도 여기에 해당함 (Core)

## 현재 게임 구조

크게 GameState와 InGameState로 구분됨 두 상태 모두 State패턴으로 구현되어 있으며, 데이터는 [Util](https://github.com/fkdl0048/merchants-journey/blob/main/Assets/2.%20Scripts/Utils/Enums.cs)에 정리되어 있음 *같이 사용해야 하는 Enum데이터는 여기에 작성 부탁*

GameState란 크게 게임을 관장하는 상태를 말함 Boot, Title, InGame, Credits 등 큰 단위를 다룸 따라서 씬으로 분리함 이에 따라 상태를 변경하는 매니저를 [GameManager](https://github.com/fkdl0048/merchants-journey/blob/main/Assets/2.%20Scripts/Core/Manager/GameManager.cs)가 관장함 최대한 GamaManager는 가벼워야 함 불변속성을 보장해야 하며, 가능하다면 분리할 것 -> 다른 매니저나 SO로 뺄 것

InGame은 GameState가 InGame일 때 관리하게 되는 상태를 말하며, 이 상태는 local(Ingame씬)에서 관리됨, 이는 [InGameSceneController](https://github.com/fkdl0048/merchants-journey/blob/main/Assets/2.%20Scripts/Controller/InGameSceneController.cs)로 관리함

현재 게임 구조의 위계는 Manager, Controller, System의 접미어를 순서로 가짐 즉, Manager가 붙은 클래스가 가장 높은 위치 (Global에 위치)하고 이는 Scripts/Core에 정리해둠, [Controller](https://github.com/fkdl0048/merchants-journey/tree/main/Assets/2.%20Scripts/Controller)는 씬 마다 각각 붙으며 Boot, MainMenu, InGame 각각 씬에 컴포넌트로 부착되어 있음

System은 Controller에서 각각 필요한, 모듈화된 State나 시스템에 DI(dependency injection)하기 위해 구분함

### InGameState

좀 더 활용적인 예시를 말한다면 InGameState와 System, Controller의 관계에 대해서 설명함

```cs
// 중략..
    public class InGameSceneController : MonoBehaviour
    {
        [Header("Scene References")]
        [SerializeField] private GameUI gameUI;
        [SerializeField] private UnitSystem unitSystem;
        [SerializeField] private StageSystem stageController;
        [SerializeField] private AudioClip gameBGM;
        
        private Dictionary<InGameState, IInGameState> states;
        private IInGameState currentState;

        private void Awake()
        {
            InitializeStates();
        }

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeStates()
        {
            states = new Dictionary<InGameState, IInGameState>
            {
                {
                    InGameState.UnitPlacement,
                    new UnitPlacementState(this, gameUI, unitSystem, stageController)
                },
                {
                    InGameState.Wave,
                    new BattleState(this, gameUI, stageController)
                },
                {
                    InGameState.StageClear,
                    new StageClearState(this, gameUI)
                },
                {
                    InGameState.StageOver,
                    new StageOverState(this, gameUI)
                },
                // stage fail state 추가..예정
            };
        }

// 중략..
        public void ChangeInGameState(InGameState newState)
        {
            currentState?.Exit();
            
            currentState = states[newState];
            currentState.Enter();
            
            Debug.Log($"Game State Changed to: {newState}");
        }
    
        private void Update()
        {
            currentState?.Update();
        }

        private void OnDestroy()
        {
            if (this.gameObject.scene.isLoaded)
            {
                currentState?.Exit();
            }
        }
        
    }
}
```

IInGameState라는 인터페이스를 활용하여 현재 InGame상태를 관리함 이는 현재 기획이 불안정하고 각각 상태가 독립적으로 관리되어야 추가 및 수정이 용이하기에 해당 구조를 채택함

각각 상태에 대한 정의는 [InGame/State](https://github.com/fkdl0048/merchants-journey/tree/main/Assets/2.%20Scripts/InGame/State)폴더에 모아둠

각각의 상태는 Mono가 아닌 CS클래스이기에 유니티 GUI로 보이지 않음, 즉 인게임에 존재하는 데이터를 사용하기 위해선 인젝션 받아야 함 -> 이를 관장하는 것이 Controller임 위를 보면 알 수 있지만 Controller는 독립적으로 구성된 UnitSystem(유닛 관리 관련 System, StateSystem 스테이지 관리 시스템) 이를 직렬화를 통해 씬에서 직접 연결함 Controller가 System에 의존하게 됨 (System은 뒤에서 설명하지만 독립적으로 구성되어야 함)

```cs
{
    InGameState.UnitPlacement,
    new UnitPlacementState(this, gameUI, unitSystem, stageController)
}
```

*stageController -> stageSystem으로 수정해야 하겠네요 수정하겠읍니다..*

이렇게 InGame에서 State를 동적 생성하여 책임을 분리하며 독립적으로 동작하는 System을 받아와서 해당 상태에서 활용하여 게임이 동작함, 중요한 점은 State로 관리되기에 수정이나 제거가 용이하다는 점 즉, 이런 구조를 유지하기 위해선 위에서 말한 Controller, Manager, System의 관계를 이해해야 함

System의 경우 State에서 직접 Manager을 사용하여 데이터를 받아오는 경우를 최대한 줄이고, State나 Controller가 방대해지는 것을 막기 위함임

예를 들어 InGameController가 모든 행위 (유닛배치, 업그레이드, 웨이브 관리 등) 모든 행위를 담당한다면 코드는 커지고 엉키게 되며 객체지향이 깨짐 -> 각각 컴포넌트, cs에 맞는 역할만 수행할 수 있도록 분리하는것이 핵심임,

## 유의사항

1. Manager는 싱글톤이기 때문에, 비지니스 로직과 인게임 로직을 명확하게 분리해야 함 즉, 인게임 로직이 싱글톤에 결합되면 안됨
2. State가 커진다면 System으로 분리할 것 마찬가지로 System도 덩치가 커진다면 분리할 것
3. Controller는 각 시스템에 분배, DI 하는 역할이지 다른 역할을 수행하면 안됨
4. 대부분의 이벤트는 코드로 작성하고 State나 씬이 변경될 때 Free할 것
