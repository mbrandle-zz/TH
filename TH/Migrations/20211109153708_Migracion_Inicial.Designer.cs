﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TH.Data;

namespace TH.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20211109153708_Migracion_Inicial")]
    partial class Migracion_Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TH.Models.Activity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("creted_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("property_id")
                        .HasColumnType("integer");

                    b.Property<DateTime>("schedule")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("character varying(35)")
                        .HasMaxLength(35);

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("id");

                    b.HasIndex("property_id");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("TH.Models.Property", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("creted_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("disabled_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("character varying(35)")
                        .HasMaxLength(35);

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("id");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("TH.Models.Survey", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("activity_id")
                        .HasColumnType("integer");

                    b.Property<string>("answers")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<DateTime>("creted_at")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("id");

                    b.HasIndex("activity_id");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("TH.Models.Activity", b =>
                {
                    b.HasOne("TH.Models.Property", "Property")
                        .WithMany()
                        .HasForeignKey("property_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TH.Models.Survey", b =>
                {
                    b.HasOne("TH.Models.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("activity_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
