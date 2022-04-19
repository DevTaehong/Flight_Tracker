using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinalProject.Commands
{
    public class GroundCommand : ICommand
    {
        public event OnGroundButtonHandler OnGrounded;
        public delegate void OnGroundButtonHandler(object sender, EventArgs e);

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            OnGrounded?.Invoke(this, new EventArgs());
        }
    }
}
