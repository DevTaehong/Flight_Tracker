using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FinalProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ViewModels.FlightViewModel cvm { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            cvm = new ViewModels.FlightViewModel();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            //App.SearchCountry = FilterTextBox.Text;
            //scrollViewer.IsEnabled = true;
            NoteList.Visibility = Visibility.Visible;
        }

        private void FilterTextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            NoteList.Visibility = Visibility.Collapsed;
        }
    }
}
