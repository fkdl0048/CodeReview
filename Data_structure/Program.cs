namespace DataStructure
{
    class MainClass
    {
        
        static void Main(string[] args)
        {
            // Graph graph = new Graph();
            // graph.BFS(0);
            //graph.SearchAll();

            Tree tree = new Tree();

            TreeNode<string> root = tree.MakeTree();

            tree.PrintTree(root);
            System.Console.WriteLine(tree.GetHeight(root));
        }
    }
}