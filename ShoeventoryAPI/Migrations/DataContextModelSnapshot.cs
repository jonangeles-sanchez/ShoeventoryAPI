﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoeventoryAPI.Data;

#nullable disable

namespace ShoeventoryAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShoeventoryAPI.Models.Merchant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MerchantName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Merchants");
                });

            modelBuilder.Entity("ShoeventoryAPI.Models.Shoe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShoeCollectionId")
                        .HasColumnType("int");

                    b.Property<string>("ShoeColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShoeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ShoePrice")
                        .HasColumnType("float");

                    b.Property<int>("ShoeQuantity")
                        .HasColumnType("int");

                    b.Property<double>("ShoeSize")
                        .HasColumnType("float");

                    b.Property<string>("ShoeType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ShoeCollectionId");

                    b.ToTable("Shoes");
                });

            modelBuilder.Entity("ShoeventoryAPI.Models.ShoeCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MerchantId")
                        .HasColumnType("int");

                    b.Property<string>("ShoeCollectionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MerchantId");

                    b.ToTable("ShoeCollections");
                });

            modelBuilder.Entity("ShoeventoryAPI.Models.Shoe", b =>
                {
                    b.HasOne("ShoeventoryAPI.Models.ShoeCollection", "ShoeCollection")
                        .WithMany("Shoes")
                        .HasForeignKey("ShoeCollectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShoeCollection");
                });

            modelBuilder.Entity("ShoeventoryAPI.Models.ShoeCollection", b =>
                {
                    b.HasOne("ShoeventoryAPI.Models.Merchant", "Merchant")
                        .WithMany("ShoeCollections")
                        .HasForeignKey("MerchantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Merchant");
                });

            modelBuilder.Entity("ShoeventoryAPI.Models.Merchant", b =>
                {
                    b.Navigation("ShoeCollections");
                });

            modelBuilder.Entity("ShoeventoryAPI.Models.ShoeCollection", b =>
                {
                    b.Navigation("Shoes");
                });
#pragma warning restore 612, 618
        }
    }
}
