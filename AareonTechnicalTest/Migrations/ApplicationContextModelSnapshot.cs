﻿// <auto-generated />
using AareonTechnicalTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AareonTechnicalTest.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.3");

            modelBuilder.Entity("AareonTechnicalTest.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Forename")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Surname")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("AareonTechnicalTest.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("AareonTechnicalTest.Models.TicketNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Note")
                        .HasMaxLength(254)
                        .HasColumnType("TEXT");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TicketId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("TicketId");

                    b.ToTable("TicketNotes");
                });

            modelBuilder.Entity("AareonTechnicalTest.Models.TicketNote", b =>
                {
                    b.HasOne("AareonTechnicalTest.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AareonTechnicalTest.Models.Ticket", "Ticket")
                        .WithMany("Notes")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("ForeignKey_TicketNote_Ticket");

                    b.Navigation("Person");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("AareonTechnicalTest.Models.Ticket", b =>
                {
                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}
