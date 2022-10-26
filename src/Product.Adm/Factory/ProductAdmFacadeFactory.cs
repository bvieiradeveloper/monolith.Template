﻿using InfraStructure.Context;
using Product.Adm.Facade.Implementation;
using Product.Adm.Repository.ProductRepository.Implementation;
using Product.Adm.UseCase.AddProduct;
using Product.Adm.UseCase.CheckStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Product.Adm.Factory
{
    public class ProductAdmFacadeFactory
    {
        public static ProductAdmFacade Create(SharedContext _db) 
        {
            var productRepository = new ProductRepository(_db);
            var addProductUseCase = new AddProductUseCase(productRepository);
            var checkStock = new CheckStockUseCase(productRepository);

            var productAdmFacade = new ProductAdmFacade(addProductUseCase, checkStock);

            return productAdmFacade;
        }
    }
}
