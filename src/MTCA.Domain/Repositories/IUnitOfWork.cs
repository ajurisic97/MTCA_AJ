using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Domain.Repositories;
public interface IUnitOfWork
{
    void BeginTransaction();
    void Commit();
    void Rollback();
    Task<int> SaveChangesAsync(bool auditing = true);
    Task SaveAndCommitAsync(bool auditing = true);
}
