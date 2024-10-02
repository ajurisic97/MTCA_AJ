using Ardalis.Specification;
using MTCA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Users.Specifications;
public class UserByUsernameSpec : SingleResultSpecification<User>
{
    public UserByUsernameSpec(string username)
    {
        Query.Where(x => x.Username == username);
    }
}
