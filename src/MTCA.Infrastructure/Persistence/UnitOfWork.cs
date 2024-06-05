using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using MTCA.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCA.Shared.Models;
using Microsoft.EntityFrameworkCore.Storage;
using MTCA.Infrastructure.Persistence.Interfaces;
using MTCA.Application.Commons.Interfaces;

namespace MTCA.Infrastructure.Persistence;
internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext; 
    private IDbContextTransaction? _transaction;
    private ICurrentUser _currentUser;
    private readonly ICurrentTenantService _currentTenantService;
    public string CurrentTenantId { get; set; }
    public UnitOfWork(ApplicationDbContext dbContext, ICurrentUser currentUser, ICurrentTenantService currentTenantService)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
        _currentTenantService = currentTenantService;
        CurrentTenantId = _currentTenantService.TenantId;
    }

    public void BeginTransaction()
    {
        if (_transaction != null)
        {
            return;
        }

        _transaction = _dbContext.Database.BeginTransaction();
    }

    public Task<int> SaveChangesAsync(bool auditing = true)
    {
        if (auditing)
        {
            UpdateAuditableEntities(_currentUser.GetUserId());

        }

        return _dbContext.SaveChangesAsync();
    }

    public void Commit()
    {
        if (_transaction == null)
        {
            return;
        }

        _transaction.Commit();
        _transaction.Dispose();
        _transaction = null;
    }

    public async Task SaveAndCommitAsync(bool auditing = true)
    {
        await SaveChangesAsync(auditing);
        Commit();
    }

    public void Rollback()
    {
        if (_transaction == null)
        {
            return;
        }

        _transaction.Rollback();
        _transaction.Dispose();
        _transaction = null;
    }

    public void Dispose()
    {
        if (_transaction == null)
        {
            return;
        }

        _transaction.Dispose();
        _transaction = null;
    }

    private void UpdateAuditableEntities(Guid userId)
    {
        IEnumerable<EntityEntry<IAuditableEntity>> entries =
            _dbContext
                .ChangeTracker
                .Entries<IAuditableEntity>();


        foreach (EntityEntry<IAuditableEntity> entityEntry in entries)
        {

            switch (entityEntry.State)
            {
                case EntityState.Added:
                    entityEntry.Entity.CreatedBy = userId;
                    entityEntry.Entity.LastModifiedBy = userId;
                    entityEntry.Entity.TenantId = CurrentTenantId;
                    break;

                case EntityState.Modified:
                    entityEntry.Entity.LastModifiedOn = DateTime.Now;
                    entityEntry.Entity.LastModifiedBy = userId;
                    entityEntry.Entity.TenantId = CurrentTenantId;
                    break;

                case EntityState.Deleted:
                    if (entityEntry.Entity is ISoftDelete softDelete)
                    {
                        softDelete.DeletedBy = userId;
                        softDelete.DeletedOn = DateTime.Now;
                        entityEntry.Entity.TenantId = CurrentTenantId;
                        entityEntry.State = EntityState.Modified;
                    }

                    break;
            }
        }
    }
}
