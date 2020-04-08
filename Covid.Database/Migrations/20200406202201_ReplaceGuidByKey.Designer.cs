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
    [Migration("20200406202201_ReplaceGuidByKey")]
    partial class ReplaceGuidByKey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Covid.Models.Entities.Alert", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("UserKey")
                        .HasColumnType("text");

                    b.Property<DateTime>("When")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("UserKey");

                    b.ToTable("Alerts");
                });

            modelBuilder.Entity("Covid.Models.Entities.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("UserXKey")
                        .HasColumnType("text");

                    b.Property<string>("UserYKey")
                        .HasColumnType("text");

                    b.Property<DateTime>("When")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("UserXKey");

                    b.HasIndex("UserYKey");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("Covid.Models.Entities.User", b =>
                {
                    b.Property<string>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("PubKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.HasKey("Key");

                    b.HasIndex("PubKey")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Covid.Models.Entities.Alert", b =>
                {
                    b.HasOne("Covid.Models.Entities.User", "User")
                        .WithMany("Alerts")
                        .HasForeignKey("UserKey");
                });

            modelBuilder.Entity("Covid.Models.Entities.Match", b =>
                {
                    b.HasOne("Covid.Models.Entities.User", "UserX")
                        .WithMany()
                        .HasForeignKey("UserXKey");

                    b.HasOne("Covid.Models.Entities.User", "UserY")
                        .WithMany()
                        .HasForeignKey("UserYKey");
                });
#pragma warning restore 612, 618
        }
    }
}