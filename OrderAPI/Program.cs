using OrderAPI.DatabaseContext;
using OrderAPI.Repositories.IRepon;
using OrderAPI.Repositories.Repon;
using OrderAPI.Services.IService;
using OrderAPI.ViewModel.JWT_Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Refit;
using System.Text;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
var Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<AuthenticatedHttpClientHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRefitClient<IOrderService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7055"))
    .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("OpenCorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddHttpClient("myHttpClient", client =>
{
    // Các thiết lập của HttpClient
    client.BaseAddress = new Uri("https://localhost:7055");
});

builder.Services.AddDbContext<OrderDbContext>(c => c.UseSqlServer("Server=DESKTOP-KULFO3T\\MINHTHAI;Database=FselOrder;Trusted_Connection=True;"));
builder.Services.AddTransient<AuthenticatedHttpClientHandler>();
builder.Services.AddTransient<IOrderRepon, OrderRepon>();
builder.Services.AddTransient<IOrderDetailRepon, OrderDetailRepon>();
builder.Services.AddMediatR(typeof(OrderRepon).Assembly);
builder.Services.AddMediatR(typeof(OrderDetailRepon).Assembly);

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;//tắt yêu cầu https khi giao tiếp giữa client và server(tắt khi chạy ở localhost, nên bật khi chạy ở môi trường sản phẩm để bảo vệ thông tin truyền tải
    options.SaveToken = true; //được sử dụng để lưu trữ JWT token trong HttpContext sau khi nó được xác thực.
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true, //xác định liệu issuer của token có hợp lệ hay không
        ValidateAudience = true, //xác định liệu audience của token có hợp lệ hay không
        ValidAudience = "FselCustomer", //tên của audience được cho phép
        ValidIssuer = "https://localhost:7055", //tên của issuer được cho phép
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsTheSecureKey1234567890")), //khóa bí mật để xác minh tính hợp lệ của token
        ValidateIssuerSigningKey = true,
    };
});
builder.Services.AddCors(policy => {
    policy.AddPolicy("OpenCorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("OpenCorsPolicy");
app.MapControllers();

app.Run();