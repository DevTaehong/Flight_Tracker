using FlightTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightTracker.Commands
{
    public class ViewOnMapCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action methodToExecute;
        private Func<bool> canExecuteEvaluator;

        private ViewModels.FlightViewModel fvm;

        public delegate void CharCreatedHandler(object sender, EventArgs e);
        public event CharCreatedHandler OnFlightCreated;


        public Flight TheFlight { get; set; }
        public ViewOnMapCommand(Action methodToExecute, Func<bool> canExecuteEvaluator)
        {
            this.methodToExecute = methodToExecute;
            this.canExecuteEvaluator = canExecuteEvaluator;
        }
        public ViewOnMapCommand(Action methodToExecute)
            : this(methodToExecute, null)
        {
        }
        public bool CanExecute(object parameter)
        {
            if (this.canExecuteEvaluator == null)
            {
                return true;
            }
            else
            {
                bool result = this.canExecuteEvaluator.Invoke();
                return result;
            }
        }
        public async void Execute(object parameter)
        {
            this.methodToExecute.Invoke();


        }
    }
}
