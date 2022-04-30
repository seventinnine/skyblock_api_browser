﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Skyblock.UI.ViewModels
{
    public class AsyncDelegateCommand : ICommand
    {
        private readonly Func<object?, Task> executeAsync;
        private readonly Predicate<object?>? canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public AsyncDelegateCommand(Func<object?, Task> executeAsync, Predicate<object?>? canExecute = null)
        {
            this.executeAsync = executeAsync ?? throw new ArgumentNullException(nameof(executeAsync));
            this.canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public async Task ExecuteAsync(object? parameter)
        {
            await executeAsync(parameter);
        }

        async void ICommand.Execute(object? parameter)
        {
            await ExecuteAsync(parameter);
        }
    }
}
