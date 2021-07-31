using Microsoft.EntityFrameworkCore;
using Mio.NhaCuaVui.HappySite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Service
{
    public class BeneficaryService
    {

        private readonly ZDbContext _context;

        public BeneficaryService(ZDbContext context)
        {
            _context = context;
        }


        public bool AddNewItem(Beneficiary beneficiary)
        {
            var beneficaryInDb = _context.Beneficiaries.Include(x => x.BenificaryCategoryQuantities).First(x => x.BeneficiaryId == beneficiary.BeneficiaryId);

            var model = beneficiary.BenificaryCategoryQuantities.Where(x => x.Quantity > 0).ToList();

            // cộng trừ số lượng
            if (beneficaryInDb.BenificaryCategoryQuantities != null && beneficaryInDb.BenificaryCategoryQuantities.Any())
            {
                var updatesQuantiy = beneficaryInDb.BenificaryCategoryQuantities.Where(x => model.Any(a => a.CategoryId == x.CategoryId)).ToList();
                if (updatesQuantiy != null && updatesQuantiy.Any())
                {
                    foreach (var item in updatesQuantiy)
                    {
                        item.Quantity = item.Quantity + model.First(x => x.CategoryId == item.CategoryId).Quantity;
                    }
                }

            }

            beneficaryInDb.BenificaryCategoryQuantities = beneficaryInDb.BenificaryCategoryQuantities == null ? new List<BenificaryCategoryQuantity>() : beneficaryInDb.BenificaryCategoryQuantities;
            // cập nhật danh sách
            var newsQuantiy = model.Where(x => !beneficaryInDb.BenificaryCategoryQuantities.Any(a => a.CategoryId == x.CategoryId)).ToList();
            if (newsQuantiy != null && newsQuantiy.Any())
            {
                foreach (var item in newsQuantiy)
                {
                    if (beneficaryInDb.BenificaryCategoryQuantities.Any(x => x.CategoryId == item.CategoryId)) continue;
                    item.BenificaryId = beneficiary.BeneficiaryId;
                    _context.BenificaryCategoryQuantities.Add(item);

                }

            }

            try
            {
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
