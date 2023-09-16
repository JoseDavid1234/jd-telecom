using JDTelecomunicaciones.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using DotNetEnv;
using JDTelecomunicaciones.Services;

Env.Load();

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddHttpClient();

/*builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(Environment.GetEnvironmentVariable("CONECTION_STRING")));
*/

var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
//builder.Services.AddDat();
//builder.Services.AddScoped<IUsuarioService,UsuarioServiceImplement>();
builder.Services.AddScoped<TicketServiceImplement>();
builder.Services.AddScoped<UsuarioServiceImplement>();
//builder.Services.AddScoped<PersonaServiceImplement>();
//builder.Services.AddScoped<ProductoServiceImplement>();
//builder.Services.AddScoped<CategoriaServiceImplement>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>{
    options.LoginPath = "/Autentificacion/IniciarSesion";
    options.LogoutPath = "/Home/Index.cshtml";
    options.AccessDeniedPath = "/Autentificacion/IniciarSesion";
    options.AccessDeniedPath = "/Home/Index";
    options.ClaimsIssuer = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    options.ExpireTimeSpan = TimeSpan.FromHours(2);
});






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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
