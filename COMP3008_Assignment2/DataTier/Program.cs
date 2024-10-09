using DataTier.Database;
using DataTier.Models;
using DataTier.Models.DataSeeding;

var builder = WebApplication.CreateBuilder(args);

DatabaseManager.CreateTables();

if (AccountsOps.GetAll().Count() == 0)
{
    RandomGenerator.Generate();
}

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
