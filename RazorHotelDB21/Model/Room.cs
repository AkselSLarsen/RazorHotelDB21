using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorHotelDB21.Model {
    public class Room {
        private int room_No;
        private int hotel_No;
        private RoomTypes types;
        private double price;

        public Room(int room_No, int hotel_No, RoomTypes types, double price) {
            this.room_No = room_No;
            this.hotel_No = hotel_No;
            this.types = types;
            this.price = price;
        }

        public int Hotel_No {
            get { return hotel_No; }
            set { hotel_No = value; }
        }
        public int Room_No {
            get { return room_No; }
            set { room_No = value; }
        }
        public RoomTypes Types {
            get { return types; }
            set { types = value; }
        }
        public double Price {
            get { return price; }
            set { price = value; }
        }


        public override bool Equals(object obj) {
            if (obj is Room) {
                Room room = (Room)obj;

                if(this.Room_No == room.Room_No && this.Hotel_No == room.Hotel_No && this.Types == room.Types && this.Price == room.Price) {
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