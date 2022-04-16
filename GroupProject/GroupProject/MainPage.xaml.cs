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
using System.Net.Http;      //*HttpClient
using Windows.Data.Json;    //*JsonObject

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GroupProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }


        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            read_API_Data();
        }

        public async void read_API_Data()
        {

            //---------------< read_API_Data() >---------------

            //< read webApi >

            string sURL = "https://opensky-network.org/api/states/all";

            HttpClient client = new HttpClient();

            string sResponse = await client.GetStringAsync(sURL);

            //</ read webApi >

            //< get Json values >
            JsonArray jsonArray = JsonArray.Parse(sResponse);

            foreach (var jsonRow in jsonArray)

            {

                //----< json Row >----

                JsonObject jsonObject = jsonRow.GetObject();



                //< values >

                string icao24 = jsonObject["icao24"].ToString();

                string callsign = jsonObject["callsign"].ToString();

                string origin_country = jsonObject["origin_country"].ToString();

                string longitude = jsonObject["longitude"].ToString();

                //</ values >



                tbxResults.Text += Environment.NewLine + "-------";

                tbxResults.Text += Environment.NewLine + "icao24=" + icao24;

                tbxResults.Text += Environment.NewLine + "callsign=" + callsign;

                tbxResults.Text += Environment.NewLine + "origin_country=" + origin_country;

                tbxResults.Text += Environment.NewLine + "longitude=" + longitude;

                //----</ json Row >----

            }
        }
    }
}
