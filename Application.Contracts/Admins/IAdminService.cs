using Application.Models.ResultTypes;

namespace Application.Contracts.Admins;

public interface IAdminService
{
    LoginResult Login(string password);

    void UpdateSystemPassword(string newPassword);

    void RegisterNewUser(long accountNumber, string password);

    void Logout();
}