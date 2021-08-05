using Microsoft.AspNetCore.Mvc;
using Mio.NhaCuaVui.HappySite.Models;
using Mio.NhaCuaVui.HappySite.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mio.NhaCuaVui.HappySite.Controllers
{
    public class ShipperRequestController : Controller
    {
        private readonly ZDbContext _context;
        public ShipperRequestController(ZDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = new ShipperRequestViewModel();
            model.AvailableDate = DateTime.Today;
            model.Cities = _context.Cities.Where(x => x.IsActive).ToList();
            model.Hours = GetHoursList();
            model.AvailableFromHour = 7;
            model.AvailableToHour = 10;
            return View(model);
        }
        private List<SelectListItem> GetHoursList()
        {
            var result = new List<SelectListItem>();
            for (int i = 0; i < 24; i++)
            {
                result.Add(new SelectListItem
                {
                    Id = i.ToString(),
                    Text = i.ToString() + " : 00",
                });
            }
            return result;
        }
        [HttpPost]
        public ActionResult CreateShipperRequest(ShipperRequestViewModel input)
        {
            if (input == null) return null;
            var shipperRequest = new ShipperRequest();
            shipperRequest.ShipperName = input.ShipperName;
            shipperRequest.ShipperPhone = input.ShipperPhone;
            shipperRequest.Transportation = input.Transportation;
            shipperRequest.AvailableDate = input.AvailableDate;
            shipperRequest.AvailableFromHour = input.AvailableFromHour;
            shipperRequest.AvailableToHour = input.AvailableToHour;
            shipperRequest.CreatedAt = DateTime.Now;
            shipperRequest.DistrictId = input.DistrictId;

            _context.ShipperRequests.Add(shipperRequest);
            _context.SaveChanges();

            return View("Success");
        }
    }
}
