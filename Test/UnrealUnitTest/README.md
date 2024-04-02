# 언리얼 테스트 코드

유니티도 그러하듯 언리얼도 따로 테스트 프레임워크를 구성해놨다. 이름은 `Automation Technical`라고 부르며 사용 후기는 개인적으로 유니티 `Test Runner`보다 더 편리하다고 생각한다. 더 모듈로 분리가 가능하고, 작성도 편리하며 확장성도 더 넓다.

또한 이런 테스트 자체를 자동화할 수 있다는 점이 매력적이다. CI/CD과정에 테스트를 자동화하고 이를 빌드까지 뽑아내는 과정을 따로 만질 필요가 없다고 느껴졌지만,, 언리얼 자체의 자료가 적어서인지 테스트 코드 관련 자료를 찾기가 매우매우 어려웠다.

## Automation Technical

가장 먼저 언리얼 에디터에서 테스트 코드를 확인할 수 있는 윈도우를 찾아야 하는데, 이는 `Unreal (Session) Frontend`라는 곳에 위치해 있다. 과거 Unreal4 버전에서는 Windows > Test관련으로 분류가 되어 있었지만 지금은 Tools > Automation으로 분류가 되어 있다. **(참고..)**

![image](https://github.com/fkdl0048/CodeReview/assets/84510455/526a1c92-5bb8-4207-995e-220b1b868396)

가장 자세한 내용은 공식문서를 읽는 것이 좋지만 항상 그러하듯 [공식문서](https://dev.epicgames.com/documentation/ko-kr/unreal-engine/automation-system-user-guide-in-unreal-engine)는 불친절하다. *사실 불친절하기 보다 모든 내용을 다룰 수 없고, 버전은 항상 올라가니 업데이트가 힘들다는 것을 잘 안다..*

유닛 테스트는 따로 JUnit, XUnit처럼 다른 테스트프레임워크를 사용할 수 있지만, 그건 `C++`언어 수준과 IDE에 종속되기에 언리얼 자체의 테스트 프레임워크를 사용하는 것이 좋다. 그러기 위해선 플러그인을 깔아야 하는데 해당 내용도 공식문서에 잘 나와 있다.

![image](https://github.com/fkdl0048/CodeReview/assets/84510455/56333fb9-78a7-48bf-a8c1-3d31c0bbc8b0)

플러그인도 `Testing`탭에 있는 관련 플러그인 중 선택하여 설치하면 된다. 공식문서에서 필수적으로 말하는 `Automation`과 `TestFramework`, `Runtime Tests`를 설치하면 된다. 이 외에 에디터나 블루프린트에 사용할 것이라면 추가로 설치하면 되는 것 같다.

세션브라우저를 통해 `Start`를 눌러 내가 지정한 테스트 코드를 실행할 수 있게 되는데, 이외에도 Unreal 자체에서 Engine자체에 제작한 테스트 코드들도 볼 수 있다.

![image](https://github.com/fkdl0048/CodeReview/assets/84510455/e6c9d2f0-bdda-4f20-9e0e-587c48251dea)

```cpp
TEST_CASE_NAMED(FUniqueTest, "System::Core::Algo::Unique", "[ApplicationContextMask][SmokeFilter]")
{
 using namespace Algo;

 {
  TArray<int32> Array;
  int32 RemoveFrom = Unique(Array);
  CHECK_MESSAGE(TEXT("`Unique` must handle an empty container"), RemoveFrom == 0);
 }
 {
  TArray<int32> Array{ 1, 2, 3 };
  Array.SetNum(Unique(Array));
  CHECK_MESSAGE(TEXT("Uniqued container with no duplicates must remain unchanged"), (Array == TArray<int32>{1, 2, 3}));
 }
 {
  TArray<int32> Array{ 1, 1, 2, 2, 2, 3, 3, 3, 3 };
  Array.SetNum(Unique(Array));
  CHECK_MESSAGE(TEXT("`Unique` with multiple duplicates must return correct result"), (Array == TArray<int32>{1, 2, 3}));
 }
 {
  TArray<int32> Array{ 1, 1, 2, 3, 3, 3 };
  Array.SetNum(Unique(Array));
  CHECK_MESSAGE(TEXT("`Unique` with duplicates and unique items must return correct result"), (Array == TArray<int32>{1, 2, 3}));
 }
 {
  FString Str = TEXT("aa");
  Str = Str.Mid(0, Unique(Str));
  CHECK_MESSAGE(TEXT("`Unique` on `FString` as an example of arbitrary random-access container must compile and return correct result"),
   Str == FString(TEXT("a")));
 }
 {
  int32 Array[] = { 1 };
  int32 NewSize = (int32)Unique(Array);
  CHECK_MESSAGE(TEXT("`Unique` must support C arrays"), NewSize == 1);
 }
 {
  TArray<int32> Array = { 1, 1 };
  int32 NewSize = Unique(MakeArrayView(Array.GetData() + 1, 1));
  CHECK_MESSAGE(TEXT("`Unique` must support ranges"), NewSize == 1);
 }
}
```

실제 엔진 내부 `Core`의 `Algo`에 있는 `Unique`함수를 테스트하는 코드이다. *이와 같이 메서드나 해당 구조에 대해 걱정되는, 세부사항적인 부분들은 차라리 테스트 코드를 보는 것이 효율적일 수 있다.*

## 테스트 코드 작성

테스트 코드 작성에 관련된 것은 [공식문서](https://dev.epicgames.com/documentation/ko-kr/unreal-engine/automation-technical-guide)를 보면 되지만, 나는 이문서를 보고 이해하기 까지 한참 걸렸다.. 아마 엔진에 대한 미숙함도 있겠지만, 과거 블로그 글이나 실제 사용 사례를 봤을 때 테스트 프레임워크도 많이 달라져서 그런 것 같다고 느꼈다.

간단하게 정리하자면, 테스트 코드는 따로 모듈로써 분리하는 것이 좋긴 하겠지만(유니티의 어셈블리*namespace*, 언리얼은 Module로 분리) 나는 간단한 내용이라 공식문서에 말하는 것 처럼 해당 폴더에 Tests로 구분했다. 즉, 의존성을 생각하지 않고 각 클래스마다 1대1 매칭을 기본으로 한다.

따라서 `[ClassFilename]Test.cpp`이 기본적인 룰이며 `SimpleComponent`에 대한 테스트 코드라면 `SimpleComponentTest.cpp`로 작성한다. 특이한 점은 Unreal에서 생성하지 않고, 따로 IDE에서 작성한다. (UnitTest) 즉 행위에 집중되기에 헤더파일도 필요하지 않다.

다음은 하나의 단위 테스트마다 매크로를 지정해주는데, Simple(단순) 과 Complex(복합)유형으로 구분한다. 나도 아직 복합은 해보지 않았지만 영상이나 예제를 봤을 때, 유니티에선 컴포넌트 단위의 테스트라고 보면 될 것 같다.

매크로에서 단순과 복합을 구분하여 `IMPLEMENT_SIMPLE_AUTOMATION_TEST` 매크로로 선언하는 반면, 복합 테스트는 `IMPLEMENT_COMPLEX_AUTOMATION_TEST`을 선언헌다. 각각 `TClass`이름과 `PrettyName`이름을 선언하여 사용하고 `TFlags`는 자동화 테스트 조건 및 동작을 지정하는데 사용한다.

```cpp
 IMPLEMENT_SIMPLE_AUTOMATION_TEST(FPlaceholderTest, "TestGroup.TestSubgroup.Placeholder Test", EAutomationTestFlags::EditorContext | EAutomationTestFlags::EngineFilter)

 bool FPlaceholderTest::RunTest(const FString& Parameters)
 {
  // true 를 반환하면 테스트 통과, false 를 반환하면 테스트 실패입니다.
  return true;
 }
```

공식 문서에서 제공하는 예제이며, 내가 가장 어려웠던? 헷갈린 부분은 `TClass`이름과 `PrettyName`이 자율형식이라는 점이다.. 나는 헤더를 만들거나 TestBase의 형식을 따라가야 하는줄 알았는데, 그냥 내가 원하는 형식으로 작성하면 된다. 이는 해당 테스트 cpp에서 계층 구조를 가지거나 구분하기 위해서 사용한다.

즉, 이후에 `Automation Frontend`에서 테스트를 실행할 때 구분되어 나타나게 된다. 이는 나중에 테스트 코드가 많아지면 유용하게 사용될 것 같다.

다른 예제에선 코루틴이나 실제 런타임에서 어느정도 쓰레드를 멈추기 위한 코드도 있어서 이해는 잘 된 것 같다.

### 실습

나는 아주아주 간단한 테스트를 준비했다. `SimpleComponent`라는 클래스를 만들고, 이 클래스가 동작하는 함수를 테스트하는 간단한 코드다.

```cpp
// SimpleComponent.h
#pragma once

#include "CoreMinimal.h"
#include "Components/ActorComponent.h"
#include "SimpleComponent.generated.h"

UCLASS( ClassGroup=(Custom), meta=(BlueprintSpawnableComponent) )
class PRACTICETESTCODE_API USimpleComponent : public UActorComponent
{
 GENERATED_BODY()

public:
 bool bDidSomething = false;

 void DoSomething();
};
```

```cpp
// SimpleComponentTest.cpp
#include "SimpleComponent.h"

void USimpleComponent::DoSomething()
{
 bDidSomething = true;
}
```

```cpp
// SimpleComponentTest.cpp
#include "Misc/AutomationTest.h"
#include "Tests/AutomationEditorCommon.h"
#include "PracticeTestCode/SimpleComponent.h"
#include "Tests/AutomationCommon.h"

constexpr int32 TestFlags = (EAutomationTestFlags::EditorContext | EAutomationTestFlags::EngineFilter);

IMPLEMENT_SIMPLE_AUTOMATION_TEST(DummyTest, "Tests.DummyTest", TestFlags);
bool DummyTest::RunTest(const FString& Parameters)
{
 return true;
}

IMPLEMENT_SIMPLE_AUTOMATION_TEST(DoSomething, "Tests.DoSomething", TestFlags);
bool DoSomething::RunTest(const FString& Parameters)
{

 USimpleComponent* Comp = NewObject<USimpleComponent>();

 Comp->DoSomething();

 ADD_LATENT_AUTOMATION_COMMAND(FEngineWaitLatentCommand(2.0f));
 //TestFalse("Did something", Comp->bDidSomething);
 TestTrue("Did something", Comp->bDidSomething);
 
 return true;
}
```

이렇게 작성하고 `Automation Frontend`에서 실행하면, `DoSomething`함수가 실행되고 `bDidSomething`이 `true`로 변경되고 2초간의 대기 후 테스트 성공을 반환한다.

![image](https://github.com/fkdl0048/CodeReview/assets/84510455/d58ffe2c-29e5-4939-b52f-5d54b89e8f1d)

## 테스트 코드에 대한 생각

테스트 코드에 관련된 책이나 객체지향책을 많이 접하다 보면 테스트 코드의 중요성을 알 수 있는데, 과연 100%에 달하는 테스트 코드를 작성하는 것이 과연 바람직할까? *높은 테스트 커버리지가 좋은 소프트웨어를 보장하진 않는다.* 따라서 스스로 필요하다고 생각하는 부분만 작성하는 것이 바람직하다.

테스트가 필요 없는 항목이나 작성하지 않아도 되는 부분까지 테스트 코드를 작성해가면서 코드를 짤 필요는 없다. 오히려 독이 되는 것 같다고 생각한다. (실제로 경험한 듯 하다..) 따라서 스스로 생각하기에 설계 측면에서 테스트 코드가 필요한 부분, 예를 들어 다형성이 필요하거나 코드 자체로 문서를 남겨야 하거나, 복잡한 로직을 단일 Task로 보고싶은 부분에 대해서 작성해보는 것이 좋다.

## 참고

- [자동화 테크니컬 가이드 (공식문서)](https://docs.unrealengine.com/4.27/ko/TestingAndOptimization/Automation/TechnicalGuide/)
- [자동화 시스템 사용자 가이드 (공식문서)](https://dev.epicgames.com/documentation/ko-kr/unreal-engine/automation-system-user-guide-in-unreal-engine)
