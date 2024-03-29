﻿using IRPr.Models;

namespace IRPr.Repositories.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Product GetProductById(int id);
    }
}
