﻿// <auto-generated />
using System;
using CargoHub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CargoHub.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20241122200039_AddItemAutoIncrement")]
    partial class AddItemAutoIncrement
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("CargoHub.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Province")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("contact_email")
                        .HasColumnType("TEXT");

                    b.Property<string>("contact_name")
                        .HasColumnType("TEXT");

                    b.Property<string>("zip_code")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("CargoHub.Models.Contact", b =>
                {
                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Phone");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("CargoHub.Models.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Item_Id")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Item_Reference")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.PrimitiveCollection<string>("Locations")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Total_Allocated")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Total_Available")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Total_Expected")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Total_On_Hand")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Total_Ordered")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Updated_At")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Inventory");
                });

            modelBuilder.Entity("CargoHub.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Commodity_Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Item_Group")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Item_Line")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Item_Type")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Model_Number")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Pack_Order_Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Short_Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Supplier_Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Supplier_Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Supplier_Part_Number")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Uid")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Uid");

                    b.Property<int>("Unit_Order_Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Unit_Purchase_Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Upc_Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Updated_At")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("CargoHub.Models.ItemGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Updated_At")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ItemGroup");
                });

            modelBuilder.Entity("CargoHub.Models.ItemLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Updated_At")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ItemLine");
                });

            modelBuilder.Entity("CargoHub.Models.ItemType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Updated_At")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ItemType");
                });

            modelBuilder.Entity("CargoHub.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Updated_At")
                        .HasColumnType("TEXT");

                    b.Property<int>("Warehouse_Id")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("CargoHub.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Bill_To")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemsJson")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Order_Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Order_Status")
                        .HasColumnType("TEXT");

                    b.Property<string>("Picking_Notes")
                        .HasColumnType("TEXT");

                    b.Property<string>("Reference")
                        .HasColumnType("TEXT");

                    b.Property<string>("Reference_Extra")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Request_Date")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Ship_To")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Shipment_Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Shipping_Notes")
                        .HasColumnType("TEXT");

                    b.Property<int>("Source_Id")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("Total_Amount")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Total_Discount")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Total_Surcharge")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Total_Tax")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Updated_At")
                        .HasColumnType("TEXT");

                    b.Property<int>("Warehouse_Id")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("CargoHub.Models.Shipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Carrier_Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Carrier_Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemsJson")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Order_Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("Order_Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Payment_Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Request_Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Service_Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Shipment_Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Shipment_Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Shipment_Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Source_Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Total_Package_Count")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Total_Package_Weight")
                        .HasColumnType("TEXT");

                    b.Property<string>("Transfer_Mode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Updated_At")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Shipment");
                });

            modelBuilder.Entity("CargoHub.Models.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Address_Extra")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Contact_Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phonenumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Updated_At")
                        .HasColumnType("TEXT");

                    b.Property<string>("Zip_Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("CargoHub.Models.Transfer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemsJson")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Transfer_From")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Transfer_Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Transfer_To")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Updated_At")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Transfer");
                });

            modelBuilder.Entity("CargoHub.Models.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactPhone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Updated_At")
                        .HasColumnType("TEXT");

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ContactPhone");

                    b.ToTable("Warehouse");
                });

            modelBuilder.Entity("CargoHub.Models.Warehouse", b =>
                {
                    b.HasOne("CargoHub.Models.Contact", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactPhone")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");
                });
#pragma warning restore 612, 618
        }
    }
}
