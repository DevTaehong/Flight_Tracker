using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightTracker.Commands
{
    public class SpiCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public event OnSpiFlightHandler OnSpiFlight;
        public delegate void OnSpiFlightHandler(object sender, EventArgs e);

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            OnSpiFlight?.Invoke(this, new EventArgs());
        }
    }
}
