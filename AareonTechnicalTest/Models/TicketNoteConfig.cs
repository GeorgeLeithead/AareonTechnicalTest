namespace AareonTechnicalTest.Models
{
	/// <summary>Ticket note POC configuration.</summary>
	public class TicketNoteConfig
	{
		/// <summary>Configure Ticket POCO.</summary>
		/// <param name="modelBuilder">Model builder.</param>
		public static void Configure(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Ticket>(
				entity =>
				{
					entity.HasKey(e => e.Id);
				});
		}

	}
}
