using BL.Abstract;
using BL.Managers;
using DAL.Abstract;
using DAL.EntityFramework;
using DAL.Generics;
using Microsoft.Extensions.DependencyInjection;

namespace BL.DependencyInjections
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryDal, EFCategoryDal>();
            services.AddScoped<ICategoryService, CategoryManager>();

            services.AddScoped<IPriceHistoryDal, EFPriceHistoryDal>();
            services.AddScoped<IPriceHistoryService, PriceHistoryManager>();

            services.AddScoped<IProductDal, EFProductDal>();
            services.AddScoped<IProductService, ProductManager>();

            services.AddScoped<IReturnHistoryDal, EFReturnHistoryDal>();
            services.AddScoped<IReturnHistoryService, ReturnHistoryManager>();

            services.AddScoped<ISellDal, EFSellDal>();
            services.AddScoped<ISellService, SellManager>();

            services.AddScoped<ISellItemDal, EFSellItemDal>();
            services.AddScoped<ISellItemService, SellItemManager>();

            services.AddScoped<IStockHistoryDal, EFStockHistoryDal>();
            services.AddScoped<IStockHistoryService, StockHistoryManager>();

            services.AddScoped<ISupplierDal, EFSupplierDal>();
            services.AddScoped<ISupplierService, SupplierManager>();

            services.AddScoped(typeof(IGenericDal<>), typeof(GenericRep<>));

            return services;
        }
    }
}
