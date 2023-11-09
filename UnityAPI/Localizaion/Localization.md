# Task: Unity Localization

프로젝트 초기 설정으로 활용하면 좋을 내용이라 정리 후, 적용해보고 유요한 부분들 취합하여 블로그 글 작성 예정

현지화를 위한 작업

- 여러 방법이 있지만 유니티에서 지원해주는 방법을 사용

Localization 패키지(자동으로 바인디되어 어드레서블 패키지도 같이 들어옴)

## 설정

![image](https://github.com/fkdl0048/ToDo/assets/84510455/01602966-6e51-4d3f-bd14-fd5b9043cfc8)

- CommandLine Locale Selector: 커맨드 라인에서 지역을 선택
- System Locale Selector: 해당 시스템(안드로이드에서 선택된 지역)
- **Specific Locale Selector**: 게임내에서 언어를 바꿈

Locale Generator로 사용할 언어파일을 만들 수 있음(따로 세팅)

Project Locale Identifier: 프로젝트에서 기본으로 사용할 언어 설정

Windows -> Asset Manangement -> Localization Table을 통해 테이블 설정 가능

### Table

![image](https://github.com/fkdl0048/ToDo/assets/84510455/5d5e25dc-0f3f-4190-9e7e-9b549134898a)

Localization Table을 통해 테이블을 만들면 설정된 언어에 따라 +1개의 테이블이 만들어짐(관리, 언어당 한개)

구조는 딕셔너리와 같이 Key와 Value로 이루어져 있음

각 언어에 대해서 메타데이터를 설정할 수 있고, 따로 언어의 value마다 하나씩 설정할 수 있음

### 적용 (Event)

![image](https://github.com/fkdl0048/ToDo/assets/84510455/b39d4016-a08b-4ae7-a90c-b0179ff7a98d)

string Reference를 통해 사용할 Key, Table을 설정하고 값을 직접 확인할 수 있음

해당 컴포넌트에서 Update String을 통해 변경할 String값을 이벤트로 설정가능

Runtime Only에서 -> Editor and Runtime으로 변경하면 에디터에서도 확인 가능

RunTime중에 이제는 오른쪽 위에 있는 툴바를 통해 언어를 변경 가능

오브젝트에서 직접 추가도 가능함

### Asset Table

string뿐만 아니라 지역에 따른 다른 Asset을 사용할 수 있음(sprite, audio 등)

해당 에셋의 점 세개의 설정으로 가서 rocalize를 누르면 맞는 이벤트를 생성해줌

### Localization Scene Controls

![image](https://github.com/fkdl0048/ToDo/assets/84510455/fe2473b9-9ad2-44a6-af0c-0e3bfc129e0f)

Windows -> Asset Manangement -> Localization Scene Controls

Editor단계에서도 언어를 변경하여 체크할 수 있음

**중요**

- TrackChange: 언어가 변경되었을 때 이벤트를 발생시킴

자동으로 인스펙터의 변경사항을 저장하여 이벤트로 생성해줌 즉, 에디터 단계의 직접적인 수정을 이벤트로 바인딩하여 기록

편리함

### Smart

내부 스트링에 들어가는 문자열의 경우 값을 받아와서 사용가능한 형태

"(Plaer_name)이 입장하셨습니다"와 같은 형태

만약 싱글플레이의 경우 플레이어를 전역으로 다루기 때문에 사용은 간단함

따로 variables Group을 만들어서 사용하면 된다.

![image](https://github.com/fkdl0048/ToDo/assets/84510455/d9b1742a-d98d-4bf0-95a9-66cbd1fa000f)

### Script

스크립트에서 접근하는 방법은 LocalizationSettings를 통해 접근

```c#
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleController : MonoBehaviour
{
    bool isChanging = false;

    public void ChangeLocale(int index)
    {
        if (isChanging)
            return;

        StartCoroutine(ChangeLocaleCoroutine(index));
    }
    
    IEnumerator ChangeLocaleCoroutine(int index)
    {
        isChanging = true;
        
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        
        isChanging = false;
    }
}
```

- 지역화 초기화 기간동안은 변경이 불가능하므로 기다려야함
  - 해당 값은 LocalizationSettings.InitializationOperation으로 확인 가능
- 지정된 지역은 인덱스로 접근하여 변경 가능

다음은 데이터를 가져오는 방법

```c#
LocalizationSettings.StringDatabase.GetLocalizedString("Table", "Key", "current locale");
```

위와 같이 사용은 못하지만 예시로 사용할 Table에서 Key값을 넣고 현재 지역을 넣어주면 해당 값을 가져올 수 있음

## Extentions

가장 중요한 데이터 처리부분

두가지 형태를 지원 csv, google sheet

### CSV

![image](https://github.com/fkdl0048/ToDo/assets/84510455/65f3e6d4-74c2-445c-819d-ac91fec3364c)

- CSV 파일을 만들어서 사용

사용에 매우 간편하지만 보안에 취약하고, 수정이 불편함

이후에 좀 고민해보면 좋을 문제

table자체를 성격에 따라 분리하여 csv로 관리하면 매우 편할 듯 하다.

나중에 대화 시스템을 만들게 된다면 다른 패키지와의 결합이 가장 문제일 듯

### 실제 적용

[적용하고 있는 프로젝트](https://github.com/GG-Studio-990001/GameOver)