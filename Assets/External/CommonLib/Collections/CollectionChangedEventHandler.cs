namespace CommonLib.Collections
{
    using System;
    using System.Collections.Generic;

    public class CollectionChangedEventArgs : EventArgs
    {
        public static readonly CollectionChangedEventArgs Reset =
            new CollectionChangedEventArgs(ChangeAction.Reset, null, -1, null, -1);

        public ChangeAction Action { get; internal set; }
        public List<object> NewItems { get; internal set; }
        public List<object> OldItems { get; internal set; }
        public int NewIndex { get; internal set; }
        public int OldIndex { get; internal set; }

        public CollectionChangedEventArgs(ChangeAction action, List<object> newItems, int newIndex, List<object> oldItems, int oldIndex)
        {
            this.Action = action;
            this.NewItems = newItems;
            this.OldItems = oldItems;
            this.NewIndex = newIndex;
            this.OldIndex = oldIndex;
        }
    }

    public delegate void CollectionChangedEventHandler(object sender, CollectionChangedEventArgs e);
}