namespace AareonTechnicalTest.Models
{
	/// <summary>Ticket model interface.</summary>
	public interface ITicket
	{
		/// <summary>Gets or sets the Content of a ticket.</summary>
		string? Content { get; set; }

		/// <summary>Gets the Unique Ticket Identifier</summary>
		int Id { get; }

		/// <summary>Gets or sets the Person identifier for ticket.</summary>
		int PersonId { get; set; }
	}
}