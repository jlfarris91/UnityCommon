namespace CommonLib.Collections
{
    using System;
    using System.Collections.Generic;

    public class ObservableCollectionProxy<TViewModel, TModel> : ObservableCollection<TViewModel>
    {
        private readonly IList<TModel> list;
        private readonly Func<TViewModel, TModel> modelSelectorFunc;

        public ObservableCollectionProxy(IList<TModel> list, Func<TViewModel, TModel> modelSelectorFunc)
        {
            ThrowIf.ArgumentIsNull(list, "list");
            ThrowIf.ArgumentIsNull(modelSelectorFunc, "modelSelectorFunc");
            this.list = list;
            this.modelSelectorFunc = modelSelectorFunc;
        }

        public override void Add(TViewModel item)
        {
            base.Add(item);

            TModel model = this.modelSelectorFunc(item);
            this.list.Add(model);
        }

        public override void Insert(int index, TViewModel item)
        {
            base.Insert(index, item);

            TModel model = this.modelSelectorFunc(item);
            this.list.Insert(index, model);
        }

        public override bool Remove(TViewModel item)
        {
            TModel model = this.modelSelectorFunc(item);
            return base.Remove(item) && this.list.Remove(model);
        }

        public override void RemoveAt(int index)
        {
            base.RemoveAt(index);
            this.list.RemoveAt(index);
        }
    }

    public class ObservableCollectionProxy<TViewModel> : ObservableCollectionProxy<TViewModel, TViewModel>
    {
        public ObservableCollectionProxy(IList<TViewModel> list, Func<TViewModel, TViewModel> modelSelectorFunc)
            : base(list, _ => modelSelectorFunc(_))
        {
        }
    }
}