namespace DataStructure
{
    class MainClass
    {
        
        static void Main(string[] args)
        {
            // Graph graph = new Graph();
            // graph.BFS(0);
            //graph.SearchAll();

            // Tree tree = new Tree();
            // TreeNode<string> root = tree.MakeTree();
            // tree.PrintTree(root);
            // System.Console.WriteLine(tree.GetHeight(root));

            PriorityQueue<Knight> q = new PriorityQueue<Knight>();
            q.Push(new Knight() {ID = 20});
            q.Push(new Knight() {ID = 30});
            q.Push(new Knight() {ID = 40});
            q.Push(new Knight() {ID = 10});
            q.Push(new Knight() {ID = 5});
            
            while (q.Count() > 0)
            {
                System.Console.WriteLine(q.Pop().ID);
            }
        }
    }
}