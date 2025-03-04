#nullable disable

using Domain.Entities;
using Domain.Interfaces;

namespace Application.Interfaces.Repositories;

public interface IUserRepository : IRepository
{
    Task<User> GetUserByUsernameAsync(string username);
    Task<bool> ValidateCredentialsAsync(string username, string password);
}
