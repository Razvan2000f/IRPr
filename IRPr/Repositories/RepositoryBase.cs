using IRPr.Models;
using IRPr.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IRPr.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ShopContext _shopContext { get; set; }

        public RepositoryBase(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public IQueryable<T> FindAll()
        {
            return _shopContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _shopContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            _shopContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _shopContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _shopContext.Set<T>().Remove(entity);
        }
    }
}
