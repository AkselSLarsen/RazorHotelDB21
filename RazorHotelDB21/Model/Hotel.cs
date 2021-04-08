using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorHotelDB21.Model {
    public class Hotel {
        private int hotel_No;
        private string name;
        private string address;

        public Hotel(int hotel_No, string name, string address) {
            this.hotel_No = hotel_No;
            this.name = name;
            this.address = address;
        }

        public int Hotel_No {
            get { return hotel_No; }
            set { hotel_No = value; }
        }
        public string Name {
            get { return name; }
            set { name = value; }
        }
        public string Address {
            get { return address; }
            set { address = value; }
        }

        public override bool Equals(object obj) {
            if (obj is Hotel) {
                Hotel hotel = (Hotel)obj;

                if (this.Hotel_No == hotel.Hotel_No && this.Name == hotel.Name && this.Address == hotel.Address) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }
    }
}
