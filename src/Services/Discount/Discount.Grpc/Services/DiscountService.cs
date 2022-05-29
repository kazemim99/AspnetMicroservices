using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountPotoService.DiscountPotoServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;
        public DiscountService(IDiscountRepository repository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<GetDicountReponse> GetDicount(GetDicountRequest request, ServerCallContext context)
        {

            var discount = await _repository.Get(request.ProductName);
            if (discount == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Dicount with {request.ProductName} not found!"));
            }
            var discountModel = _mapper.Map<GetDicountReponse>(discount);

            return discountModel;
        }

        public override async Task<CreateDisountResponse> CreateDiscount(CreateDisountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request);
            await _repository.Create(coupon);
            return _mapper.Map<CreateDisountResponse>(coupon);
        }
        public override async Task<DeleteDisountResponse> DeleteDiscount(DeleteDisountRequest request, ServerCallContext context)
        {
            await _repository.Delete(request.ProductName);
            return new DeleteDisountResponse();

        }
        public override async Task<GetAllDicountResponse> GetAllDicount(GetAllDicountRequest request, ServerCallContext context)
        {
            var coupons = await _repository.GetAll();
            var discoutResponse = _mapper.Map<List<GetDicountReponse>>(coupons);
            var model = new GetAllDicountResponse();
            model.Model.AddRange(discoutResponse);
            return model;
    }
    public override async Task<UpdateDisountResponse> UpdateDiscount(UpdateDisountRequest request, ServerCallContext context)
    {
            var coupon = _mapper.Map<Coupon>(request);
            await _repository.Update(coupon);
            return _mapper.Map<UpdateDisountResponse>(coupon);
        }
}
}
 