// Created by Nicholas Maddox
using Microsoft.EntityFrameworkCore;
using MvcEldenRingBossLore.Models;
using MvcEldenRingBossLore.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MvcEldenRingBossLoreContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MvcEldenRingBossLoreContext"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MvcEldenRingBossLoreContext"))
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
