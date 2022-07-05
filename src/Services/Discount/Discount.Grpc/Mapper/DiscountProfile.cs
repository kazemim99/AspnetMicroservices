using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Mapper
{
    public class DiscountProfile:Profile
    {
        public DiscountProfile()
        {
            CreateMap<Coupon, GetDicountReponse>().ReverseMap();
            CreateMap<List<Coupon>, List<GetDicountReponse>>().ReverseMap();
            CreateMap<CreateDisountRequest, Coupon>().ReverseMap();
            CreateMap<UpdateDisountRequest, Coupon>().ReverseMap();
            CreateMap<Coupon, CreateDisountResponse>().ReverseMap();
            CreateMap<Coupon, UpdateDisountResponse>().ReverseMap();
            CreateMap<Coupon, GetAllDicountResponse>();
        }
    }
}
 