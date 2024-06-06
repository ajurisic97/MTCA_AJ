using MediatR;
using MTCA.Application.Commons.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Users.Events;
internal class CacheInvalidationIdentityHandler
    : INotificationHandler<IdentityChangeEvent>
{
    private readonly ICacheService _cacheService;

    public CacheInvalidationIdentityHandler(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public Task Handle(IdentityChangeEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.UserIds, cancellationToken);

    }
    public async Task HandleInternal(List<string> ids, CancellationToken cancellationToken)
    {
        foreach (var id in ids)
        {
            await _cacheService.RemoveAsync(id, cancellationToken);

        }
    }
}
