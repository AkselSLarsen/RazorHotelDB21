using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB21.Interfaces;
using RazorHotelDB21.Model;

namespace RazorHotelDB21.Pages.Hotels
{
    public class IndexModel : PageModel {
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        public List<Hotel> Hotels { get; private set; }

        private IAsyncService<Hotel> hotelService;

        public IndexModel(IAsyncService<Hotel> hotelService) {
            this.hotelService = hotelService;
        }

        public async Task OnGet() {
            if (!String.IsNullOrEmpty(FilterCriteria)) {
                Hotels = await hotelService.GetItemsWithAttributeLike(0, FilterCriteria);
            } else
                Hotels = await hotelService.GetAllItems();
        }
    }
}