#nullable disable

using Domain.Interfaces;

namespace Domain.ValueObjects;

public class Password : IPassword
{
    public string Hash { get; private set; }

    public static Password Create(string plainTextPassword)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainTextPassword);

        return new Password
        {
            Hash = hashedPassword
        };
    }

    public bool Verify(string plainTextPassword)
    {
        return BCrypt.Net.BCrypt.Verify(plainTextPassword, Hash);
    }
}
