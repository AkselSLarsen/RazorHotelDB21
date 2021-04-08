using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB21.Interfaces;
using RazorHotelDB21.Model;
using RazorHotelDB21.Util;

namespace RazorHotelDB21.Pages.Hotels {
    public class EditModel : PageModel {

        private Hotel OldHotel {
            get {
                return new Hotel(OldHotel_No, OldName, OldAddress);
            }
            set {
                OldHotel_No = value.Hotel_No;
                OldName = value.Name;
                OldAddress = value.Address;
            }
        }

        private Hotel NewHotel {
            get {
                return new Hotel(NewHotel_No, NewName, NewAddress);
            }
            set {
                NewHotel_No = value.Hotel_No;
                NewName = value.Name;
                NewAddress = value.Address;
            }
        }

        [BindProperty(SupportsGet = true)]
        public int OldHotel_No { get; set; }
        [BindProperty(SupportsGet = true)]
        public int NewHotel_No { get; set; }
        [BindProperty(SupportsGet = true)]
        public string OldName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string NewName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string OldAddress { get; set; }
        [BindProperty(SupportsGet = true)]
        public string NewAddress { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Error { get; set; }

        private IAsyncService<Hotel> hotelService;

        public EditModel(IAsyncService<Hotel> hotelService) {
            this.hotelService = hotelService;
        }

        public async Task OnGet(int id) {
            OldHotel = await hotelService.GetItem(new int[] { id });

            NewHotel = OldHotel;
        }

        public async Task<IActionResult> OnPostEdit() {
            Error = await ConstraintHelper.HotelEditConstraints(OldHotel, NewHotel, hotelService);
            if(Error != null) {
                return null;
            }

            bool success = await hotelService.UpdateItem(NewHotel, new int[] { OldHotel.Hotel_No });
            if(success) {
                return RedirectToPage("Index");
            } else {
                Error = "Failed to update the hotel";
                return RedirectToPage("Edit", OldHotel_No);
            }
        }

        public async Task<IActionResult> OnPostDelete() {
            Hotel re = await hotelService.DeleteItem(new int[] { OldHotel.Hotel_No });

            if(re != null) {
                return RedirectToPage("Index");
            } else {
                Error = "Failed to delete the hotel";
                return Page();
            }
        }
    }
}