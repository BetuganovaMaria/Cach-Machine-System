using Application.Contracts.Users;
using Application.Models;

namespace Application.Application.Users;

internal class CurrentUserManager : ICurrentUserService
{
    public User? User { get; set; }
}