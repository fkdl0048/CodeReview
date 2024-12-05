# CustomEditor

> 유니티 에디터 확장에 대한 정리

게임 개발을 지속하다 보면 결국 효율성이나 편의성을 위해 커스텀으로 에디터를 확장해야할 때가 많다. *어떻게 보면 이게 진짜 클라이언트 프로그래머의 역량같기도 하다. 결국 기획자가 편하게 작업할 수 있는 환경을 만드는 것*

이번에 시작한 프로젝트에서 자동화된 디펜스? 게임을 만들게 되었는데, 이 때 맵이나 경로 등을 쉽게 만질 수 있도록 커스텀 에디터를 만들게 되면서 이에 대한 정리를 하게 되었다.

Unity는 다양한 방법으로 에디터를 확장할 수 있다. 많이 사용하는 방법은 있지만 때에 따라 맞춰서 사용해야 하기에 여러 방법을 알아두는 것이 좋다..!

## 컴포넌트 기반 커스텀 에디터

- [화물 경로 설정 에디터 CS 파일](https://github.com/fkdl0048/merchants-journey/blob/main/Assets/2.%20Scripts/Editor/CargoPathEditor.cs)
- [개발 이슈](https://github.com/fkdl0048/merchants-journey/issues/4)

코드를 보면 알 수 있지만 사용방법은 상당히 간단하다. *유니티에서 미리 해줌*

```csharp
[CustomEditor(typeof(Cargo))]
public class CargoPathEditor : Editor
```

- `Editor` 클래스를 상속받아야 한다.
- `CustomEditor` 속성을 사용하여 어떤 컴포넌트에 대한 커스텀 에디터인지 설정한다.

현재 코드를 해석해보자면, 내가 Cargo라는 클래스에 대한 커스텀 에디터를 만들었다는 것이다. **이렇게 하면 Cargo 클래스에 대한 인스펙터 창이 커스텀 에디터로 변경된다.**

```csharp
    private SerializedProperty serializedPathPointsProp;
    private SerializedProperty moveSpeedProp;
    private SerializedProperty waitTimeAtPointProp;
    private SerializedProperty widthProp;
    private SerializedProperty heightProp;
    private SerializedProperty autoStartProp;

    private void OnEnable()
    {
        cargo = (Cargo)target;
        serializedPathPointsProp = serializedObject.FindProperty("serializedPathPoints");
        moveSpeedProp = serializedObject.FindProperty("moveSpeed");
        waitTimeAtPointProp = serializedObject.FindProperty("waitTimeAtPoint");
        widthProp = serializedObject.FindProperty("width");
        heightProp = serializedObject.FindProperty("height");
        autoStartProp = serializedObject.FindProperty("autoStart");
        
        SceneView.duringSceneGui += OnSceneGUI;
    }
```

이 코드는 특정 MonoBehaviour 클래스(여기서는 Cargo)의 직렬화된 프로퍼티(serialized properties)에 접근하고 이를 편집할 수 있게 설정하는 부분이다. 좀 더 쉽게 말하자면 Cargo클래스에서 내가 커스텀한 인스펙터에 노출하고 싶은 변수를 설정하는 부분이다.

```csharp
    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }
```

`OnEnable`과 `OnDisable` 메서드는 에디터가 활성화되거나 비활성화될 때 호출되는 메서드이다. 여기서는 SceneView의 duringSceneGui 이벤트에 대한 이벤트 핸들러를 추가하거나 제거하는 부분이다.

*이처럼 에디터도 결국 Unity의 MonoBehaviour이기 때문에 MonoBehaviour의 생명주기 메서드를 사용할 수 있다.*

```csharp
public override void OnInspectorGUI()
{
    serializedObject.Update();
    ...
    EditorGUILayout.PropertyField(moveSpeedProp);
    ...
    serializedObject.ApplyModifiedProperties();
}
```

`OnInspectorGUI`는 이미 정의되어 있는(Editor) 메서드를 override하여 Inspector의 커스텀 UI를 정의합니다. 즉 시각적으로 보여질 부분에 대한 설정을 하는 부분이다.

`Update()`로 Unity 데이터를 가져오고, `ApplyModifiedProperties()`로 변경된 데이터를 반영합니다.

```csharp
private void OnSceneGUI(SceneView sceneView)
{
    ...
}
```

앞서 이벤트로 등록한 메서드인 `OnSceneGUI`는 SceneView의 Scene GUI를 그리는 메서드이다. 여기서는 SceneView에 경로를 그리는 로직을 작성하였다.

## 툴 기반 커스텀 에디터

- [커스텀 타일 에디터 CS 파일](https://github.com/fkdl0048/merchants-journey/blob/main/Assets/2.%20Scripts/Editor/GridTileEditor.cs)
- [작성 이슈](https://github.com/fkdl0048/merchants-journey/issues/2)

```csharp
public class GridTileEditor : EditorWindow
{
    ...
}
```

- `EditorWindow` 클래스를 상속받아 사용한다.
- Unity의 상단 메뉴(Window 탭 등)에 등록하여 호출할 수 있다.

```csharp
    [MenuItem("Tools/Grid Tile Editor")]
    public static void ShowWindow()
    {
        GetWindow<GridTileEditor>("Grid Tile Editor");
    }
```

MenuItem 애트리뷰트를 사용하여 상단 메뉴에 등록할 수 있다. `ShowWindow` 메서드는 해당 메뉴를 클릭했을 때 호출되는 메서드이다. 즉, 이 메서드를 통해 `GridTileEditor` 창을 띄울 수 있다.

```csharp
 private void OnEnable()
{
    SceneView.duringSceneGui += OnSceneGUI;
}

private void OnDisable()
{
    SceneView.duringSceneGui -= OnSceneGUI;
}
```

메뉴를 통해 창을 띄웠다면 유니티 이벤트인 `OnEnable`과 `OnDisable` 메서드를 사용하여 SceneView의 이벤트 핸들러를 추가하고 제거한다. *컴포넌트 기반과 동일함*

```csharp
   private void OnGUI()
    {
        ...
    }
```

`OnGUI` 메서드는 `OnInspectorGUI`와 비슷한 역할을 한다. 창에 표시할 UI를 정의하는 부분이다.

```csharp
private void OnSceneGUI(SceneView sceneView)
{
    ...
}
```

`OnSceneGUI` 메서드는 컴포넌트 기반 커스텀 에디터와 동일한 역할을 한다. 여기서는 SceneView에 타일을 그리는 로직을 작성하였다.

## 그 외

그 외에도 다양한 커스텀 에디터 방법이 있다. 툴바를 사용하거나 PropertyDrawer, SceneView을 커스텀하거나, 단순화된 ScriptableWizard를 사용하는 방법도 있다. 이외에도 과거에 사용해본 Unity 3세대 UI인 UIToolKit을 사용하여 커스텀도 가능하다.

- [ReInput-tool](https://github.com/BRIDGE-DEV/ReInput-tool)

## 정리

두 가지 방식을 알아봤는데, 결국은 상속받는 클래스만 다르고 사용하는 방법은 비슷하다. 사용할 핵심 로직은 함수로 분리하고 이를 핸들로 등록된 이벤트에 등록하여 사용한다. 유니티 라이프사이클을 동일하게 가져가기에 할당 해제도 여기서 관리하고, 정의부분과 구현부분을 분리하여 코드를 작성하는 것이 좋다.

컴포넌트 기반 커스텀 에디터는 조금 더 인스펙터에 가까운 설정이나 클래스에 직접 참조하여 사용하기에 적합한 방법이고, 툴 기반 커스텀 에디터는 특정 기능을 수행하기 위한 전용 툴을 만들 때 사용하기 적합한 방법이다.
