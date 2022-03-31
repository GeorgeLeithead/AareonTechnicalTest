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
		}
	}
}