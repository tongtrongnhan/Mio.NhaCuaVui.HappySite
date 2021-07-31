using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mio.NhaCuaVui.HappySite.Authentication;
using Mio.NhaCuaVui.HappySite.ExtensionMethod;
using Mio.NhaCuaVui.HappySite.Models;
using Mio.NhaCuaVui.HappySite.Service;

namespace Mio.NhaCuaVui.HappySite.Areas.HomeAdmin.Controllers
{
    [Area("HomeAdmin")]
    [BasedAuthentication("Admin", "Manager")]

    public class DonatorOrganizationsController : Controller
    {
        private readonly ZDbContext _context;
        public DonatorOrganizationsController(ZDbContext context)
        {
            _context = context;
        }


        // GET: HomeAdmin/DonatorOrganizations
        public async Task<IActionResult> Index()
        {
            var zDbContext = _context.DonatorOrganizations
                                .Include(d => d.DonatorOrganizationType)
                                .Include(x => x.ValidatedUser)
                                .Include(x => x.DonationCategoryQuantities).ThenInclude(x => x.Category)
                                .Include(b => b.Ward).ThenInclude(x => x.District).ThenInclude(x => x.City)
                                                            .OrderByDescending(x => x.DonatorOrganizationId);
            
            ;
            return View(await zDbContext.ToListAsync());
        }

        // GET: HomeAdmin/DonatorOrganizations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donatorOrganization = await _context.DonatorOrganizations
                .Include(d => d.DonatorOrganizationType)
                .Include(d => d.Need)
                .Include(d => d.ValidatedUser)
                .Include(d => d.Ward)
                .FirstOrDefaultAsync(m => m.DonatorOrganizationId == id);
            if (donatorOrganization == null)
            {
                return NotFound();
            }

            return View(donatorOrganization);
        }

        // GET: HomeAdmin/DonatorOrganizations/Create
        public IActionResult Create()
        {
            ViewData["DonatorOrganizationTypeId"] = new SelectList(_context.DonatorOrganizationTypes, "DonatorOrganizationTypeId", "DonatorOrganizationTypeId");
            ViewData["NeedId"] = new SelectList(_context.Needs, "NeedId", "NeedId");
            ViewData["ValidatedUserId"] = new SelectList(_context.Users, "UserId", "Email");
            ViewData["WardId"] = new SelectList(_context.Wards, "WardId", "WardId");
            return View();
        }

        // POST: HomeAdmin/DonatorOrganizations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonatorOrganizationId,DonatorOrganizationTypeId,ProposetorName,ProposetorPhone,ProposetorEmail,NeedId,WardId,OrganizationName,Street,Note,ContactName,ContactPhone,ContactEmail,HadTransportation,CreatedAt,ValidatedAt,IsValidated,ValidatedUserId,NotValidateMessage")] DonatorOrganization donatorOrganization)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donatorOrganization);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DonatorOrganizationTypeId"] = new SelectList(_context.DonatorOrganizationTypes, "DonatorOrganizationTypeId", "DonatorOrganizationTypeId", donatorOrganization.DonatorOrganizationTypeId);
            ViewData["NeedId"] = new SelectList(_context.Needs, "NeedId", "NeedId", donatorOrganization.NeedId);
            ViewData["ValidatedUserId"] = new SelectList(_context.Users, "UserId", "Email", donatorOrganization.ValidatedUserId);
            ViewData["WardId"] = new SelectList(_context.Wards, "WardId", "WardId", donatorOrganization.WardId);
            return View(donatorOrganization);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donatorOrganization = _context.DonatorOrganizations.
                                Include(x => x.Ward).ThenInclude(x => x.District).ThenInclude(x => x.City)
                                .Include(x => x.DonationCategoryQuantities).ThenInclude(x => x.Category)
                                .First(x => x.DonatorOrganizationId == id);
            if (donatorOrganization == null)
            {
                return NotFound();
            }
            ViewData["DonatorOrganizationTypeId"] = new SelectList(_context.DonatorOrganizationTypes, "DonatorOrganizationTypeId", "Name", donatorOrganization.DonatorOrganizationTypeId);

