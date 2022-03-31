namespace AareonTechnicalTest.Models
{
	/// <summary>Ticket POC configuration.</summary>
	public static class TicketConfig
	{
		/// <summary>Configure Ticket POCO.</summary>
		/// <param name="modelBuilder">Model builder.</param>
		public static void Configure(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Person>(
				entity =>
				{
					entity.HasKey(e => e.Id);
				});
		}
	}
}