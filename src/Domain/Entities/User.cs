#nullable disable

using Domain.Common;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Fullname { get; set; }

    public static User Create(string username, string fullname)
    {
        return new User
        {
            Username = username,
            Fullname = fullname
        };
    }

    private User() { }
}
