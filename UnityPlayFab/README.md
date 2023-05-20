# Unity GBass Playfab

[플레이 팹 설명](https://fkdl0048.github.io/unity/unity_in_PlayFab/)
[플레이 팹 사용법](https://fkdl0048.github.io/unity/unity_in_PlayFab_1/)

플레이 팹에 대한 설명, 사용법은 위 링크 참고

코드에 대한 리뷰만 작성

가벼운 학교 프로젝트라 Playfab API 맛보기 정도와 UI 아키텍처 정도만 구현하며 공부했다.

![image](https://github.com/fkdl0048/ToDo/assets/84510455/402369d2-aa8d-408c-b998-0da88d92700f)

MVC모델로 사용했으며 다른 프로젝트도 차차 리뷰하겠지만 MVVM이나 MVP도 적용해보는 중이다.

가장 적합한 모델은 MVP가 가장 적합하고 가벼운 것 같고 좀 더 Reative한 코드는 MVVM을 UniRX와 Zenject를 사용하여 구현한게 더 보기 좋은 것 같다.

해당 내용은 포스팅 예정..

플레이 팹 코드 리뷰인데 사족이 긴 느낌..

```cs
    public void SellItem(string itemID)
    {
        string itemInstanceId = null;
        int price = 0;
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), result =>
        {
            var inventory = result.Inventory;
            foreach (var item in inventory)
            {
                if (item.ItemId == itemID)
                {
                    itemInstanceId = item.ItemInstanceId;
                    price = (int)item.UnitPrice;
                }
            }
            
            var request = new ConsumeItemRequest() {ConsumeCount = 1, ItemInstanceId = itemInstanceId};
            PlayFabClientAPI.ConsumeItem(request, result =>
            {
                SetPlayerMoney(price);
                
                var popup = GameManager.UI.UINavigation.PopupPush("DefalutPopup") as DefalutPopup;
                popup.SetText("판매 성공!");
                OnUpdateMoney?.Invoke();
            }, error =>
            {
                var popup = GameManager.UI.UINavigation.PopupPush("DefalutPopup") as DefalutPopup;
                popup.SetText("판매 실패!");
            });
            
        }, LogFailure);
    }
```

대부분의 코드가 위처럼 작성되어 있기 때문에 비슷하다고 생각된다.

가장 아쉬운 부분은 Action으로 동기 처리를 해결하긴 했지만 asny/awati도 많이 사용하면서 해보고 싶었는데 지원하지 않아서 아쉬웠다..

API자체를 깔끔하게 자는 방법을 생각해 봤는데 지금처럼 말고 해당 메서드들 중 사용할 메서드만 뽑아서 클래스로 만들어서 제공하는게 좀 더 유연할 것 같다는 생각이 든다.

노출되지 않게 네임스페이스로 가리고 사용할 메서드만 최소 기능들만 노출시켜 인자로 받아 사용하는 방법,, 지금 코드는 너무 더럽다는 느낌이 강하다.

또한 해당 클래스로 묶어두면 테스트 코드를 짜기도 훨씬 쉬울 것 같다는 생각이 든다.

API 자체가 변화가 많이 되면서 영어의 필요성, 그리고 공식문서의 중요성을 다시한번.. 대부분의 블로그에 있는 API함수가 더이상 사용되지 않는 경우가 많았다.

그리고 플레이 팹의 경우에는 테스트 코드를 짜기가 쉽지 않다.  

실제 계정이 등록되어 있는지 확인하는 메서드나 그런 일련의 과정인 Test코드가 원 코드보다 더 덩치가 커지는.. 그렇지만 실제 CleanCode에서는 그런 부분이 맞다고 하니.. 짜볼까?? 하다가 너무 귀찮기도 하고.. 지금 진행중인 프로젝트도 너무 많아서 진행중인 프로젝트에 TDD를 적극도입하는 걸로 합의..!
