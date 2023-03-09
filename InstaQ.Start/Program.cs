 using System.Globalization;
using Hangfire;
using Microsoft.AspNetCore.Localization;
using InstaQ.Start.Extensions;
using InstaQ.Start.Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddWebServices();
builder.Services.AddDomainServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddInstagramServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddAuthenticationServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddEventsHandlers(builder.Configuration);
builder.Services.AddHangfireServices(builder.Configuration);
var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


var cultures = new List<CultureInfo> { CultureInfo.GetCultureInfo("Ru-ru") };
var x = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(cultures.First()),
    SupportedCultures = cultures,
    SupportedUICultures = cultures
};
app.UseStaticFiles();
app.UseRequestLocalization(x);

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHangfireDashboard("/tasks",
    new DashboardOptions { Authorization = new[] { new HangfireAuthorizationFilter() } });
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();