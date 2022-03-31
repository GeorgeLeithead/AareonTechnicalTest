namespace AareonTechnicalTest.Models
{
	/// <summary>Ticket POCO</summary>
	public class Ticket : ITicket
	{
		/// <summary>Gets or sets the Content of a ticket.</summary>
		[DisplayName("Content of ticket")]
		[Description("The ticket contents")]
		public string? Content { get; set; }

		/// <summary>Gets the Unique Ticket Identifier</summary>
		[Key]
		[DisplayName("Ticket Id")]
		[Description("Unique ticket identifier")]
		public int Id { get; }

		/// <summary>Gets or sets the collection of notes for the ticket.</summary>
		[DisplayName("Ticket Notes")]
		[Description("Notes taken against the ticket")]
		public virtual ICollection<TicketNote>? Notes { get; set; }

		/// <summary>Gets or sets the Person identifier for ticket.</summary>
		[DisplayName("Person Id")]
		[Description("The person identifier for the ticket")]
		public int PersonId { get; set; }
	}
}