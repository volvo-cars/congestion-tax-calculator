using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Volvo.Infrastructure.SharedKernel.Repositories
{
    public interface IGenericRepository<TKey, TEntity>
    {
        public Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
        public Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
    }

    public interface IRepository<TEntity> : IGenericRepository<int, TEntity>
    {
    }
}