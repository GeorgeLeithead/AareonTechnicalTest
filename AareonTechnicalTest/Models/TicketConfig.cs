namespace AareonTechnicalTest.Models
{
	/// <summary>Ticket POC configuration.</summary>
	public static class TicketConfig
	{
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