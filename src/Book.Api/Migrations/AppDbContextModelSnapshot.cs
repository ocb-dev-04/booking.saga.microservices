﻿// <auto-generated />
using System;
using Book.Api.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Book.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Book.Api.Saga.BookingSagaData", b =>
                {
                    b.Property<Guid>("CorrelationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("BookingFinished")
                        .HasColumnType("boolean");

                    b.Property<string>("CarPlateNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CarRegistrationId")
                        .HasColumnType("uuid");

                    b.Property<bool>("CarRented")
                        .HasColumnType("boolean");

                    b.Property<string>("CurrentState")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ErrorMessage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("FailedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("FlightBooked")
                        .HasColumnType("boolean");

                    b.Property<string>("FlightCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FlightFrom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("FlightRegistrationId")
                        .HasColumnType("uuid");

                    b.Property<string>("FlightTo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("HotelBooked")
                        .HasColumnType("boolean");

                    b.Property<string>("HotelName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("HotelRegistrationId")
                        .HasColumnType("uuid");

                    b.Property<bool>("SomeErrorOcurred")
                        .HasColumnType("boolean");

                    b.Property<string>("StackTrace")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("SuccessOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("TravelerId")
                        .HasColumnType("uuid");

                    b.HasKey("CorrelationId");

                    b.HasIndex("CorrelationId", "TravelerId", "SuccessOnUtc", "FailedOnUtc", "SomeErrorOcurred");

                    b.ToTable("BookingSagaData");
                });
#pragma warning restore 612, 618
        }
    }
}
