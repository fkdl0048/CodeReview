# NullObjectPattern  

좋은 코드, 나쁜 코드 6장에 등장하는 내용으로 이런 패턴이 존재하는지 몰랐지만 알게 모르게 쓰고 있던 패턴이였다..  

널 객체(오브젝트) 패턴의 가장 핵심은 NEP(NullPointerException)을 방지의 목적이다.  

이를 위해 자주 `if(object == null) or if(object != null)`와 같은 예외처리를 두거나 if로 한번 묶어서 처리하지만 잘못하다 NEP로 뻑이 날 수 있기 때문에 기본적으로 null대신 nullobject를 반환하는 형태의 패턴이다.  

그렇다고 단점이 없는 것이 아닌 오히려 **에러나 예외를 발견하기 어려워 질 수 있다**는 점이다.

*이 패턴을 도입했을 때 코드가 마구 늘어난다면 적절하지 않은 상황* 

책에서도 무분별한 사용이 아닌 **적절한 사용**을 말한다.  

구현 방법은 생각보다 다양한 것 같다.  

가장 많이 사용하는 방법이 싱글톤으로 NullObject를 만들어 두고 필요할때 할당하는 방법이다.  

내가 구현에 가장 간단하고 직관적인 방법으로 코드를 소개한다.  


```cs
namespace NullObjcetPattern
{
    public interface IAnimal
    {
        public void Say();
    }

    public class Cat : IAnimal
    {
        public void Say()
        {
            System.Console.WriteLine("냐옹이다옹~");
        }
    }

    public class NullAnimal : IAnimal
    {
        public void Say()
        {
            // 아무것도 하지 않음
        }
    }

    class NullObjcetPattern
    {
        static void Main(string[] args)
        {
            IAnimal[] animals = new IAnimal[5];

            animals[0] = new Cat();
            animals[1] = new NullAnimal();
            animals[2] = new Cat();
            animals[3] = new NullAnimal();
            animals[4] = new Cat();

            for (int i = 0; i < animals.Length; i++)
            {
                animals[i].Say();
            }
        }
    }
}
```

출력

```
PS D:\CodeReview\NullObjectPattern> dotnet run
냐옹이다옹~
냐옹이다옹~
냐옹이다옹~
```

지금은 null오브젝트를 할당해서 조금 낭비가 있지만 대부분 추상클래스나 인터페이스로 상속받아 싱글톤으로 만들어 필요에 의해 null이라고 판단되는 경우 nullobject를 할당해준다.  

이후에 작업을 해도 NEP가 나는 경우 없이 실행됨을 알 수 있다.  

사용에 용이한 점은 코드 자체가 null검사 구문이 사라져 깔끔한 것을 알 수 있다.  

하지만 다른 개발자가 널 오브젝트의 존재를 모른다면 문제가 될 수 있는 패턴..  

이런 예제로 봐서 이해가 안될 수 있지만 널 객체 패턴이 유용한 경우가 꽤 있다.  

1. 문자열을 합치는 경우

클래스의 필요한 문자열을 검색해서 합치는 경우 합치는(호출하는)쪽에선 반환 받는 값이 null인지 확인할 필요 없이 합치면 되고 반환하는 쪽에선 null일 경우 빈 문자열을 반환하면 된다.  

