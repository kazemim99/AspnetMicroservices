using Discount.Grpc.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Extensions
{
    public static class InitialDb
    {
        public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : DbContext
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var db = serviceScope.ServiceProvider.GetService<T>();

            db.Database.Migrate();
            if (db is DiscountContext)
            {
                var discountContext = db as DiscountContext;
                if (!discountContext.Coupons.Any())
                {
                    discountContext.Coupons.Add(new Coupon()
                    {
                        ProductName = "ProductName_1"
                    });
                }
            }

        }
    }
}
