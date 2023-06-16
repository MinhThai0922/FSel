using Chat.Data;
using Chat.Data.Entity;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("localdb");
var migrationsAssembly = typeof(Program).Assembly.GetName().Name;
// Add services to the container.    
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ManageAppDbContext>(options =>
    options.UseSqlServer(b => b.MigrationsAssembly(migrationsAssembly)));

builder.Services.AddIdentity<ManageUser, IdentityRole>()
    .AddEntityFrameworkStores<ManageAppDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseStaticFiles();
app.UseIdentityServer();
app.UseAuthorization();
app.UseAuthentication();
app.UseEndpoints(endpoints =>
{
        endpoints.MapRazorPages();
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
