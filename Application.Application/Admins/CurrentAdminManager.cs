using Application.Contracts.Admins;
using Application.Models;

namespace Application.Application.Admins;

internal class CurrentAdminManager : ICurrentAdminService
{
    public Admin? Admin { get; set; }
}