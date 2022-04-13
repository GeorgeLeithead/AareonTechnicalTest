namespace AareonTechnicalTest.Models
{
	/// <summary>Ticket note POC configuration.</summary>
	public class TicketNoteConfig
	{
		/// <summary>Configure Ticket POCO.</summary>
		/// <param name="modelBuilder">Model builder.</param>
		public static void Configure(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<TicketNote>(
				entity =>
				{
					entity.HasKey(e => e.Id);
					entity.HasOne(e => e.Person).WithMany(e => e.TicketNotes).HasForeignKey(e => e.PersonId);
					entity.HasOne(e => e.Ticket).WithMany(e => e.TicketNotes).HasForeignKey(e => e.TicketId);
				});
		}
	}
}