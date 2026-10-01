using BinaryTreeReconstructor;
using System.Diagnostics;

public static class Tester
{
    public static void Main()
    {
        List<int> inOrderSequence   = [9, 5, 1, 7, 2, 12, 8, 4, 3, 11];
        List<int> postOrderSequence = [9, 1, 2, 12, 7, 5, 3, 11, 4, 8];

        PrintSequence("Start", inOrderSequence, postOrderSequence);

        var runtimes  = new Dictionary<string, long>();
        var stopwatch = Stopwatch.StartNew();

        var tree = BinaryTree<int>.BuildTree(inOrderSequence, postOrderSequence);

        runtimes["treeCreationTime"] = stopwatch.ElapsedMilliseconds;
        stopwatch.Restart();

        var resultInOrderSequence   = tree.InOrderTraversal();
        var resultPostOrderSequence = tree.PostOrderTraversal();

        stopwatch.Stop();
        runtimes["treeTraversalTime"] = stopwatch.ElapsedMilliseconds;

        var inOrderIsAccurate   = inOrderSequence.SequenceEqual(resultInOrderSequence);
        var postOrderIsAccurate = postOrderSequence.SequenceEqual(resultPostOrderSequence);
        
        PrintSequence("Result", resultInOrderSequence, resultPostOrderSequence);
        PrintResults(runtimes, (inOrderIsAccurate && postOrderIsAccurate));
    }

    private static void PrintSequence(string label, List<int> inOrderSequence, List<int> postOrderSequence)
    {
        Console.WriteLine();
        Console.WriteLine($"{label} in-order:   {{{string.Join(',', inOrderSequence)}}}");
        Console.WriteLine($"{label} post-order: {{{string.Join(',', postOrderSequence)}}}");
    }

    private static void PrintResults(Dictionary<string, long> runtimes, bool result)
    {
        Console.WriteLine();
        Console.WriteLine($"Tree reconstruction time: {runtimes["treeCreationTime"]}ms");
        Console.WriteLine($"Tree traversal time:      {runtimes["treeTraversalTime"]}ms");
        Console.WriteLine();
        Console.WriteLine($"Tree was accurately reconstructed: {result}");
        Console.WriteLine();
    }
}