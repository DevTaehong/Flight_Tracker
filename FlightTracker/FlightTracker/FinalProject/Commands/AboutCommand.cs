using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;


namespace FlightTracker.Commands
{
    public class AboutCommand : ICommand
    {
        //Aboutpage command
        //Note closing fix later(fixed i think
        public event EventHandler CanExecuteChanged;
        private readonly Page page;


        public AboutCommand(Page page)
        {
            this.page = page;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }


        //should work now???
        public void Execute(object parameter)
        {
            page.Frame.Navigate(typeof(AboutPage));
        }

        public void FireCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
