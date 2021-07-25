using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mio.NhaCuaVui.HappySite.ExtensionMethod;
using Mio.NhaCuaVui.HappySite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Controllers
{
    public class UserController : Controller
    {
        private readonly ZDbContext _context;
        public UserController(ZDbContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            var user = _context.Users.Include(x => x.UserUserRoles).ThenInclude(x => x.UserRole).FirstOrDefault(x => x.IsActive && (x.Email.ToLower() == userName.ToLower() && x.Password == password.Hash()) || (x.Phone.ToLower() == userName.ToLower() && x.Password == password.Hash()));

            if (user == null)
            {
                ViewBag.ErrorMessage = "Thông tin đăng nhập không chính xác, hoặc tài khoản chưa kích hoạt";
                return View();
            }

            var authenModel = new AuthenticationModel()
            {
                UserGuid = user.UserGuid,
                UserId = user.UserId,
                UserName = user.Name,
                UserRoles = user.UserUserRoles.Select(x => x.UserRole.Name).ToList()
            };

            HttpContext.Session.SetCurrentAuthentication(authenModel);

            var lastRequestURL = HttpContext.Session.GetString("LastRequestURL");

            if (string.IsNullOrEmpty(lastRequestURL) == false) return Redirect(lastRequestURL);

            return Redirect("/HomeAdmin/HomeAdmin");
        }
    }
}
