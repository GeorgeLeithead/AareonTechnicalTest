#nullable disable

namespace AareonTechnicalTest.Migrations
{
	/// <summary>Added Auto History.</summary>
	public partial class AddedAutoHistory : Migration
	{
		/// <summary>Upgrade EF model.</summary>
		/// <param name="migrationBuilder">Migration builder.</param>
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "Content",
				table: "Tickets",
				type: "TEXT",
				nullable: true,
				oldClrType: typeof(string),
				oldType: "TEXT");

			migrationBuilder.CreateTable(
				name: "AutoHistory",
				columns: table => new
				{
					Id = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					RowId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
					TableName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
					Changed = table.Column<string>(type: "TEXT", nullable: true),
					Kind = table.Column<int>(type: "INTEGER", nullable: false),
					Created = table.Column<DateTime>(type: "TEXT", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AutoHistory", x => x.Id);
				});
		}

		/// <summary>Downgrade EF model.</summary>
		/// <param name="migrationBuilder">Migration builder.</param>
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "AutoHistory");

			migrationBuilder.AlterColumn<string>(
				name: "Content",
				table: "Tickets",
				type: "TEXT",
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "TEXT",
				oldNullable: true);
		}
	}
}
