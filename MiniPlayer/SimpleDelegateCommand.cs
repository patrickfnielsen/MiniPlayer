using System;
using System.Windows.Input;

namespace MiniPlayer
{
    public class SimpleDelegateCommand : ICommand
    {
        private readonly Action<object> _methodToExecute;
        private readonly Func<bool> _canExecuteEvaluator;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public SimpleDelegateCommand(Action<object> methodToExecute, Func<bool> canExecuteEvaluator)
        {
            _methodToExecute = methodToExecute;
            _canExecuteEvaluator = canExecuteEvaluator;
        }

        public SimpleDelegateCommand(Action<object> methodToExecute) : this(methodToExecute, () => true)
        { }

        public bool CanExecute(object parameter)
        {
            return _canExecuteEvaluator.Invoke();
        }

        public void Execute(object parameter)
        {
            _methodToExecute.Invoke(parameter);
        }
    }
}