            if (donatorOrganization.WardId == null)
            {
                ViewData["CityId"] = new SelectList(_context.Cities.Where(x => x.IsActive), "CityId", "Name");
                ViewData["DistrictId"] = new SelectList(new List<District>(), "DistrictId", "Name");
                ViewData["WardId"] = new SelectList(new List<Ward>(), "WardId", "Name");

            }
            else
            {
                var listDistrict = new List<District>() { donatorOrganization.Ward.District };
                ViewData["CityId"] = new SelectList(_context.Cities.Where(x => x.IsActive), "CityId", "Name", donatorOrganization.Ward.District.CityId);
                ViewData["DistrictId"] = new SelectList(listDistrict, "DistrictId", "Name", donatorOrganization.Ward.DistrictId);
                ViewData["WardId"] = new SelectList(new List<Ward>() { donatorOrganization.Ward }, "WardId", "Name", donatorOrganization.WardId);

            }
            return View(donatorOrganization);
        }

      

        // POST: HomeAdmin/Beneficiaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, DonatorOrganization donator)
        {
            if (id != donator.DonatorOrganizationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var donatorInDb = _context.DonatorOrganizations.Include(x => x.DonationCategoryQuantities).FirstOrDefault(x => x.DonatorOrganizationId == id);
                    // Update parent
                    _context.Entry(donatorInDb).CurrentValues.SetValues(donator);

                    // Delete children
                    foreach (var existingChild in donatorInDb.DonationCategoryQuantities.ToList())
                    {
                        if (!donator.DonationCategoryQuantities.Any(c => c.DonatorOrganizationId == existingChild.DonatorOrganizationId && c.CategoryId == existingChild.CategoryId))
                            _context.DonationCategoryQuantities.Remove(existingChild);
                    }

                    // Update and Insert children
                    if(donator.DonationCategoryQuantities != null && donator.DonationCategoryQuantities.Any())
                    {
                        foreach (var childModel in donator.DonationCategoryQuantities)
                        {
                            var existingChild = donatorInDb.DonationCategoryQuantities
                                .Where(c => c.DonatorOrganizationId == childModel.DonatorOrganizationId && c.CategoryId == childModel.CategoryId)
                                .SingleOrDefault();

                            if (existingChild != null)
                                // Update child
                                _context.Entry(existingChild).CurrentValues.SetValues(childModel);
                            else
                            {
                                // Insert child
                                var newChild = new DonationCategoryQuantity
                                {
                                    CategoryId = childModel.CategoryId,
                                    DonatorOrganizationId = id,
                                    Quantity = childModel.Quantity,
                                    //...
                                };
                                donatorInDb.DonationCategoryQuantities.Add(newChild);
                            }
                        }
                    }
                    

                    var user = HttpContext.Session.GetCurrentAuthentication();
                    donatorInDb.ValidatedUserId = user.UserId;
                    donatorInDb.ValidatedAt = DateTime.Now;
                    donatorInDb.CreatedAt = DateTime.Now;


                    if (donatorInDb.IsValidated != donator.IsValidated)
                    {

                        if (donator.IsValidated == true)
                        {
                            donatorInDb.IsValidated = true;


                        }
                        else
                        {
                            donatorInDb.IsValidated = donator.IsValidated;
                            donatorInDb.NotValidateMessage = donator.NotValidateMessage;
                        }
                    }


                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DonatorOrganizationTypeId"] = new SelectList(_context.DonatorOrganizationTypes, "DonatorOrganizationTypeId", "Name", donator.DonatorOrganizationTypeId);
            ViewData["CityId"] = new SelectList(_context.Cities.Where(x => x.IsActive), "CityId", "Name");
            ViewData["WardId"] = new SelectList(_context.Wards, "WardId", "Name", donator.WardId);
            return View(donator);
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
            if(model == null || model.Any() == false) return RedirectToAction("Index");

            var donatorService = new DonatorService(_context);

            donatorService.AddNewItem(donator);

            return RedirectToAction("Index");
        }

      

        // GET: HomeAdmin/DonatorOrganizations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donatorOrganization = await _context.DonatorOrganizations
                .Include(d => d.DonatorOrganizationType)
                .Include(d => d.Need)
                .Include(d => d.ValidatedUser)
                .Include(d => d.Ward)
                .FirstOrDefaultAsync(m => m.DonatorOrganizationId == id);
            if (donatorOrganization == null)
            {
                return NotFound();
            }

            return View(donatorOrganization);
        }

        // POST: HomeAdmin/DonatorOrganizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donatorOrganization = await _context.DonatorOrganizations.FindAsync(id);
            _context.DonatorOrganizations.Remove(donatorOrganization);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonatorOrganizationExists(int id)
        {
            return _context.DonatorOrganizations.Any(e => e.DonatorOrganizationId == id);
        }
    }
}
