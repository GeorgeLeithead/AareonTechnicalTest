namespace AareonTechnicalTest.Models
{
	/// <summary>Ticket model interface.</summary>
	public interface ITicket
	{
		/// <summary>Gets or sets the Content of a ticket.</summary>
		string Content { get; set; }

		/// <summary>Gets the Unique Ticket Identifier</summary>
		int Id { get; set; }

		/// <summary>Gets or sets the Person object for ticket.</summary>
		Person Person { get; set; }

		/// <summary>Gets or sets the person identifier for the ticket.</summary>
		int PersonId { get; set; }

		/// <summary>Gets or sets the collection of notes for the ticket.</summary>
		ICollection<TicketNote> TicketNotes { get; set; }
	}
}