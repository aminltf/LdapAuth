using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Infrastructure.Ldap.Repositories;
using Infrastructure.Ldap.Services;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Configure LdapSettings from appsettings.json
builder.Services.Configure<LdapSettings>(builder.Configuration.GetSection("LdapSettings"));

// Add dependency injection for IUserRepository
builder.Services.AddScoped<IUserRepository>(sp =>
{
    var settings = sp.GetService<IOptions<LdapSettings>>()?.Value;
    return new LdapRepository(settings!);
});

// Add dependency injection for IAuthenticationService
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
