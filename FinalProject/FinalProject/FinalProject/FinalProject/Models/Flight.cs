using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Flight
    {

        public string Name { get; set; }
        public string Number { get; set; }
        public string Country { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Velocity { get; set; }
        public string OnGround { get; set; }

        public string SpiFlight { get; set; }

        public string FlightsAsString => string.Join(",", Country);

        override public string ToString()
        {
            return Name + ", " + Number + ", " + Country + ", " + Longitude + ", " + Latitude;
        }

        public Flight(string name, string num, string country, string lng, string lat, string vel, string onGround, string spiFlight)
        {
            if(name == "")
            {
                Name = "NULL";
            } else
            {
                Name = name;
            }

            if(num == "")
            {
                Number = "NULL";
            }
            else
            {
                Number = num;
            }

            if(country == "")
            {
                Country = "NULL";
            }
            else
            {
                Country = country;
            }

            if(lng == "")
            {
                Longitude = "NULL";
            }
            else
            {
                Longitude = lng;
            }

            if(lat == "")
            {
                Latitude = "NULL";
            }
            else
            {
                Latitude = lat;
            }

            if(vel == "")
            {
                Velocity = "NULL";
            }
            else
            {
                Velocity = vel;
            }

            if (onGround == "")
            {
                OnGround = "NULL";
            }
            else
            {
                OnGround = onGround;
            }

            if (spiFlight == "")
            {
                SpiFlight = "NULL";
            }
            else
            {
                SpiFlight = spiFlight;
            }

        }

    }
}
