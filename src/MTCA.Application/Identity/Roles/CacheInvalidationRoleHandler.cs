using MediatR;
using Microsoft.Extensions.Options;
using MTCA.Application.Commons.Cache;
using MTCA.Application.Commons.Models;
using MTCA.Application.Identity.Roles.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Roles;
internal class CacheInvalidationRoleHandler
    : INotificationHandler<RoleCreatedEvent>
{
    private readonly IOptionsMonitor<CacheTimeSetupInMinutes> _cacheTimeSetupOption;
    private readonly ICacheService _cacheService;
    public CacheInvalidationRoleHandler(IOptionsMonitor<CacheTimeSetupInMinutes> cacheTimeSetupOption, ICacheService cacheService)
    {
        _cacheTimeSetupOption = cacheTimeSetupOption;
        _cacheService = cacheService;
    }

    public Task Handle(RoleCreatedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(cancellationToken);
    }

    public async Task HandleInternal(CancellationToken cancellationToken)
    {
        await _cacheService.RemoveAsync(nameof(_cacheTimeSetupOption.CurrentValue.roles), cancellationToken);
        await _cacheService.RemoveAsync(nameof(_cacheTimeSetupOption.CurrentValue.roleUsers), cancellationToken);
        await _cacheService.RemoveAsync(nameof(_cacheTimeSetupOption.CurrentValue.rolePermissions), cancellationToken);
    }
}
