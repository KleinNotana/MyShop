using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entity;

public partial class MyShopDbContext : DbContext
{
    public string connectionString { get; set; }
    public MyShopDbContext(string con)
    {
        connectionString = con;
    }

    

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order1> Order1s { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CATEGORY__3214EC27AA607E34");

            entity.ToTable("CATEGORY");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CUSTOMER__3214EC27801F38D5");

            entity.ToTable("CUSTOMER");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Age).HasColumnName("AGE");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(30)
                .HasColumnName("CUSTOMER_NAME");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(30)
                .HasColumnName("PHONE_NUMBER");
        });

        modelBuilder.Entity<Order1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ORDER1__3214EC27CF7A3DD1");

            entity.ToTable("ORDER1");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CustomerId).HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.OrderDate)
                .HasColumnType("date")
                .HasColumnName("ORDER_DATE");

            entity.HasOne(d => d.Customer).WithMany(p => p.Order1s)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_ORDER1_CUSTOMER");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId }).HasName("PK__ORDER_DE__6321D5126DB4B103");

            entity.ToTable("ORDER_DETAIL");

            entity.Property(e => e.OrderId).HasColumnName("ORDER_ID");
            entity.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");
            entity.Property(e => e.Amount).HasColumnName("AMOUNT");
            entity.Property(e => e.Price).HasColumnName("PRICE");
            entity.Property(e => e.TotalPrice).HasColumnName("TOTAL_PRICE");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORDER_DETAIL_ORDER1");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetailProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORDER_DETAIL_PRODUCT");

            entity.HasOne(d => d.Pr).WithMany(p => p.OrderDetailPrs)
                .HasPrincipalKey(p => new { p.Id, p.Price })
                .HasForeignKey(d => new { d.ProductId, d.Price })
                .HasConstraintName("FK_ORDER_DETAIL_PRODUCTPRICE");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PRODUCT__3214EC27976716B6");

            entity.ToTable("PRODUCT");

            entity.HasIndex(e => new { e.Id, e.Price }, "CK_PRODUCT").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Amount).HasColumnName("AMOUNT");
            entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.ImgPath)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("IMG_PATH");
            entity.Property(e => e.Price)
                .IsRequired()
                .HasColumnName("PRICE");
            entity.Property(e => e.ProductName)
                .HasMaxLength(30)
                .HasColumnName("PRODUCT_NAME");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_PRODUCT_CATEGORY");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
