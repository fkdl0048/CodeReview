# TDD..?

TDD라는 말을 처음 접한다면 관련된 책을 읽어보지 않았을 경우가 가장크다.

웹앱과 달리 게임개발은 좀 더 폐쇄적이라고 생각하기도 하고 개발풀도 적다보니 실제 대학생레벨에서 TDD를 실제로 사용하고 개발하는 사람은 본적 없는 것 같다.

테스트코드 또한..

나도 이번 프로젝트에 TDD는 처음 연습겸..? 사용해보는 것 같고 평소엔 기능 개발 후 간단한 유닛테스트만 작성했다.

TDD에 대한 궁금증도 있었고, 이를 맹신하고 강요하는 사례도 들어봐서 조금 걱정이 되긴 하지만 해보지도 않고 평가하는 것은 맹신하는 것과 똑같기 때문에 직접 해보고 내 생각을 정리해보려고 한다.

**아래부턴 TDD나 MVP에 관한 정리이지만 내가 정리한 글보다 검색하여 이해하는 것이 더 좋은 정보일 수 있다.**

TDD를 간단하게 요약하자면 구현 코드가 작성되기 전에 테스트를 먼저 작성함으로써 무엇을 프로그래밍 해야할지 파악하고 이미 구현된 내용을 깨뜨리지 않는지 확인하기 위한 개발 방법론이다.

가능한 최소한의 구현으로 시스템이 동작하게 만들고 필요한 경우 리팩터링을 수행하며 애플리케이션을 개발해나간다.

## TTD를 해야하는 이유

- 자동화된 테스트를 통해 코드에 대한 믿음이 생겨 쉽게 리팩터링할 수 있다는 자신감을 갖게 된다.
- 이는 문서의 역할을 하게 되어 새로운 개발자가 팀에 합류하게 되면 테스트를 통해 모든 기능의 작동 및 요구 사항을 이해할 수 있다.
- 버그가 발생에 대한 두려움이 없어진다.
- 코드에 너무 많은 결합이 있는지 쉽게 파악할 수 있다.

이런 이점이 있지만 이를 이해하기 위해선 객체지향적 설계와 실제로 TDD를 한번 체험해보면 도움이 된다.

*이런 이점이 있지만, 사실 제대로 적용하기 위해선 철저한 객체지향적 설계를 필요로 한다.*

## Unity에서 TDD를 사용하는 법

