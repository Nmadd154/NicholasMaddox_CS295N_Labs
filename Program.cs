using Microsoft.EntityFrameworkCore;
using MvcEldenRingBossLore.Models;
using MvcEldenRingBossLore.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure database - using SQLite for all environments (macOS compatible)
builder.Services.AddDbContext<MvcEldenRingBossLoreContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MvcEldenRingBossLoreContext") ?? "Data Source=MvcEldenRingBossLore.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
