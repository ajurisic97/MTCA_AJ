using Ardalis.Specification;
using MTCA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MTCA.Application.Catalog.People.Specifications;
public class PeopleBaseSpec : Specification<Person>
{
    public PeopleBaseSpec(Guid? id, string? fullName, int page, int pageSize, bool counter)
    {
        if (counter)
        {
            Query
                .Where(x => id == null || x.Id == id)
                .Where(x => fullName == null || (x.FirstName + " " + x.LastName).Contains(fullName));
        }
        else
        {
            Query
                .Where(x => id == null || x.Id == id)
                .Where(x => fullName == null || (x.FirstName + " " + x.LastName).Contains(fullName))
               .OrderByDescending(x => x.LastModifiedOn)
               .Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
