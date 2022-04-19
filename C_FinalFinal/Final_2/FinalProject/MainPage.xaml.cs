using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.WindowManagement;
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
        // Track open app windows in a Dictionary.
        public Commands.AboutCommand AboutCommand { get; set; }
      
        public static Dictionary<UIContext, AppWindow> AppWindows { get; set; }
            = new Dictionary<UIContext, AppWindow>();
        public MainPage()
        {
            this.InitializeComponent();
            cvm = new ViewModels.FlightViewModel();
            this.AboutCommand = new Commands.AboutCommand(this);
           
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            NoteList.Visibility = Visibility.Visible;
            display.Visibility = Visibility.Visible;
            SearchButton.IsEnabled = false;
            countryNameTextBlock.Visibility = Visibility.Collapsed;
        }

        private void FilterTextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            NoteList.Visibility = Visibility.Collapsed;
            SearchButton.IsEnabled = true;
            display.Visibility = Visibility.Collapsed;
        }

        private void GroundRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            FilterTextBox.Text = "";
            countryNameTextBlock.Visibility = Visibility.Visible;
            SearchButton.IsEnabled = false;
        }

        private void SpiRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            FilterTextBox.Text = "";
            countryNameTextBlock.Visibility = Visibility.Visible;
            SearchButton.IsEnabled = false;
        }

        private void NaRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            FilterTextBox.Text = "";
            countryNameTextBlock.Visibility = Visibility.Visible;
            SearchButton.IsEnabled = false;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                AppViewBackButtonVisibility.Collapsed;
        }
    }
}
