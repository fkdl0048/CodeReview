# 언리얼 컨벤션

[제공된 가이드 규칙](https://github.com/Allar/ue5-style-guide/tree/v2?tab=readme-ov-file)

추가로 언리얼 [C++ 스타일 가이드](https://github.com/futurelabunseen/B-JeonganLee/blob/main/Document/StudyNote/UnrealLecture/Part1/Lecture2.md)는 Part1의 2강에서 다룸

## 용어 정리

### Levels/Maps

쉽게 레벨과 멥으로 부르며 스테이즈, 스테이지, 맵 등으로도 불린다.

### Identifiers

식별자로 불리며, 이름, 태그, 레이어 등으로 불린다.

### Cases

쉽게 파스칼, 카멜, 스테이크 등을 뜻하며 이를 섞어서 잘 사용한다.

C++는 파스칼 케이스를 사용하고, 블루프린트는 카멜 케이스를 사용한다. 클래스 명, 구조체 명, 열거형 명은 파스칼 케이스를 사용하고, 변수, 함수, 이벤트, 프로퍼티, 파라미터, 멤버 변수, 멤버 함수, 멤버 이벤트, 멤버 프로퍼티, 멤버 파라미터는 카멜 케이스를 사용한다.

추가로 대문자 및 밑줄은 매크로명이나 열거형 상수에 사용한다.

```c++
// MyActor.h - 헤더 파일
#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Actor.h"
#include "MyActor.generated.h"

// UCLASS 매크로를 사용하여 언리얼 엔진의 클래스 시스템에 이 클래스를 등록
UCLASS()
class MYPROJECT_API AMyActor : public AActor
{
    GENERATED_BODY()

public:
    // Sets default values for this actor's properties
    AMyActor();

protected:
    // Called when the game starts or when spawned
    virtual void BeginPlay() override;

public: 
    // Called every frame
    virtual void Tick(float DeltaTime) override;

    // Example of a public member variable, following the Unreal Engine naming conventions
    UPROPERTY(VisibleAnywhere)
    int32 MyPublicInteger;

private:
    // Example of a private member variable with the 'm' prefix
    int32 mMyPrivateInteger;
};
cpp
Copy code
// MyActor.cpp - 소스 파일
#include "MyActor.h"

// Constructor
AMyActor::AMyActor()
{
    // Set this actor to call Tick() every frame. You can turn this off to improve performance if you don't need it.
    PrimaryActorTick.bCanEverTick = true;

    // Initialize variables
    MyPublicInteger = 0;
    mMyPrivateInteger = 0;
}

// Called when the game starts or when spawned
void AMyActor::BeginPlay()
{
    Super::BeginPlay();
    
    // Custom initialization code here
}

// Called every frame
void AMyActor::Tick(float DeltaTime)
{
    Super::Tick(DeltaTime);

    // Custom tick code here
}
```

### Variables / Properties

대부분의 상황에서 `변수`와 `속성`은 서로 바꿔서 사용가능하다.

### Property

일반적으로 클래스에 정의된 변수를 나타내며 `C#`의 프로퍼티와 같이 `BP_Barrel`은 `bExploded`의 참조로 사용될 수 있다.

### Variable

지역변수를 나타낸다.

## 0. 원칙 (Principles)

> "스타일에 대한 논쟁은 무의미합니다. 스타일 가이드가 있어야 하고 이를 따라야 합니다." : 레베카 머피

대부분의 원칙은 해당 팀 컨벤션에 맞추면 되지만, 그럼에도 엔진과 언어에는 기본적인 규칙이 존재한다.

*스타일 가이드는 살아있는 문서이다.*

스타일 가이드를 준수하여 매번 재학습이 아닌 같은 추상화 모델/메타 모델을 공유하는 것을 목적으로 한다. 스타일에 대한 청크가 없어지고 생산적인 생성 및 유지가 가능해진다.

스타일 가이드에서 어긋난다면 이를 바로잡도록 노력해야 한다. 혹시 주위에 있다면 친절하게 알려줄 것.

스타일 가이드가 없는 팀은 팀이 아니다. 법을 어기지 말것

## Globally Enforced Opinions (전역적으로 강제되는 의견)

### 금지된 문자 (Forbidden Characters)

#### Identifiers

식별자는 어떤 종류의 경우에도 절대적으로 강제하지 않는 한 다음은 사용하지 말 것

- 모든 종류의 공백
  - ex) `My Variable`
- 역 슬래시
- 기호
  - ex) `@`, `#`, `$`, `%` 등등
