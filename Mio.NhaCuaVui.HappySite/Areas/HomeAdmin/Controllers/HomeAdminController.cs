using Microsoft.AspNetCore.Mvc;
using Mio.NhaCuaVui.HappySite.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Areas.HomeAdmin
{
    [Area("HomeAdmin")]

    public class HomeAdminController : Controller
    {
        [BasedAuthentication()]
        public IActionResult Index()
        {
            return View();
        }
    }
}
