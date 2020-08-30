namespace CommonLib.Collections
{
    using System.Collections;
    using System.Collections.Generic;

    public class TreeEnumerator<T> : IEnumerator<T>
    {
        private readonly ITreeNode<T> firstNode; 
        private ITreeNode<T> node;

        private readonly Queue<ITreeNode<T>> openList = new Queue<ITreeNode<T>>();

        public TreeEnumerator(ITreeNode<T> node)
        {
            this.firstNode = node;
            this.openList.Enqueue(node);
        }

        public bool MoveNext()
        {
            //
            //       1
            //   2       3
            // 4   5   6   7
            //

            // 1. Pop node off the open list
            this.node = this.openList.Dequeue();

            // 2. If node is null we are done
            if (this.node == null)
            {
                return false;
            }

            // 3. Add each child to open list
            ITreeNode<T> child = this.node.FirstChild;

            while (child != null)
            {
                this.openList.Enqueue(child);
                child = child.NextSibling;
            }

            return false;
        }

        public void Reset()
        {
            this.node = null;
            this.openList.Clear();
            this.openList.Enqueue(this.firstNode);
        }

        public T Current
        {
            get { return this.node.Data; }
        }

        object IEnumerator.Current
        {
            get { return this.Current; }
        }

        public void Dispose()
        {
        }
    }
}