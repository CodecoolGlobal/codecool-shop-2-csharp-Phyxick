using System;
using System.Linq;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Services;
using Microsoft.Extensions.Configuration;

namespace Codecool.CodecoolShop.Helpers
{
    public class ServiceHelper
    {
        private IProductDao _productDao;
        private IProductCategoryDao _productCategoryDao;
        private ISupplierDao _supplierDao;
        private IUserDao _userDao;
        private ICartDao _cartDao;
        private static ServiceHelper _instance;

        private static ServiceHelper GetInstance()
        {
            return _instance ??= new ServiceHelper();
        }

        private void SetDaos()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();


            var mode = config.GetSection("Mode").Value;
            switch (mode)
            {
                case "sql":
                    var connectionString = config.GetSection("ConnectionStrings")
                        .GetChildren()
                        .First(x => x.Key == "DefaultConnection").Value;
                    _productDao = ProductDaoDB.GetInstance(connectionString);
                    _productCategoryDao = ProductCategoryDaoDB.GetInstance(connectionString);
                    _supplierDao = SupplierDaoDB.GetInstance(connectionString);
                    _userDao = UserDaoDB.GetInstance(connectionString);
                    _cartDao = CartDaoDB.GetInstance(connectionString);
                    break;
                case "memory":
                    _productDao = ProductDaoMemory.GetInstance();
                    _productCategoryDao = ProductCategoryDaoMemory.GetInstance();
                    _supplierDao = SupplierDaoMemory.GetInstance();
                    Startup.SetupInMemoryDatabases();
                    break;
                default:
                    throw new Exception("Invalid database mode in config file!");
            }
        }

        public static ProductService GetProductService()
        {
            ServiceHelper serviceHelper = GetInstance();
            serviceHelper.SetDaos();

            return new ProductService(serviceHelper._productDao, serviceHelper._productCategoryDao,
                serviceHelper._supplierDao);
        }

        public static UserService GetUserService()
        {
            ServiceHelper serviceHelper = GetInstance();
            serviceHelper.SetDaos();

            return new UserService(serviceHelper._userDao);
        }

        public static CartService GetCartService()
        {
            ServiceHelper serviceHelper = GetInstance();
            serviceHelper.SetDaos();

            return new CartService(serviceHelper._cartDao);
        }
    }
}
