#nullable disable

using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Settings;
using System.DirectoryServices;
using System.Runtime.Versioning;

namespace Infrastructure.Ldap.Repositories;

public class LdapRepository : IUserRepository
{
    private readonly LdapSettings _settings;

    public LdapRepository(LdapSettings settings) => _settings = settings;

    [SupportedOSPlatform("windows")]
    public async Task<User> GetUserByUsernameAsync(string username)
    {
        try
        {
            using var entry = new DirectoryEntry(_settings.ServerUrl, $"{username}@{_settings.Domain}", null);
            using var searcher = new DirectorySearcher(entry);
            searcher.Filter = $"(&(objectClass=user)(sAMAccountName={username}))";
            searcher.PropertiesToLoad.Add("displayName");

            var result = await Task.Run(searcher.FindOne);

            if (result != null)
            {
                return User.Create(
                    username,
                    result.Properties["displayName"].Count > 0 ? result.Properties["displayName"][0].ToString() : string.Empty
                );
            }

            return null;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error occurred while fetching user by username: {ex.Message}", ex);
        }
    }

    [SupportedOSPlatform("windows")]
    public async Task<bool> ValidateCredentialsAsync(string username, string password)
    {
        try
        {
            using (var entry = new DirectoryEntry(_settings.ServerUrl, $"{username}@{_settings.Domain}", password))
            {
                using (var searcher = new DirectorySearcher(entry))
                {
                    searcher.Filter = $"(&(objectClass=user)(sAMAccountName={username}))";

                    var result = await Task.Run(() => searcher.FindOne());

                    return result != null;
                }
            }
        }
        catch (UnauthorizedAccessException)
        {
            return false;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error occurred while validating credentials: {ex.Message}", ex);
        }
    }
}
