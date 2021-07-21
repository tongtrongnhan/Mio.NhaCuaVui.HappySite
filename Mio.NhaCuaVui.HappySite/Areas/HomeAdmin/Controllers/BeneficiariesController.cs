using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mio.NhaCuaVui.HappySite.Authentication;
using Mio.NhaCuaVui.HappySite.ExtensionMethod;
using Mio.NhaCuaVui.HappySite.Models;

namespace Mio.NhaCuaVui.HappySite.Areas.HomeAdmin.Controllers
{
    [Area("HomeAdmin")]
    [BasedAuthentication("Admin","Manager")]
    public class BeneficiariesController : Controller
    {
        private readonly ZDbContext _context;

        public BeneficiariesController(ZDbContext context)
        {
            _context = context;
        }

        // GET: HomeAdmin/Beneficiaries
        public async Task<IActionResult> Index()
        {
            var zDbContext = _context.Beneficiaries
                            .Include(b => b.BeneficiaryType)
                            .Include(b => b.Need)
                            .Include(x => x.ValidatedUser)
                            .Include(x => x.BenificaryCategoryQuantities).ThenInclude(x => x.Category)
                            .Include(b => b.Ward).ThenInclude(x => x.District).ThenInclude(x => x.City);
            return View(await zDbContext.ToListAsync());
        }

        // GET: HomeAdmin/Beneficiaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beneficiary = await _context.Beneficiaries
                .Include(b => b.BeneficiaryType)
                .Include(b => b.Need)
                .Include(b => b.Ward)
                .FirstOrDefaultAsync(m => m.BeneficiaryId == id);
            if (beneficiary == null)
            {
                return NotFound();
            }

            return View(beneficiary);
        }

        // GET: HomeAdmin/Beneficiaries/Create
        public IActionResult Create()
        {
            ViewData["BeneficiaryTypeId"] = new SelectList(_context.BeneficiaryTypes, "BeneficiaryTypeId", "BeneficiaryTypeId");
            ViewData["NeedId"] = new SelectList(_context.Needs, "NeedId", "NeedId");
            ViewData["WardId"] = new SelectList(_context.Wards, "WardId", "WardId");
            return View();
        }

