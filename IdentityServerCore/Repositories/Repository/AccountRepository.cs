using IdentityServerEFCore.Data;
using IdentityServerEFCore.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServerEFCore.Repositories.IRepository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };
            return await userManager.CreateAsync(user, model.Password);
        } 
    }
}