- 모든 유니코드 문자
  - ex) `👍`, `👎`, `👌` 등등

가능한 다음 문자만 포함해야 한다. (RegEx: `[A-Za-z0-9_]` *정규 표현식입니다.*)

- ABCDEFGHIJKLMNOPQRSTUVWXYZ
- abcdefghijklmnopqrstuvwxyz
- 1234567890
- _ (sparingly)

## 목차

- [자산 명명 규칙](#자산-명명-규칙)

### 자산 명명 규칙

에셋 명명 규칙을 준수하는 프로젝트를 향해야 다른 프로젝트에 대한 이해도 높아지고 사용되는 자산에 대한 검색, 관리, 구문 및 분석이 효과적으로 일어남

대부분은 에셋유형의 약어 뒤에 밑줄이 오는 접두가사 붙는다.

#### 기본적인 에셋 이름 - `Prefix_BaseAssetName_Variant_Suffix`

모든 자산에는 기본 자산이름이 있어야한다. 기본 자산 이름은 관련 자산의 **논리적 그룹**을 나타낸다. 이 논리 그룹의 일부인 모든 자산은 해당 표준을 따라야 한다. `Prefix_BaseAssetName_Variant_Suffix`

`Perfix`는 아래 표를 통해 자산 유형에 따라 다르게 지정된다. `Surffix`

`BaseAssetName`이 자산 그룹의 맥락과 관련하여 짧고 쉽게 알아볼 수 있는 이름으로 결정되어야 한다. 예를 들어 `Bob`이라는 캐릭터가 있는 경우 모든 자산의 `BaseAssetName`은 `Bob`이어야 한다.

고유하고 구체적인 에셋 변형의 경우 `Variant`은 에셋 기본 이름의 하위 집합인 에셋의 논리적 그룹을 나타내는 짧고 쉽게 인식할 수 있는 이름이어야 한다. 예들 들어 `Bob`이라는 캐릭터가 스킨을 가지고 있다면 `Bob_Evil`, `Bob_Retro`와 같이 나타낸다.

고유하지만 일반적인 에셋의 경우 (형태만 다르거나, 확장의 경우) `Variant`는 두자리 숫자로 나타낸다. 예를 들어 환경 에셋인 `Rock`에 대한 변형은 `Rock_01`, `Rock_02`와 같이 나타낸다. 만약 에셋의 개수가 100개를 넘어가는 경우엔 분할이 필요하다는 신호다.

위 두가지를 조합하여서 사용도 가능하다. 에셋 변형이 만들어진 방식에 따라 더욱 확장된다면 다음과 같이 사용할 수 있다. `Flooring` -> `Flooring_Marble_01`, `Flooring_Maple_01`, `Flooring_Tile_Squares_01`

#### 예시

| Asset Type              | Asset Name                                                 |
| ----------------------- | ---------------------------------------------------------- |
| Skeletal Mesh           | SK_Bob                                                     |
| Material                | M_Bob                                                      |
| Texture (Diffuse/Albedo)| T_Bob_D                                                    |
| Texture (Normal)        | T_Bob_N                                                    |
| Texture (Evil Diffuse)  | T_Bob_Evil_D                                               |

---

| Asset Type              | Asset Name                                                 |
| ----------------------- | ---------------------------------------------------------- |
| Static Mesh (01)        | S_Rock_01                                                  |
| Static Mesh (02)        | S_Rock_02                                                  |
| Static Mesh (03)        | S_Rock_03                                                  |
| Material                | M_Rock                                                     |
| Material Instance (Snow)| MI_Rock_Snow                                               |

### 에셋 이름 수정자

위 기본 양식에서 말한 `Perfix`와 `Suffix`는 에셋의 유형에 따라 다르게 지정된다.

#### 기본적인 (Common)

| Asset Type              | Prefix     | Suffix     | Notes                            |
| ----------------------- | ---------- | ---------- | -------------------------------- |
| Level / Map             |            |            | Map이라는 폴더가 필요함           |
| Level (Persistent)      |            | _P         |                                  |
| Level (Audio)           |            | _Audio     |                                  |
| Level (Lighting)        |            | _Lighting  |                                  |
| Level (Geometry)        |            | _Geo       |                                  |
| Level (Gameplay)        |            | _Gameplay  |                                  |
| Blueprint               | BP_        |            |                                  |
| Material                | M_         |            |                                  |
| Static Mesh             | S_         |            |          |
| Skeletal Mesh           | SK_        |            |                                  |
| Texture                 | T_         | _?         |    |
| Particle System         | PS_        |            |                                  |
| Widget Blueprint        | WBP_       |            |                                  |

#### 애니메이션

| Asset Type              | Prefix     | Suffix     | Notes                            |
| ----------------------- | ---------- | ---------- | -------------------------------- |
| Aim Offset              | AO_        |            |                                  |
| Aim Offset 1D           | AO_        |            |                                  |
| Animation Blueprint     | ABP_       |            |                                  |
| Animation Composite     | AC_        |            |                                  |
| Animation Montage       | AM_        |            |                                  |
| Animation Sequence      | A_         |            |                                  |
| Blend Space             | BS_        |            |                                  |
| Blend Space 1D          | BS_        |            |                                  |
| Level Sequence          | LS_        |            |                                  |
| Morph Target            | MT_        |            |                                  |
| Paper Flipbook          | PFB_       |            |                                  |
| Rig                     | Rig_       |            |                                  |
| Control Rig             | CR_        |            |                                  |
| Skeletal Mesh           | SK_        |            |                                  |
| Skeleton                | SKEL_      |            |                                  |

#### AI

| Asset Type              | Prefix     | Suffix     | Notes                            |
| ----------------------- | ---------- | ---------- | -------------------------------- |
| AI Controller           | AIC_       |            |                                  |
| Behavior Tree           | BT_        |            |                                  |
| Blackboard              | BB_        |            |                                  |
| Decorator               | BTDecorator_ |          |                                  |
| Service                 | BTService_ |            |                                  |
| Task                    | BTTask_    |            |                                  |
| Environment Query       | EQS_       |            |                                  |
| EnvQueryContext         | EQS_       | Context    |                                  |

#### Blueprints

| Asset Type              | Prefix     | Suffix     | Notes                            |
| ----------------------- | ---------- | ---------- | -------------------------------- |
| Blueprint               | BP_        |            |                                  |
| Blueprint Component   | BP_        | Component  | 즉, BP_InventoryComponent       |
| Blueprint Function Library | BPFL_   |            |                                  |
| Blueprint Interface     | BPI_       |            |                                  |
| Blueprint Macro Library | BPML_      |            | 가능하면 사용하지 말것 |
| Enumeration             | E          |            | No underscore.                   |
| Structure               | F or S     |            | No underscore.                   |
| Tutorial Blueprint      | TBP_       |            |                                  |
| Widget Blueprint        | WBP_       |            |                                  |

#### Materials

| Asset Type                    | Prefix     | Suffix     | Notes                            |
| ----------------------------- | ---------- | ---------- | -------------------------------- |
| Material                      | M_         |            |                                  |
| Material (Post Process)       | PP_        |            |                                  |
| Material Function             | MF_        |            |                                  |
| Material Instance             | MI_        |            |                                  |
| Material Parameter Collection | MPC_       |            |                                  |
| Subsurface Profile            | SP_        |            |                                  |
| Physical Materials            | PM_        |            |                                  |
| Decal                         | M_, MI_    | _Decal     |                                  |

#### Textures

| Asset Type              | Prefix     | Suffix     | Notes                            |
| ----------------------- | ---------- | ---------- | -------------------------------- |
| Texture                 | T_         |            |                                  |
| Texture (Diffuse/Albedo/Base Color)| T_ | _D      |                                  |
| Texture (Normal)        | T_         | _N         |                                  |
| Texture (Roughness)     | T_         | _R         |                                  |
| Texture (Alpha/Opacity) | T_         | _A         |                                  |
| Texture (Ambient Occlusion) | T_     | _O         |                                  |
| Texture (Bump)          | T_         | _B         |                                  |
| Texture (Emissive)      | T_         | _E         |                                  |
| Texture (Mask)          | T_         | _M         |                                  |
| Texture (Specular)      | T_         | _S         |                                  |
| Texture (Metallic)      | T_         | _M         |                                  |
| Texture (Packed)        | T_         | _*         | |
| Texture Cube            | TC_        |            |                                  |
| Media Texture           | MT_        |            |                                  |
| Render Target           | RT_        |            |                                  |
| Cube Render Target      | RTC_       |            |                                  |
| Texture Light Profile   | TLP        |            |                                  |

#### Miscellaneous (기타)

| Asset Type                 | Prefix     | Suffix     | Notes                            |
| -------------------------- | ---------- | ---------- | -------------------------------- |
| Animated Vector Field      | VFA_       |            |                                  |
| Camera Anim                | CA_        |            |                                  |
| Color Curve                | Curve_     | _Color     |                                  |
| Curve Table                | Curve_     | _Table     |                                  |
| Data Asset                 | *_         |            | Prefix should be based on class. |
| Data Table                 | DT_        |            |                                  |
| Float Curve                | Curve_     | _Float     |                                  |
| Foliage Type               | FT_        |            |                                  |
| Force Feedback Effect      | FFE_       |            |                                  |
| Landscape Grass Type       | LG_        |            |                                  |
| Landscape Layer            | LL_        |            |                                  |
| Matinee Data               | Matinee_   |            |                                  |
| Media Player               | MP_        |            |                                  |
| Object Library             | OL_        |            |                                  |
| Redirector                 |            |            | These should be fixed up ASAP.   |
| Sprite Sheet               | SS_        |            |                                  |
| Static Vector Field        | VF_        |            |                                  |
| Substance Graph Instance   | SGI_       |            |                                  |
| Substance Instance Factory | SIF_       |            |                                  |
| Touch Interface Setup      | TI_        |            |                                  |
| Vector Curve               | Curve_     | _Vector    |                                  |

#### Paper 2D

| Asset Type              | Prefix     | Suffix     | Notes                            |
| ----------------------- | ---------- | ---------- | -------------------------------- |
| Paper Flipbook          | PFB_       |            |                                  |
| Sprite                  | SPR_       |            |                                  |
| Sprite Atlas Group      | SPRG_      |            |                                  |
| Tile Map                | TM_        |            |                                  |
| Tile Set                | TS_        |            |                                  |

#### Physics

| Asset Type              | Prefix     | Suffix     | Notes                            |
| ----------------------- | ---------- | ---------- | -------------------------------- |
| Physical Material       | PM_        |            |                                  |
| Physics Asset           | PHYS_      |            |                                  |
| Destructible Mesh       | DM_        |            |                                  |

#### Sounds

| Asset Type              | Prefix     | Suffix     | Notes                            |
| ----------------------- | ---------- | ---------- | -------------------------------- |
| Dialogue Voice          | DV_        |            |                                  |
| Dialogue Wave           | DW_        |            |                                  |
| Media Sound Wave        | MSW_       |            |                                  |
| Reverb Effect           | Reverb_    |            |                                  |
| Sound Attenuation       | ATT_       |            |                                  |
| Sound Class             |            |            | 접두사, 접미사 x  SoundClasses폴더에 넣기|
| Sound Concurrency       |            | _SC        | SoundClass의 이름을 따서 명명 |
| Sound Cue               | A_         | _Cue       |                                  |
| Sound Mix               | Mix_       |            |                                  |
| Sound Wave              | A_         |            |                                  |

#### User Interface

| Asset Type              | Prefix     | Suffix     | Notes                            |
| ----------------------- | ---------- | ---------- | -------------------------------- |
| Font                    | Font_      |            |                                  |
| Slate Brush             | Brush_     |            |                                  |
| Slate Widget Style      | Style_     |            |                                  |
| Widget Blueprint        | WBP_       |            |                                  |

#### Effects

| Asset Type              | Prefix     | Suffix     | Notes                            |
| ----------------------- | ---------- | ---------- | -------------------------------- |
| Particle System         | PS_        |            |                                  |
| Material (Post Process) | PP_        |            |                                  |

### 콘텐츠 디렉터리 구조

자산 이름과 마찬가지로 프로젝트의 디렉토리 구조 스타일은 지켜져야 한다. 앞서 지켜진 에셋 명명 규칙에 맞춰 구조를 분리하기 때문에 이를 어길시 혼란이 발생한다. *앞서 명명 규칙에서 폴더 구조에 맞춰서 만들어졌다.*

<pre>
|-- Content
    |-- GenericShooter
        |-- Art
        |   |-- Industrial
        |   |   |-- Ambient
        |   |   |-- Machinery
        |   |   |-- Pipes
        |   |-- Nature
        |   |   |-- Ambient
        |   |   |-- Foliage
        |   |   |-- Rocks
        |   |   |-- Trees
        |   |-- Office
        |-- Characters
        |   |-- Bob
        |   |-- Common
        |   |   |-- Animations
        |   |   |-- Audio
        |   |-- Jack
        |   |-- Steve
        |   |-- Zoe
        |-- Core
        |   |-- Characters
        |   |-- Engine
        |   |-- GameModes
        |   |-- Interactables
        |   |-- Pickups
        |   |-- Weapons
        |-- Effects
        |   |-- Electrical
        |   |-- Fire
        |   |-- Weather
        |-- Maps
        |   |-- Campaign1
        |   |-- Campaign2
        |-- MaterialLibrary
        |   |-- Debug
        |   |-- Metal
        |   |-- Paint
        |   |-- Utility
        |   |-- Weathering
        |-- Placeables
        |   |-- Pickups
        |-- Weapons
            |-- Common
            |-- Pistols
            |   |-- DesertEagle
            |   |-- RocketPistol
            |-- Rifles
</pre>

#### 폴더 이름 규칙

- 항상 PascalCase를 사용한다
- 공백을 사용하지 마라
- 유니코드 문자 및 기타 기호를 사용하지 마라

*이는 에셋이름의 금지 규칙과 동일하다.*

#### 프로젝트별 에셋에 대해 최상위 폴더'

프로젝트의 모든 에셋은 프로젝트 이름을 딴 폴더 안에 있어야 한다. 이름이 `Generic Shooter`인 경우 모든 콘텐츠가 `Content/GenericShooter.`위치에 있어야 한다.

이 접근 방식에는 다음과 같은 이유가 있다.

- 글로벌 에셋 없음
  - 코드 스타일 가이드에는 전역 네임스페이스가 없어야 한다고 명시되어 있기에 동일한 원칙을 따른다. 해당 폴더 외부에 존재한다면 필요없는 동작을 조장하므로 ..
- 마이그레이션 충돌 감소
  - 다른 프로젝트의 복사가 일어날 때 가장 쉬운 방법은 콘텐츠 브라우저 마이그레이션 기능을 사용하는 것
- 샘플, 템플릿 및 마켓플레이스의 콘텐츠 위험 없음
- 프로젝트의 콘텐츠가 다른 프로젝트의 콘텐츠와 충돌하지 않음
  - 이미 최상위 프로젝트 폴더의 이름이 고유하게 지정되어 있는 한 새 에셋들은 프로젝트를 방해하지 못한다.
- DLC, 하위 프로젝트 및 패치는 쉽게 유지 관리가 된다.
  - 이러한 프로젝트 구조를 가져가면 이후 출시 계획에서 유리하다. (별도로 관리가 가능하고 최소한의 노력으로 마이그레이션 가능)

#### 로컬 테스트를 위해 개발자 폴더 사용

일반적으로 프로젝트를 진행하며 해당 메인 프로젝트에 대한 위험을 감당하기 보다 자유롭게 실험할 수 있는 샌드박스를 갖는 것은 매우 일반적이다. 따라서 개발자 폴더를 사용하여 이러한 실험을 할 수 있다.

#### 모든 맵 파일은 Maps라는 폴더에 속한다

맵 파일은 특별하기에 모든 프로젝트가 고유한 맵 이름 지정 시스템을 갖는 것이 일반적이다. (즉, 모든 맵 파일은 팀, 프로젝트에서 설정한 컨벤션으로)

*실제로 위의 에셋 명명 규칙에서도 Map파일은 접두사 접미사가 지정되지 않는다.*

중요한 점은 `Maps`폴더 내에서도 레벨로 관리하는 것이 좋다는 것. 이는 레벨을 관리하기 쉽게 하고, 레벨을 찾기 쉽게 한다.

#### 중요한 블루프린트 및 기타 자산 폴더

`/Content/Project/Core`프로젝트 작업에 절대적으로 중요한 에셋은 폴더를 사용하라. 예를 들어 기본 `GameMode`, `Character`, `PlayerController`, `GameState`, `PlayerState`및 관련 블루프린트는 `/Content/Project/Core`폴더에 위치해야 한다.

이렇게 하면 다른 동료에게 "이것을 만지지 말라"라는 메시지가 명확하게 전달된다. 엔지니어만 이를 조작할 수 있도록 제한을 둘 수 있는 것

*그렇다면 대부분의 엔지니어가 만지게 되는 영역이 Core로, 아트는 Art와 같이 분리가 명확해진다..?*

#### 폴더를 생성하지 마십시오 (Assets, AssetType)

- 모든 것이 에셋이기 때문에 `Asset`이라는 이름은 불필요하다.
- `Texture`라는 폴더는 `Material`에 중복된다.

#### 매우 큰 에셋은 자체 폴더 레이아웃을 갖는다

각 자산마다 고유한 목적을 갖고 있는 관련 파일이 엄청나게 많은 경우 (다운받은 라이브러리가 하위 폴더로 다 정렬이 되어 있다면 root를 하나 더 늘린다고 생각)

#### MaterialLibrary

모든 형태의 재사용 가능한 머터리얼이나 텍츠처를 사용하는 경우는 `MaterialLibrary`폴더에 위치시킨다.

이렇게하면 전역적으로 자료를 보관하고 쉽게 찾아볼 수 있다.

#### 빈 폴더는 없애라

빈 폴더가 있어서는 안 된다. 콘텐츠 브라우저를 복잡하게 만든다.

### Blueprints

블루 프린트에 관한 규칙

#### 컴파일

모든 블루프린트는 경고와 오류가 전혀 없이 컴파일 되어야 한다. 블루프린트의 오류는 특히 매우 예상치 못한 동작으로 이어질 수 있으니 즉시 수정해야 한다.

즉, 손상된 블루프린트가 있다면 코드베이스로 올리지 말것

#### 변수

`variable`, `property` 같은 의미로 사용될 수 있다.

##### 명명

- 모든 변수이름은 명확하고 모호하지 않으며 설명이 포함된 명사여야 한다.
- 파스칼 케이스를 사용한다.
  - ex) `Score`
