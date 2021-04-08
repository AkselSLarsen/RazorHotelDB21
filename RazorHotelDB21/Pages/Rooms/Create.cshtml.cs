using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB21.Interfaces;
using RazorHotelDB21.Model;
using RazorHotelDB21.Util;

namespace RazorHotelDB21.Pages.Rooms {
    public class CreateModel : PageModel {
        private IAsyncService<Hotel> hotelService;
        private IAsyncService<Room> roomService;

        [BindProperty(SupportsGet = true)]
        public string Error { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Hotel_No { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Room_No { get; set; }
        [BindProperty(SupportsGet = true)]
        public RoomTypes Types { get; set; }
        [BindProperty(SupportsGet = true)]
        public double Price { get; set; }

        public CreateModel(IAsyncService<Room> roomService, IAsyncService<Hotel> hotelService) {
            this.roomService = roomService;
            this.hotelService = hotelService;
        }

        public void OnGet(int id) {
            Hotel_No = id;
        }

        public async Task<IActionResult> OnPost() {
            Room room = new Room(Room_No, Hotel_No, Types, Price);

            Error = await ConstraintHelper.RoomCreateConstraints(room, roomService);
            if (Error != null) {
                return null;
            }

            bool success = await roomService.CreateItem(room);
            if (success) {
                return Redirect($"~/Rooms?id={Hotel_No}");
            } else {
                Error = "Failed to create the room";
                return RedirectToPage("Create", Hotel_No);
            }
        }
    }
}