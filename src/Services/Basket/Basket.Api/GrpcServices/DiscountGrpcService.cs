using Discount.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.Api.GrpcServices
{
    public class DiscountGrpcService : IDiscountGrpcService
    {
        private readonly DiscountPotoService.DiscountPotoServiceClient _discountPotoServiceClient;

        public DiscountGrpcService(DiscountPotoService.DiscountPotoServiceClient discountPotoServiceClient)
        {
            _discountPotoServiceClient = discountPotoServiceClient;
        }

        public async Task<GetDicountReponse> GetDiscount(string productName)
        {
            var discountRequest = new GetDicountRequest
            {
                ProductName = productName
            };

            return await _discountPotoServiceClient.GetDicountAsync(discountRequest);
        }
    }
    public interface IDiscountGrpcService
    {
        Task<GetDicountReponse> GetDiscount(string productName);
    }
}
