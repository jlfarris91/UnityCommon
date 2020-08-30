namespace CommonLib
{
    using System;

    public class DelegateCommand<T> : ICommand<T>
    {
        private readonly Action<T> execute;
        private readonly Func<T, bool> canExecute;

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public void Execute(T data)
        {
            if (this.execute == null)
            {
                return;
            }

            this.execute(data);
        }

        public bool CanExecute(T data)
        {
            return this.execute != null && (this.canExecute == null || this.canExecute(data));
        }

        void ICommand.Execute(object data)
        {
            this.Execute((T) data);
        }

        bool ICommand.CanExecute(object data)
        {
            return this.CanExecute((T) data);
        }
    }

    public class DelegateCommand : DelegateCommand<object>
    {
        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute = null)
            : base(execute, canExecute)
        {
        }
    }
}