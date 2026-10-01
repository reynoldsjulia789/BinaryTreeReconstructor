using BinaryTreeReconstructor;
using System.Diagnostics;

public static class Tester
{
    public static void Main()
    {
        List<int> inOrderSequence   = [9, 5, 1, 7, 2, 12, 8, 4, 3, 11];
        List<int> postOrderSequence = [9, 1, 2, 12, 7, 5, 3, 11, 4, 8];

        Console.WriteLine();
        Console.WriteLine($"Start: in-order: {{{string.Join(',', inOrderSequence)}}}");
        Console.WriteLine($"Start: in-order: {{{string.Join(',', postOrderSequence)}}}");

        var stopwatch = Stopwatch.StartNew();

        var tree = BinaryTree<int>.BuildTree(inOrderSequence, postOrderSequence);

        var treeCreationTime = stopwatch.ElapsedMilliseconds;

        stopwatch.Restart();

        var resultInOrderSequence   = tree.InOrderTraversal();
        var resultPostOrderSequence = tree.PostOrderTraversal();

        stopwatch.Stop();

        Console.WriteLine();
        Console.WriteLine($"Result: in-order: {{{string.Join(',', resultInOrderSequence)}}}");
        Console.WriteLine($"Result: post-order: {{{string.Join(',', resultPostOrderSequence)}}}");

        Console.WriteLine();
        Console.WriteLine($"Tree reconstruction time: {treeCreationTime}ms");
        Console.WriteLine($"Tree in-order & post-order traversal time: {stopwatch.ElapsedMilliseconds}ms");

        var inOrderIsAccurate   = inOrderSequence.SequenceEqual(resultInOrderSequence);
        var postOrderIsAccurate = postOrderSequence.SequenceEqual(resultPostOrderSequence);

        Console.WriteLine();
        Console.WriteLine($"Tree was accurately reconstructed: {inOrderIsAccurate && postOrderIsAccurate}");
    }
}