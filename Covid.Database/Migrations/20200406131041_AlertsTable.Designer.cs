﻿// <auto-generated />
using System;
using Covid.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Covid.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200406131041_AlertsTable")]
    partial class AlertsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Covid.Models.Alert", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<Guid?>("UserGuid")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("When")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("UserGuid");

                    b.ToTable("Alerts");
                });

            modelBuilder.Entity("Covid.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<Guid?>("UserXGuid")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserYGuid")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("When")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("UserXGuid");

                    b.HasIndex("UserYGuid");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("Covid.Models.User", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Guid");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Covid.Models.Alert", b =>
                {
                    b.HasOne("Covid.Models.User", "User")
                        .WithMany("Alerts")
                        .HasForeignKey("UserGuid");
                });

            modelBuilder.Entity("Covid.Models.Match", b =>
                {
                    b.HasOne("Covid.Models.User", "UserX")
                        .WithMany()
                        .HasForeignKey("UserXGuid");

                    b.HasOne("Covid.Models.User", "UserY")
                        .WithMany()
                        .HasForeignKey("UserYGuid");
                });
#pragma warning restore 612, 618
        }
    }
}
