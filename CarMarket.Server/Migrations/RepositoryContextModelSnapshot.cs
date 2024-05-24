﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarMarket.Server.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CountryId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                            Name = "Belarus"
                        },
                        new
                        {
                            Id = new Guid("80a1bca8-6643-2b20-b5de-024705497d4a"),
                            Name = "Russia"
                        },
                        new
                        {
                            Id = new Guid("80a1bca8-6643-2b20-b5de-024705411d4a"),
                            Name = "USA"
                        },
                        new
                        {
                            Id = new Guid("80a1bca8-6643-2b20-b5de-024115497d4a"),
                            Name = "German"
                        },
                        new
                        {
                            Id = new Guid("80a1bca8-6643-2220-b5de-02c115497d4a"),
                            Name = "Japan"
                        },
                        new
                        {
                            Id = new Guid("80a1bca8-6143-2b11-b5de-024115497d4a"),
                            Name = "China"
                        },
                        new
                        {
                            Id = new Guid("81a1bca8-6143-2b12-b4de-024111197d4b"),
                            Name = "Great Britain"
                        });
                });

            modelBuilder.Entity("Entities.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AddressId");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("House")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("80abb1a8-a64d-4b20-b5de-0a470aa9764a"),
                            City = "Berlin",
                            CountryId = new Guid("80a1bca8-6643-2b20-b5de-024115497d4a"),
                            House = 2,
                            Street = "Karl-Liebknecht-Str"
                        },
                        new
                        {
                            Id = new Guid("10a1bca8-66a3-2b20-b5de-0a4a05497d4a"),
                            City = "Minsk",
                            CountryId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                            House = 12,
                            Street = "Советская"
                        },
                        new
                        {
                            Id = new Guid("7aa1eca8-5643-2b20-b5de-0a7705411d4a"),
                            City = "London",
                            CountryId = new Guid("81a1bca8-6143-2b12-b4de-024111197d4b"),
                            House = 9,
                            Street = "John-Adam-Str"
                        },
                        new
                        {
                            Id = new Guid("20a1bca8-6643-2b20-b5de-02411aa97d4a"),
                            City = "Munich",
                            CountryId = new Guid("80a1bca8-6643-2b20-b5de-024115497d4a"),
                            House = 21,
                            Street = "Alter-Hof-Str"
                        });
                });

            modelBuilder.Entity("Entities.Models.Brand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("BrandId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            Id = new Guid("80abb1a8-a64d-4b20-b5de-0a470aa97d4a"),
                            Name = "BMW"
                        },
                        new
                        {
                            Id = new Guid("a0a1bca8-66a3-2b20-b5de-0a4a05497d4a"),
                            Name = "МАЗ"
                        },
                        new
                        {
                            Id = new Guid("8aa1eca8-5643-2b20-b5de-0a7705411d4a"),
                            Name = "Jaguar"
                        },
                        new
                        {
                            Id = new Guid("80a1bca8-6643-2b20-b5de-02411aa97d4a"),
                            Name = "Volkswagen"
                        },
                        new
                        {
                            Id = new Guid("80a1bca8-6643-2220-b5de-02c115497a4a"),
                            Name = "Porsche"
                        },
                        new
                        {
                            Id = new Guid("80a1bca8-6143-2b11-b5de-024115417d4a"),
                            Name = "Mercedes-Benz"
                        },
                        new
                        {
                            Id = new Guid("81a1bca8-6143-2b12-b4de-022111197d4b"),
                            Name = "Audi"
                        },
                        new
                        {
                            Id = new Guid("81a1bca8-6143-2b12-b4de-0221aa197aab"),
                            Name = "Haval"
                        },
                        new
                        {
                            Id = new Guid("82a1bca8-6143-2332-b4de-0221aa197aab"),
                            Name = "Toyota"
                        });
                });

            modelBuilder.Entity("Entities.Models.Car", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CarId");

                    b.Property<Guid>("BrandId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CarShopId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CarcaseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CarShopId");

                    b.HasIndex("CarcaseId");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Id = new Guid("80abb123-a23d-3b20-b5de-0a570aa9764a"),
                            BrandId = new Guid("80abb1a8-a64d-4b20-b5de-0a470aa97d4a"),
                            CarShopId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                            CarcaseId = new Guid("80a1bca8-6143-2b11-b5de-024ee5497d4a"),
                            Name = "BMW 2 seria F44",
                            Price = 27999.0,
                            Year = "2020"
                        },
                        new
                        {
                            Id = new Guid("80abb123-a23d-4b20-b5de-0a5701a9764a"),
                            BrandId = new Guid("80abb1a8-a64d-4b20-b5de-0a470aa97d4a"),
                            CarShopId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                            CarcaseId = new Guid("801b1ca8-664d-1b20-b5de-024705497d4a"),
                            Name = "BMW X6 F16",
                            Price = 39900.0,
                            Year = "2015"
                        },
                        new
                        {
                            Id = new Guid("80abb123-a23d-4120-b5de-0a570aa9764a"),
                            BrandId = new Guid("80a1bca8-6143-2b11-b5de-024115417d4a"),
                            CarShopId = new Guid("3d490a70-94c2-4d15-9494-5248280c2ce3"),
                            CarcaseId = new Guid("a1a1bca8-6143-2b13-14de-024131ee7d4b"),
                            Name = "Mercedes-Benz CLS C218, X218",
                            Price = 27400.0,
                            Year = "2015"
                        },
                        new
                        {
                            Id = new Guid("81abb123-a23d-4b20-b5de-0a570aa9764a"),
                            BrandId = new Guid("80a1bca8-6143-2b11-b5de-024115417d4a"),
                            CarShopId = new Guid("3d490a70-94c2-4d15-9494-5248280c2ce3"),
                            CarcaseId = new Guid("80a1bca8-6643-2b20-b5de-02470541124a"),
                            Name = "Mercedes-Benz E-Класс W212, S212, C207, A207",
                            Price = 19999.0,
                            Year = "2013"
                        });
                });

            modelBuilder.Entity("Entities.Models.CarShop", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CarShopId");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("CarShops");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                            AddressId = new Guid("10a1bca8-66a3-2b20-b5de-0a4a05497d4a"),
                            Name = "AutoShop Ltd",
                            Phone = "+375296578123"
                        },
                        new
                        {
                            Id = new Guid("3d490a70-94c2-4d15-9494-5248280c2ce3"),
                            AddressId = new Guid("80abb1a8-a64d-4b20-b5de-0a470aa9764a"),
                            Name = "CarShop Ltd",
                            Phone = "+49446578123"
                        });
                });

            modelBuilder.Entity("Entities.Models.Carcase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CarcaseId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Carcases");

                    b.HasData(
                        new
                        {
                            Id = new Guid("801b1ca8-664d-1b20-b5de-024705497d4a"),
                            Name = "SUV"
                        },
                        new
                        {
                            Id = new Guid("80a1bca8-6643-2b20-b5de-024705417d4a"),
                            Name = "Cabriolet"
                        },
                        new
                        {
                            Id = new Guid("80a1bca8-6643-2b20-b5de-02470541124a"),
                            Name = "Coupe"
                        },
                        new
                        {
                            Id = new Guid("83a1bca8-6643-2b20-b5de-024115497d4a"),
                            Name = "Passenger Van"
                        },
                        new
                        {
                            Id = new Guid("80a14ca8-6643-2220-b5da-02c115e97d4a"),
                            Name = "Limousine"
                        },
                        new
                        {
                            Id = new Guid("80a1bca8-6143-2b11-b5de-024ee5497d4a"),
                            Name = "Sedan"
                        },
                        new
                        {
                            Id = new Guid("a1a1bca8-6143-2b13-14de-024131ee7d4b"),
                            Name = "Station Wagon"
                        });
                });

            modelBuilder.Entity("Entities.Models.Address", b =>
                {
                    b.HasOne("Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Entities.Models.Car", b =>
                {
                    b.HasOne("Entities.Models.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.CarShop", "CarShop")
                        .WithMany("Cars")
                        .HasForeignKey("CarShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Carcase", "Carcase")
                        .WithMany()
                        .HasForeignKey("CarcaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("CarShop");

                    b.Navigation("Carcase");
                });

            modelBuilder.Entity("Entities.Models.CarShop", b =>
                {
                    b.HasOne("Entities.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Entities.Models.CarShop", b =>
                {
                    b.Navigation("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}
