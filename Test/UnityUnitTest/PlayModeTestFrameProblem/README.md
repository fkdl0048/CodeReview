# PlayModeTestFrameProblem

유니티에서 PlayMode의 Unit Test를 사용하다 생긴 문제를 기록한다.

[문제 이슈](https://github.com/GG-Studio-990001/GameOver/issues/165)

## 문제가 생긴 원인

![image](https://github.com/fkdl0048/CodeReview/assets/84510455/6f31eeca-59f0-4784-9dfa-1844c7b56241)

테스트 코드를 원래 EditMode에서 작성하다가 최근에 Runtime에 동작하는 즉, PlayMode에 있어야한 인게임 코드들도 컴포넌트 테스트와 같이 구조를 잡아야 한다는 것을 알게 됨

![image](https://github.com/fkdl0048/CodeReview/assets/84510455/e9901049-f0c6-4977-a12d-f652032946b3)

위처럼 옮겨서 동작하는데 문제가 없었는데, PlayMode테스트에서는 살짝 동작이 달라야 하는 부분이 있어서 해당 부분을 수정함

*Test애트리뷰트에서 UnityTest애트리뷰트로 변경 후, 코루틴으로 테스트함.*

코루틴 테스트의 경우 실제 인 게임에서 프레임 단위를 제어하기 위해서 사용되는데 UnityTest애트리뷰트를 사용하여 제어한다.

[공식문서 참고](https://docs.unity3d.com/kr/2018.4/Manual/PlaymodeTestFramework.html)

## 테스트할 코드

> PlayerClass

```cs
using Runtime.CH1.Main.PlayerFunction;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.CH1.Main
{
    [RequireComponent(typeof(Animator))]
    public class TopDownPlayer : MonoBehaviour
    {
        // TODO 이거 데이터로 빼야함
        [SerializeField] private float moveSpeed = 5.0f;
        [SerializeField] private float animSpeed = 0.5f;
        
        private Vector2 _movementInput;
        private TopDownMovement _movement;
        private TopDownAnimation _animation;
        private TopDownInteraction _interaction;
        
        private const string Interaction = "Object";

        private void Awake()
        {
            _movement = new TopDownMovement(moveSpeed, transform);
            _animation = new TopDownAnimation(GetComponent<Animator>(), animSpeed);
            _interaction = new TopDownInteraction(transform, LayerMask.GetMask(Interaction));
        }
        
        private void Update()
        {
            _animation.SetMovementAnimation(_movementInput);
        }

        private void FixedUpdate() => _movement.Move(_movementInput);

        private void OnMove(InputValue value) => _movementInput = value.Get<Vector2>();
        
        private void OnInteraction() => _interaction.Interact(_movement.Direction);
    }
}
```

> MovementClass

```cs
using UnityEngine;

namespace Runtime.CH1.Main
{
    public class TopDownMovement
    {
        public Vector2 Direction => _previousMovementInput;
        
        private readonly Transform _transform;
        private readonly float _moveSpeed = 5.0f;
        private Vector2 _previousMovementInput;
        
        public TopDownMovement(float moveSpeed, Transform transform)
        {
            _moveSpeed = moveSpeed;
            _transform = transform;
        }
        
        public void Move(Vector2 movementInput)
        {
            if (movementInput == Vector2.zero)
            {
                return;
            }
            
            if (movementInput.magnitude > 1.0f)
            {
                movementInput = _previousMovementInput;
            }
            
            Vector2 movement = movementInput * (_moveSpeed * Time.deltaTime);
            
            _transform.Translate(movement);
            
            _previousMovementInput = movementInput;
        }
    }
}
```

> AnimationClass

```cs
using UnityEngine;

namespace Runtime.CH1.Main.PlayerFunction
{
    public class TopDownAnimation
    {
        private readonly Animator _animator;
        private readonly float _animationSpeed;
        private static readonly int Moving = Animator.StringToHash(IsMoving);
        private static readonly int Horizontal1 = Animator.StringToHash(Horizontal);
        private static readonly int Vertical1 = Animator.StringToHash(Vertical);

        private const string IsMoving = "IsMoving";
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        
        public TopDownAnimation(Animator animator, float animationSpeed = 1.0f)
        {
            _animator = animator;
            _animator.speed = _animationSpeed = animationSpeed;
        }
        
        public void SetMovementAnimation(Vector2 movementInput)
        {
            // TODO 리터럴값 제거, 애니메이션 확장되는대로
            if (movementInput == Vector2.zero)
            {
                _animator.SetBool(Moving, false);
                return;
            }
            
            _animator.SetBool(Moving, true);
            
            _animator.SetFloat(Horizontal1, movementInput.x);
            _animator.SetFloat(Vertical1, movementInput.y);
        }
    }
}
```

> InteractionClass

```cs
using Runtime.CH1.Main.Interface;
using UnityEngine;

namespace Runtime.CH1.Main.PlayerFunction
{
    public class TopDownInteraction
    {
        private readonly Transform _transform;
        private readonly int _interactionLayerMask;
        private readonly float _interactionDistance;
        
        public TopDownInteraction(Transform transform, int interactionLayerMask, float interactionDistance = 1.0f)
        {
            _transform = transform;
            _interactionLayerMask = interactionLayerMask;
            _interactionDistance = interactionDistance;
        }
        
        public bool Interact(Vector2 direction)
        {
            RaycastHit2D hit = Physics2D.Raycast(_transform.position, direction, _interactionDistance, _interactionLayerMask);
            
            if (hit.collider != null)
            {
                hit.collider.GetComponent<IInteractive>()?.Interact();
                return true;
            }
            
            return false;
        }
    }
}
```

코드 구조는 최대한 가독성과 재사용성을 좋게하기 위해 분리하였다.

*아직은 많이 수정해야 함*

## 테스트 코드(문제가 발생한)

```cs
using NUnit.Framework;
using Runtime.CH1.Main;
using Runtime.CH1.Main.PlayerFunction;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Runtime
{
    [TestFixture]
    public class TopDownPlayerTests
    {
        private GameObject _playerObject;
        private TopDownPlayer _player;
        private TopDownMovement _movement;
        private TopDownAnimation _animation;
        private TopDownInteraction _interaction;
        
        [UnitySetUp]
        public IEnumerator Setup()
        {
            _playerObject = new GameObject("Player");
            Animator animator = _playerObject.AddComponent<Animator>();
            
            // TODO 에섯 변경되는대로 수정
            animator.runtimeAnimatorController = Resources.Load("Sample/Player") as RuntimeAnimatorController;
            if (animator.runtimeAnimatorController == null)
            {
                Debug.LogError("Animator Controller is null");
            }
            
            _player = _playerObject.AddComponent<TopDownPlayer>();
            _player.transform.position = Vector3.zero;
            
            yield return null;
            
            _movement = new TopDownMovement(5.0f, _player.transform);
            _animation = new TopDownAnimation(_player.GetComponent<Animator>(), 0.5f);
            _interaction = new TopDownInteraction(_player.transform, LayerMask.GetMask("Object"), 1f);
        }
        
        [UnityTest]
        public IEnumerator TestPlayerMovement()
        {
            Vector2 movementInput = new Vector2(1.0f, 0.0f);
            
            _movement.Move(movementInput);
            
            yield return null;
            
            Assert.AreNotEqual(Vector3.zero, _player.transform.position);
        }
        
        [UnityTest]
        public IEnumerator TestPlayerAnimation()
        {
            Animator animator = _playerObject.GetComponent<Animator>();
            _animation.SetMovementAnimation(new Vector2(1f, 0f)); 
            
            bool isMoving = animator.GetBool("IsMoving");
            
            yield return null;
            
            Assert.IsTrue(isMoving);
        }
        
        [UnityTest]
        public IEnumerator TestPlayerInteraction()
        {
            GameObject interactionObject = new GameObject("InteractionTestObject");
            interactionObject.AddComponent<CircleCollider2D>();
            interactionObject.AddComponent<NpcInteraction>();
            interactionObject.layer = LayerMask.NameToLayer("Object");
            
            interactionObject.transform.position = new Vector3(1.0f, 0.0f, 0.0f);
            
            yield return null;
            
            Assert.IsTrue(_interaction.Interact(Vector2.right));
            
            GameObject.DestroyImmediate(interactionObject);
        }
        
        [UnityTest]
        public IEnumerator TestPlayerInteractionFail()
        {
            GameObject interactionObject = new GameObject("InteractionTestObject");
            interactionObject.AddComponent<CircleCollider2D>();
            interactionObject.AddComponent<NpcInteraction>();
            interactionObject.layer = LayerMask.NameToLayer("Object");
            
            interactionObject.transform.position = new Vector3(1.0f, 0.0f, 0.0f);
            
            yield return null;
            
            Assert.IsFalse(_interaction.Interact(Vector2.left));
            
            GameObject.DestroyImmediate(interactionObject);
        }
        
        [UnityTearDown]
        public IEnumerator TearDown()
        {
            GameObject.Destroy(_playerObject);
            
            yield return null;
        }
    }
}
```

위와 같은 구조로 작성했다.

문제는 가끔은 성공하고, 가끔은 실패하기 때문에 나는 LayCast를 사용하는 부분이 문제가 있다고 생각했다.

```cs
public bool Interact(Vector2 direction)
{
    RaycastHit2D hit = Physics2D.Raycast(_transform.position, direction, _interactionDistance, _interactionLayerMask);
    
    if (hit.collider != null)
    {
        hit.collider.GetComponent<IInteractive>()?.Interact();
        return true;
    }
    
    return false;
}
```

## 해결 방법

해결 과정이나 느낀점은 블로그에 작성하도록 하고, 해결 방법은 아래와 같다.

```cs
using NUnit.Framework;
using Runtime.CH1.Main;
using Runtime.CH1.Main.PlayerFunction;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Runtime
{
    [TestFixture]
    public class TopDownPlayerTests
    {
        private GameObject _playerObject;
        private TopDownPlayer _player;
        private TopDownMovement _movement;
        private TopDownAnimation _animation;
        private TopDownInteraction _interaction;
        
        [UnitySetUp]
        public IEnumerator Setup()
        {
            _playerObject = new GameObject("Player");
            Animator animator = _playerObject.AddComponent<Animator>();
            
            // TODO 에섯 변경되는대로 수정
            animator.runtimeAnimatorController = Resources.Load("Sample/Player") as RuntimeAnimatorController;
            if (animator.runtimeAnimatorController == null)
            {
                Debug.LogError("Animator Controller is null");
            }
            
            _player = _playerObject.AddComponent<TopDownPlayer>();
            _player.transform.position = Vector3.zero;
            
            _movement = new TopDownMovement(5.0f, _player.transform);
            _animation = new TopDownAnimation(_player.GetComponent<Animator>(), 0.5f);
            _interaction = new TopDownInteraction(_player.transform, LayerMask.GetMask("Object"), 1f);
            
            yield return new WaitForFixedUpdate();
        }
        
        [UnityTest]
        public IEnumerator TestPlayerMovement()
        {
            Vector2 movementInput = new Vector2(1.0f, 0.0f);
            
            _movement.Move(movementInput);
            
            yield return new WaitForFixedUpdate();
            
            Assert.AreNotEqual(Vector3.zero, _player.transform.position);
        }
        
        [UnityTest]
        public IEnumerator TestPlayerAnimation()
        {
            Animator animator = _playerObject.GetComponent<Animator>();
            _animation.SetMovementAnimation(new Vector2(1f, 0f)); 
            
            bool isMoving = animator.GetBool("IsMoving");
            
            yield return new WaitForFixedUpdate();
            
            Assert.IsTrue(isMoving);
        }
        
        [UnityTest]
        public IEnumerator TestPlayerInteraction()
        {
            GameObject interactionObject = new GameObject("InteractionTestObject1");
            interactionObject.AddComponent<CircleCollider2D>();
            interactionObject.AddComponent<NpcInteraction>();
            interactionObject.layer = LayerMask.NameToLayer("Object");
            
            interactionObject.transform.position = new Vector3(1.0f, 0.0f, 0.0f);
            
            yield return new WaitForFixedUpdate();
            
            Assert.IsTrue(_interaction.Interact(Vector2.right));
            
            Object.DestroyImmediate(interactionObject);
        }
        
        [UnityTest]
        public IEnumerator TestPlayerInteractionFail()
        {
            GameObject interactionObject = new GameObject("InteractionTestObject2");
            interactionObject.AddComponent<CircleCollider2D>();
            interactionObject.AddComponent<NpcInteraction>();
            interactionObject.layer = LayerMask.NameToLayer("Object");
            
            interactionObject.transform.position = new Vector3(1.0f, 0.0f, 0.0f);
            
            yield return new WaitForFixedUpdate();
            
            Assert.IsFalse(_interaction.Interact(Vector2.left));
            
            Object.DestroyImmediate(interactionObject);
        }
        
        [UnityTearDown]
        public IEnumerator TearDown()
        {
            Object.Destroy(_playerObject);

            _interaction = null;
            _movement = null;
            _animation = null;
            
            yield return new WaitForFixedUpdate();
        }
    }
}
```

한 눈에 봐도 코드가 크게 달라진게 없다.

여기서 문제는 `yield return null`을 사용했던 부분을 `yield return new WaitForFixedUpdate()`로 변경했다.

생각하지 못했던 부분이 EditMode에선 프레임을 넘기기 위해 `yield return null`을 사용했는데, PlayMode에서는 `yield return new WaitForFixedUpdate()`를 사용해야 한다는 것이다.

[관련 문서](https://docs.unity3d.com/Packages/com.unity.test-framework@1.1/manual/reference-attribute-unitytest.html)

문서를 보면 알 수 있듯이 Edit Mode에서 한 프레임을 건너 뛰고 실행하는 모습을 보고 단순 코드를 작성해서 생신 문제다.

좀 더 깊게 들어가서 정확하게 말하면 `TestPlayerInteraction`테스트에서 생성한 오브젝트가 전 yield return null때문에 원자적으로 일어나 삭제되지 않고 `TestPlayerInteractionFail`에서도 영향을 주었던 것이다.

이제는 테스트 종료 시 해당 오브젝트 삭제 및 Player도 전부 정리해주기 때문에 문제가 없어졌다.

*Movement의 경우도 플레이어 위치를 옮기는 것에 대한 생각도 해야했다.*

코드간의 의존성뿐만 아니라 테스트끼리의 연관성에 대해서도 생각해야 한다는 것을 알게 되었다.

[해결 PR](https://github.com/GG-Studio-990001/GameOver/pull/171)
