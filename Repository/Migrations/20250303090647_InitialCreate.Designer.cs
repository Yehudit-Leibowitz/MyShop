﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(ApiDbToCodeContext))]
    [Migration("20250303090647_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entity.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Category_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Category_Name");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Entity.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Order_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime")
                        .HasColumnName("Order_Date");

                    b.Property<int?>("OrderSum")
                        .HasColumnType("int")
                        .HasColumnName("Order_Sum");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("User_Id");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Entity.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Order_Item_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemId"));

                    b.Property<int?>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("Order_Id");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("Product_Id");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("Order_Item", (string)null);
                });

            modelBuilder.Entity("Entity.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Product_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("Category_Id");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("money");

                    b.Property<string>("ProductName")
                        .HasMaxLength(20)
                        .HasColumnType("nchar(20)")
                        .HasColumnName("Product_Name")
                        .IsFixedLength();

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Entity.Rating", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RATING_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RatingId"));

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("HOST");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("METHOD")
                        .IsFixedLength();

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("PATH");

                    b.Property<DateTime?>("RecordDate")
                        .HasColumnType("datetime")
                        .HasColumnName("Record_Date");

                    b.Property<string>("Referer")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("REFERER");

                    b.Property<string>("UserAgent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("USER_AGENT");

                    b.HasKey("RatingId");

                    b.ToTable("RATING", (string)null);
                });

            modelBuilder.Entity("Entity.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("FirstName")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("firstName")
                        .IsFixedLength();

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .IsFixedLength();

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nchar(20)")
                        .IsFixedLength();

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nchar(20)")
                        .IsFixedLength();

                    b.HasKey("UserId")
                        .HasName("PK_User");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Entity.Order", b =>
                {
                    b.HasOne("Entity.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("WF_User_Id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entity.OrderItem", b =>
                {
                    b.HasOne("Entity.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_Order_Id");

                    b.HasOne("Entity.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_Product_Id");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Entity.Product", b =>
                {
                    b.HasOne("Entity.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_Category_Id");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Entity.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Entity.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Entity.Product", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Entity.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
