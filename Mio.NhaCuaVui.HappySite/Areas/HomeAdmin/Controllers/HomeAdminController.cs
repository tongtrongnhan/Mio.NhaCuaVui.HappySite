using Microsoft.AspNetCore.Mvc;
using Mio.NhaCuaVui.HappySite.Authentication;
using Mio.NhaCuaVui.HappySite.ExtensionMethod;
using Mio.NhaCuaVui.HappySite.Models;
using Mio.NhaCuaVui.HappySite.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Areas.HomeAdmin
{
    [Area("HomeAdmin")]

    public class HomeAdminController : Controller
    {
        private readonly ZDbContext _context;

        public HomeAdminController(ZDbContext context)
        {
            _context = context;
        }

        [BasedAuthentication()]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetHTMLCategory(int index, int categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            if (category == null) return Json(new { IsSuccess = false });

            var model = new CategoryIndexViewModel()
            {
                Category = category,
                Index = index
            };

            var html = this.RenderView("_CategoryIndex", model, true);

            return Json(new { IsSuccess = true, html = html });
        }

        public IActionResult CheckNumber(int donatorId, int categoryId, int number)
        {
            int maxNumber = 0;
            int pendingNumber = 0;
            var categoryQuantity = _context.DonationCategoryQuantities.FirstOrDefault(x => x.DonatorOrganizationId == donatorId && x.CategoryId == categoryId);
            if (categoryQuantity == null) return Json("");

            maxNumber = categoryQuantity.Quantity;

            var allPendingDeliveryQuantity = _context.DeliveryCategories.Where(x => x.CategoryId == categoryId && x.Delivery.DonatorOrganizationId == donatorId && x.Delivery.IsDelivery == false).ToList();
            if(allPendingDeliveryQuantity == null || allPendingDeliveryQuantity.Any() == false)
            {
                pendingNumber = 0;
            }    else
            {
                pendingNumber = allPendingDeliveryQuantity.Sum(x => x.Quantity);
            }


            if(maxNumber < pendingNumber + number)
            {
                return Json(new { IsSuccess = false, maxNumber = maxNumber, pendingNumber = pendingNumber, validNumber = (maxNumber-pendingNumber) });
            }


            return Json(new { IsSuccess = true });
        }
    }
}
