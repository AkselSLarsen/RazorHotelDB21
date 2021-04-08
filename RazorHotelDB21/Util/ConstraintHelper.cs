using RazorHotelDB21.Interfaces;
using RazorHotelDB21.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorHotelDB21.Util {
    public static class ConstraintHelper {

        public static async Task<string> HotelCreateConstraints(Hotel hotel, IAsyncService<Hotel> hotelService) {
            if(HotelConstraints(hotel) != null) {
                return HotelConstraints(hotel);
            }

            Hotel h = await hotelService.GetItem(new int[] { hotel.Hotel_No });
            if (h != null) {
                return $"Hotel {h.Name} has that hotel number, please use another one.";
            }
            return null;
        }

        public static async Task<string> HotelEditConstraints(Hotel oldHotel, Hotel newHotel, IAsyncService<Hotel> hotelService) {
            if(oldHotel.Hotel_No == newHotel.Hotel_No) {
                if (HotelConstraints(newHotel) != null) {
                    return HotelConstraints(newHotel);
                }
                return null;
            } else {
                return await HotelCreateConstraints(newHotel, hotelService);
            }
        }

        private static string HotelConstraints(Hotel hotel) {
            if (hotel.Hotel_No < 1) {
                return "Hotel_No must be a positive integer.";
            }
            return null;
        }

        public static async Task<string> RoomCreateConstraints(Room room, IAsyncService<Room> roomService) {
            if (RoomConstraints(room) != null) {
                return RoomConstraints(room);
            }

            Room r = await roomService.GetItem(new int[] { room.Room_No, room.Hotel_No });
            if (r != null) {
                return $"Room number {r.Room_No} already exists in hotel with the number {r.Hotel_No}.";
            }
            return null;
        }

        public static async Task<string> RoomEditConstraints(Room oldRoom, Room newRoom, IAsyncService<Room> roomService) {
            if (oldRoom.Room_No == newRoom.Room_No && oldRoom.Hotel_No == newRoom.Hotel_No) {
                if (RoomConstraints(newRoom) != null) {
                    return RoomConstraints(newRoom);
                }
                return null;
            } else {
                return await RoomCreateConstraints(newRoom, roomService);
            }
        }

        private static string RoomConstraints(Room room) {
            if(room.Room_No < 1) {
                return "Room_No must be a positive integer.";
            } else if(room.Hotel_No < 1) {
                return "Hotel_No must be a positive integer.";
            }
            return null;
        }

    }
}
