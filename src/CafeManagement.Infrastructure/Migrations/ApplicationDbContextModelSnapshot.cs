﻿// <auto-generated />
using System;
using CafeManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CafeManagement.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CafeManagement.Infrastructure.Entities.Cafe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Logo")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Cafes");
                });

            modelBuilder.Entity("CafeManagement.Infrastructure.Entities.Employee", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(9)
                        .HasColumnType("character varying(9)");

                    b.Property<Guid?>("CafeId")
                        .HasColumnType("uuid");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CafeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("CafeManagement.Infrastructure.Entities.Employee", b =>
                {
                    b.HasOne("CafeManagement.Infrastructure.Entities.Cafe", "Cafe")
                        .WithMany("Employees")
                        .HasForeignKey("CafeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Cafe");
                });

            modelBuilder.Entity("CafeManagement.Infrastructure.Entities.Cafe", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
