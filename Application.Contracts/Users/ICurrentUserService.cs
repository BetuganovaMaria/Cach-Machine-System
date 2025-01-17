using Application.Models;

namespace Application.Contracts.Users;

public interface ICurrentUserService
{
    User? User { get; }
}