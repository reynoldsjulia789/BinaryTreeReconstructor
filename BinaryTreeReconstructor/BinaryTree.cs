namespace BinaryTreeReconstructor;

public class BinaryTree<Type>
{
    private Node? Root { get; set; }

    public static BinaryTree<Type> BuildTree(List<Type> inOrderSequence, List<Type> postOrderSequence)
    {
        BinaryTree<Type> tree = new();

        tree.BuildTreeHelperStart(inOrderSequence, postOrderSequence);

        return tree;
    }

    private void BuildTreeHelperStart(List<Type> inOrderSequence, List<Type> postOrderSequence)
    {
        this.Root = BuildTreeHelper(new(inOrderSequence.ToArray()), new(postOrderSequence.ToArray()));
    }

    private Node? BuildTreeHelper(ReadOnlySpan<Type> inOrderSequence, ReadOnlySpan<Type> postOrderSequence)
    {
        if (inOrderSequence.Length != postOrderSequence.Length)
        {
            throw new ArgumentException("BinaryTree.BuildTree: in order and post order sequences are different lengths");
        }

        if (inOrderSequence.Length == 0) return null;

        if (inOrderSequence.Length == 1) return new Node(inOrderSequence[0]);

        var node   = new Node(postOrderSequence[^1]); // ^1 = last index in span/list/array
        var inOrderRootIdx = inOrderSequence.IndexOf(node.Data);

        var LEFTinOrder    = inOrderSequence  .Slice(0, inOrderRootIdx);
        var LEFTpostOrder  = postOrderSequence.Slice(0, LEFTinOrder.Length);

        node.Left  = BuildTreeHelper(LEFTinOrder, LEFTpostOrder);

        var RIGHTinOrder   = inOrderSequence  .Slice(inOrderRootIdx + 1);
        var RIGHTpostOrder = postOrderSequence.Slice((LEFTinOrder.Length), (postOrderSequence.Length - LEFTinOrder.Length - 1));

        node.Right = BuildTreeHelper(RIGHTinOrder, RIGHTpostOrder);

        return node;
    }


    /// <summary>
    /// Traverses the tree and adds the data to a list in order.
    /// Time Complexity: O(NlogN)
    /// </summary>
    /// <returns>Returns an in order traversal of the tree as a list</returns>
    public List<Type> InOrderTraversal()
    {
        return InOrderTraversal(this.Root, []);
    }

    private List<Type> InOrderTraversal(Node? curr, List<Type> sequence)
    {
        if (curr is null) return sequence;

        InOrderTraversal(curr.Left, sequence);
        sequence.Add(curr.Data);
        InOrderTraversal(curr.Right, sequence);

        return sequence;
    }

    /// <summary>
    /// Traverses the tree and adds the data to a list post order
    /// Time Complexity: O(NlogN)
    /// </summary>
    /// <returns>Returns a post order traversal of the tree as a list</returns>
    public List<Type> PostOrderTraversal()
    {
        return PostOrderTraversal(this.Root, []);
    }

    private List<Type> PostOrderTraversal(Node? curr, List<Type> sequence)
    {
        if (curr is null) return sequence;

        PostOrderTraversal(curr.Left, sequence);
        PostOrderTraversal(curr.Right, sequence);
        sequence.Add(curr.Data);

        return sequence;
    }

    /// <summary>
    /// Nodes of the Binary Tree
    /// </summary>
    protected class Node(Type data, Node? left = null, Node? right = null)
    {
        public Type Data { get; set; } = data;
        public Node? Left { get; set; } = left;
        public Node? Right { get; set; } = right;
    }
}