- bool은 b접두사를 사용하지만, 두가지 상태가 아닌 명확한 상태로 표현하는 것이 좋다.
  - ex) `bDead`, `bIsDead`
- 복잡한 상태
  - 상태가 양분되지 않는다면 bool을 추가하는 것이 아닌 열거형을 사용하라.
- 블루프린트의 모든 변수 참조에는 항상 컨텍스트가 있으므로 모든 변수 이름은 해당 컨텍스트와 중복되어서는 안 된다.
  - Bad: `PlayerHealth`, `PlayerHealthValue`
  - Good: `Health`, `HealthValue`
- 아토믹(원자적) 유형의 이름을 포함하지 않는다.
  - Bad: `Int32`, `Float64`
  - Good: `Count`, `Price`
- 배열도 마찬가지로 사용하지만 복수 명사로 명명하라

##### 편집 가능한 변수

- 동작을 구성하기 위해 값을 변경해도 안전한 변수로 표시해야 한다. (Editable)
- 이런 값을 제한하기 위해Clamp나(슬라이더)

##### 카테고리

클래스에 변수 수가 적은 경우에는 범주가 필요하지 않다.

 |-- Config
 | |-- Animations
 | |-- Effects
 | |-- Audio
 | |-- Recoil
 | |-- Timings
 |-- Animations
 |-- State
 |-- Visuals

