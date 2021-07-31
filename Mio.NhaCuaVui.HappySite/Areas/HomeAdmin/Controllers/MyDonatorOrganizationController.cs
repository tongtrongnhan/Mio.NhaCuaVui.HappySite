using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mio.NhaCuaVui.HappySite.Authentication;
using Mio.NhaCuaVui.HappySite.ExtensionMethod;
using Mio.NhaCuaVui.HappySite.Models;
using Mio.NhaCuaVui.HappySite.Models.ViewModels;
using Mio.NhaCuaVui.HappySite.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Areas.HomeAdmin.Controllers
{
    [Area("HomeAdmin")]
    [BasedAuthentication("Donator")]

    public class MyDonatorOrganizationController : Controller
    {
        private readonly ZDbContext _context;

        public MyDonatorOrganizationController(ZDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetCurrentAuthentication();
            var userInDb = _context.Users.FirstOrDefault( x => x.UserId == user.UserId);

            var model = new MyDonatorOrganizationViewModel();

            if(userInDb == null || userInDb.MyDonatorOrganizationId == null)
            {
                model.HadOrganization = false;
                return View(model);
            }

            model.HadOrganization = true;
            model.Donator = _context.DonatorOrganizations
                            .Include(x => x.DonationCategoryQuantities).ThenInclude(x => x.Category).ThenInclude(x => x.Need)    
                            .FirstOrDefault(x => x.DonatorOrganizationId == userInDb.MyDonatorOrganizationId.Value);

            model.PendingForDeliveries = _context.Deliveries.Include(x => x.DeliveryCategories).ThenInclude(x => x.Category).ThenInclude(x => x.Need)
                                                            .Include(x => x.UserCreate)
                                                            .Include(x => x.Beneficiary).ThenInclude(x => x.Ward).ThenInclude(x => x.District).ThenInclude(x => x.City)
                                                            .Where(x =>
                                                                   x.DonatorOrganizationId == userInDb.MyDonatorOrganizationId
                                                                   && x.IsValidated == false
                                                                   && x.IsDelivery == false).ToList();

            model.Deliveries = _context.Deliveries.Include(x => x.DeliveryCategories).ThenInclude(x => x.Category).ThenInclude(x => x.Need)
                                                           .Include(x => x.UserCreate)
                                                           .Include(x => x.Beneficiary).ThenInclude(x => x.Ward).ThenInclude(x => x.District).ThenInclude(x => x.City)
                                                           .Where(x =>
                                                                  x.DonatorOrganizationId == userInDb.MyDonatorOrganizationId
                                                                  && x.IsDelivery == true).ToList();


            return View(model);
        }


        public IActionResult Manager(int id)
        {

            var model = new MyDonatorOrganizationViewModel();

            model.HadOrganization = true;
            model.Donator = _context.DonatorOrganizations
                            .Include(x => x.DonationCategoryQuantities).ThenInclude(x => x.Category).ThenInclude(x => x.Need)
                            .FirstOrDefault(x => x.DonatorOrganizationId == id);

            model.PendingForDeliveries = _context.Deliveries.Include(x => x.DeliveryCategories).ThenInclude(x => x.Category).ThenInclude(x => x.Need)
                                                            .Include(x => x.UserCreate)
                                                            .Include(x => x.Beneficiary).ThenInclude(x => x.Ward).ThenInclude(x => x.District).ThenInclude(x => x.City)
                                                            .Where(x =>
                                                                   x.DonatorOrganizationId == id
                                                                   && x.IsValidated == false
                                                                   && x.IsDelivery == false).ToList();

            model.Deliveries = _context.Deliveries.Include(x => x.DeliveryCategories).ThenInclude(x => x.Category).ThenInclude(x => x.Need)
                                                           .Include(x => x.UserCreate)
                                                           .Include(x => x.Beneficiary).ThenInclude(x => x.Ward).ThenInclude(x => x.District).ThenInclude(x => x.City)
                                                           .Where(x =>
                                                                  x.DonatorOrganizationId == id
                                                                  && x.IsDelivery == true).ToList();


            return View(model);
        }




        public IActionResult AddNewItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Needs = _context.Needs.Include(x => x.Categories).ToList();

            var donatorOrganization = _context.DonatorOrganizations
                .Include(d => d.DonatorOrganizationType)
                .Include(d => d.Need)
                .Include(d => d.ValidatedUser)
                .Include(d => d.Ward)
                .FirstOrDefaultAsync(m => m.DonatorOrganizationId == id).Result;


            if (donatorOrganization == null)
            {
                return NotFound();
            }

            return View(donatorOrganization);
        }


        [HttpPost]
        public IActionResult AddNewItem(DonatorOrganization donator)
        {
            if (donator == null || donator.DonationCategoryQuantities == null) return RedirectToAction("Index");
            var model = donator.DonationCategoryQuantities.Where(x => x.Quantity > 0).ToList();
            if (model == null || model.Any() == false) return RedirectToAction("Index");

            var donatorService = new DonatorService(_context);

            donatorService.AddNewItem(donator);

            return RedirectToAction("Index");
        }


        public IActionResult DeliveryManagerCreate(int id)
        {
            var user = HttpContext.Session.GetCurrentAuthentication();
            var userInDb = _context.Users.FirstOrDefault(x => x.UserId == user.UserId);

            // check permission
            if (userInDb == null || userInDb.MyDonatorOrganizationId == null)
            {
                return RedirectToAction("Index");
            }

            var model = new DeliveryCRUDViewModel();
            model.DonatorOrganizationId = id;
            model.MyCategories = _context.DonationCategoryQuantities.Include(x => x.Category).ThenInclude(x => x.Need)
                                                                    .Where(x => x.DonatorOrganizationId == id && x.Quantity > 0)
                                                                    .Select(x => x.Category).ToList();
            model.AllActiveBeneficiary = _context.Beneficiaries.Where(x => x.IsValidated == true).ToList();


            return View(model);
        }

        [HttpPost]
        public IActionResult DeliveryManagerCreate(DeliveryCRUDViewModel model)
        {
            var user = HttpContext.Session.GetCurrentAuthentication();
            var userInDb = _context.Users.FirstOrDefault(x => x.UserId == user.UserId);

            // check permission
            if (userInDb == null || userInDb.MyDonatorOrganizationId == null)
            {
                return RedirectToAction("Index");
            }

            var delivery = new Delivery()
            {
                BeneficiaryId = model.BeneficiaryId,
                BeneficiaryName = model.BeneficiaryName,
                CreatedAt = DateTime.Now,
                DeliveryDate = model.DeliveryDate,
                DonatorOrganizationId = model.DonatorOrganizationId,
                UserCreateId = user.UserId,

            };

            if(model.DeliveryCategories != null && model.DeliveryCategories.Any())
            {
                delivery.DeliveryCategories = model.DeliveryCategories.Select(x => new DeliveryCategory()
                {
                    CategoryId = x.CategoryId,
                    Quantity = x.Quantity
                }).ToList();
            }

            _context.Deliveries.Add(delivery);
            _context.SaveChanges();

            if(userInDb.MyDonatorOrganizationId != model.DonatorOrganizationId)
            {
                return RedirectToAction("Manager", new { id= model .DonatorOrganizationId});
            }    

            return RedirectToAction("Index");
        }

        public IActionResult DeliveryAction(int id)
        {
            var delivery = _context.Deliveries
                                        .Include(x => x.Beneficiary)
                                        .Include(x => x.DeliveryCategories).ThenInclude(x => x.Category)
                                        .FirstOrDefault(x => x.DeliveryId == id && x.IsDelivery == false && x.IsValidated == false);
            return View(delivery);
        }

        [HttpPost]

        public IActionResult DeliveryAction(Delivery model)
        {
            var delivery = _context.Deliveries
                                        .Include(x => x.Beneficiary)
                                        .Include(x => x.DeliveryCategories).ThenInclude(x => x.Category)
                                        .FirstOrDefault(x => x.DeliveryId == model.DeliveryId && x.IsDelivery == false && x.IsValidated == false);

            if(delivery.DeliveryCategories != null || delivery.DeliveryCategories.Any())
            {
                foreach(var item in delivery.DeliveryCategories)
                {
                    item.Quantity = model.DeliveryCategories.First(x => x.CategoryId == item.CategoryId).Quantity;
                }

                var donator = _context.DonatorOrganizations.Include(x => x.DonationCategoryQuantities).FirstOrDefault(x => x.DonatorOrganizationId == delivery.DonatorOrganizationId);
                if (donator != null && donator.DonationCategoryQuantities != null && donator.DonationCategoryQuantities.Any())
                {
                    foreach (var category in donator.DonationCategoryQuantities)
                    {
                        var newCategory = model.DeliveryCategories.FirstOrDefault(x => x.CategoryId == category.CategoryId);
                        int updateCategoryQuantity = newCategory == null? 0 : newCategory.Quantity;

                        category.Quantity = category.Quantity - updateCategoryQuantity;

                    }
                }

            }


            delivery.DeliveredAt = DateTime.Now;
            delivery.IsDelivery = true;
            delivery.IsValidated = null;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
