using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mio.NhaCuaVui.HappySite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Constant
{
    public static class Constant
    {
        public static SelectList ThreeOption(bool? value)
        {
            var threeOption = new List<SelectValue>()
            {
                new SelectValue()
                {
                    Text = "Chưa rõ",
                    Value = null
                },

                new SelectValue()
                {
                    Text = "Không",
                    Value = "False"
                },
                 new SelectValue()
                {
                    Text = "Có",
                    Value = "True"
                },
            };
            return new SelectList(threeOption, "Value", "Text", value == null? null : value.ToString());
        }

    }

    public class SelectValue
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
}
