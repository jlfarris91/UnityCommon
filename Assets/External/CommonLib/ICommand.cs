namespace CommonLib
{
    public interface ICommand
    {
        void Execute(object data);
        bool CanExecute(object data);
    }

    public interface ICommand<in T> : ICommand
    {
        void Execute(T data);
        bool CanExecute(T data);
    }
}