using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Mio.NhaCuaVui.HappySite.ExtensionMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Authentication
{
    public class BasedAuthentication : ActionFilterAttribute
    {
        private List<string> _userRoles;

        public BasedAuthentication(params string[] userRoles)
        {
            _userRoles = userRoles.ToList();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext context = filterContext.HttpContext;
            HttpRequest request = filterContext.HttpContext.Request;

            var routingValues = filterContext.RouteData.Values;
            var currentArea = (string)routingValues["area"] ?? string.Empty;
            var currentController = (string)routingValues["controller"] ?? string.Empty;
            var currentAction = (string)routingValues["action"] ?? string.Empty;

            var currentAuthentication = context.Session.GetCurrentAuthentication();


            if (currentAuthentication == null)
            {
                //context.Session.SetString(TextConstant.LastRequestURL, request.GetDisplayUrl());
                filterContext.Result = new RedirectResult("/user/login");
                return;
            }

            // if doesn't belong to any application
            if (string.IsNullOrEmpty(currentArea))
            {
                return;
            }

            // chỉ require login
            if(_userRoles == null || _userRoles.Any() == false)
            {
                return;
            }    

            // authen
            bool hadPermission = _userRoles.Any(x => currentAuthentication.UserRoles.Contains(x));
            if(hadPermission)
            {
                return;
            }    

            else
            {
                filterContext.Result = new RedirectResult("/user/login");
            }    


        }
    }
}
