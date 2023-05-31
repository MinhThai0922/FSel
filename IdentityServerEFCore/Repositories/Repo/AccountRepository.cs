using IdentityModel;
using IdentityServerEFCore.Data;
using IdentityServerEFCore.Models;
using IdentityServerEFCore.Repositories.IRepo;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityServerEFCore.Repositories.Repo
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AccountRepository(AspNetIdentityDbContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> SignUpAsync(SignUpModel createAccUser)
        {
            //using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            IdentityUser user = new IdentityUser
            {
                Email = createAccUser.Email,
                UserName = createAccUser.FirstName
            };
            List<string> roles = new List<string>();
            if(createAccUser.view_customer)
            {
                roles.Add("view_customer");
            }
            if (createAccUser.update_customer)
            {
                roles.Add("update_customer");
            }
            if (createAccUser.delete_customer)
            {
                roles.Add("delete_customer");
            }
            if (createAccUser.create_customer)
            {
                roles.Add("create_customer");
            }
            IdentityResult result = await _userManager.CreateAsync(user, createAccUser.Password);
                if (result.Succeeded)
                {
                    result = _userManager.AddClaimsAsync(user, new Claim[]
                            {
                            new Claim(JwtClaimTypes.Name, createAccUser.FirstName +" "+ createAccUser.LastName),
                            new Claim(JwtClaimTypes.GivenName, createAccUser.FirstName),
                            new Claim(JwtClaimTypes.FamilyName, createAccUser.LastName),
                            new Claim(JwtClaimTypes.WebSite, "http://"+createAccUser.FirstName + createAccUser.LastName+".com"),
                            new Claim("location", "somewhere")
                            }
                        ).Result;
                    if (result.Succeeded)
                    {
                    result = _userManager.AddToRolesAsync(user, roles).Result;
                    }
                }
            return result;
        }
    }
}
