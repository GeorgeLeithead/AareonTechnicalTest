namespace AareonTechnicalTest.Models
{
	/// <summary>Ticket note POCO interface.</summary>
	public interface ITicketNote
	{
		/// <summary>Gets or the note identifier.</summary>
		int Id { get; }

		/// <summary>Gets or sets the note for a ticket.</summary>
		string? Note { get; set; }

		/// <summary>Gets or sets the ticket object.</summary>
		Ticket? Ticket { get; set; }

		/// <summary>Gets or sets the ticket foreign key identifier.</summary>
		int TicketId { get; set; }


		/// <summary>Gets or sets the person unique identifier who created the note.</summary>
		int PersonId { get; set; }

		/// <summary>Gets or sets the person who created the note.</summary>
		Person? Person { get; set; }
	}
}