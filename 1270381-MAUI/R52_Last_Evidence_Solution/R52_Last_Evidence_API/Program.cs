using Microsoft.EntityFrameworkCore;
using R52_Last_Evidence_API.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DeviceDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("db")));
builder.Services.AddControllersWithViews().AddNewtonsoftJson(op =>
{
    op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
    op.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
});
builder.Services.AddCors(op => {
    op.AddPolicy("EnableCORS", builder => {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
var app = builder.Build();

app.UseStaticFiles();
app.UseCors("EnableCORS");
app.UseRouting();
app.MapControllers();

app.Run();
