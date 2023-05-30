using IdentityServerEFCore.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServerEFCore.Repositories.IRepo
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel model);
    }
}
