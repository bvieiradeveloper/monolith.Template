﻿using _Shared.Domain.Entity;
using _Shared.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Adm.Domain.Entity
{
    public class ProductEntity : BaseEntity, IAggregatorRoot
    {
        public ProductEntity(ProductProps productProps) : base(productProps.id)
        {
            Name = productProps.Name;
            Description = productProps.Description; 
            PurchasePrice = productProps.PurchasePrice; 
            Stock = productProps.Stock; 
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public long PurchasePrice { get; private set; }
        public long Stock { get; private set; }

        public void SetName(string name)
        {
            Name = name.Trim();
        }

        public void SetDescription(string description)
        {
            Description = description.Trim();
        }

        public void SetPurchasePrice(long purchasePrace)
        {
            PurchasePrice = purchasePrace;
        }

        public void SetStock(long stock)
        {
            Stock = stock;
        }
    }
}