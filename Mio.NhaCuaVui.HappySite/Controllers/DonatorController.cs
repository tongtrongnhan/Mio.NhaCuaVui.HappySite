using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mio.NhaCuaVui.HappySite.Models;
using Mio.NhaCuaVui.HappySite.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Controllers
{
    public class DonatorController : Controller
    {
        private readonly ZDbContext _context;
        public DonatorController(ZDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _context.DonatorOrganizations
                                .Include(x => x.ValidatedUser)
                                .Include(x => x.DonatorOrganizationType)
                                .Include(x => x.DonationCategoryQuantities).ThenInclude(x => x.Category)
                                .Include(x => x.Ward).ThenInclude(x => x.District).ThenInclude(x => x.City)
                                .Where(x => x.IsValidated == true).ToList();
            return View(list);
        }


        public IActionResult Propose()
        {
            var model = new DonatorProposeViewModel();
            model.AllDonatorOrganizationType = _context.DonatorOrganizationTypes.ToList();
            model.AllNeed = _context.Needs.Include(x => x.Categories).ToList();
            model.Cities = _context.Cities.Where(x => x.IsActive).ToList();
            return View(model);
        }

        public IActionResult ProposeSuccess()
        {
            return View();
        }

        public IActionResult CreatePropose(DonatorProposeViewModel model)
        {
            if (model == null || model.ProposetorPhone == null) return null;
            var donator = new DonatorOrganization();

            donator.ProposetorName = model.ProposetorName;
            donator.ProposetorPhone = model.ProposetorPhone;
            donator.OrganizationName = model.OrganizationName;
            donator.DonatorOrganizationTypeId = model.DonatorOrganizationTypeId;
            donator.HadTransportation = model.HadTransportation;

            if(model.DonationCategoryQuantities != null && model.DonationCategoryQuantities.Any())
            {
                donator.DonationCategoryQuantities = model.DonationCategoryQuantities.Select(x => new DonationCategoryQuantity()
                {
                    CategoryId = x.CategoryId,
                    Quantity = x.Quantity
                }).ToList();
            }


            donator.WardId = model.WardId == 0? null : model.WardId;
            donator.Street = model.Street;
            donator.ProposetorEmail = model.ProposetorEmail;

            donator.CreatedAt = DateTime.Now;


            _context.DonatorOrganizations.Add(donator);



            _context.SaveChanges();

            return RedirectToAction("ProposeSuccess");
        }

    }
}
