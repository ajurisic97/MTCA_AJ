using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MTCA.Domain.Repositories;
using MTCA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Infrastructure.Persistence.Repositories;
// Inherited from Ardalis.Specification's RepositoryBase<T>
public class ApplicationDbRepository<T> : RepositoryBase<T>, IRepository<T>
    where T : class
{
    private readonly ApplicationDbContext _dbContext;
    public ApplicationDbRepository(ApplicationDbContext dbContext)
        :base(dbContext)
    {
        _dbContext = dbContext;
    }

    

    public override async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<T>().AddAsync(entity);

        return entity;

    }

    public override async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Remove(entity);

    }
    public override async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().RemoveRange(entities);

    }

    public async override Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
    {
        var table = typeof(T);
        var hasLastModified = table.GetProperty("LastModifiedOn") != null;
        if (hasLastModified)
        {
            var param = Expression.Parameter(typeof(T));
            var memberAccess = Expression.Property(param, "LastModifiedOn");

            var convertedMemberAccess = Expression.Convert(memberAccess, typeof(object));
            var orderPredicate = Expression.Lambda<Func<T, object>>(convertedMemberAccess, param);

            return await _dbContext.Set<T>().OrderByDescending(orderPredicate).ToListAsync(cancellationToken);
        }
        else
        {
            return await _dbContext.Set<T>().ToListAsync(cancellationToken);

        }
    }
}
