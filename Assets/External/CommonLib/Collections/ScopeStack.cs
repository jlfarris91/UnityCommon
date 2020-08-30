namespace CommonLib.Collections
{
    using System.Collections.Generic;

    public class ScopeStack<TItem>
    {
        protected Stack<TItem> Items = new Stack<TItem>(); 

        public TItem Current
        {
            get { return this.Items.Count > 0 ? this.Items.Peek() : default(TItem); }
        }

        public int Count
        {
            get { return this.Items.Count; }
        }

        public virtual void Begin(TItem item)
        {
            this.Items.Push(item);
        }

        public virtual void End()
        {
            if (this.Items.Count < 1)
            {
                return;
            }

            this.Items.Pop();
        }
    }
}