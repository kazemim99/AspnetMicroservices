using Discount.Api.Entities;
using Discount.Api.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Discount.Api.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly DiscountContext _context;

        public DiscountRepository(DiscountContext context)
        {
            _context = context;
        }

        public async Task Create(Coupon input)
        {
            await _context.AddAsync(input);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string productName)
        {

            var coupon = await Get(productName);
            _context.Remove(coupon);
            await _context.SaveChangesAsync();
        }

        public async Task<Coupon> Get(string productName)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(a => a.ProductName == productName);
            if (coupon == null)
                throw new CouponNotFoundException(productName);
            return coupon;
        }

        public async Task<List<Coupon>> GetAll()
        {
            return await _context.Coupons.ToListAsync();
        }

        public async Task Update(Coupon input)
        {
            await Get(input.ProductName);
            _context.Coupons.Update(input);
            await _context.SaveChangesAsync();
        }
    }
}
