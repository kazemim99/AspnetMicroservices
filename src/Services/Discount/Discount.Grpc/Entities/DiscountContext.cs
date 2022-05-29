using Discount.Grpc.Entities.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Entities
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
