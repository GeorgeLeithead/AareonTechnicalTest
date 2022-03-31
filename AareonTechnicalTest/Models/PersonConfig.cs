namespace AareonTechnicalTest.Models
{
	/// <summary>Person POCO configuration.</summary>
	public static class PersonConfig
	{
		/// <summary>Configure Person POCO.</summary>
		/// <param name="modelBuilder">Model Builder.</param>
		public static void Configure(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Ticket>(
				entity =>
				{
					entity.HasKey(e => e.Id);
				});
			modelBuilder.Entity<TicketNote>(
				entity =>
				{
					entity.HasKey(e => e.Id);
					entity.HasOne(e => e.Ticket).WithMany(e => e.Notes).HasForeignKey(e => e.PersonId).HasConstraintName("ForeignKey_TicketNote_Person");
				});
		}
	}
}