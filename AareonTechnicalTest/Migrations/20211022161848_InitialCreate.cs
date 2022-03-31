namespace AareonTechnicalTest.Migrations
{
	/// <summary>Initial create of EF Migration model.</summary>
	public partial class InitialCreate : Migration
	{
		/// <summary>Upgrade EF model.</summary>
		/// <param name="migrationBuilder">Migration builder.</param>
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Persons",
				columns: table => new
				{
					Id = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Forename = table.Column<string>(type: "TEXT", nullable: true),
					Surname = table.Column<string>(type: "TEXT", nullable: true),
					IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Persons", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Tickets",
				columns: table => new
				{
					Id = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Content = table.Column<string>(type: "TEXT", nullable: true),
					PersonId = table.Column<int>(type: "INTEGER", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Tickets", x => x.Id);
				});
		}

		/// <summary>Downgrade EF model.</summary>
		/// <param name="migrationBuilder">Migration builder.</param>
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Persons");

			migrationBuilder.DropTable(
				name: "Tickets");
		}
	}
}
