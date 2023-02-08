﻿// <auto-generated />
using System;
using KitchenStorage.DataBase.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KitchenStorage.DataBase.Migrations
{
    [DbContext(typeof(KitchenContext))]
    [Migration("20230208105010_Days")]
    partial class Days
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("KitchenStorage.Entities.Day", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<byte>("DayOfWeek")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.ToTable("Day");
                });

            modelBuilder.Entity("KitchenStorage.Entities.DayFood", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DayId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("Meal")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.ToTable("DayFood");
                });

            modelBuilder.Entity("KitchenStorage.Entities.DaysFoods", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DayFoodId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DayId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FoodId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("DayFoodId");

                    b.HasIndex("FoodId");

                    b.ToTable("DaysFoods");
                });

            modelBuilder.Entity("KitchenStorage.Entities.Food", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Type")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("KitchenStorage.Entities.FoodHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FoodId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("Meal")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.ToTable("FoodHistories");
                });

            modelBuilder.Entity("KitchenStorage.Entities.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("KitchenStorage.Entities.Inventory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("AlertLimit")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MeasurementTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("MeasurementTypeId");

                    b.HasIndex("Id", "GroupId", "TypeId");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("KitchenStorage.Entities.InventoryPartition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("InventoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("InventoryId");

                    b.ToTable("InventoryPartitions");
                });

            modelBuilder.Entity("KitchenStorage.Entities.MeasurementType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.ToTable("MeasurementTypes");
                });

            modelBuilder.Entity("KitchenStorage.Entities.Norm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FoodId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InventoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.HasIndex("InventoryId");

                    b.ToTable("Norms");
                });

            modelBuilder.Entity("KitchenStorage.Entities.Note", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Importance")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("KitchenStorage.Entities.TypeConvert", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FromTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("FromValue")
                        .HasColumnType("float");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<Guid>("ToTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("ToValue")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("FromTypeId", "ToTypeId");

                    b.ToTable("TypeConvert");
                });

            modelBuilder.Entity("KitchenStorage.Entities.DayFood", b =>
                {
                    b.HasOne("KitchenStorage.Entities.Day", "Day")
                        .WithMany("DayFood")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Day");
                });

            modelBuilder.Entity("KitchenStorage.Entities.DaysFoods", b =>
                {
                    b.HasOne("KitchenStorage.Entities.DayFood", "DayFood")
                        .WithMany("DaysFoods")
                        .HasForeignKey("DayFoodId");

                    b.HasOne("KitchenStorage.Entities.Food", "Food")
                        .WithMany("DaysFoods")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DayFood");

                    b.Navigation("Food");
                });

            modelBuilder.Entity("KitchenStorage.Entities.FoodHistory", b =>
                {
                    b.HasOne("KitchenStorage.Entities.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");
                });

            modelBuilder.Entity("KitchenStorage.Entities.Inventory", b =>
                {
                    b.HasOne("KitchenStorage.Entities.Group", "Group")
                        .WithMany("Inventories")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KitchenStorage.Entities.MeasurementType", "MeasurementType")
                        .WithMany()
                        .HasForeignKey("MeasurementTypeId");

                    b.Navigation("Group");

                    b.Navigation("MeasurementType");
                });

            modelBuilder.Entity("KitchenStorage.Entities.InventoryPartition", b =>
                {
                    b.HasOne("KitchenStorage.Entities.Inventory", "Inventory")
                        .WithMany("Partitions")
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");
                });

            modelBuilder.Entity("KitchenStorage.Entities.Norm", b =>
                {
                    b.HasOne("KitchenStorage.Entities.Food", "Food")
                        .WithMany("Norms")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KitchenStorage.Entities.Inventory", "Inventory")
                        .WithMany("Norms")
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Inventory");
                });

            modelBuilder.Entity("KitchenStorage.Entities.Day", b =>
                {
                    b.Navigation("DayFood");
                });

            modelBuilder.Entity("KitchenStorage.Entities.DayFood", b =>
                {
                    b.Navigation("DaysFoods");
                });

            modelBuilder.Entity("KitchenStorage.Entities.Food", b =>
                {
                    b.Navigation("DaysFoods");

                    b.Navigation("Norms");
                });

            modelBuilder.Entity("KitchenStorage.Entities.Group", b =>
                {
                    b.Navigation("Inventories");
                });

            modelBuilder.Entity("KitchenStorage.Entities.Inventory", b =>
                {
                    b.Navigation("Norms");

                    b.Navigation("Partitions");
                });
#pragma warning restore 612, 618
        }
    }
}
