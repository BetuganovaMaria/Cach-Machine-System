using Application.Abstractions.Repositories;
using Application.Contracts.Admins;
using Application.Models;
using Application.Models.ResultTypes;

namespace Application.Application.Admins;

internal class AdminService : IAdminService
{
    private readonly IAdminRepository _repository;
    private readonly CurrentAdminManager _currentAdminManager;

    public AdminService(IAdminRepository repository, CurrentAdminManager currentAdminManager)
    {
        _repository = repository;
        _currentAdminManager = currentAdminManager;
    }

    public LoginResult Login(string password)
    {
        Admin? admin = _repository.FindAdminByPassword(password);

        if (admin is null)
        {
            return new LoginResult.Failure(new NotFound());
        }

        _currentAdminManager.Admin = admin;
        return new LoginResult.Success();
    }

    public void UpdateSystemPassword(string newPassword)
    {
        _repository.UpdateSystemPassword(newPassword);
    }

    public void RegisterNewUser(long accountNumber, string password)
    {
        _repository.RegisterUser(accountNumber, password);
    }

    public void Logout()
    {
        _currentAdminManager.Admin = null;
    }
}