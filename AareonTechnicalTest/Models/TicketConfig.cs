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

			modelBuilder.Entity<TicketNote>(
				entity =>
				{
					entity.HasKey(e => e.Id);
					entity.HasOne(tn => tn.Ticket).WithMany(t => t.Notes).HasForeignKey(tn => tn.TicketId).HasConstraintName("ForeignKey_TicketNote_Ticket");
				});
		}
	}
}