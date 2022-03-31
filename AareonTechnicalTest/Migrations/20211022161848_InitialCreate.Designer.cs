﻿// <auto-generated />
namespace AareonTechnicalTest.Migrations
{
	[DbContext(typeof(ApplicationContext))]
	[Migration("20211022161848_InitialCreate")]
	partial class InitialCreate
	{

		/// <summary>EF built target model.</summary>
		/// <param name="modelBuilder">Model builder.</param>
		protected override void BuildTargetModel(ModelBuilder modelBuilder)
		{
#pragma warning disable 612, 618
			modelBuilder
				.HasAnnotation("ProductVersion", "5.0.11");

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
#pragma warning restore 612, 618
		}
	}
}