namespace DataStructure
{
    class Stack_Queue_Test
    {
        Stack<int> stack = new Stack<int>();
        Queue<int> queue = new Queue<int>();
        
        private void Test()
        {
            stack.Push(1);
            stack.Peek();
            stack.Pop();

            queue.Enqueue(1);
            queue.Peek();
            queue.Dequeue();
        } 
    }
}