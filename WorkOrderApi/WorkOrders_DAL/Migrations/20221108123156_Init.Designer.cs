﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkOrders_DAL.DbContexts;

#nullable disable

namespace WorkOrders_DAL.Migrations
{
    [DbContext(typeof(WorkOrderDbContext))]
    [Migration("20221108123156_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WorkOrders_BAL.Entities.Immutable.WeekdayEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DayOfWeek")
                        .HasMaxLength(7)
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Weekdays");

                    b.HasData(
                        new
                        {
                            Id = new Guid("edbd799f-cbc2-4dd7-af73-9a1214d7d53f"),
                            DayOfWeek = 1,
                            Name = "Mon"
                        },
                        new
                        {
                            Id = new Guid("e3b1fbd3-c6f9-4b35-bc6a-53f05b35414c"),
                            DayOfWeek = 2,
                            Name = "Tue"
                        },
                        new
                        {
                            Id = new Guid("5b5f2880-fd6b-4c33-bbb1-83c7bd56e1cb"),
                            DayOfWeek = 3,
                            Name = "Wed"
                        },
                        new
                        {
                            Id = new Guid("cf4060f2-4ad0-4560-9a2f-77886802c637"),
                            DayOfWeek = 4,
                            Name = "Thu"
                        },
                        new
                        {
                            Id = new Guid("5f325a05-57be-4689-a30e-22705b763571"),
                            DayOfWeek = 5,
                            Name = "Fri"
                        },
                        new
                        {
                            Id = new Guid("3e5a02de-2f3f-4ef0-9355-acb6c8b5a8b1"),
                            DayOfWeek = 6,
                            Name = "Sat"
                        },
                        new
                        {
                            Id = new Guid("f4aae4fd-e33e-436a-9a86-d64612ad9272"),
                            DayOfWeek = 7,
                            Name = "Sun"
                        });
                });

            modelBuilder.Entity("WorkOrders_BAL.Entities.Mutable.DistrictEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Districts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f126e1bb-c067-4a9b-8b34-68ab0e780469"),
                            Name = "East"
                        },
                        new
                        {
                            Id = new Guid("5ec1e23d-40dd-4f84-9e5f-ffe8fecdc4c1"),
                            Name = "West"
                        },
                        new
                        {
                            Id = new Guid("0a447cfb-c6a1-4c27-9519-ab4c808763b4"),
                            Name = "North"
                        },
                        new
                        {
                            Id = new Guid("e0c2ca9f-70b7-4845-ac1e-3ba43724f455"),
                            Name = "South"
                        },
                        new
                        {
                            Id = new Guid("a0f3d9a2-ec2c-49b0-b7bb-71ee41df1c8c"),
                            Name = "Central"
                        },
                        new
                        {
                            Id = new Guid("42972f19-99fe-4b0c-9725-bafa51dbfa82"),
                            Name = "Northeast"
                        },
                        new
                        {
                            Id = new Guid("6fce05e4-e206-42ec-9f17-a74e1be771c6"),
                            Name = "Northwest"
                        },
                        new
                        {
                            Id = new Guid("6ec89009-d74d-4b20-badc-dccbdf8f6231"),
                            Name = "Southeast"
                        },
                        new
                        {
                            Id = new Guid("81047810-bb96-4941-ae93-c6b2b4252a4f"),
                            Name = "Southwest"
                        });
                });

            modelBuilder.Entity("WorkOrders_BAL.Entities.Mutable.LaborRateEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("LbrRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TechsCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TechsCount")
                        .IsUnique();

                    b.ToTable("LaborRates");

                    b.HasData(
                        new
                        {
                            Id = new Guid("dae17957-8354-4e0d-8619-665a7a73e871"),
                            LbrRate = 80m,
                            TechsCount = 1
                        },
                        new
                        {
                            Id = new Guid("d61898b6-e5bd-4295-b370-c8b2c0e17459"),
                            LbrRate = 140m,
                            TechsCount = 2
                        },
                        new
                        {
                            Id = new Guid("b17a2779-169c-4d48-b9e7-8e118345b386"),
                            LbrRate = 195m,
                            TechsCount = 3
                        });
                });

            modelBuilder.Entity("WorkOrders_BAL.Entities.Mutable.LeadtechEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Leadtechs");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8e744b13-68d3-497c-876c-161a8b142e23"),
                            Name = "Burton"
                        },
                        new
                        {
                            Id = new Guid("24d9d5a3-531c-456e-9922-c8f6e2b8a206"),
                            Name = "Cartier"
                        },
                        new
                        {
                            Id = new Guid("c182f8f4-07ec-4e3c-9505-5ff7ea388cfc"),
                            Name = "Khan"
                        },
                        new
                        {
                            Id = new Guid("15b6718a-6c69-499e-a6a5-118fb8fddabf"),
                            Name = "Ling"
                        },
                        new
                        {
                            Id = new Guid("c615542d-b11c-44a6-9c2d-b70118812cf9"),
                            Name = "Lopez"
                        },
                        new
                        {
                            Id = new Guid("c61da18d-8dab-4e80-bb3e-e588bdfbe768"),
                            Name = "Michner"
                        });
                });

            modelBuilder.Entity("WorkOrders_BAL.Entities.Mutable.PaymentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Payments");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d68db0c8-1a1c-4a3a-ba37-c2e1c37895a0"),
                            Name = "Account"
                        },
                        new
                        {
                            Id = new Guid("cfca6b93-c9ba-4a9b-b5fb-eb2158e9d60c"),
                            Name = "C.O.D."
                        },
                        new
                        {
                            Id = new Guid("512f0a14-7a92-422a-8642-81901269e8f7"),
                            Name = "Credit"
                        },
                        new
                        {
                            Id = new Guid("8871bee7-5e34-4a3d-9498-72c1590ed2c5"),
                            Name = "P.O."
                        },
                        new
                        {
                            Id = new Guid("2a7a7db9-3fa8-4fc3-92cf-0d1804d86189"),
                            Name = "Warranty"
                        });
                });

            modelBuilder.Entity("WorkOrders_BAL.Entities.Mutable.ServiceEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Services");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3a3f036b-7753-45cb-9853-d0cc73659f4f"),
                            Name = "Assess"
                        },
                        new
                        {
                            Id = new Guid("f547cb61-be5e-44a5-a86c-9e037cb5d724"),
                            Name = "Deliver"
                        },
                        new
                        {
                            Id = new Guid("bad19515-0e60-491a-a197-0154e0602d38"),
                            Name = "Install"
                        },
                        new
                        {
                            Id = new Guid("2d89d47a-e12b-4ca7-875f-5028159326da"),
                            Name = "Repair"
                        },
                        new
                        {
                            Id = new Guid("13e018f2-e406-4f1c-81ef-4b3b960fc722"),
                            Name = "Replace"
                        });
                });

            modelBuilder.Entity("WorkOrders_BAL.Entities.Mutable.WorkOrderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DistrictId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("LbrHrs")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("LeadtechId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("PartsCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("PaymentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ReqDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Rush")
                        .HasColumnType("bit");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Techs")
                        .HasColumnType("int");

                    b.Property<string>("WO")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("WorkDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("WtyLbr")
                        .HasColumnType("bit");

                    b.Property<bool>("WtyParts")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("WO")
                        .IsUnique();

                    b.ToTable("WorkOrders");
                });
#pragma warning restore 612, 618
        }
    }
}