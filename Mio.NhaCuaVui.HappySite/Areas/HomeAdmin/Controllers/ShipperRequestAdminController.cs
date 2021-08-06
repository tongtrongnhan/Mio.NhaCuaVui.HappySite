using Microsoft.AspNetCore.Mvc;
using Mio.NhaCuaVui.HappySite.Authentication;
using Mio.NhaCuaVui.HappySite.Models;
using Mio.NhaCuaVui.HappySite.Models.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Mio.NhaCuaVui.HappySite.ExtensionMethod;

namespace Mio.NhaCuaVui.HappySite.Areas.HomeAdmin.Controllers
{
    [Area("HomeAdmin")]
    [BasedAuthentication("Admin", "Manager")]
    public class ShipperRequestAdminController : Controller
    {
        private readonly ZDbContext _context;

        public ShipperRequestAdminController(ZDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var shipperRequest = _context.ShipperRequests
                .Include(x=> x.District).ThenInclude(x=> x.City)
                .OrderByDescending(i => i.ShipperRequestId)
                .Select(i => new ShipperRequestViewModel { 
                    District = i.District,
                    ValidatedBy = i.ValidatedUser,
                    ShipperRequestId = i.ShipperRequestId,
                    AvailableDate = i.AvailableDate,
                    AvailableFromHour = i.AvailableFromHour,
                    AvailableToHour = i.AvailableToHour,
                    IsValidated = i.IsValidated,
                    ShipperName = i.ShipperName,
                    ShipperPhone = i.ShipperPhone,
                    Transportation = i.Transportation,
                    ValidateMessage = i.ValidateMessage,
                    ValidatedAt = i.ValidatedAt
                })
                .ToList();
            return View(shipperRequest);
        }
        public IActionResult Delete(int id)
        {
            var model = _context.ShipperRequests
                .Include(x=> x.District).ThenInclude(x=> x.City)
                .FirstOrDefault(x => x.ShipperRequestId == id);
            if(model == null)
            {
                return NotFound();
            }

            var request = new ShipperRequestViewModel()
            {
                District = model.District,
                ValidatedBy = model.ValidatedUser,
                ShipperRequestId = model.ShipperRequestId,
                AvailableDate = model.AvailableDate,
                AvailableFromHour = model.AvailableFromHour,
                AvailableToHour = model.AvailableToHour,
                IsValidated = model.IsValidated,
                ShipperName = model.ShipperName,
                ShipperPhone = model.ShipperPhone,
                Transportation = model.Transportation,
                ValidateMessage = model.ValidateMessage,
                ValidatedAt = model.ValidatedAt

            };
            return View("Delete", request);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var request = _context.ShipperRequests.Find(id);
            _context.ShipperRequests.Remove(request);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Validate(int id)
        {
            var model = _context.ShipperRequests
                .Include(x => x.District).ThenInclude(x => x.City)
                .FirstOrDefault(x => x.ShipperRequestId == id);
            if (model == null)
            {
                return NotFound();
            }

            var request = new ShipperRequestViewModel()
            {
                District = model.District,
                ValidatedBy = model.ValidatedUser,
                ShipperRequestId = model.ShipperRequestId,
                AvailableDate = model.AvailableDate,
                AvailableFromHour = model.AvailableFromHour,
                AvailableToHour = model.AvailableToHour,
                IsValidated = model.IsValidated,
                ShipperName = model.ShipperName,
                ShipperPhone = model.ShipperPhone,
                Transportation = model.Transportation,
                ValidateMessage = model.ValidateMessage,
                ValidatedAt = model.ValidatedAt
            };
            return View("Validate", request);
        }

        [HttpPost]
        public IActionResult ValidateConfirm(ShipperRequestViewModel input)
        {
            var request = _context.ShipperRequests.Find(input.ShipperRequestId);
            if(request == null)
            {
                return RedirectToAction(nameof(Index));
            }
            request.IsValidated = input.IsValidated;
            request.ValidateMessage = input.ValidateMessage;
            request.ValidatedAt = DateTime.Now;
            request.ValidatedUserId = HttpContext.Session.GetCurrentAuthentication().UserId;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
    
}
