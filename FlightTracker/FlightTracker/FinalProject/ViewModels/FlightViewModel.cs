using FlightTracker.Commands;
using FlightTracker.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.Devices.Geolocation;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace FlightTracker.ViewModels
{
    public class FlightViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<Flight> Flights = new ObservableCollection<Flight>();
        
        public ObservableCollection<Flight> FlightsTemp = new ObservableCollection<Flight>();

        public ObservableCollection<Flight> FlightsSpi = new ObservableCollection<Flight>();

        public ObservableCollection<Flight> FlightsGround = new ObservableCollection<Flight>();

        public ObservableCollection<Flight> FlightsNa = new ObservableCollection<Flight>();

        public event PropertyChangedEventHandler PropertyChanged;

        private Flight _selectedFlight;

        private string _filter;
        private Geopoint snPoint;

        static HttpClient client = new HttpClient();

        public List<Flight> _allFlights = new List<Flight>();

        public GroundCommand groundCommand = new GroundCommand();

        public SpiCommand spiCommand = new SpiCommand();

        public naCommand naCommand = new naCommand();

      //  public ExitCommand ExitCommand { get; set; }
        public Geopoint FlightCenter { get; set; }
        public string FlightName { get; set; }
        public string FlightNum { get; set; }
        public string FlightCountry { get; set; }
        public string FlightLong { get; set; }
        public string FlightLat { get; set; }
        public string FlightVel { get; set; }
        public string FlightGround { get; set; }
        public string FlightSpi { get; set; }
       
       
        public FlightViewModel()
        {
            LoadFlights();
            groundCommand.OnGrounded += GroundCommand_OnGrounded;
            spiCommand.OnSpiFlight += SpiCommand_OnSpiFlight;
            naCommand.OnNaFlight += NaCommand_OnNaFlight;
        }

        private void NaCommand_OnNaFlight(object sender, EventArgs e)
        {
            Flights.Clear(); // clear existing flights data
            _allFlights.Clear();

            foreach (var flight in FlightsNa)
            {
                Flights.Add(flight); // readd flights data from the API
                _allFlights.Add(flight);
            }
        }

        private void SpiCommand_OnSpiFlight(object sender, EventArgs e)
        {
            FlightsTemp.Clear();

            for (int i = 0; i < FlightsSpi.Count; i++)
            {
                if (FlightsSpi[i].SpiFlight == "True")
                {
                    FlightsTemp.Add(FlightsSpi[i]); // Find SPI flight == true and then and save the flight into Flight2 collection
                }
            }

            Flights.Clear(); // to display spi true flights, clear existing flights
            _allFlights.Clear();
            foreach (Flight FlightTemp in FlightsTemp) // from temp collection, add only SPI flights true flights 
            {
                Flights.Add(FlightTemp);
                _allFlights.Add(FlightTemp);
            }
        }

        private void GroundCommand_OnGrounded(object sender, EventArgs e)
        {
            FlightsTemp.Clear();

            for (int i = 0; i < FlightsGround.Count; i++)
            {
                if (FlightsGround[i].OnGround == "True")
                {
                    FlightsTemp.Add(FlightsGround[i]); // Find onGround == true and then and save the flight into Flight2 collection
                }
            }

            Flights.Clear(); // to display onground true flights, clear existing flights
            _allFlights.Clear();
            foreach (Flight FlightTemp in FlightsTemp) // from temp collection, add only onGround true flights 
            {
                Flights.Add(FlightTemp);
                _allFlights.Add(FlightTemp);
            }
        }
        public ICommand ViewOnMapCommand
        {
            get
            {
                return new ViewOnMapCommand(viewOnMapCommand);
            }
        }

        // Track open app windows in a Dictionary.
        public static Dictionary<UIContext, AppWindow> AppWindows { get; set; }
            = new Dictionary<UIContext, AppWindow>();

        private async void viewOnMapCommand()
        {
            Map map = new Map(); 
            map.cvm = this;
            map.cvm.SelectedFlight = SelectedFlight;
            //map.Add
            // Create a new window.
            AppWindow appWindow = await AppWindow.TryCreateAsync();

            // Create a Frame and navigate to the Page you want to show in the new window.
            Frame appWindowContentFrame = new Frame();
            appWindowContentFrame.Navigate(typeof(Map), SelectedFlight);


            // Attach the XAML content to the window.
            ElementCompositionPreview.SetAppWindowContent(appWindow, appWindowContentFrame);

            // Add the new page to the Dictionary using the UIContext as the Key.
            AppWindows.Add(appWindowContentFrame.UIContext, appWindow);
            appWindow.Title = "Flight: " +  SelectedFlight.Name;

            // When the window is closed, be sure to release
            // XAML resources and the reference to the window.
            appWindow.Closed += delegate
            {
                MainPage.AppWindows.Remove(appWindowContentFrame.UIContext);
                appWindowContentFrame.Content = null;
                appWindow = null;
            };

            // Show the window.
            await appWindow.TryShowAsync();

           


        }



        public Flight SelectedFlight
        {
            get
            {
                return _selectedFlight;
            }
            set
            {
                _selectedFlight = value;
                if (value == null)
                {
                    FlightName = "No flight selected";
                    FlightName = "";
                    FlightNum = "";
                    FlightCountry = "";
                    FlightLong = "";
                    FlightLat = "";
                    FlightVel = "";
                    FlightGround = "";
                    FlightSpi = "";
                }
                else
                {
                    BasicGeoposition snPosition = new BasicGeoposition { Latitude = Convert.ToDouble(SelectedFlight.Latitude), Longitude = Convert.ToDouble(SelectedFlight.Longitude) };
                    Geopoint snPoint = new Geopoint(snPosition);

                    FlightCenter = snPoint;
                    FlightName = "Name: " + value.Name;
                    FlightNum = "Number: " + value.Number;
                    FlightCountry = "Country: " + value.Country;
                    FlightLong = "Longitude: " + value.Longitude;
                    FlightLat = "Latitude: " + value.Latitude;
                    FlightVel = "Velocity: " + value.Velocity;
                    FlightGround = "On Ground: " + value.OnGround;
                    FlightSpi = "SPI Flgiht: " + value.SpiFlight;

                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FlightName"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FlightNum"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FlightCountry"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FlightLong"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FlightLat"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FlightVel"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FlightGround"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FlightSpi"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FlightCenter"));
            }
        }
        public Geopoint Center
        {
            get
            {
                return snPoint;

            }
            set
            {
                snPoint = value;
                if (value == snPoint) { return; }
 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Center"));
               
            }
        }



        public string Filter
        {
            get { return _filter; }
            set
            {
                if (_filter == value) { return; }
                _filter = value;
                // Call our filtering method
                PerformFiltering();
                // Invoke the PropertyChanged 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Filter"));
            }
        }

        private void PerformFiltering()
        {
            if (_filter == null)
            {
                _filter = "";
            }
            //If _filter has a value (ie. user entered something in Filter textbox)
            //Lower-case and trim string
            var lowerCaseFilter = Filter.ToLowerInvariant().Trim();

            //Use LINQ query to get all flight model country that match filter text, as a list
            var result =
                _allFlights.Where(d => d.FlightsAsString.ToLowerInvariant()
                .Contains(lowerCaseFilter))
                .ToList();

            //Get list of values in current filtered list that we want to remove
            //(ie. don't meet new filter criteria)
            var toRemove = Flights.Except(result).ToList();

            //Loop to remove items that fail filter
            foreach (var x in toRemove)
            {
                Flights.Remove(x);
            }

            var resultCount = result.Count;
            // Add back in correct order.
            for (int i = 0; i < resultCount; i++)
            {
                var resultItem = result[i];
                if (i + 1 > Flights.Count || !Flights[i].Equals(resultItem))
                {
                    Flights.Insert(i, resultItem);
                }
            }
        }

        public void LoadFlights()
        {
            //https://stackoverflow.com/questions/55292746/reading-json-data-from-external-api-using-c-sharp

            string id_url = "https://opensky-network.org/api/states/all";

          
            WebRequest requst = WebRequest.Create(id_url);
            requst.Method = "GET";

            HttpWebResponse response = requst.GetResponse() as HttpWebResponse;

            var encod = ASCIIEncoding.ASCII;

            using (var flights = new System.IO.StreamReader(response.GetResponseStream(), encod))
            {
                string result = flights.ReadToEnd();
                var json = JObject.Parse(result);
                Parse(json);
            }
        }

        public void Parse(JObject json)
        {
            var test = json.GetValue("states");
            foreach (var obj in test)
            {
                
                Flight tmp = new Flight(obj[1].ToString(), obj[0].ToString(),
                obj[2].ToString(), obj[5].ToString(), obj[6].ToString(), obj[9].ToString(), obj[8].ToString(), obj[15].ToString());

                Flights.Add(tmp);
                _allFlights.Add(tmp);

                FlightsSpi.Add(tmp);
                FlightsGround.Add(tmp);
                FlightsNa.Add(tmp);
            }
        }
    }

}
