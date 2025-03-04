#nullable disable

namespace Domain.Interfaces;

public interface IPassword
{
    bool Verify(string plainTextPassword);
}
