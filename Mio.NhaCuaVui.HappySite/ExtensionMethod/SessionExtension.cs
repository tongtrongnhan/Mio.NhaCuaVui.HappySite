using Microsoft.AspNetCore.Http;
using Mio.NhaCuaVui.HappySite.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.ExtensionMethod
{
    public static class SessionExtension
    {
        private static string AuthenticationName = "Authentication_Current";
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static AuthenticationModel GetCurrentAuthentication(this ISession session)
        {
            return session.GetObjectFromJson<AuthenticationModel>(AuthenticationName);
        }

        public static void SetCurrentAuthentication(this ISession session, AuthenticationModel authenticationModel)
        {
            session.SetObjectAsJson(AuthenticationName, authenticationModel);
        }


        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
