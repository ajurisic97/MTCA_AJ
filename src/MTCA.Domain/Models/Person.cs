using MTCA.Domain.Models;
using MTCA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softly.ERP.Domain.Models;

public class Person : AuditableEntity
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public virtual ICollection<User>? Users { get; set; } = new List<User>();

    public Person()
    {

    }
    private Person(Guid id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    public static Person Create(string firstName, string lastName)
    {
        return new Person(Guid.NewGuid(), firstName, lastName);
    }

    public void Update(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }


    #region Rules

    public const int FirstNameMaxLength = 50;
    public const int LastNameMaxLength = 50;

    #endregion

}
