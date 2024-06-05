using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Domain.Models;

public class Role
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; } 
    public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    public ICollection<User> Users { get; set; } = new List<User>();


    public Role()
    {
        //EF CORE config
    }

    private Role(Guid id, string name, string? description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public static Role Create(string name, string? description)
    {
        return new Role(Guid.NewGuid(), name, description);
    }


    public static Role CreateForMigration(Guid id, string name, string? description)
    {
        return new Role(id, name, description);
    }
    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
    }

}
