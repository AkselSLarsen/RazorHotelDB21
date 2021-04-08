using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB21.Interfaces;
using RazorHotelDB21.Model;

namespace RazorHotelDB21.Pages.Rooms {
    public class IndexModel : PageModel {
        private IAsyncService<Room> roomService;
        private IAsyncService<Hotel> hotelService;

        [BindProperty(SupportsGet = true)]
        public List<Room> Rooms { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Hotel_No { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Show { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }


        public IndexModel(IAsyncService<Room> roomService, IAsyncService<Hotel> hotelService) {
            this.roomService = roomService;
            this.hotelService = hotelService;
        }

        public async Task OnGet(int id) {
            Hotel_No = id;

            Rooms = await roomService.GetItemsWithKey(1, Hotel_No);

            List<Room> tmp = new List<Room>();
            RoomTypes type = RoomTypes.S;
            bool showAll = true;
            if (Show == "S") {
                showAll = false;
            } else if (Show == "D") {
                type = RoomTypes.D;
                showAll = false;
            } else if (Show == "F") {
                type = RoomTypes.F;
                showAll = false;
            }

            if (!showAll) {
                foreach (Room room in Rooms) {
                    if (room.Types == type) {
                        tmp.Add(room);
                    }
                }
                Rooms = tmp;
            }

            if(SortBy == "Room_No") {
                Rooms.Sort(
                    Comparer<Room>.Create((x,y) => x.Room_No > y.Room_No ? 1 : x.Room_No < y.Room_No ? -1 : 0)
                );
            } else if(SortBy == "Price") {
                Rooms.Sort(
                    Comparer<Room>.Create((x, y) => x.Price > y.Price ? 1 : x.Price < y.Price ? -1 : 0)
                );
            }
        }

        public async Task OnPostSort() {
            await OnGet(Hotel_No);
        }
    }
}