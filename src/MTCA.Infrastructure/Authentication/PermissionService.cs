using Microsoft.EntityFrameworkCore;
using MTCA.Domain.Models;
using MTCA.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Infrastructure.Authentication;
public class PermissionService : IPermissionService
{
    private readonly ApplicationDbContext _context;

    public PermissionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HashSet<string>> GetPermissionsAsync(Guid userId)
    {
        ICollection<Role>[] roles = await _context.Set<User>()
            .IgnoreQueryFilters()
            .Include(x => x.Roles)
            .ThenInclude(x => x.Permissions)
            .Where(x => x.Id == userId)
            .Select(x => x.Roles)
            .ToArrayAsync();
        return roles
            .SelectMany(x => x)
            .SelectMany(x => x.Permissions)
            .Select(x => x.Name)
            .ToHashSet();
    }
}
