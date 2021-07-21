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
    public class BeneficiaryController : Controller
    {
        private readonly ZDbContext _context;
        public BeneficiaryController(ZDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _context.Beneficiaries
                               .Include(x => x.ValidatedUser)
                               .Include(x => x.BeneficiaryType)
                               .Include(x => x.BenificaryCategoryQuantities).ThenInclude(x => x.Category)
                               .Include(x => x.Ward).ThenInclude(x => x.District).ThenInclude(x => x.City)
                               .Where(x => x.IsValidated == true).ToList();
            return View(list);
        }

        public IActionResult Propose()
        {
            var model = new BeneficaryProposeViewModel();
            model.AllBeneficiaryType = _context.BeneficiaryTypes.ToList();
            model.AllNeed = _context.Needs.Include(x => x.Categories).ToList();
            model.Cities = _context.Cities.Where(x => x.IsActive).ToList();
            return View(model);
        }

        public IActionResult ProposeSuccess()
        {
            return View();
        }

        public IActionResult CreatePropose(BeneficaryProposeViewModel model)
        {
            if (model == null || model.ProposetorPhone == null) return null;
            var beneficiary = new Beneficiary();


            beneficiary.ProposetorName = model.ProposetorName;
            beneficiary.ProposetorPhone = model.ProposetorPhone;
            beneficiary.OrganizationName = model.OrganizationName;
            beneficiary.BeneficiaryTypeId = model.BenificaryTypeId;
            beneficiary.HadTransportation = model.HadTransportation;
            beneficiary.NumberOfPeople = model.NumberOfPeople == null? 0 : model.NumberOfPeople.Value;

            if (model.BenificaryCategoryQuantities != null && model.BenificaryCategoryQuantities.Any())
            {
                beneficiary.BenificaryCategoryQuantities = model.BenificaryCategoryQuantities.Select(x => new BenificaryCategoryQuantity()
                {
                    CategoryId = x.CategoryId,
                    Quantity = x.Quantity
                }).ToList();
            }


            beneficiary.WardId = model.WardId == 0 ? null : model.WardId;
            beneficiary.Street = model.Street;
            beneficiary.ProposetorEmail = model.ProposetorEmail;

            beneficiary.CreatedAt = DateTime.Now;


            _context.Beneficiaries.Add(beneficiary);

            _context.SaveChanges();

            return RedirectToAction("ProposeSuccess");
        }

    }
}
