using Ardalis.Specification;
using MTCA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Users.Specifications;
public class UsersSearchSpec : Specification<User>
{
    public UsersSearchSpec(Guid? personId, string? username, int page, int pageSize, bool counter)
    {
        if (counter)
        {
            Query
                .Where(x => personId == null || x.PersonId == personId)
                .Search(x => x.Username, "%" + username + "%", username != null);
        }
        else
        {
            Query
                .Where(x => personId == null || x.PersonId == personId)
                .Search(x => x.Username, "%" + username + "%", username != null)
                .OrderBy(x => x.LastModifiedOn)
                .Skip((page - 1) * pageSize).Take(pageSize);
        }


    }
}

