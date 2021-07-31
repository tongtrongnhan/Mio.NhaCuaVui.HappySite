using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mio.NhaCuaVui.HappySite.Models;
using Mio.NhaCuaVui.HappySite.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ZDbContext _context;
        public HomeController(ZDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new HomePageViewModel();

            model.NumberOfDonator = _context.DonatorOrganizations.Count(x => x.IsValidated == true);
            model.NumberOfPeople = _context.Beneficiaries.Where(x => x.IsValidated == true).Sum(x => x.NumberOfPeople);

            model.DonatorCategories = _context.DonationCategoryQuantities
                                        .Include(x => x.Category).ThenInclude(x => x.Need)
                                        .Where(x => x.DonatorOrganization.IsValidated == true).ToList()
                                        .GroupBy(x => x.Category).Select(x => new DonationCategoryQuantity()
                                        {
                                            Category = x.Key,
                                            Quantity = x.Sum(s => s.Quantity)
                                            
                                        })
                                        .ToList();

            model.BenificaryCategories = _context.BenificaryCategoryQuantities
                                        .Include(x => x.Category).ThenInclude(x => x.Need)
                                        .Where(x => x.Beneficiary.IsValidated == true).ToList()
                                        .GroupBy(x => x.Category).Select(x => new BenificaryCategoryQuantity()
                                        {
                                            Category = x.Key,
                                            Quantity = x.Sum(s => s.Quantity)

                                        })
                                        .ToList();


            return View(model);
        }


        public IActionResult GetDistrict(int CityId)
        {
            var districts = _context.Districts.Where(x => x.CityId == CityId).ToList();
            if (districts == null || districts.Count == 0) return Json("[]");

            return Json(districts.Select(x => new { text = x.Name, id = x.DistrictId }).ToArray());
        }

        public IActionResult GetWard(int DistrictId)
        {
            var wards = _context.Wards.Where(x => x.DistrictId == DistrictId).ToList();
            if (wards == null || wards.Count == 0) return Json((new List<Ward>() { new Ward() { WardId = 0, Name = "Vui lòng chọn Quận trước" } }).Select(x => new { text = x.Name, id = x.WardId }).ToArray());

            return Json(wards.Select(x => new { text = x.Name, id = x.WardId }).ToArray());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Instruction()
        {
            return View();
        }
    }
}
