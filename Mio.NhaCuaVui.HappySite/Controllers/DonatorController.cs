using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mio.NhaCuaVui.HappySite.Models;
using Mio.NhaCuaVui.HappySite.Models.ViewModels;
using Mio.NhaCuaVui.HappySite.Service;
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


        public IActionResult Propose(int? id)
        {
            var model = new DonatorProposeViewModel();
            model.AllDonatorOrganizationType = _context.DonatorOrganizationTypes.ToList();
            model.AllNeed = _context.Needs.Include(x => x.Categories).ToList();
            model.Cities = _context.Cities.Where(x => x.IsActive).ToList();

            if(id != null)
            {
                var beneficary = _context.Beneficiaries.Find(id.Value);
                if(beneficary != null)
                {
                    model.Note = "Tôi muốn đóng góp cho tổ chức: " +beneficary.OrganizationDisplayName;
                }    
            }    

            return View(model);
        }

        public IActionResult Authority()
        {
            return View();
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
            donator.Note = model.Note;

            donator.CreatedAt = DateTime.Now;


            _context.DonatorOrganizations.Add(donator);



            _context.SaveChanges();

            var emailService = new EmailService();

            string bodyformat = @"
Người đề cử: {0} - Số điện thoại: {1}
Tổ chức: {2}
Ghi chú: {3}
Đường dẫn: https://goidooi.com/HomeAdmin/DonatorOrganizations/Edit/{3}

 
";
            string body = string.Format(bodyformat, donator.ProposetorName, donator.ProposetorPhone, donator.OrganizationName, donator.DonatorOrganizationId,donator.Note);

            emailService.SendEmailAsync("Có người muốn donate, lúc " + DateTime.Now.ToLongTimeString().ToString(), body, "nhan@nhacuavui.com", "ngoc@nhacuavui.com", "nhung@nhacuavui.com", "info@nhacuavui.com", "huynh.nguyen@nhacuavui.com");


            return RedirectToAction("ProposeSuccess");
        }

    }
}
