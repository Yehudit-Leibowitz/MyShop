using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Entity;
namespace Repository;

public partial class ApiDbToCodeContext : DbContext
{
    public ApiDbToCodeContext()
    {
    }

    public ApiDbToCodeContext(DbContextOptions<ApiDbToCodeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Rating> Ratings { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=SRV2\\PUPILS;Database=api_db_to_code;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .HasColumnName("Category_Name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("Order_Date");
            entity.Property(e => e.OrderSum).HasColumnName("Order_Sum");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("WF_User_Id");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("Order_Item");

            entity.Property(e => e.OrderItemId).HasColumnName("Order_Item_Id");
            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_Order_Id");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Product_Id");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ProductName)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("Product_Name");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Category_Id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_User");

            entity.Property(e => e.FirstName)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<Rating>(entity =>
        {
            entity.ToTable("RATING");

            entity.Property(e => e.RatingId).HasColumnName("RATING_ID");

            entity.Property(e => e.Host)
                .HasColumnName("HOST")
                .HasMaxLength(50);

            entity.Property(e => e.Method)
                .HasColumnName("METHOD")
                .HasMaxLength(10)
                .IsFixedLength();

            entity.Property(e => e.Path)
                .HasColumnName("PATH")
                .HasMaxLength(50);

            entity.Property(e => e.RecordDate)
             .HasColumnName("Record_Date")
             .HasColumnType("datetime");

            entity.Property(e => e.Referer)
                .HasColumnName("REFERER")
                .HasMaxLength(100);

            entity.Property(e => e.UserAgent).HasColumnName("USER_AGENT");
            entity.Property(e => e.UserAgent).HasColumnName("USER_ID");

        });
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
