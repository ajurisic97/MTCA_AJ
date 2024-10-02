using MTCA.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Commons.Behaviors;
public class ValidationHelper
{
    public async Task<bool> ValidForeignKey<TEntity>(Guid? id, IRepository<TEntity> repository)
        where TEntity : class
    {
        if (id == null)
        {
            return true;
        }
        var entity = await repository.GetByIdAsync(id.Value);
        if (entity != null)
        {
            return true;
        }
        return false;
    }

}
