using AplikacioniSloj;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlojPodataka.Interfejsi;
using SlojPodataka.Repozitorijumi;
using DomenskiSloj;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IKorisnikRepo>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var stringKonekcije = configuration.GetConnectionString("MojKonekcioniString");

    return new clsKorisnikRepo(stringKonekcije);
});

builder.Services.AddScoped<IRezultatRepo>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var stringKonekcije = configuration.GetConnectionString("MojKonekcioniString");

    return new clsRezultatRepo(stringKonekcije);
});

builder.Services.AddScoped<IIspitRepo>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var stringKonekcije = configuration.GetConnectionString("MojKonekcioniString");

    return new clsIspitRepo(stringKonekcije);
});

builder.Services.AddScoped<IPrijavaRepo>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var stringKonekcije = configuration.GetConnectionString("MojKonekcioniString");

    return new clsPrijavaRepo(stringKonekcije);
});

builder.Services.AddScoped<clsKorisnikServis>();
builder.Services.AddScoped<clsRezultatServis>();
builder.Services.AddScoped<clsPrijavaServis>();
builder.Services.AddScoped<clsIspitServis>();
builder.Services.AddScoped<clsPoslovnaPravila>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Pocetna}/{id?}");

app.Run();
