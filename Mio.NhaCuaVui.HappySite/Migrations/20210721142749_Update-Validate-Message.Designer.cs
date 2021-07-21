﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mio.NhaCuaVui.HappySite.Models;

namespace Mio.NhaCuaVui.HappySite.Migrations
{
    [DbContext(typeof(ZDbContext))]
    [Migration("20210721142749_Update-Validate-Message")]
    partial class UpdateValidateMessage
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.Beneficiary", b =>
                {
                    b.Property<int>("BeneficiaryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BeneficiaryTypeId")
                        .HasColumnType("int");

                    b.Property<string>("ContactEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("HadTransportation")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsValidated")
                        .HasColumnType("bit");

                    b.Property<int?>("NeedId")
                        .HasColumnType("int");

                    b.Property<string>("NotValidateMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfPeople")
                        .HasColumnType("int");

                    b.Property<string>("OrganizationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProposetorEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProposetorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProposetorPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ValidatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ValidatedUserId")
                        .HasColumnType("int");

                    b.Property<int?>("WardId")
                        .HasColumnType("int");

                    b.HasKey("BeneficiaryId");

                    b.HasIndex("BeneficiaryTypeId");

                    b.HasIndex("NeedId");

                    b.HasIndex("ValidatedUserId");

                    b.HasIndex("WardId");

                    b.ToTable("Beneficiaries");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.BeneficiaryType", b =>
                {
                    b.Property<int>("BeneficiaryTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BeneficiaryTypeId");

                    b.ToTable("BeneficiaryTypes");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.BenificaryCategoryQuantity", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("BenificaryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "BenificaryId");

                    b.HasIndex("BenificaryId");

                    b.ToTable("BenificaryCategoryQuantity");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NeedId")
                        .HasColumnType("int");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.HasIndex("NeedId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CityId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.District", b =>
                {
                    b.Property<int>("DistrictId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DistrictId");

                    b.HasIndex("CityId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.DonationCategoryQuantity", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("DonatorOrganizationId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "DonatorOrganizationId");

                    b.HasIndex("DonatorOrganizationId");

                    b.ToTable("DonationCategoryQuantity");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.DonatorOrganization", b =>
                {
                    b.Property<int>("DonatorOrganizationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DonatorOrganizationTypeId")
                        .HasColumnType("int");

                    b.Property<bool?>("HadTransportation")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsValidated")
                        .HasColumnType("bit");

                    b.Property<int?>("NeedId")
                        .HasColumnType("int");

                    b.Property<string>("NotValidateMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrganizationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProposetorEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProposetorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProposetorPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ValidatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ValidatedUserId")
                        .HasColumnType("int");

                    b.Property<int?>("WardId")
                        .HasColumnType("int");

                    b.HasKey("DonatorOrganizationId");

                    b.HasIndex("DonatorOrganizationTypeId");

                    b.HasIndex("NeedId");

                    b.HasIndex("ValidatedUserId");

                    b.HasIndex("WardId");

                    b.ToTable("DonatorOrganizations");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.DonatorOrganizationType", b =>
                {
                    b.Property<int>("DonatorOrganizationTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DonatorOrganizationTypeId");

                    b.ToTable("DonatorOrganizationTypes");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.Need", b =>
                {
                    b.Property<int>("NeedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NeedId");

                    b.ToTable("Needs");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid>("UserGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.UserRole", b =>
                {
                    b.Property<int>("UserRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("UserRoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.UserUserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("UserRoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "UserRoleId");

                    b.HasIndex("UserRoleId");

                    b.ToTable("UserUserRole");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.Ward", b =>
                {
                    b.Property<int>("WardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WardId");

                    b.HasIndex("DistrictId");

                    b.ToTable("Wards");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.Beneficiary", b =>
                {
                    b.HasOne("Mio.NhaCuaVui.HappySite.Models.BeneficiaryType", "BeneficiaryType")
                        .WithMany("Beneficiaries")
                        .HasForeignKey("BeneficiaryTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mio.NhaCuaVui.HappySite.Models.Need", "Need")
                        .WithMany()
                        .HasForeignKey("NeedId");

                    b.HasOne("Mio.NhaCuaVui.HappySite.Models.User", "ValidatedUser")
                        .WithMany()
                        .HasForeignKey("ValidatedUserId");

                    b.HasOne("Mio.NhaCuaVui.HappySite.Models.Ward", "Ward")
                        .WithMany()
                        .HasForeignKey("WardId");

                    b.Navigation("BeneficiaryType");

                    b.Navigation("Need");

                    b.Navigation("ValidatedUser");

                    b.Navigation("Ward");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.BenificaryCategoryQuantity", b =>
                {
                    b.HasOne("Mio.NhaCuaVui.HappySite.Models.Beneficiary", "Beneficiary")
                        .WithMany("BenificaryCategoryQuantities")
                        .HasForeignKey("BenificaryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mio.NhaCuaVui.HappySite.Models.Category", "Category")
                        .WithMany("BenificaryCategoryQuantities")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beneficiary");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.Category", b =>
                {
                    b.HasOne("Mio.NhaCuaVui.HappySite.Models.Need", "Need")
                        .WithMany("Categories")
                        .HasForeignKey("NeedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Need");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.District", b =>
                {
                    b.HasOne("Mio.NhaCuaVui.HappySite.Models.City", "City")
                        .WithMany("Districts")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.DonationCategoryQuantity", b =>
                {
                    b.HasOne("Mio.NhaCuaVui.HappySite.Models.Category", "Category")
                        .WithMany("DonationCategoryQuantities")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mio.NhaCuaVui.HappySite.Models.DonatorOrganization", "DonatorOrganization")
                        .WithMany("DonationCategoryQuantities")
                        .HasForeignKey("DonatorOrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("DonatorOrganization");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.DonatorOrganization", b =>
                {
                    b.HasOne("Mio.NhaCuaVui.HappySite.Models.DonatorOrganizationType", "DonatorOrganizationType")
                        .WithMany("DonatorOrganizations")
                        .HasForeignKey("DonatorOrganizationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mio.NhaCuaVui.HappySite.Models.Need", "Need")
                        .WithMany()
                        .HasForeignKey("NeedId");

                    b.HasOne("Mio.NhaCuaVui.HappySite.Models.User", "ValidatedUser")
                        .WithMany()
                        .HasForeignKey("ValidatedUserId");

                    b.HasOne("Mio.NhaCuaVui.HappySite.Models.Ward", "Ward")
                        .WithMany()
                        .HasForeignKey("WardId");

                    b.Navigation("DonatorOrganizationType");

                    b.Navigation("Need");

                    b.Navigation("ValidatedUser");

                    b.Navigation("Ward");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.UserUserRole", b =>
                {
                    b.HasOne("Mio.NhaCuaVui.HappySite.Models.User", "User")
                        .WithMany("UserUserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mio.NhaCuaVui.HappySite.Models.UserRole", "UserRole")
                        .WithMany("UserUserRoles")
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.Ward", b =>
                {
                    b.HasOne("Mio.NhaCuaVui.HappySite.Models.District", "District")
                        .WithMany("Wards")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("District");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.Beneficiary", b =>
                {
                    b.Navigation("BenificaryCategoryQuantities");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.BeneficiaryType", b =>
                {
                    b.Navigation("Beneficiaries");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.Category", b =>
                {
                    b.Navigation("BenificaryCategoryQuantities");

                    b.Navigation("DonationCategoryQuantities");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.City", b =>
                {
                    b.Navigation("Districts");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.District", b =>
                {
                    b.Navigation("Wards");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.DonatorOrganization", b =>
                {
                    b.Navigation("DonationCategoryQuantities");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.DonatorOrganizationType", b =>
                {
                    b.Navigation("DonatorOrganizations");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.Need", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.User", b =>
                {
                    b.Navigation("UserUserRoles");
                });

            modelBuilder.Entity("Mio.NhaCuaVui.HappySite.Models.UserRole", b =>
                {
                    b.Navigation("UserUserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
