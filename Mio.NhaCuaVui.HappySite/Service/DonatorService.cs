using Microsoft.EntityFrameworkCore;
using Mio.NhaCuaVui.HappySite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Service
{
    public class DonatorService
    {

        private readonly ZDbContext _context;

        public DonatorService(ZDbContext context)
        {
            _context = context;
        }


        public bool AddNewItem(DonatorOrganization donator)
        {
            var donatorInDb = _context.DonatorOrganizations.Include(x => x.DonationCategoryQuantities).First(x => x.DonatorOrganizationId == donator.DonatorOrganizationId);

            var model = donator.DonationCategoryQuantities.Where(x => x.Quantity > 0).ToList();

            // cộng trừ số lượng
            if (donatorInDb.DonationCategoryQuantities != null && donatorInDb.DonationCategoryQuantities.Any())
            {
                var updatesQuantiy = donatorInDb.DonationCategoryQuantities.Where(x => model.Any(a => a.CategoryId == x.CategoryId)).ToList();
                if (updatesQuantiy != null && updatesQuantiy.Any())
                {
                    foreach (var item in updatesQuantiy)
                    {
                        item.Quantity = item.Quantity + model.First(x => x.CategoryId == item.CategoryId).Quantity;
                    }
                }

            }

            donatorInDb.DonationCategoryQuantities = donatorInDb.DonationCategoryQuantities == null ? new List<DonationCategoryQuantity>() : donatorInDb.DonationCategoryQuantities;
            // cập nhật danh sách
            var newsQuantiy = model.Where(x => !donatorInDb.DonationCategoryQuantities.Any(a => a.CategoryId == x.CategoryId)).ToList();
            if (newsQuantiy != null && newsQuantiy.Any())
            {
                foreach (var item in newsQuantiy)
                {
                    if (donatorInDb.DonationCategoryQuantities.Any(x => x.CategoryId == item.CategoryId)) continue;
                    item.DonatorOrganizationId = donator.DonatorOrganizationId;
                    _context.DonationCategoryQuantities.Add(item);

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
