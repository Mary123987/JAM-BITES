using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JAM_BITES.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<JAM_BITES.Models.Contacto> DataContacto { get; set; }
    public DbSet<JAM_BITES.Models.Cuenta> DataCuenta { get; set; }
    public DbSet<JAM_BITES.Models.Usuario> DataUsuario { get; set; }
    public DbSet<JAM_BITES.Models.Categoria> DataCategoria { get; set; }
    public DbSet<JAM_BITES.Models.Producto> DataProducto { get; set; }
    public DbSet<JAM_BITES.Models.Carrito> DataCarrito { get; set; }
    public DbSet<JAM_BITES.Models.Pago> DataPago { get; set; }
    public DbSet<JAM_BITES.Models.Pedido> DataPedido { get; set; }
    public DbSet<JAM_BITES.Models.DetallePedido> DataDetallePedido { get; set; }

}
