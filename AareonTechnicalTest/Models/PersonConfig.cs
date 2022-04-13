namespace AareonTechnicalTest.Models
{
	/// <summary>Person POCO configuration.</summary>
	public static class PersonConfig
	{
		/// <summary>Configure Person POCO.</summary>
		/// <param name="modelBuilder">Model Builder.</param>
		public static void Configure(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Person>(
				entity =>
				{
					entity.HasKey(e => e.Id);
					entity.HasMany(e => e.Tickets).WithOne(e => e.Person).HasConstraintName("ForeignKey_Person_Tickets");
					entity.HasMany(e => e.TicketNotes).WithOne(e => e.Person).HasConstraintName("ForeignKey_Person_TicketNotes");
				});
		}
	}
}