- 가변 접근 수
  - Editable변수를 공용 변수로 취급합니다 . 편집할 수 없는 변수를 보호된 변수로 처리합니다.
- 개인 변수
  - 변수는 해당 변수가 정의된 클래스 내에서만 액세스되어야 하고 자식 클래스에서는 액세스할 수 없다는 사실이 알려지지 않은 한 변수를 비공개로 표시하지 마라. (객체지향의 성격과 다른건지..?)

*이 부분은 블루프린트 사용해보고 나서 더 작성*

#### 함수, 이벤트, 이벤트 디스패처

##### 함수 명명

Functions, Events, and Event Dispatchers의 이름을 지정하는 것은 매우매우 중요하다. 다음과 같은 체크리스트가 있을 수 있다.

- 순수함수인지?
- 상태 정보를 가져오는 중인지?
- 핸들러인지?
- RPC인지?
- 목적이 무엇인지?

이러한 질문에 대한 답은 다음으로 얻을 수 있다.

- 모든 기능은 동사여야 한다
  - 모든 함수와 이벤트는 특정 형태의 작업을 수행하므로 모두 동사로 시작해야 한다. 가능하다면 현재시제로 표현해야 한다.
    - `Fire`- 캐릭터/무기 클래스에 있는 경우 캐릭터/무기라는 컨텍스트가 존재하므로 좋은 예이다. 반대로 모호한 컨텍스트라면 좋지않다.
    - `Jump`- 캐릭터 클래스에 있는 경우 좋은 예이다. 하지만 `Jump`이라는 이름은 `Jump`라는 컨텍스트가 없는 경우 좋지 않다.
    - `IsEnemy` 동사로 사용 (좋은)
    - `Dead` - 죽은건지 죽을 것인지 모호하다. (나쁜)
    - `Color` ?? 너무 모호하다.
