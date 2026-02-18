// Created by Nicholas Maddox
using Microsoft.EntityFrameworkCore;
using MvcEldenRingBossLore.Models;
using MvcEldenRingBossLore.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var baseConn = builder.Configuration.GetConnectionString("MySqlBase");
var user = builder.Configuration["user"];
var password = builder.Configuration["password"];

var finalConn = $"{baseConn}User={user};Password={password};";

builder.Services.AddDbContext<MvcEldenRingBossLoreContext>(options =>
    options.UseMySql(
        finalConn,
        ServerVersion.AutoDetect(finalConn)
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