        // POST: HomeAdmin/Beneficiaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BeneficiaryId,BeneficiaryTypeId,ProposetorName,ProposetorPhone,ProposetorEmail,NeedId,WardId,OrganizationName,NumberOfPeople,HadTransportation,Street,Note,ContactName,ContactPhone,ContactEmail")] Beneficiary beneficiary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(beneficiary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BeneficiaryTypeId"] = new SelectList(_context.BeneficiaryTypes, "BeneficiaryTypeId", "BeneficiaryTypeId", beneficiary.BeneficiaryTypeId);
            ViewData["NeedId"] = new SelectList(_context.Needs, "NeedId", "NeedId", beneficiary.NeedId);
            ViewData["WardId"] = new SelectList(_context.Wards, "WardId", "WardId", beneficiary.WardId);
            return View(beneficiary);
        }

        // GET: HomeAdmin/Beneficiaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beneficiary = _context.Beneficiaries.
                                Include(x => x.Ward).ThenInclude(x => x.District).ThenInclude(x => x.City)
                                .Include(x => x.BenificaryCategoryQuantities).ThenInclude(x => x.Category)
                                .First( x => x.BeneficiaryId == id);
            if (beneficiary == null)
            {
                return NotFound();
            }
            ViewData["BeneficiaryTypeId"] = new SelectList(_context.BeneficiaryTypes, "BeneficiaryTypeId", "Name", beneficiary.BeneficiaryTypeId);
            
            if(beneficiary.WardId == null)
            {
                ViewData["CityId"] = new SelectList(_context.Cities.Where(x => x.IsActive), "CityId", "Name");
                ViewData["DistrictId"] = new SelectList(new List<District>(), "DistrictId", "Name");
                ViewData["WardId"] = new SelectList(new List<Ward>(), "WardId", "Name");

            }else
            {
                var listDistrict = new List<District>() { beneficiary.Ward.District };
                ViewData["CityId"] = new SelectList(_context.Cities.Where(x => x.IsActive), "CityId", "Name", beneficiary.Ward.District.CityId);
                ViewData["DistrictId"] = new SelectList(listDistrict, "DistrictId", "Name", beneficiary.Ward.DistrictId);
                ViewData["WardId"] = new SelectList(new List<Ward>() { beneficiary.Ward }, "WardId", "Name", beneficiary.WardId);

            }
            return View(beneficiary);
        }

        // POST: HomeAdmin/Beneficiaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Beneficiary beneficiary)
        {
            if (id != beneficiary.BeneficiaryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var beneficaryInDb = _context.Beneficiaries.Include(x => x.BenificaryCategoryQuantities).FirstOrDefault(x => x.BeneficiaryId == id);
                    // Update parent
                    _context.Entry(beneficaryInDb).CurrentValues.SetValues(beneficiary);

                    // Delete children
                    foreach (var existingChild in beneficaryInDb.BenificaryCategoryQuantities.ToList())
                    {
                        if (!beneficiary.BenificaryCategoryQuantities.Any(c => c.BenificaryId == existingChild.BenificaryId && c.CategoryId == existingChild.CategoryId))
                            _context.BenificaryCategoryQuantities.Remove(existingChild);
                    }

                    // Update and Insert children
                    foreach (var childModel in beneficiary.BenificaryCategoryQuantities)
                    {
                        var existingChild = beneficaryInDb.BenificaryCategoryQuantities
                            .Where(c => c.BenificaryId == childModel.BenificaryId && c.CategoryId == childModel.CategoryId)
                            .SingleOrDefault();

                        if (existingChild != null)
                            // Update child
                            _context.Entry(existingChild).CurrentValues.SetValues(childModel);
                        else
                        {
                            // Insert child
                            var newChild = new BenificaryCategoryQuantity
                            {
                                CategoryId = childModel.CategoryId,
                                BenificaryId = id,
                                Quantity = childModel.Quantity,
                                //...
                            };
                            beneficaryInDb.BenificaryCategoryQuantities.Add(newChild);
                        }
                    }

                    var user = HttpContext.Session.GetCurrentAuthentication();
                    beneficaryInDb.ValidatedUserId = user.UserId;
                    beneficaryInDb.ValidatedAt = DateTime.Now;


                    if (beneficaryInDb.IsValidated != beneficiary.IsValidated)
                    {

                        if (beneficiary.IsValidated == true)
                        {
                            beneficaryInDb.IsValidated = true;
                            

                        }
                        else
                        {
                            beneficaryInDb.IsValidated = beneficiary.IsValidated;
                            beneficaryInDb.NotValidateMessage = beneficiary.NotValidateMessage;
                        }
                    }


                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeneficiaryExists(beneficiary.BeneficiaryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BeneficiaryTypeId"] = new SelectList(_context.BeneficiaryTypes, "BeneficiaryTypeId", "Name", beneficiary.BeneficiaryTypeId);
            ViewData["CityId"] = new SelectList(_context.Cities.Where(x => x.IsActive), "CityId", "Name");
            ViewData["WardId"] = new SelectList(_context.Wards, "WardId", "Name", beneficiary.WardId);
            return View(beneficiary);
        }

        // GET: HomeAdmin/Beneficiaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beneficiary = await _context.Beneficiaries
                .Include(b => b.BeneficiaryType)
                .Include(b => b.Need)
                .Include(b => b.Ward)
                .FirstOrDefaultAsync(m => m.BeneficiaryId == id);
            if (beneficiary == null)
            {
                return NotFound();
            }

            return View(beneficiary);
        }

        // POST: HomeAdmin/Beneficiaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var beneficiary = await _context.Beneficiaries.FindAsync(id);
            _context.Beneficiaries.Remove(beneficiary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeneficiaryExists(int id)
        {
            return _context.Beneficiaries.Any(e => e.BeneficiaryId == id);
        }
    }
}
