﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NTU_IOT.DataAccess;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NTU_IOT.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20230706163141_seedDevicetypes")]
    partial class seedDevicetypes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("NTU_IoT.Models.DeviceType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("table_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("topic_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("device_types");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5c04a400-eaab-4530-ade5-7a8dd9527d24"),
                            name = "Physio",
                            table_name = "physio",
                            topic_name = "Physio"
                        },
                        new
                        {
                            Id = new Guid("35cadad3-d405-43a2-a706-389bb94cd6ad"),
                            name = "Environmental",
                            table_name = "env",
                            topic_name = "Env"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
