﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RocaWebApi.Api;

namespace RocaWebApi.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200614150308_AddWorkerAsTrackableEntityAndWithUser")]
    partial class AddWorkerAsTrackableEntityAndWithUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("RocaWebApi.Api.Features.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnName("address")
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnName("deleted_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("character varying(120)")
                        .HasMaxLength(120);

                    b.Property<string>("Phone")
                        .HasColumnName("phone")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnName("updated_at")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users");
                });

            modelBuilder.Entity("RocaWebApi.Api.Features.Workers.Worker", b =>
                {
                    b.Property<int?>("Id")
                        .HasColumnName("id")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnName("deleted_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnName("updated_at")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id")
                        .HasName("pk_workers");

                    b.ToTable("workers");
                });

            modelBuilder.Entity("RocaWebApi.Api.Features.Workers.Worker", b =>
                {
                    b.HasOne("RocaWebApi.Api.Features.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("Id")
                        .HasConstraintName("fk_workers_users_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
