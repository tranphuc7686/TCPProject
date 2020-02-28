﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TCPProject.Model;

namespace TCPProject.Migrations
{
    [DbContext(typeof(DataDbContext))]
    [Migration("20190730081312_editlowcase")]
    partial class editlowcase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("CrawlDataTool.Model.Data", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Caption");

                    b.Property<int>("CategoryId");

                    b.Property<int>("IsUser");

                    b.Property<string>("SrcThumbail");

                    b.Property<int>("TypeData");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("datas");
                });

            modelBuilder.Entity("CrawlDataTool.Service.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ApplicationsId");

                    b.Property<string>("Name");

                    b.Property<string>("UrlCategory");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationsId");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("TCPProject.Model.Applications", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("applications");
                });

            modelBuilder.Entity("CrawlDataTool.Model.Data", b =>
                {
                    b.HasOne("CrawlDataTool.Service.Category", "Category")
                        .WithMany("Datas")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CrawlDataTool.Service.Category", b =>
                {
                    b.HasOne("TCPProject.Model.Applications", "Applications")
                        .WithMany("Categories")
                        .HasForeignKey("ApplicationsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
