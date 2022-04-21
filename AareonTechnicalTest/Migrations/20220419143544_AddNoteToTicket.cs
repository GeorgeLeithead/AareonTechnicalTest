namespace AareonTechnicalTest.Migrations
{
	/// <summary>Add note to ticket EF Migration.</summary>
	public partial class AddNoteToTicket : Migration
	{
		/// <summary>Upgrade EF model.</summary>
		/// <param name="migrationBuilder">Migration builder.</param>
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "Content",
				table: "Tickets",
				type: "TEXT",
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "TEXT",
				oldNullable: true);

			migrationBuilder.CreateTable(
				name: "TicketNotes",
				columns: table => new
				{
					Id = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Note = table.Column<string>(type: "TEXT", maxLength: 254, nullable: false),
					PersonId = table.Column<int>(type: "INTEGER", nullable: false),
					TicketId = table.Column<int>(type: "INTEGER", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TicketNotes", x => x.Id);
					table.ForeignKey(
						name: "FK_TicketNotes_Persons_PersonId",
						column: x => x.PersonId,
						principalTable: "Persons",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_TicketNotes_Tickets_TicketId",
						column: x => x.TicketId,
						principalTable: "Tickets",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Tickets_PersonId",
				table: "Tickets",
				column: "PersonId");

			migrationBuilder.CreateIndex(
				name: "IX_TicketNotes_PersonId",
				table: "TicketNotes",
				column: "PersonId");

			migrationBuilder.CreateIndex(
				name: "IX_TicketNotes_TicketId",
				table: "TicketNotes",
				column: "TicketId");

			migrationBuilder.AddForeignKey(
				name: "FK_Tickets_Persons_PersonId",
				table: "Tickets",
				column: "PersonId",
				principalTable: "Persons",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}

		/// <summary>Downgrade EF model.</summary>
		/// <param name="migrationBuilder">Migration builder.</param>
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Tickets_Persons_PersonId",
				table: "Tickets");

			migrationBuilder.DropTable(
				name: "TicketNotes");

			migrationBuilder.DropIndex(
				name: "IX_Tickets_PersonId",
				table: "Tickets");

			migrationBuilder.AlterColumn<string>(
				name: "Content",
				table: "Tickets",
				type: "TEXT",
				nullable: true,
				oldClrType: typeof(string),
				oldType: "TEXT");
		}
	}
}
