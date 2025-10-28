using EVCharging.WebApp.TrongLH.Models;
using EVCharging.WebApp.TrongLH.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add gRPC services
builder.Services.AddSingleton<EnergySupplyGrpcService>();
builder.Services.AddSingleton<StationGrpcService>();

// Add configuration for gRPC
builder.Services.Configure<GrpcSettings>(builder.Configuration.GetSection("GrpcSettings"));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();