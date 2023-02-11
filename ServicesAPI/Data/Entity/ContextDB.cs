using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ServicesAPI.Models.Users;

namespace ServicesAPI.Data.Entity
{
    public class ContextDB : IdentityDbContext<User>
    {
    }
}
