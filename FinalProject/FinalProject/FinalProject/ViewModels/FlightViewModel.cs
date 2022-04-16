using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Models;
using Newtonsoft.Json.Linq;

namespace FinalProject.ViewModels
{
    public class FlightViewModel : INotifyPropertyChanged
    {
       
        public ObservableCollection<Flight> Flights = new ObservableCollection<Flight>();

        public event PropertyChangedEventHandler PropertyChanged;

        private Flight _selectedFlight;

        private string _filter;

        static HttpClient client = new HttpClient();

        public List<Flight> _allFlights = new List<Flight>();

        public string FlightName { get; set; }
        public string FlightNum { get; set; }
        public string FlightCountry { get; set; }
        public string FlightLong { get; set; }
        public string FlightLat { get; set; }
        public string FlightVel { get; set; }
        public FlightViewModel()
        {
            LoadFlights();
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
                }
                else
                {
                    FlightName = "Name: " + value.Name;
                    FlightNum = "Number: " + value.Number;
                    FlightCountry = "Country: " + value.Country;
                    FlightLong = "Longitude: " + value.Longitude;
                    FlightLat = "Latitude: " + value.Latitude;
                    FlightVel = "Velocity: " + value.Velocity;

                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FlightName"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FlightNum"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FlightCountry"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FlightLong"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FlightLat"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FlightVel"));
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

            //Use LINQ query to get all note model names that match filter text, as a list
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
            // https://stackoverflow.com/questions/55292746/reading-json-data-from-external-api-using-c-sharp

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
                //if (obj[2].ToString() == "Canada")
                //{
                    Flight tmp = new Flight(obj[1].ToString(), obj[0].ToString(),
                    obj[2].ToString(), obj[5].ToString(), obj[6].ToString(), obj[9].ToString(), obj[8].ToString());

                    Flights.Add(tmp);
                    _allFlights.Add(tmp);
                //}
            }
        }
    }

}
