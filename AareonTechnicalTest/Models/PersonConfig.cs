namespace AareonTechnicalTest.Models
{
	/// <summary>Person POCO configuration.</summary>
	public static class PersonConfig
	{
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