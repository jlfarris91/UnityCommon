namespace CommonLib.Collections
{
    using System.Collections;
    using System.Collections.Generic;

    public class TreeNodeChildren<T> : IEnumerable<ITreeNode<T>>
    {
        private readonly ITreeNode<T> node;

        public TreeNodeChildren(ITreeNode<T> node)
        {
            this.node = node;
        }

        public IEnumerator<ITreeNode<T>> GetEnumerator()
        {
            return new TreeNodeChildEnumerator<T>(this.node);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public class TreeNodeChildEnumerator<T> : IEnumerator<ITreeNode<T>>
    {
        private readonly ITreeNode<T> node;

        private bool started;

        public TreeNodeChildEnumerator(ITreeNode<T> node)
        {
            this.node = node;
            this.started = false;
        }

        public bool MoveNext()
        {
            if (!this.started)
            {
                this.started = true;
                this.Current = this.node.FirstChild;
            }
            else if (this.Current != null)
            {
                this.Current = this.Current.NextSibling;
            }

            return this.Current != null;
        }

        public void Reset()
        {
        }

        public ITreeNode<T> Current { get; private set; }

        object IEnumerator.Current
        {
            get { return this.Current; }
        }

        public void Dispose()
        {
        }
    }
}