![image](https://github.com/BRIDGE-DEV/BRIDGE_Archive/assets/84510455/096bd350-d457-4c16-a355-d435cd700741)

쉽게 단위 테스트를 먼저 C#레벨에서 본다면 NUnit프레임워크를 사용하여 Mono라이브러리가 아닌 C#라이브러리로 테스트할 수 있다.

나아가 Mono에 관한 테스트도 Unity TestRunner를 통해 지원하기 때문에 실상 테스트하기 어려운 비동기나 싱글톤으로 구현되어 있지 않다면 유니티에서 모든 동작에 대한 테스트가 가능하다.

*하지만 게임 특성 상 불가피하게 QA를 통해서만 발견되는 문제가 있긴 하다.

## Test 코드 작성

위에서 말한 Presenter에 대한 테스트코드를 먼저 작성해보자.

```cs
using NUnit.Framework;

namespace Tests.Editor
{
    [TestFixture]
    public class SettingsUIPresenterTests
    {
        [Test]
        public void WhenMusicVolumeChangesThenMusicVolumeIsSet()
        {
            // Given
            var settingsData = new SettingsData();
            var settingsUIPresenter = new SettingsUIPresenter(settingsData);
            
            // When
            settingsUIPresenter.SetMusicVolume(0.5f);
            
            // Then
            Assert.AreEqual(0.5f, settingsData.MusicVolume);
        }
    }
}
```

먼저 가장 간단한 기능 테스트인 음악 볼륨 조절에 관한 테스트를 작성했다.

*실제로는 빨간줄이 그어져 있다. 테스트만 작성했을 뿐 클래스를 만들지 않았기 때문*

과연 작성하면서 어디까지 생각하고 만들어야 할까에 대한 고민이 많았는데, 다른 사람들의 작성법이나 책에서도 볼 수 있듯이 먼저 요구사항에 맞게 테스트를 작성하고 살을 붙여나가는 방식으로 진행해본다.(여기서 살이란, 클래스와 테스트 코드)

위 기능을 테스트하기 위해 먼저 SettingsData라는 클래스와 SettingsUIPresenter라는 클래스를 만들어야 한다.

```cs
using System;

namespace Runtime.Common.Domain
{
    public class SettingsData
    {
        public float MusicVolume
        {
            get => _musicVolume;
            set { Math.Clamp(value, 0, 1); _musicVolume = value; }
        }
        
        private float _musicVolume;
    }
}
```

```cs
using Runtime.Common.Domain;

namespace Runtime.Common.Presentation
{
    public class SettingsUIPresenter
    {
        private readonly SettingsData _settingsData;

        public SettingsUIPresenter(SettingsData settingsData)
        {
            _settingsData = settingsData;
        }

        public void SetMusicVolume(float volume)
        {
            _settingsData.MusicVolume = volume;
        }
    }
}
```

이렇게 요구사항에 맞는 클래스를 생성하여 테스트에 충족될 수 있게 설계한다.

![image](https://github.com/BRIDGE-DEV/BRIDGE_Archive/assets/84510455/8756e36f-86f1-4f8e-80d8-0c32470dd061)

위는 유니티 툴내에 TestRunner를 통해 테스트를 진행한 결과이다.

이후에 런타임이나 PlayMode, 에디터 작업 등 IDE작업 중에 활용하면 좋지만, 지금과 같이 단순 C#레벨의 코드 작성은 IDE에서 진행하는 것이 효율적이다.

![image](https://github.com/BRIDGE-DEV/BRIDGE_Archive/assets/84510455/252a8515-6a69-44f6-975f-871b52292cd1)

TestRunner나 유니티내 어셈블리에 관한 내용은 과거에 정리한 글이 있어서 첨부한다.

[TestRunner 정리글](https://fkdl0048.github.io/unity/unity_in_TestRunner/)

이제는 SettingUIVIew를 만들어서 Presenter와 하여 1:1 관계를 만들어 본다.

여기서 View는 이후에 Canvas안에 MonoBehaviour를 상속받은 클래스로 만들어서 사용할 것이기 때문에 사실은 생성자의 형태로 사용 불가능하다.

하지만 지금은 구조를 잡는 과정이기에 C#클래스로 작성 후 이후에 변경한다.

먼저 View를 테스트하기 위해서는 NSubstitute를 사용하여 View를  Mocking해야 한다.

쉽게 말해서 흉내내는 객체를 만드는 것이다.

```cs
using NSubstitute;
using NUnit.Framework;
using Runtime.Common.Domain;
using Runtime.Common.Presentation;
using Runtime.Common.View;
using UnityEngine;

namespace Tests.Editor
{
    [TestFixture]
    public class SettingsUIPresenterTests
    {
        [Test]
        public void WhenMusicVolumeChangesThenMusicVolumeIsSet()
        {
            // Given
            var settingsData = new SettingsData();
            var settingsUIView = Substitute.For<SettingsUIView>();
            var settingsUIPresenter = new SettingsUIPresenter(settingsUIView, settingsData);
            
            // When
            settingsUIPresenter.SetMusicVolume(0.5f);
            
            // Then
            Assert.AreEqual(0.5f, settingsData.MusicVolume);
        }
        
        [Test]
        public void WhenMusicVolumeChangesThenMusicVolumeIsSetOnView()
        {
            // Given
            var settingsData = new SettingsData();
            var settingsUIView = Substitute.For<SettingsUIView>();
            var settingsUIPresenter = new SettingsUIPresenter(settingsUIView, settingsData);
            
            // When
            settingsUIPresenter.SetMusicVolume(0.5f);
            
            // Then
            settingsUIView.Received(1).SetMusicVolume(0.5f);
        }
    }
}
```

View가 추가되면서 테스트 코드도 변경되었다.

첫 번째 테스트코드는 Model과 Presenter간의 테스트를 하기 위한 것이고, 두 번째 테스트코드는 View와 Presenter간의 테스트를 위한 것이다.

여기서 사용된 `Substitute.For<SettingsUIView>()`는 SettingsUIView를 흉내내는 객체를 만들어서 사용한다.

Then의 Received는 해당 함수가 호출되었는지를 확인하는 것이다. (인자가 1이라 한번 확인)

즉, 잘 연결되어 있다면 When에서 Presenter에서 실행한 SetMusicVolume이 View에서도 실행되어야 한다.

이제 View를 만들어보자.

```cs
namespace Runtime.Common.View
{
    public class SettingsUIView
    {
        public virtual void SetMusicVolume(float volume)
        {
            
        }
    }
}
```

현재는 테스트를 위한 클래스이기 때문에 실제로는 아무것도 하지 않는다.

이제 테스트를 통과하기 위해 Presenter를 수정한다.

```cs
using Runtime.Common.Domain;
using Runtime.Common.View;

namespace Runtime.Common.Presentation
{
    public class SettingsUIPresenter
    {
        private readonly SettingsUIView _settingsUIView;
        private readonly SettingsData _settingsData;

        public SettingsUIPresenter(SettingsUIView settingsUIView, SettingsData settingsData)
        {
            _settingsUIView = settingsUIView;
            _settingsData = settingsData;
        }

        public void SetMusicVolume(float volume)
        {
            _settingsUIView.SetMusicVolume(volume);
            _settingsData.MusicVolume = volume;
        }
    }
}
```

![image](https://github.com/BRIDGE-DEV/BRIDGE_Archive/assets/84510455/597e6713-308c-4a11-9aba-b3ef262bdd01)

테스트가 통과되었고, 이제 어느정도 구조가 잡혔기 때문에 리팩터링을 통해 코드를 정리한다.

현재 테스트 코드에서 반복되는 부분인 Given을 SetUp으로 변경하고, 테스트 코드를 분리한다.

```cs
using NSubstitute;
using NUnit.Framework;
using Runtime.Common.Domain;
using Runtime.Common.Presentation;
using Runtime.Common.View;

namespace Tests.Editor
{
    [TestFixture]
    public class SettingsUIPresenterTests
    {
        private SettingsUIView _settingsUIView;
        private SettingsData _settingsData;
        private SettingsUIPresenter _settingsUIPresenter;
        
        [SetUp]
        public void SetUp()
        {
            _settingsUIView = Substitute.For<SettingsUIView>();
            _settingsData = new SettingsData();
            _settingsUIPresenter = new SettingsUIPresenter(_settingsUIView, _settingsData);
        }
        
        [Test]
        public void WhenMusicVolumeChangesThenMusicVolumeIsSet()
        {
            _settingsUIPresenter.SetMusicVolume(0.5f);

            Assert.AreEqual(0.5f, _settingsData.MusicVolume);
        }
        
        [Test]
        public void WhenMusicVolumeChangesThenMusicVolumeIsSetOnView()
        {
            _settingsUIPresenter.SetMusicVolume(0.5f);
            
            _settingsUIView.Received(1).SetMusicVolume(0.5f);
        }
        
        [TearDown]
        public void TearDown()
        {
            _settingsUIView = null;
            _settingsData = null;
            _settingsUIPresenter = null;
        }
    }
}
```

SetUp애트리뷰트는 테스트가 실행되기 전에 실행되는 메서드이고, TearDown은 테스트가 끝난 후 실행되는 메서드이다.

구조를 정리했다면 이제 테스트 코드를 요구사항에 맞게 더 추가하면 된다.

원하는 요구사항에 대한 테스트코드가 다 작성이 되었다면 이제 View의 메서드를 구현하고 실제 유니티에 적용하는 단계이다.

먼저 View를 유니티 씬에 attach시켜야 하기에 MonoBehaviour를 상속받은 클래스로 만든다.

각 컴포넌트와 연결 그리고 이벤트 메서드를 통해 초기화 과정(Presenter 바인딩)을 진행한다.

### 구조 개선

생각보다 MonoBehaviour구조 때문에 되게 애를 먹었다.

크게 발생한 문제는 다음과 같다.

- MonoBehaviour를 상속받은 클래스는 생성자를 사용할 수 없기 때문에 작성한 테스트 코드가 수정이 되어야 한다.
- NSubstitute를 사용하여 테스트를 진행하는 개념이 생각보다 더 어려웠다.
  - 흉내내는 객체는 Received를 사용하여 해당 메서드에 대한 메서드 실행 여부를 판단한다.
  - 자료가 적어서 실제 View에서 Virtual로 선언하여 사용해야 함을 몰랐다. 좀 더 알아보니 인터페이스를 사용하는 것이 더 좋은 방법 같다.
- 설정 정보의 경우 데이터가 저장되어야 하기 때문에 어드레서블을 사용하였는데 비동기라 테스트가 어려웠다.
  - 동기화를 위해 `WaitForCompletion`키워드를 사용했다.
  - 좀 더 좋은 방법으론 yield return을 사용하여 코루틴을 사용하여 (PlayMode에서) 테스트를 진행하는 것이 좋을 것 같다.
- 음향의 경우 view -> Presenter -> Model -> Presenter -> View로 이동하는데 조금 무의미한 작업을 하게 되는? 문제점이 있다.

이 외에도 "이거 왜 안돼?" 이슈로.. 작업이 되게 늘어지면서 사실상 TDD는 실패했다고 봐도 무방하다.

뒤에 후기에 적겠지만, TDD를 잘 하려면 테스트 코드부터 많이 짜봐야 할 것 같다.

구성된 최종은 다음과 같다.

```cs
using Runtime.Common.Presentation;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Common.View
{
    public class SettingsUIView : MonoBehaviour
    {
        private SettingsUIPresenter _presenter;
        
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;

        private void Start()
        {
            _presenter = PresenterFactory.CreateSettingsUIPresenter(this);

            musicVolumeSlider.onValueChanged.AddListener(OnSliderMusicValueChanged);
            sfxVolumeSlider.onValueChanged.AddListener(OnSliderSfxValueChanged);
        }

        public virtual void SetViewMusicVolume(float volume)
        {
            musicVolumeSlider.value = volume;
        }
        
        public virtual void SetViewSfxVolume(float volume)
        {
            sfxVolumeSlider.value = volume;
        }
        
        private void OnSliderMusicValueChanged(float value)
        {
            _presenter.SetMusicVolume(value);
        }
        
        private void OnSliderSfxValueChanged(float value)
        {
            _presenter.SetSfxVolume(value);
        }
    }
}
```

```cs
using Runtime.Common.Domain;
using Runtime.Common.View;

namespace Runtime.Common.Presentation
{
    public class SettingsUIPresenter
    {
        private readonly SettingsUIView _settingsUIView;
        private readonly SettingsData _settingsData;
        
        public SettingsData SettingsData => _settingsData;

        public SettingsUIPresenter(SettingsUIView settingsUIView, SettingsData settingsData)
        {
            _settingsUIView = settingsUIView;
            _settingsData = settingsData;
            
            _settingsUIView.SetViewMusicVolume(_settingsData.MusicVolume);
            _settingsUIView.SetViewSfxVolume(_settingsData.SfxVolume);
        }

        public void SetMusicVolume(float volume)
        {
            _settingsData.MusicVolume = volume;
            _settingsUIView.SetViewMusicVolume(_settingsData.MusicVolume);
        }
        
        public void SetSfxVolume(float volume)
        {
            _settingsData.SfxVolume = volume;
            _settingsUIView.SetViewSfxVolume(_settingsData.SfxVolume);
        }
    }
}
```

```cs
using Runtime.Common.Domain;
using Runtime.Common.View;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Runtime.Common.Presentation
{
    public static class PresenterFactory
    {
        public static SettingsUIPresenter CreateSettingsUIPresenter(SettingsUIView view)
        {
            return new SettingsUIPresenter(view, Addressables.LoadAssetAsync<SettingsData>("SettingsData").WaitForCompletion());
            //return new SettingsUIPresenter(view, Resources.Load<SettingsData>("SettingsData"));
        }
    }
}
```

```cs
using System;
using UnityEngine;

namespace Runtime.Common.Domain
{
    [CreateAssetMenu(fileName = "SettingsData", menuName = "ScriptableObject/SettingsData", order = 0)]
    public class SettingsData : ScriptableObject
    {
        [SerializeField][Range(0, 1)] private float musicVolume;
        [SerializeField][Range(0, 1)] private float sfxVolume;
        
        public float MusicVolume
        {
            get => musicVolume;
            set { Math.Clamp(value, 0, 1); musicVolume = value; }
        }
        
        public float SfxVolume
        {
            get => sfxVolume;
            set { Math.Clamp(value, 0, 1); sfxVolume = value; }
        }
    }
}
```

테스트 코드는 다음과 같다.

```cs
using NSubstitute;
using NUnit.Framework;
using Runtime.Common.Domain;
using Runtime.Common.Presentation;
using Runtime.Common.View;
using UnityEngine;

namespace Tests.Editor
{
    [TestFixture]
    public class SettingsUIPresenterTests
    {
        private SettingsUIView _view;
        private SettingsData _model;
        private SettingsUIPresenter _presenter;
        
        [SetUp]
        public void SetUp()
        {
            _view = Substitute.For<SettingsUIView>();
            _model = ScriptableObject.CreateInstance<SettingsData>();
            _presenter = new SettingsUIPresenter(_view, _model);
            
            _model.MusicVolume = 0.5f;
            _model.SfxVolume = 0.5f;
        }
        
        [Test]
        public void CheckInitialValue()
        {
            Assert.AreEqual(0.5f, _model.MusicVolume);
            Assert.AreEqual(0.5f, _model.SfxVolume);
        }
        
        [Test]
        public void WhenMusicVolumeChangesViewIsUpdated()
        {
            _presenter.SetMusicVolume(0.75f);
            _view.Received(1).SetViewMusicVolume(0.75f);
        }
        
        [Test]
        public void WhenSfxVolumeChangesViewIsUpdated()
        {
            _presenter.SetSfxVolume(0.3f);
            _view.Received(1).SetViewSfxVolume(0.3f);
        }
        
        [Test]
        public void WhenMusicVolumeChangesModelIsUpdated()
        {
            _presenter.SetMusicVolume(0.75f);
            Assert.AreEqual(0.75f, _model.MusicVolume);
        }
        
        [Test]
        public void WhenSfxVolumeChangesModelIsUpdated()
        {
            _presenter.SetSfxVolume(0.3f);
            Assert.AreEqual(0.3f, _model.SfxVolume);
        }
        
        [Test]
        public void WhenMusicVolumeChagesEqualModelAndPresenter()
        {
            _model.MusicVolume = 0.31f;
            Assert.AreEqual(_presenter.SettingsData.MusicVolume, _model.MusicVolume);
        }
        
    }
}
```

![image](https://github.com/BRIDGE-DEV/BRIDGE_Archive/assets/84510455/cdd7fbb6-565f-44ff-8e9f-379902d068e6)

![image](https://github.com/BRIDGE-DEV/BRIDGE_Archive/assets/84510455/2ef16099-ab49-4883-9a46-1b21b0d84042)

## 정리

테스트 코드는 이미 프로젝트에서 작업해본 경험이 있어서 그렇게 큰 모험은 아니였지만, TDD자체는 많이 어색했던 것 같다.

경험 부족, 좀 더 구조를 멀리 보지 못한 것?, 유니티 특성 이슈 등등..

중간부터는 TDD를 포기하고 계속 리팩터링하며 버그를 잡았다.

사실 버그부분은 경험이 적어서 유니티 특성이나 Nsubstitute에 대한 이해가 부족했던 것 같다.

과거 `좋은 코드 나쁜 코드`에서 페이크, 목, 스텁에 대한 내용을 정리했는데, 이번 내용을 통해 좀 시야가 넓어진 것 같아서 기분은 좋다.

정리하자면 TDD를 하기 이전에 OPP에 대한 이해가 필요하고, 테스트 코드를 작성하는 것에 대한 경험이 필요하다. (생각보다 많이.)

작성한 코드는 실제 프로젝트에 적용되는 코드이기에 계속 개선해 나가면서 구조화할 생각이다.

이후에 사용되는 UI들도 위와 같은 형태를 유지하면서 개발해보려고 한다.