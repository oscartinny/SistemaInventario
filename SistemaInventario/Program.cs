using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using SistemaInventario.DAO.Data;
using SistemaInventario.DAO.Repositorio;
using SistemaInventario.DAO.Repositorio.IRepositorio;
using SistemaInventario.Utilidades;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
/*
 * Al Inicializar una proyecto viene por default AddDefaultIdentity, lo cual no permite manejar roles de usuario
 * Se deberá de cambiar por AddIdentity<IdentityUser, IdentityRole>
 */
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//AddScope permite una instacia de servicio que se crea una sola vez 
//y puede seguirse usando las veces que sea necesaria.
//Se creo el servicio para unidad de trabajo utilizando la interface IUnidadTrabajo y su clase de implementaci�n 
//Ahora ya puede ocuparse en todos los controllers
//Para que un servicio sea ocupado en alguna clase o controller se deber� de agregar en el contructor
//para poder ser inicializada y utilizada.
//Se registra el servicio IUnidadTrabajo con el tipo concreto UnidadTrabajo.
builder.Services.AddScoped<IUnidadTrabajo, UnidadTrabajo>(); 

builder.Services.AddRazorPages();

//Se agrega el servicio para envio de correos
builder.Services.AddSingleton<IEmailSender, EmailSender>();

//Todos los servicios se deber�n agregar antes de builder.Build
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    pattern: "{area=Inventario}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
