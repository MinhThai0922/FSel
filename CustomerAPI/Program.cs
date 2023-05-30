using Microsoft.EntityFrameworkCore;
using APICustomer.DatabaseContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CustomerAPI.Repositories.IRepon;
using CustomerAPI.Repositories.Repon;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Ocelot.Values;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("myHttpClient", client =>
{
    // Các thiết lập của HttpClient
    client.BaseAddress = new Uri("https://localhost:7055");
});

builder.Services.AddAuthentication("Bearer")
    .AddIdentityServerAuthentication("Bearer", options =>
    {
        options.Authority = "https://localhost:7109";
        options.ApiName = "api";
    });


builder.Services.AddDbContext<CustomerDBContext>(c => c.UseSqlServer("Server=DESKTOP-KULFO3T\\MINHTHAI;Database=FselCustomer;Trusted_Connection=True;"));
builder.Services.AddTransient<IUserRepon, UserRepon>();
builder.Services.AddTransient<ICustomerRepon, CustomerRepon>();
builder.Services.AddMediatR(typeof(CustomerRepon).Assembly);

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
//{
//    options.RequireHttpsMetadata = false;//tắt yêu cầu https khi giao tiếp giữa client và server(tắt khi chạy ở localhost, nên bật khi chạy ở môi trường sản phẩm để bảo vệ thông tin truyền tải
//    options.SaveToken = true; //được sử dụng để lưu trữ JWT token trong HttpContext sau khi nó được xác thực.
//    options.TokenValidationParameters = new TokenValidationPara   meters()
//    {
//        ValidateIssuer = true, //xác định liệu issuer của token có hợp lệ hay không
//        ValidateAudience = true, //xác định liệu audience của token có hợp lệ hay không
//        ValidAudience = "FselCustomer", //tên của audience được cho phép
//        ValidIssuer = "https://localhost:7055", //tên của issuer được cho phép
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsTheSecureKey1234567890")), //khóa bí mật để xác minh tính hợp lệ của token
//        ValidateIssuerSigningKey = true,
//    };
//});

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("OpenCorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("OpenCorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();