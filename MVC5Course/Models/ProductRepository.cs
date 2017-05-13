using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        public override IQueryable<Product> All()
        {
            return base.All().Where(p => !p.IsDeleted);
        }

        public Product FindByProductId(int id)
        {
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }

        public IQueryable<Product> FindByAll(bool Active,int ShowCnt = 0)
        {
            var data = this.All()
                .Where(p => p.Active.HasValue && p.Active.Value == Active)
                .OrderByDescending(p => p.ProductId);

            if(ShowCnt > 0)
            {
                return data.Take(ShowCnt);
            }
            return data;
        }

        public void Update(Product UpdData)
        {
            this.UnitOfWork.Context.Entry(UpdData).State = EntityState.Modified;
        }
	}

	public  interface IProductRepository : IRepository<Product>
	{

	}
}