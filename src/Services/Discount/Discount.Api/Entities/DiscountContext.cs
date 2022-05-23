using Discount.Api.Entities.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Discount.Api.Entities
{
    public class DiscountContext : DbContext
    {

        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
        }
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CouponConfig).Assembly);
        }
    }

}
