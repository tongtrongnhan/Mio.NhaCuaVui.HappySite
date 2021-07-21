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

namespace Mio.NhaCuaVui.HappySite.Areas.UserAdmin.Controllers
{
    [Area("UserAdmin")]
    [BasedAuthentication("Admin","Manager")]
    public class UserAdminController : Controller
    {
        private readonly ZDbContext _context;

        public UserAdminController(ZDbContext context)
        {
            _context = context;
        }

        // GET: UserAdmin/UserAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.Include(x => x.UserUserRoles).ThenInclude(x => x.UserRole).ToListAsync());
        }

        // GET: UserAdmin/UserAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: UserAdmin/UserAdmin/Create
        public IActionResult Create()
        {
            ViewBag.AllRoles = _context.UserRoles.ToList();

            return View();
        }

        // POST: UserAdmin/UserAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user, List<int> userRoleIds)
        {
            if (ModelState.IsValid)
            {
                user.Password = user.Password.Hash();
                user.UserGuid = Guid.NewGuid();
                if (userRoleIds != null || userRoleIds.Any())
                {
                    user.UserUserRoles = userRoleIds.Select(x => new UserUserRole()
                    {
                        UserRoleId = x
                    }).ToList();
                }    
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: UserAdmin/UserAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.Users.Include(x => x.UserUserRoles).ThenInclude(x => x.UserRole).FirstOrDefault(x => x.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            ViewBag.AllRoles = _context.UserRoles.ToList();

            return View(user);
        }

        // POST: UserAdmin/UserAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user, List<int> userRoleIds)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userInDb = _context.Users.Find(user.UserId);
                    userInDb.IsActive = user.IsActive;
                    userInDb.Name = user.Name;
                    userInDb.Email = user.Email;
                    userInDb.Phone = user.Phone;
                    userInDb.UserUserRoles.Clear();
                    if (userRoleIds != null || userRoleIds.Any())
                    {
                        userInDb.UserUserRoles = userRoleIds.Select(x => new UserUserRole()
                        {
                            UserRoleId = x,
                            UserId = userInDb.UserId
                        }).ToList();
                    }


                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            return View(user);
        }

        // GET: UserAdmin/UserAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: UserAdmin/UserAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
