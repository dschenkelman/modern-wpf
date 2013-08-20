namespace ModernWPF.MVVM.Commands
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class DelegateCommand<T> : ICommand
    {
        private readonly Func<Task> execute;

        public DelegateCommand(Func<Task> execute)
        {
            this.execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await this.execute.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }
}
