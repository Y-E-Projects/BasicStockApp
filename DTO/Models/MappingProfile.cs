using AutoMapper;
using EL.Concrete;

namespace DTO.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Sell Mapping
            CreateMap<Sell, DetailModel.Sell>()
                .ForMember(dest => dest.SellDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.ReturnSells, opt => opt.MapFrom(src => src.Items.SelectMany(i => i.ReturnHistories)));

            CreateMap<SellItem, DetailModel.SellItemDetail>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<ReturnHistory, DetailModel.ReturnSellWithSellDetail>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<Sell, ListModel.Sell>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src => src.Items.Count));

            // Supplier Mapping
            CreateMap<Supplier, ListModel.Supplier>()
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Products.Count));

            // StockHistory Mapping
            CreateMap<StockHistory, ListModel.StockHistory>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<StockHistory, DetailModel.StockHistory>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product.Name));

            // Product Mapping
            CreateMap<Product, ListModel.Product>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Product, DetailModel.Product>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => src.Supplier != null ? src.Supplier.Name : null))
                .ForMember(dest => dest.MinQuantity, opt => opt.MapFrom(src => src.MinimumQuantity))
                .ForMember(dest => dest.TotalSell, opt => opt.MapFrom(src => src.SellItems.Sum(x => x.Quantity)))
                .ForMember(dest => dest.ReturnSell, opt => opt.MapFrom(src => src.ReturnHistories.Sum(x => x.Quantity)))
                .ForMember(dest => dest.PriceHistories, opt => opt.MapFrom(src => src.PriceHistories.OrderBy(ph => ph.CreatedAt)));

            CreateMap<PriceHistory, DetailModel.PriceHistoryDetail>()
                .ForMember(dest => dest.BackPrice, opt => opt.MapFrom(src => src.BackPrice))
                .ForMember(dest => dest.NewPrice, opt => opt.MapFrom(src => src.NewPrice))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<Product, ListModel.TopSellProduct>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.TotalQuantity, opt => opt.MapFrom(src =>
                    src.SellItems.Sum(si => si.Quantity) -
                    src.SellItems.SelectMany(si => si.ReturnHistories).Sum(rh => rh.Quantity)))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src =>
                    src.SellItems.Sum(si => si.LineTotal) -
                    src.SellItems.SelectMany(si => si.ReturnHistories).Sum(rh => rh.UnitPrice * rh.Quantity)
                ));
        }
    }
}
