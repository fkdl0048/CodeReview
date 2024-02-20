# 팩맘 코드 리뷰

프로젝트에서 진행한 협업자 우연님의 밀린 코드 리뷰

## SoundSystem

사운드 매니저를 싱글톤으로 두는 게 좋을지 궁금합니다.

지금 SoundSystem을 어떤 구조로 수정해야 하는지 알려주세요

저는 각 시스템마다의 정보만 인스펙터를 통해 적어주고 알아서 파싱해서 저장해두고 사용되는 스크립트에서 접근해서 사용하는 방법을 생각 중 입니다..

계속 고민해봐야할 시스템이라 어떤 방향이든 계속 생각해보겠습니다. (지금은 조금 문제가 있다고 생각)

## 코드 구조 궁금한 점 (피드백?)

인스펙터로 주입하는 방식은 같은 프리팹 내에서만 하는 게 좋은 것 같습니다.(저도 수정 예정)

다른 레벨이라면 코드를 통한 참조로 연결해보는 것도 도전해보면 좋을 것 같아요

### PMGameController

*PMGameController.cs 70번째 줄*
```cs
 private void AssignController()
        {
            pacmom.gameController = this;

            for (int i = 0; i < dusts.Length; i++)
            {
                dusts[i].gameController = this;
            }

            foreach (Transform coin in coins)
            {
                coin.GetComponent<Coin>().gameController = this;
            }

            foreach (Transform vacuum in vacuums)
            {
                vacuum.GetComponent<Vacuum>().gameController = this;
            }
        }
```

이런 DI 과정은 좋은 데 PacMom의 경우에도 이미 인스펙터로 지정된 상태이더라구요

PacMom에선 이를 인스펙터로 노출하지 않고 Controller 입력받는게 좋을 것 같은데, 이렇게 구현하신 목적이 궁금합니다!

반대로 Dust의 경우엔 런타임에 잘 할당되는 것 같아서 궁금합니다.

구조가 간단해서 Controller에서 전체를 관리한다고 하면 관리받는 대상은 관리하는 사람을 모르는 게 좋을 것 같습니다.

### PM Sprite Controller

각 Sprite 컨트롤러를 인터페이스로 만들어서 관리하면 더 좋을 것 같다.

추상적인 말이긴 한데 이 부분이 중복성을 인터페이스로 묶어서 만들어보면 도움이 되실 것 같아서 추천드립니다.

### Timer

위 내용과 같이 Timer도 마찬가지로 서로 의존성을 가지고 있는 것 보다 Gm에서 주입을 통해 넣어주는게 좋을 것 같아요

### PlayerInput

이 부분은 이후 설정창 때문에 제가 추가 작업을 통해서 Input을 하나로 둬야 합니다. (저번 작업 이어서)

++ 설정창을 구현 예정인지 궁금합니다.

### PMUIController

저는 인스펙터에 3가지 관리자를 두기보다 다 따로 네이밍을 하는게 좋다고 생각하는데 어떠신가요?

or UI쪽에 달아주는 것도 좋을 것 같아요 (성격에 맞춰서)

### Rapley

처음 public 부분 캡슐화되면 좋을 것 같습니다. (movement안쪽처럼)

## 정리

전체적으로 서브 시스템이라 구조를 탄탄하게 굳이 가져가지 않아도 좋지만, 지금처럼 리팩터링을 통해 이후 작업할 시스템에 대해서 좋은 구조를 고민해보는 것도 좋은 것 같습니다.