using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
namespace FinalProject.Commands
{
    public class ExitCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly ViewModels.FlightViewModel flightViewModel;


        public ExitCommand(ViewModels.FlightViewModel flightViewModel)
        {
            this.flightViewModel = flightViewModel;
        }


        public bool CanExecute(object parameter)
        {
            //return dialogPage
            return true;
        }

        public  void Execute(object parameter)
        {
        
            try
            {
                Application.Current.Exit();
            }
            catch (Exception)
            {
                Debug.WriteLine("ERROR WHILE CRASHING");
            }
        }

        public void FireCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
