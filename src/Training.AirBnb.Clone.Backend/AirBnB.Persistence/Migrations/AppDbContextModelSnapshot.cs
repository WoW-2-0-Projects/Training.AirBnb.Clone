﻿// <auto-generated />
using System;
using AirBnB.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AirBnB.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AirBnB.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EmailAddress")
                        .IsUnique();

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
