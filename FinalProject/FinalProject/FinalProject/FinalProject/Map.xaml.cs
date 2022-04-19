using FinalProject.Models;
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
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;
using System.Threading.Tasks;
using Windows.UI.WindowManagement;
using FinalProject.ViewModels;
using Windows.Storage.Streams;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Map : Page
    {
        public Map MyAppWindow { get; set; }
        public FlightViewModel cvm { get; set; }
        public Flight flight;
        
        public Map()
        {
            this.DataContext = cvm;
            InitializeComponent();
            



        }
        public Map(FlightViewModel cvm)
        {
            
            //this.InitializeComponent();
            
            //System.Diagnostics.Debug.WriteLine("Selected Name at line 40 " + cvm.SelectedFlight.Name);

        }
        public void AddSpaceNeedleIcon(Flight flight)
        {
            cvm = new FlightViewModel();
            

            var MyLandmarks = new List<MapElement>();
            
            System.Diagnostics.Debug.WriteLine("Name " + flight.Name);
            System.Diagnostics.Debug.WriteLine("Country " + flight.Country);
            System.Diagnostics.Debug.WriteLine("Longitude " + flight.Longitude);
            System.Diagnostics.Debug.WriteLine("Latitude " + flight.Latitude);
            System.Diagnostics.Debug.WriteLine("Velocity " + flight.Velocity);

            

            BasicGeoposition snPosition = new BasicGeoposition { Latitude = Convert.ToDouble(flight.Latitude), Longitude = Convert.ToDouble(flight.Longitude) };
            Geopoint snPoint = new Geopoint(snPosition);

           

            var planeIcon = new MapIcon
            {
                Location = snPoint,
                NormalizedAnchorPoint = new Point(0.5, 1.0),
                ZIndex = 0,
                Title = flight.Name
            };
            planeIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/ResizedPlane.scale-100 .png"));
            MyLandmarks.Add(planeIcon);

            var LandmarksLayer = new MapElementsLayer
            {
                ZIndex = 1,
                MapElements = MyLandmarks
            };

            myMap.Layers.Add(LandmarksLayer);

            myMap.MapServiceToken = "Agn0WVzQpdp__REsIWHaC4Wj5Av2FGD1clxxRvyEzkGUzXg7t1JrKJ4X-Dvd08Rc";
            myMap.Center = snPoint;
            myMap.ZoomLevel = 7;

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           
            
            flight  = e.Parameter as Flight;

            AddSpaceNeedleIcon(flight);

            System.Diagnostics.Debug.WriteLine("Selected Name at line 55 " + flight);

        }

        public static implicit operator Map(AppWindow v)
        {
            throw new NotImplementedException();
        }

       
    }
}
