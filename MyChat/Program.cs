using MyChat;
using MyChat.Hubs;
using MyChat.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddCors();
//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(builder => 
//    {
//        builder.AllowAnyOrigin();

//        //builder.WithOrigins("http://localhost:3000/")
//        //    .AllowAnyHeader()
//        //    .AllowAnyMethod()
//        //    .AllowCredentials()
//        //    .AllowAnyOrigin();
//    });
//});
builder.Services.AddTransient<IUserRepo, UserRepo>();
builder.Services.AddSingleton<IDictionary<string, UserConnection>>(otps => new Dictionary<string, UserConnection>());
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
//app.UseCors();

// global cors policy
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());
app.MapHub<ChatHub>("/chat");
app.Run();
