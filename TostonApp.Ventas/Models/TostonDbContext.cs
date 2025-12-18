using Microsoft.EntityFrameworkCore;

namespace TostonApp.Ventas.Models
{
    public class TostonDbContext : DbContext
    {
        public TostonDbContext(DbContextOptions<TostonDbContext> options)
            : base(options)
        {
        }

        // ============================
        // DbSets del módulo de ventas
        // ============================
        public DbSet<Cotizacion> Cotizaciones { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoDetalle> PedidoDetalles { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Devolucion> Devoluciones { get; set; }
        public DbSet<Agenda> Agenda { get; set; }
        public DbSet<Domicilio> Domicilios { get; set; }
        public DbSet<CarritoCompra> CarritoCompra { get; set; }

        // ============================
        // Tablas compartidas
        // ============================
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Insumo> Insumos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Compra> Compras { get; set; }

        // ============================
        // Clientes/Usuarios
        // ============================
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ============================
            // USUARIOS (CLIENTES)
            // ============================
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.HasKey(e => e.ID_Usuario);

                entity.Property(e => e.Cedula)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(e => e.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Apellido)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Correo_Electronico)
                      .HasMaxLength(150);

                entity.Property(e => e.Activo)
                      .HasDefaultValue(true);

                // Índices
                entity.HasIndex(e => e.Cedula).IsUnique();
                entity.HasIndex(e => e.Correo_Electronico);
            });

            // ============================
            // COTIZACION
            // ============================
            modelBuilder.Entity<Cotizacion>(entity =>
            {
                entity.ToTable("Cotizaciones");
                entity.HasKey(e => e.ID_Cotizacion);
            });

            // ============================
            // PEDIDO
            // ============================
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.ToTable("Pedidos");
                entity.HasKey(e => e.ID_Pedido);

                entity.HasIndex(e => e.Fecha);
                entity.HasIndex(e => e.ID_Usuario);

                entity.Property(e => e.Total).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Activo).HasDefaultValue(true);

                // Relación con Usuario
                entity.HasOne(p => p.Usuario)
                      .WithMany(u => u.Pedidos)
                      .HasForeignKey(p => p.ID_Usuario)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================
            // PEDIDO DETALLES (PRODUCTOS DEL PEDIDO)
            // ============================
            modelBuilder.Entity<PedidoDetalle>(entity =>
            {
                entity.ToTable("PedidoDetalles");
                entity.HasKey(e => e.ID_PedidoDetalle);

                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Subtotal).HasColumnType("decimal(18,2)");

                entity.HasOne(d => d.Pedido)
                      .WithMany(p => p.Detalles)
                      .HasForeignKey(d => d.ID_Pedido)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Producto)
                      .WithMany()
                      .HasForeignKey(d => d.ID_Producto)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.ID_Pedido);
                entity.HasIndex(e => e.ID_Producto);
            });

            // ============================
            // VENTAS
            // ============================
            modelBuilder.Entity<Venta>(entity =>
            {
                entity.ToTable("Ventas");
                entity.HasKey(e => e.ID_venta);

                entity.Property(e => e.Total).HasColumnType("decimal(18,2)");

                entity.HasOne(v => v.Pedido)
                      .WithMany()
                      .HasForeignKey(v => v.ID_Pedido)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.ID_Pedido);

                entity.Property(e => e.MetodoPago)
                      .IsRequired()
                      .HasMaxLength(50);
            });

            // ============================
            // DEVOLUCIONES
            // ============================
            modelBuilder.Entity<Devolucion>(entity =>
            {
                entity.ToTable("Devoluciones");
                entity.HasKey(e => e.ID_devolucion);

                entity.HasOne(d => d.Venta)
                      .WithMany()
                      .HasForeignKey(d => d.ID_venta)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.ID_venta);
            });

            // ============================
            // AGENDA
            // ============================
            modelBuilder.Entity<Agenda>(entity =>
            {
                entity.ToTable("Agenda");
                entity.HasKey(e => e.ID_agenda);
            });

            // ============================
            // ENTREGAS / DOMICILIOS
            // ============================
            modelBuilder.Entity<Domicilio>(entity =>
            {
                entity.ToTable("Entregas");
                entity.HasKey(e => e.ID_entrega);

                entity.HasOne(d => d.Venta)
                      .WithMany()
                      .HasForeignKey(d => d.ID_venta)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.ID_venta);
            });

            // ============================
            // CARRITO
            // ============================
            modelBuilder.Entity<CarritoCompra>(entity =>
            {
                entity.ToTable("CarritoCompra");
                entity.HasKey(e => e.ID_Carrito);

                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Subtotal).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Activo).HasDefaultValue(true);

                entity.HasIndex(e => e.ID_Producto);
                entity.HasIndex(e => e.ID_Usuario);
                entity.HasIndex(e => e.Activo);

                // Carrito -> Producto
                entity.HasOne(c => c.Producto)
                      .WithMany()
                      .HasForeignKey(c => c.ID_Producto)
                      .OnDelete(DeleteBehavior.Restrict);

                // Carrito -> Usuario (nullable)
                entity.HasOne(c => c.Usuario)
                      .WithMany()
                      .HasForeignKey(c => c.ID_Usuario)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================
            // PRODUCTOS
            // ============================
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Productos");
                entity.HasKey(e => e.ID_Producto);

                entity.Property(e => e.Precio).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Activo).HasDefaultValue(true);

                entity.HasIndex(e => e.Nombre);
            });

            // ============================
            // RESTO TABLAS COMPARTIDAS (básico)
            // ============================
            modelBuilder.Entity<Insumo>(entity =>
            {
                entity.ToTable("Insumos");
                entity.HasKey(e => e.ID_Insumo);
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.ToTable("Proveedores");
                entity.HasKey(e => e.ID_Proveedor);
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.ToTable("Compras");
                entity.HasKey(e => e.ID_Compra);
            });
        }
    }
}