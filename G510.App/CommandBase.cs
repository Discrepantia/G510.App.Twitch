using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace G510.App
{
    public class BaseCommand : ICommand
    {
        private Action<Object> execute;

        private Predicate<Object> canExecute;

        private event EventHandler CanExecuteChangedInternal;

        public BaseCommand(Action<Object> execute) : this(execute, DefaultCanExecute) { }

        public BaseCommand(Action<Object> execute, Predicate<Object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            if (canExecute == null)
                throw new ArgumentNullException("canExecute");

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; this.CanExecuteChangedInternal += value; }
            remove { CommandManager.RequerySuggested -= value; this.CanExecuteChangedInternal -= value; }
        }

        public bool CanExecute(Object parameter)
        {
            return this.canExecute != null && this.canExecute(parameter);
        }

        public void Execute(Object parameter)
        {
            this.execute(parameter);
        }

        public void OnCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChangedInternal;
            if (handler != null) 
                handler.Invoke(this, EventArgs.Empty); 
        }

        private static bool DefaultCanExecute(Object parameter)
        {
            return true;
        }
    }
}
