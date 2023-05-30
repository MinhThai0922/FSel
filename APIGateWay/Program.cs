using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Configuration.AddJsonFile("APIGateway.json", optional: false, reloadOnChange: true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot(builder.Configuration);
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

app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.UseHttpsRedirection();
app.UseCors("OpenCorsPolicy");
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
await app.UseOcelot();
app.Run();
