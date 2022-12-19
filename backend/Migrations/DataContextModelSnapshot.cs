﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using backend.Data;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("backend.models.Customer", b =>
                {
                    b.Property<Guid>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("DateAdded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DateUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int>("RoleID")
                        .HasColumnType("integer");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CustomerID");

                    b.HasIndex("RoleID");

                    b.ToTable("customer");

                    b.HasData(
                        new
                        {
                            CustomerID = new Guid("6b103cd3-7903-4afd-8077-62ecc7e6a70c"),
                            DateAdded = new DateTimeOffset(new DateTime(2022, 12, 18, 20, 45, 20, 116, DateTimeKind.Unspecified).AddTicks(9677), new TimeSpan(0, 0, 0, 0, 0)),
                            DateUpdated = new DateTimeOffset(new DateTime(2022, 12, 18, 20, 45, 20, 116, DateTimeKind.Unspecified).AddTicks(9681), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "john@mail.com",
                            Name = "John",
                            Password = "doe100",
                            Phone = "555-555-5555",
                            RoleID = 1,
                            Status = true,
                            Surname = "Doe"
                        });
                });

            modelBuilder.Entity("backend.models.models.CustomerCollection", b =>
                {
                    b.Property<Guid>("CCID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CPID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerID")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("DateAdded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DateUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.HasKey("CCID");

                    b.HasIndex("CPID");

                    b.HasIndex("CustomerID");

                    b.ToTable("customer_collection");
                });

            modelBuilder.Entity("backend.models.models.CustomerProduct", b =>
                {
                    b.Property<Guid>("CPID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerID")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("DateAdded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DateUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Products")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Subtotal")
                        .HasColumnType("double precision");

                    b.HasKey("CPID");

                    b.HasIndex("CustomerID");

                    b.ToTable("customer_product");
                });

            modelBuilder.Entity("backend.models.models.Order", b =>
                {
                    b.Property<Guid>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CCID")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("DateAdded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DateUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("boolean");

                    b.Property<int>("ItemCount")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("OrderTotal")
                        .HasColumnType("double precision");

                    b.HasKey("OrderID");

                    b.HasIndex("CCID");

                    b.ToTable("order");
                });

            modelBuilder.Entity("backend.models.models.Product", b =>
                {
                    b.Property<Guid>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("DateAdded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DateUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("InStock")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PDTypeID")
                        .HasColumnType("integer");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.HasKey("ProductID");

                    b.HasIndex("PDTypeID");

                    b.ToTable("product");
                });

            modelBuilder.Entity("backend.models.models.ProductType", b =>
                {
                    b.Property<int>("PDTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PDTypeID"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PDTypeID");

                    b.ToTable("product_type");
                });

            modelBuilder.Entity("backend.models.models.Roles", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoleID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RoleID");

                    b.ToTable("roles");

                    b.HasData(
                        new
                        {
                            RoleID = 1,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("backend.models.Customer", b =>
                {
                    b.HasOne("backend.models.models.Roles", "Roles")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("backend.models.models.CustomerCollection", b =>
                {
                    b.HasOne("backend.models.models.CustomerProduct", "CustomerProduct")
                        .WithMany()
                        .HasForeignKey("CPID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("CustomerProduct");
                });

            modelBuilder.Entity("backend.models.models.CustomerProduct", b =>
                {
                    b.HasOne("backend.models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("backend.models.models.Order", b =>
                {
                    b.HasOne("backend.models.models.CustomerCollection", "CustomerCollection")
                        .WithMany()
                        .HasForeignKey("CCID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerCollection");
                });

            modelBuilder.Entity("backend.models.models.Product", b =>
                {
                    b.HasOne("backend.models.models.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("PDTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductType");
                });
#pragma warning restore 612, 618
        }
    }
}
