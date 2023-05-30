using IdentityServerEFCore.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServerEFCore.Repositories.IRepository
{
    public interface IAccountRepository 
    {
        Task<IdentityResult> SignUpAsync(SignUpModel model);
    }
}
