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
    public class EditModel : PageModel {
        private Room OldRoom {
            get {
                return new Room(OldRoom_No, OldHotel_No, OldTypes, OldPrice);
            }
            set {
                OldRoom_No = value.Room_No;
                OldHotel_No = value.Hotel_No;
                OldTypes = value.Types;
                OldPrice = value.Price;
            }
        }

        private Room NewRoom {
            get {
                return new Room(NewRoom_No, NewHotel_No, NewTypes, NewPrice);
            }
            set {
                NewRoom_No = value.Room_No;
                NewHotel_No = value.Hotel_No;
                NewTypes = value.Types;
                NewPrice = value.Price;
            }
        }

        [BindProperty(SupportsGet = true)]
        public int OldRoom_No { get; set; }
        [BindProperty(SupportsGet = true)]
        public int NewRoom_No { get; set; }
        [BindProperty(SupportsGet = true)]
        public int OldHotel_No { get; set; }
        [BindProperty(SupportsGet = true)]
        public int NewHotel_No { get; set; }
        [BindProperty(SupportsGet = true)]
        public RoomTypes OldTypes { get; set; }
        [BindProperty(SupportsGet = true)]
        public RoomTypes NewTypes { get; set; }
        [BindProperty(SupportsGet = true)]
        public double OldPrice { get; set; }
        [BindProperty(SupportsGet = true)]
        public double NewPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Error { get; set; }

        private IAsyncService<Room> roomService;

        public EditModel(IAsyncService<Room> roomService) {
            this.roomService = roomService;
        }

        public async Task OnGetAsync(string id) {
            string[] ids = id.Split('-');
            int Room_No = int.Parse(ids[0]);
            int Hotel_No = int.Parse(ids[1]);

            OldRoom = await roomService.GetItem(new int[] { Room_No, Hotel_No });

            NewRoom = OldRoom;
        }

        public async Task<IActionResult> OnPostEdit() {
            Error = await ConstraintHelper.RoomEditConstraints(OldRoom, NewRoom, roomService);
            if (Error != null) {
                return null;
            }

            bool success = await roomService.UpdateItem(NewRoom, new int[] { OldRoom.Room_No, OldRoom.Hotel_No });

            if (success) {
                return Redirect($"~/Rooms?id={OldHotel_No}");
            } else {
                Error = "Failed to update the room";
                return RedirectToPage("Edit", $"{OldRoom.Room_No}-{OldRoom.Hotel_No}");
            }
        }

        public async Task<IActionResult> OnPostDelete() {
            Room re = await roomService.DeleteItem(new int[] { OldRoom.Room_No, OldRoom.Hotel_No });

            if (re != null) {
                return Redirect($"~/Rooms?id={OldHotel_No}");
            } else {
                Error = "Failed to delete the room";
                return RedirectToPage("Edit", $"{OldRoom.Room_No}-{OldRoom.Hotel_No}");
            }
        }
    }
}