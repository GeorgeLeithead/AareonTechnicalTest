namespace AareonTechnicalTest.Models
{
	/// <summary>Ticket POC configuration.</summary>
	public static class TicketConfig
	{
		/// <summary>Configure Ticket POCO.</summary>
		/// <param name="modelBuilder">Model builder.</param>
		public static void Configure(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Ticket>(
				entity =>
				{
					entity.HasKey(e => e.Id);
					entity.HasOne(e => e.Person).WithMany(e => e.Tickets).HasForeignKey(e => e.PersonId);
				});
		}
	}
}