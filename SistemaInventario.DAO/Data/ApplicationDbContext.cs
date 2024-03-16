using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SistemaInventario.Modelos;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SistemaInventario.DAO.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Se crea la propiedad en base a la clase modelo, esta propiedad será la que sea exportada a una tabla en la BD
        //El nombre que le demos a la propiedad será el que tenga la tabla
        //Para crear una nueva migración deberemos entrar a la Consola administrador de 
        //paquetes y ejecutar los siguientes comando dentro del proyecto en donde se encuentr el DBContext
        //add-migration [nombreMigracion] - comando para ejecutar una migracion para agregar nuevas configuraciones del modelo de DB.
        //update-database - Comando para ejecutar los archivos de migración que se encuentren pendientes de ejecutar

        public DbSet<Bodega> Bodegas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Marca> Marcas { get; set; }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }


        //Metodo sobre escrito para que la aplicación tome las configuraciones de los modelos desde las clases que hereden o implementen IEntityTypeConfiguration
        //Ver SistemaInventario.DAO.Configuracion
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}