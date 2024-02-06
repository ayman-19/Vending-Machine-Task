using AutoMapper;
using TaskFlapKap.DataTransfareObject.Product;
using TaskFlapKap.Domain.Model;

namespace TaskFlapKap.Application.Mapping.Products
{
	internal class ProductProfile : Profile
	{
		public ProductProfile()
		{
			CreateMap<Product, ProductResponse>().ForMember(p => p.SellerName, op => op.MapFrom(d => d.User!.UserName)).ReverseMap();

			//CreateMap<List<Product>, List<ProductResponse>>().ReverseMap();
			CreateMap<Product, Productdto>().ReverseMap();
		}
	}
}
