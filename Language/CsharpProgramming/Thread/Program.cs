using System;
using System.Threading;

namespace MultiThrdApp
{

    class Program
    {
        static void Main(string[] args)
        {
            MyClass myClass = new MyClass();
            myClass.Run();
        }
    }
    class MyClass
    {
        private int counter = 1000;

        private object lockObject = new Object();

        public void Run()
        {
            // 10개의 쓰레드가 동일 메서드 실행
            for (int i = 0; i < 10; i++)
            {
                new Thread(UnsafeCalc).Start();    
            }
        }

        // Thread-Safe하지 않은 메서드 
        private void UnsafeCalc()
        {

            lock(lockObject)
            {
                // 객체 필드를 모든 쓰레드가 
                // 자유롭게 변경
                counter++;

                // 가정 : 다른 복잡한 일을 한다
                for (int i = 0; i < counter; i++)
                    for (int j = 0; j < counter; j++) ;

                // 필드값 읽기
                Console.WriteLine(counter);
            }
        }
    }
}
