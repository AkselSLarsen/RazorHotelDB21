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
    public class CreateModel : PageModel {
        private IAsyncService<Hotel> hotelService;

        [BindProperty(SupportsGet = true)]
        public string Error { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Hotel_No { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Address { get; set; }


        public CreateModel(IAsyncService<Hotel> hotelService) {
            this.hotelService = hotelService;
        }

        public void OnGet() {

        }

        public async Task<IActionResult> OnPost() {
            Hotel hotel = new Hotel(Hotel_No, Name, Address);

            Error = await ConstraintHelper.HotelCreateConstraints(hotel, hotelService);
            if (Error != null) {
                return null;
            }

            bool success = await hotelService.CreateItem(hotel);
            if (success) {
                return RedirectToPage("Index");
            } else {
                Error = "Failed to create the hotel";
                return null;
            }
        }
    }
}