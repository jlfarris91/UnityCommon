namespace CommonLib.Collections
{
    public interface IObservableCollection
    {
        event CollectionChangedEventHandler CollectionChanged;
    }
}