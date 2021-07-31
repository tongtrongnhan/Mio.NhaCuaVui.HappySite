using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mio.NhaCuaVui.HappySite.Models;

namespace Mio.NhaCuaVui.HappySite.Controllers
{
    public class ReportController : Controller
    {
        private readonly ZDbContext _context;
        public ReportController(ZDbContext context)
        {
            _context = context;
        }

        public IActionResult DeliverySuccess()
        {
            var allDeliveryValid = _context.Deliveries
                                        .Include(x => x.DonatorOrganization)
                                        .Include(x => x.ValidatedByUser)
                                        .Include(x => x.Beneficiary).ThenInclude(x => x.Ward).ThenInclude(x => x.District).ThenInclude(x => x.City)
                                        .Include(x => x.DeliveryCategories).ThenInclude(x => x.Category).ThenInclude(x => x.Need)
                                        .Where(x => x.IsValidated == true && x.BeneficiaryId != null).OrderByDescending(x => x.DeliveryId).ToList();

            return View(allDeliveryValid);
        }
    }
}
