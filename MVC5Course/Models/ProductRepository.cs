using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVC5Course.Models
{
    public class ProductRepository : EFRepository<Product>, IProductRepository
    {
        public override IQueryable<Product> All()
        {
            return base.All().Where(p => !p.IsDeleted);
        }

        public IQueryable<Product> All(bool ShowAll)
        {
            if (ShowAll)
            {
                return base.All();
            }
            else
            {
                return this.All();
            }
        }

        public Product FindByProductId(int id)
        {
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }

        public IQueryable<Product> FindByAll(bool Active, bool ShowAll = false, int ShowCnt = 0)
        {
            var all = All(ShowAll);
            var data = all
                .Where(p => p.Active.HasValue && p.Active.Value == Active)
                .OrderByDescending(p => p.ProductId);

            if (ShowCnt > 0)
            {
                return data.Take(ShowCnt);
            }
            return data;
        }

        public void Update(Product UpdData)
        {
            this.UnitOfWork.Context.Entry(UpdData).State = EntityState.Modified;
        }

        public override void Delete(Product entity)
        {
            entity.IsDeleted = true;
        }
    }

    public interface IProductRepository : IRepository<Product>
    {

    }
}