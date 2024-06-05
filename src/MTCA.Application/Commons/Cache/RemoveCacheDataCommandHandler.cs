using MTCA.Application.Commons.Abstractions;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using MTCA.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Commons.Cache;
internal class RemoveCacheDataCommandHandler : ICommandHandler<RemoveCacheDataCommand, bool>
{
    private readonly ICacheService _cacheService;
    private readonly IRepository<User> _userRepository;
    public RemoveCacheDataCommandHandler(ICacheService cacheService, IRepository<User> userRepository)
    {
        _cacheService = cacheService;
        _userRepository = userRepository;
    }

    public async Task<Result<bool>> Handle(RemoveCacheDataCommand request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.ListAsync(cancellationToken);

        if (users.Any())
        {
            foreach (var user in users)
            {
                var userCache = await _cacheService.GetAsync<HashSet<string>?>(user.Id.ToString());
                if(userCache != null)
                {
                    await _cacheService.RemoveAsync(user.Id.ToString());

                }
            }
            return true;
        }
        return false;
    }
}
