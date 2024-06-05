using Azure.Core;
using JsonNet.ContractResolvers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MTCA.Application.Commons.Interfaces;
using MTCA.Domain.Authorization;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using MTCA.Shared.Multitenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Infrastructure.Persistence.Initialization;
internal class ApplicationDbInitializer
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ICurrentTenantService _currentTenant;
    private readonly ILogger<ApplicationDbInitializer> _logger;
    private readonly IServiceProvider _serviceProvider;

    public ApplicationDbInitializer(ApplicationDbContext dbContext, ICurrentTenantService currentTenant, ILogger<ApplicationDbInitializer> logger, IServiceProvider serviceProvider, IPasswordHasher passwordHasher)
    {
        _dbContext = dbContext;
        _currentTenant = currentTenant;
        _logger = logger;
        _serviceProvider = serviceProvider;
        _passwordHasher = passwordHasher;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (_dbContext.Database.GetMigrations().Any())
        {
            if ((await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
            {
                _logger.LogInformation("Applying Migrations for '{tenantId}' tenant.", _currentTenant.TenantId);
                await _dbContext.Database.MigrateAsync(cancellationToken);
            }

            if (await _dbContext.Database.CanConnectAsync(cancellationToken))
            {
                _logger.LogInformation("Connection to {tenantId}'s Database Succeeded.", _currentTenant.TenantId);

                await SeedDatabaseAsync(cancellationToken);
            }
        }
    }

    

    private async Task SeedDatabaseAsync(CancellationToken cancellationToken)
    {
        await IdentitySeedAsync(cancellationToken);
        await CustomSeedAsync(cancellationToken);


    }

    private async Task IdentitySeedAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        await scope.ServiceProvider.GetRequiredService<ICurrentTenantService>()
            .SetTenant(_currentTenant.ApiKey);

        var _unitOfWorkRepository = scope.ServiceProvider.GetService<IUnitOfWork>();
        var _userRepository = scope.ServiceProvider.GetService<IRepository<User>>();
        var _roleRepository = scope.ServiceProvider.GetService<IRepository<Role>>();
        var _userRoleRepository = scope.ServiceProvider.GetService<IRepository<UserRole>>();
        var _rolePermissionRepository = scope.ServiceProvider.GetService<IRepository<RolePermission>>();
        var _permissionRepository = scope.ServiceProvider.GetService<IRepository<Permission>>();

        var rootPath = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName + "/MTCA.Infrastructure";
        var jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new PrivateSetterContractResolver()
        };

        #region Permission
        var a = await _permissionRepository!.ListAsync();
        if (!_permissionRepository!.ListAsync().Result.Any())
        {
            var permissions = Permissions._all;
            var permissionList = new List<Permission>();
            foreach (var permission in permissions)
            {
                var newPermission = new Permission
                {
                    Name = permission.Name,
                };
                permissionList.Add(newPermission);
            }
            await _permissionRepository.AddRangeAsync(permissionList);
        }

        #endregion

        #region User

        if (!(await _userRepository.ListAsync(cancellationToken)).Any())
        {
            if (_currentTenant.TenantId == MultitenancyConstants.Root.Id)
            {
                var passwordHash = _passwordHasher.Hash(UserConstants.SuperAdmin.DefaultPassword);
                var superAdmin = User.CreateForMigration(Guid.Parse(UserConstants.SuperAdmin.DefaultId), UserConstants.SuperAdmin.DefaultUsername, passwordHash);
                await _userRepository.AddAsync(superAdmin);
            }
            var passwordHash2 = _passwordHasher.Hash(UserConstants.Admin.DefaultPassword);
            var admin = User.CreateForMigration(Guid.Parse(UserConstants.Admin.DefaultId), UserConstants.Admin.DefaultUsername, passwordHash2);
            await _userRepository.AddAsync(admin);
        }

        #endregion

        #region Role

        if (!(await _roleRepository.ListAsync(cancellationToken)).Any())
        {
            if (_currentTenant.TenantId == MultitenancyConstants.Root.Id)
            {
                var superAdminRole = Role.CreateForMigration(Guid.Parse(RoleConstants.SuperAdmin.Id), RoleConstants.SuperAdmin.Name, RoleConstants.SuperAdmin.Description);
                await _roleRepository.AddAsync(superAdminRole, cancellationToken);
            }
            var adminRole = Role.CreateForMigration(Guid.Parse(RoleConstants.Admin.Id), RoleConstants.Admin.Name, RoleConstants.Admin.Description);
            await _roleRepository.AddAsync(adminRole, cancellationToken);
        }

        #endregion

        #region UserRole

        if (!(await _userRoleRepository.ListAsync(cancellationToken)).Any())
        {
            if (_currentTenant.TenantId == MultitenancyConstants.Root.Id)
            {
                var root = UserRole.Assign(Guid.Parse(RoleConstants.SuperAdmin.Id), Guid.Parse(UserConstants.SuperAdmin.DefaultId));
                await _userRoleRepository.AddAsync(root, cancellationToken);
            }
            var tenant = UserRole.Assign(Guid.Parse(RoleConstants.Admin.Id), Guid.Parse(UserConstants.Admin.DefaultId));
            await _userRoleRepository.AddAsync(tenant, cancellationToken);
        }

        #endregion

        #region RolePermission
        if (!(await _rolePermissionRepository.ListAsync(cancellationToken)).Any())
        {
            var permissions = await _permissionRepository.ListAsync(cancellationToken);
            var rolePermissionList = new List<RolePermission>();

            if (_currentTenant.TenantId == MultitenancyConstants.Root.Id)
            {
                permissions.ForEach(x =>
                {
                    rolePermissionList.Add(RolePermission.Create(Guid.Parse(RoleConstants.SuperAdmin.Id), x.Id));
                });

            }

            permissions.ForEach(x =>
            {
                rolePermissionList.Add(RolePermission.Create(Guid.Parse(RoleConstants.Admin.Id), x.Id));
            });
            foreach (var per in rolePermissionList)
            {
                await _rolePermissionRepository.AddAsync(per, cancellationToken);

            }
        }

        #endregion

        await _unitOfWorkRepository.SaveAndCommitAsync();


    }

    private async Task CustomSeedAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        await scope.ServiceProvider.GetRequiredService<ICurrentTenantService>()
            .SetTenant(_currentTenant.ApiKey);
        var _unitOfWorkRepository = scope.ServiceProvider.GetService<IUnitOfWork>();

        var rootPath = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName + "/MTCA.Infrastructure";
        var jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new PrivateSetterContractResolver()
        };

        await _unitOfWorkRepository.SaveAndCommitAsync();
    }
}
