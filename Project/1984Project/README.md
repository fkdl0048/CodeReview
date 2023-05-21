# 1984 Project

브릿지 정규 프로젝트

[프로젝트 깃헙](https://github.com/The-Day-In-1984/1984_Project)

기획 2, 플밍 3, 아트 1, 사운드 1로 구성된 팀 프로젝트

PM, 플밍으로 참여했고 전체적인 게임 개발 플로우를 담당했다.

## 프로젝트 스케줄링

![image](https://github.com/fkdl0048/ToDo/assets/84510455/a2b486f2-f808-4d1c-b2ea-4749b476e1ea)

게임 개발 프로세스를 애자일방식을 많이 참고하여 최대한 비동기 방식으로 각자 작업 시간에 맞춰서 개발을 진행한다.

*아트가 기획에게 궁금한 내용을 멘션을 걸면 기획은 자신이 작업하는 시간에 답변이 가능하기 때문에 최선의 답을 준다.*

이런 비동기 프로세스 작업에서 가장 유리하다고 생각한 부분이 깃헙 해당 레포였으므로 이슈와 디스커션을 통해 의사소통을 진행

![image](https://github.com/fkdl0048/ToDo/assets/84510455/c03ef7ba-0f9c-4cd7-90cc-afa8ebd50775)

실제 아트의 대한 여러 인원의 피드백

각자의 작업 기한, 분량에 대한 객관적인 지표 + 일정관리가 가능해짐

실제 작업 레포이기 때문에 실시간 빌드 및 아트, 사운드, 기획의 확인/추가 등이 가능해지고 쉽게 링크를 걸어서 편리함이 아주 큼..!

이런 구조는 내가 실제 TODO를 작성하는 방식에서 차용했으며, 다른 프로젝트에도 그대로 진행중이다.

## 코드 

담당한 부분은 UI, 게임 로직, 매니저, 임무, 시스템 등이다.

### 임무

프로젝트에서 가장 간단한 부분이라 질 좋은 코드를 만들고 싶었다.  

여러 고민 중에 동작과 기능을 분리하여 유연한 구조를 만들었다..

[해당 이슈](https://github.com/The-Day-In-1984/1984_Project/issues/41)

[해당 코드](https://github.com/The-Day-In-1984/1984_Project/tree/main/1984/Assets/Scripts/Runtime/UI/Mission)

```cs
public interface IMission
{
    bool IsCompleted { get; }
    void OnMissionStart();
    void OnMissionComplete();
}
```

사용할 인터페이스, 한 가지 큰 미션은 작은 여러 미션들로 이루어져 있으니 해당 미션 컨트롤러 산하의 여러 미션을 두고 모든 미션이 완료되면 종료를 기준으로 한다.

*컨트롤러*

```cs
using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    private readonly List<IMission> _missions = new List<IMission>();

    public void AddMission(IMission mission)
    {
        _missions.Add(mission);
    }

    private void Start()
    {
        foreach (var mission in _missions)
        {
            mission.OnMissionStart();
        }
    }

    public bool CheckAllMissionsComplete()
    {
        foreach (var mission in _missions)
        {
            if (!mission.IsCompleted)
            {
                return false;
            }
        }
        
        Debug.Log("성공");
        
        return true;
    }
}
```

미션에 사용되는 로직

```cs
using System;

public class CountLogic
{
    public Action OnCountChanged;
    public Action OnCountMax;
    
    private float _count;
    private readonly float _maxCount;
    
    public CountLogic(float maxCount, float count = 0)
    {
        _maxCount = maxCount;
        _count = count;
    }
    
    public void AddCount(int count)
    {
        _count += count;
        OnCountChanged?.Invoke();
        
        if (_count >= _maxCount)
        {
            OnCountMax?.Invoke();
        }
    }
}
```

```cs
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickMission : MonoBehaviour, IMission, IPointerClickHandler
{
    [Header("Target")]
    [SerializeField] private int successValue = 5;

    private MissionController _missionController;
    private CountLogic _countLogic;
    
    public bool IsCompleted { get; private set; }
    
    private void Awake()
    {
        _missionController = GetComponentInParent<MissionController>();
        _missionController.AddMission(this);
        
        _countLogic = new CountLogic(successValue);
        _countLogic.OnCountMax += OnMissionComplete;
    }

    public void OnMissionStart()
    {
        IsCompleted = false;
    }

    public void OnMissionComplete()
    {
        IsCompleted = true;
        _missionController.CheckAllMissionsComplete();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsCompleted)
            return;

        _countLogic.AddCount(1);
    }
}
```

이 두가지 기능과 동작을 결합하여 한가지 미션을 만들고 해당 미션을 컨트롤러에 추가하여 여러가지 미션을 만들 수 있게 된다.

### 시스템