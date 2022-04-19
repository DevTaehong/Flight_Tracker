using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinalProject.Commands
{
    public class naCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public event OnNaFlightHandler OnNaFlight;
        public delegate void OnNaFlightHandler(object sender, EventArgs e);

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            OnNaFlight?.Invoke(this, new EventArgs());
        }
    }
}
