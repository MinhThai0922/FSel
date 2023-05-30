using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServerEFCore;
using IdentityServerEFCore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("localdb");

var migrationsAssembly = typeof(Program).Assembly.GetName().Name;


builder.Services.AddDbContext<AspNetIdentityDbContext>(options =>
 options.UseSqlServer(connectionString,
 b => b.MigrationsAssembly(migrationsAssembly)));


builder.Services.AddIdentity<IdentityUser, IdentityRole>()
 .AddEntityFrameworkStores<AspNetIdentityDbContext>().AddDefaultTokenProviders();

builder.Services.AddIdentityServer().AddAspNetIdentity<IdentityUser>()
.AddConfigurationStore(options =>
{
    options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
})
.AddDeveloperSigningCredential()
.AddOperationalStore(options =>
{
    options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
    options.EnableTokenCleanup = true;
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    InitializeDatabase(app);
}
app.MapGet("/", () => "Hello World!");
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.Run();
static void InitializeDatabase(IApplicationBuilder app)
{
    using (var serviceScope =
            app.ApplicationServices
                .GetService<IServiceScopeFactory>().CreateScope())
    {
        serviceScope
            .ServiceProvider
                .GetRequiredService<PersistedGrantDbContext>()
                .Database.Migrate();

        var context =
            serviceScope.ServiceProvider
                .GetRequiredService<ConfigurationDbContext>();
        context.Database.Migrate();
        if (!context.Clients.Any())
        {
            foreach (var client in Config.Clients)
            {
                context.Clients.Add(client.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.ApiScopes.Any())
        {
            foreach (var resource in Config.ApiScopes)
            {
                context.ApiScopes.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.IdentityResources.Any())
        {
            foreach (var resource in Config.Ids)
            {
                context.IdentityResources.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.ApiResources.Any())
        {
            foreach (var resource in Config.Apis)
            {
                context.ApiResources.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }
        var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        var angella = userMgr.FindByNameAsync("angella").Result;
        if (angella == null)
        {
            angella = new IdentityUser
            {
                UserName = "angella",
                Email = "angella.freeman@email.com",
                EmailConfirmed = true
            };
            var result = userMgr.CreateAsync(angella, "Pass123$").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result =
                userMgr.AddClaimsAsync(
                    angella,
                    new Claim[]
                    {
                            new Claim(JwtClaimTypes.Name, "Angella Freeman"),
                            new Claim(JwtClaimTypes.GivenName, "Angella"),
                            new Claim(JwtClaimTypes.FamilyName, "Freeman"),
                            new Claim(JwtClaimTypes.WebSite, "http://angellafreeman.com"),
                            new Claim("location", "somewhere")
                    }
                ).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
        }
    }


}
