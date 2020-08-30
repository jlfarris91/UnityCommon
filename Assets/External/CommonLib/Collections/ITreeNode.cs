namespace CommonLib.Collections
{
    using System.Collections.Generic;

    public interface ITreeNode<T> : IEnumerable<T>
    {
        ITreeNode<T> NextSibling { get; }

        ITreeNode<T> PrevSibling { get; }

        ITreeNode<T> Parent { get; }

        ITreeNode<T> FirstChild { get; }

        ITreeNode<T> LastChild { get; }

        T Data { get; }

        ITreeNode<T> Root { get; }

        IEnumerable<ITreeNode<T>> ChildNodes { get; }

        ITreeNode<T> AddChild(ITreeNode<T> child);

        ITreeNode<T> InsertChild(ITreeNode<T> child, int index);

        void RemoveChild(ITreeNode<T> child);
    }

    public enum TreeTraverseOrder
    {
        DepthFirst,
        BreadthFirst
    }
}