- 속성 RepNotify 기능 항상 OnRep_Variable
  - ???
- Bool을 반환하는 정보 함수는 질문을 해야 합니다.
  - 즉, 동사형태로 Is, Was, Can 등
- 이벤트 핸드러와 디스패처는 다음으로 시작해야 한다. `On`
  - `OnDeath`, `OnRespawn`, `OnJump`, `OnFire`, `OnColorChange`
- RPC호출에는 Target이라는 접두사가 붙어야 한다.
  - `ServerFire`, `ServerRespawn`, `ServerColorChange`
- 모든 함수에는 반환 노드가 있어야 한다.
  - 모든 함수에는 반환 노드가 있어야 하며 예외는 없다.
- 어떤 함수도 50개 이상의 노드를 가질 수 없다.
  - 50개를 초과한다면 가독성과 유지 관리를 위해 더 작은 기능으로 나눠야 한다. (클린코드)
- 모든 Public함수에는 설명이 있어야 한다.
  - 모든 함수에는 설명이 있어야 하며 이 설명은 함수의 목적과 사용법을 설명해야 한다. (블루프린트와 연결)
- 모든 사용자 정의 정적 플러그인 `BlueprintCallable`기능은 플러그인 이름으로 분류되어야 합니다.
  - ??
- 블루프린트 그래프 () //TODO 블루프린트에 대한 이해부족
  - 스파게티 금지
  - 노드가 아닌 와이어 정렬
  - 흰색 Exec 라인이 최우선 순위
  - 그래프에는 합리적인 설명이 포함
  - 그래프는 적절한 경우 캐스팅 오류 처리

*아래 추가 내용은 차후에 더 작성, 블루프린트/텍스처/레벨 등등*
