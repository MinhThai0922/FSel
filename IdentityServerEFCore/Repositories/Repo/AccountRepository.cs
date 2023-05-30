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
                }
            


            return result;
        }
    }
}
