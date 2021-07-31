using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mio.NhaCuaVui.HappySite.ExtensionMethod;
using Mio.NhaCuaVui.HappySite.Models;
using Mio.NhaCuaVui.HappySite.Models.ViewModels;
using Mio.NhaCuaVui.HappySite.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Controllers
{
    public class BeneficiaryController : Controller
    {
        private readonly ZDbContext _context;
        private IHostingEnvironment _env;

        public BeneficiaryController(ZDbContext context, IHostingEnvironment env)
        {
            _env = env;
            _context = context;
        }

        private string GetFilePath()
        {
            var webRoot = _env.WebRootPath;
            var PathWithFolderName = System.IO.Path.Combine(webRoot, "Upload/Photo");


            if (!Directory.Exists(PathWithFolderName))
            {
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(PathWithFolderName);
            }

            return PathWithFolderName;

        }
        

        public IActionResult Index(int? categoryId)
        {
            var list = _context.Beneficiaries
                               .Include(x => x.ValidatedUser)
                               .Include(x => x.Deliveries).ThenInclude(x => x.DeliveryCategories).ThenInclude(x => x.Category)
                               .Include(x => x.Deliveries).ThenInclude(x => x.DonatorOrganization)
                               .Include(x => x.BeneficiaryType)
                               .Include(x => x.BenificaryCategoryQuantities).ThenInclude(x => x.Category)
                               .Include(x => x.Ward).ThenInclude(x => x.District).ThenInclude(x => x.City)
                               .Where(x => x.IsValidated == true);
            if(categoryId.HasValue)
            {
                list = list.Where(x => x.BenificaryCategoryQuantities.Any(c => c.CategoryId == categoryId.Value));

            }   
            
            var model = list.OrderByDescending(x => x.BeneficiaryId).ToList();

            var district = _context.Districts.Include(x => x.City).Where(x => x.City.IsActive).ToList();
            ViewBag.Districts =  new SelectList(district, "DistrictId", "FullName");


            var category = _context.Categories.ToList();
            ViewBag.Category = new SelectList(category, "CategoryId", "Name", categoryId.HasValue?  categoryId.Value : null);

            return View(model);
        }

        public IActionResult Search(List<int> DistrictIds, List<int> CategoryIds)
        {
            var list = _context.Beneficiaries
                   .Include(x => x.ValidatedUser)
                   .Include(x => x.BeneficiaryType)
                   .Include(x => x.BenificaryCategoryQuantities).ThenInclude(x => x.Category)
                   .Include(x => x.Ward).ThenInclude(x => x.District).ThenInclude(x => x.City)
                   .Where(x => x.IsValidated == true);

            if (DistrictIds != null && DistrictIds.Any())
            {
                list = list.Where(x => DistrictIds.Contains(x.Ward.DistrictId)).OrderByDescending(x => x.BeneficiaryId);
            }

            if (CategoryIds != null && CategoryIds.Any())
            {
                list = list.Where(x => x.BenificaryCategoryQuantities.Any(c => CategoryIds.Contains(c.CategoryId))).OrderByDescending(x => x.BeneficiaryId);
            }

            var html = this.RenderView("_List", list.ToList(), true);

            return Json(new { html = html });
                   
                   
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
            beneficiary.Note = model.Note;

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

            var emailService = new EmailService();

            string bodyformat = @"
Người đề cử: {0} - Số điện thoại: {1}
Tổ chức: {2}
Đường dẫn: https://goidooi.com/HomeAdmin/Beneficiaries/Edit/{3}

 
";
            string body = string.Format(bodyformat, beneficiary.ProposetorName, beneficiary.ProposetorPhone, beneficiary.OrganizationName, beneficiary.BeneficiaryId);

            emailService.SendEmailAsync("Yêu cầu hỗ trợ, lúc " + DateTime.Now.ToLongTimeString().ToString(), body, "nhan@nhacuavui.com", "ngoc@nhacuavui.com", "nhung@nhacuavui.com", "info@nhacuavui.com");

            return RedirectToAction("ProposeSuccess");
        }

    }
}
