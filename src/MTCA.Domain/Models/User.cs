using MTCA.Shared.Models;
using MTCA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Domain.Models;

public class User : AuditableEntity
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }    
    public string Password { get; private set; }
    public Guid? PersonId { get; private set; } 
    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiryTime { get; private set; }
    public virtual Person? Person { get; set; }
    public ICollection<Role> Roles { get; set; } = new List<Role>();


    public User()
    {
    }

    private User(Guid id, string username, string password, Guid? personId, string? refreshToken, DateTime? refreshTokenExpiryTime)
    {
        Id = id;
        Username = username;
        Password = password;
        PersonId = personId;
        RefreshToken = refreshToken;
        RefreshTokenExpiryTime = refreshTokenExpiryTime;
    }

    public static User Create(string username, string password, Guid? personId)
    {
        return new User(Guid.NewGuid(), username, password, personId, null, null) ;
    }

    public static User CreateForMigration(Guid id, string username, string password)
    {
        return new User(id, username, password, null, null, null);
    }

    public void SetRefreshToken(string refreshToken, DateTime expiryTime)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpiryTime = expiryTime;
    }

    public void Update(string username, string password, Guid? personId)
    {
        Username = username;
        Password = password;
        PersonId = personId;
    }

    public void UpdateUsername(string username)
    {
        Username = username;
    }

    public void UpdatePassword(string password)
    {
        Password = password;
    }
    #region Rules

    public const int UsernameMaxLength = 50;
    public const int PasswordMaxLength = 100;

    #endregion
}
