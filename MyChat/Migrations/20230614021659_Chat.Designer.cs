﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyChat.Context;

#nullable disable

namespace MyChat.Migrations
{
    [DbContext(typeof(MyChatContext))]
    [Migration("20230614021659_Chat")]
    partial class Chat
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MyChat.Models.MessageTables", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoomID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("RoomID");

                    b.HasIndex("UserID");

                    b.ToTable("MessageTables");
                });

            modelBuilder.Entity("MyChat.Models.Rooms", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("RoomName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("MyChat.Models.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("RoomsUser", b =>
                {
                    b.Property<Guid>("RoomsID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsersID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoomsID", "UsersID");

                    b.HasIndex("UsersID");

                    b.ToTable("RoomsUser");
                });

            modelBuilder.Entity("MyChat.Models.MessageTables", b =>
                {
                    b.HasOne("MyChat.Models.Rooms", "Rooms")
                        .WithMany("MessageTables")
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyChat.Models.User", "Users")
                        .WithMany("MessageTables")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rooms");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("RoomsUser", b =>
                {
                    b.HasOne("MyChat.Models.Rooms", null)
                        .WithMany()
                        .HasForeignKey("RoomsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyChat.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyChat.Models.Rooms", b =>
                {
                    b.Navigation("MessageTables");
                });

            modelBuilder.Entity("MyChat.Models.User", b =>
                {
                    b.Navigation("MessageTables");
                });
#pragma warning restore 612, 618
        }
    }
}
