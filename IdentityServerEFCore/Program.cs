using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServerEFCore;
using IdentityServerEFCore.Data;
using IdentityServerEFCore.Repositories.IRepo;
using IdentityServerEFCore.Repositories.Repo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var seed = args.Contains("/seed");
if (seed)
{
    args = args.Except(new[] { "/seed" }).ToArray();
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
string connectionString = builder.Configuration.GetConnectionString("localdb");
var migrationsAssembly = typeof(Program).Assembly.GetName().Name;

if (seed)
{
    SeedData.EnsureSeedData(connectionString);
}

builder.Services.AddDbContext<AspNetIdentityDbContext>(options =>
 options.UseSqlServer(connectionString,
 b => b.MigrationsAssembly(migrationsAssembly)));

builder.Services.AddTransient<IAccountRepository, AccountRepository>();
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
app.MapControllers();
app.MapGet("/", () => "Hello World!");
app.MapGet("/seed", () => {
    SeedData.EnsureSeedData(connectionString);
    return "Done";
});
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.Run();

