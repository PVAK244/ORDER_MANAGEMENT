using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataAccess
{
    public partial class OrderManagementContext : DbContext
    {
        public OrderManagementContext()
        {
        }

        public OrderManagementContext(DbContextOptions<OrderManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomerObject> Customers { get; set; }
        public virtual DbSet<OrderObject> Orders { get; set; }
        public virtual DbSet<OrderLineObject> OrderLines { get; set; }
        public virtual DbSet<ProductObject> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server =(local); database = ORDER_MANAGEMENT;uid=sa;pwd=123456;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Vietnamese_CI_AS");

            modelBuilder.Entity<CustomerObject>(entity =>
            {
                entity.ToTable("CUSTOMER");

                entity.Property(e => e.CustomerId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .HasColumnName("CITY");

                entity.Property(e => e.CustomerAddress)
                    .HasMaxLength(50)
                    .HasColumnName("CUSTOMER_ADDRESS");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(50)
                    .HasColumnName("CUSTOMER_NAME");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(10)
                    .HasColumnName("POSTAL_CODE")
                    .IsFixedLength(true);

                entity.Property(e => e.State)
                    .HasMaxLength(10)
                    .HasColumnName("STATE")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<OrderObject>(entity =>
            {
                entity.ToTable("ORDER");

                entity.Property(e => e.OrderId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ORDER_ID");

                entity.Property(e => e.CustomerId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("ORDER_DATE");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CUSTOMER_ORDER");
            });

            modelBuilder.Entity<OrderLineObject>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId });

                entity.ToTable("ORDER_LINE");

                entity.Property(e => e.OrderId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ORDER_ID");

                entity.Property(e => e.ProductId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.OrderedQuantity)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ORDERED_QUANTITY");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderLines)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDER_LINE_ORDER");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderLines)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDER_LINE_PRODUCT");
            });

            modelBuilder.Entity<ProductObject>(entity =>
            {
                entity.ToTable("PRODUCT");

                entity.Property(e => e.ProductId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(50)
                    .HasColumnName("PRODUCT_DESCRIPTION");

                entity.Property(e => e.ProductFinish)
                    .HasMaxLength(50)
                    .HasColumnName("PRODUCT_FINISH");

                entity.Property(e => e.StandardPrice)
                    .HasColumnType("money")
                    .HasColumnName("STANDARD_PRICE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
