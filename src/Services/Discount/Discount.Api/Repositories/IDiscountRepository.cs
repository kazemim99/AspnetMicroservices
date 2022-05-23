﻿using Discount.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Api.Repositories
{
    public interface IDiscountRepository
    {
        Task Delete(string productName);
        Task Create(Coupon input);
        Task Update(Coupon input);
        Task<Coupon> Get(string productName);
        Task<List<Coupon>> GetAll();
    }
}
