﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiTravel.Data;

#nullable disable

namespace WebApiTravel.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230930171935_ChangeNullableFalse")]
    partial class ChangeNullableFalse
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApiTravel.Models.Travel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Occupancy")
                        .HasColumnType("int");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<int>("Sqft")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Travels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenity = "",
                            CreatedDate = new DateTime(2023, 10, 1, 0, 19, 35, 288, DateTimeKind.Local).AddTicks(1206),
                            Details = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                            ImageUrl = "",
                            Name = "Royal Hotel",
                            Occupancy = 5,
                            Rate = 200.0,
                            Sqft = 550,
                            UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Amenity = "",
                            CreatedDate = new DateTime(2023, 10, 1, 0, 19, 35, 288, DateTimeKind.Local).AddTicks(1224),
                            Details = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                            ImageUrl = "",
                            Name = "Diamond Hotel",
                            Occupancy = 5,
                            Rate = 300.0,
                            Sqft = 1100,
                            UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Amenity = "",
                            CreatedDate = new DateTime(2023, 10, 1, 0, 19, 35, 288, DateTimeKind.Local).AddTicks(1228),
                            Details = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                            ImageUrl = "",
                            Name = "Gold Hotel",
                            Occupancy = 5,
                            Rate = 300.0,
                            Sqft = 1050,
                            UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("WebApiTravel.Models.TravelNumber", b =>
                {
                    b.Property<int>("TravelNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SpecialDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TravelId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("TravelNo");

                    b.HasIndex("TravelId");

                    b.ToTable("TravelNumbers");
                });

            modelBuilder.Entity("WebApiTravel.Models.TravelNumber", b =>
                {
                    b.HasOne("WebApiTravel.Models.Travel", "Travel")
                        .WithMany()
                        .HasForeignKey("TravelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Travel");
                });
#pragma warning restore 612, 618
        }
    }
}