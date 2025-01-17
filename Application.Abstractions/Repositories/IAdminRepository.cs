using Application.Models;

namespace Application.Abstractions.Repositories;

public interface IAdminRepository
{
    Admin? FindAdminByPassword(string password);

    void UpdateSystemPassword(string newPassword);

    void RegisterUser(long accountNumber, string password);
}