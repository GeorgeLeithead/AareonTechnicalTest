namespace AareonTechnicalTest.Models
{
	/// <summary>Ticket POCO</summary>
	public class Ticket : ITicket
	{
		/// <summary>Gets the Unique Ticket Identifier</summary>
		[Key]
		[DisplayName("Ticket Id")]
		[Description("Unique ticket identifier")]
		public int Id { get; }

		/// <summary>Gets or sets the Content of a ticket.</summary>
		[DisplayName("Content of ticket")]
		[Description("The ticket contents")]
		public string? Content { get; set; }

		/// <summary>Gets or sets the Person identifier for ticket.</summary>
		[DisplayName("Person Id")]
		[Description("The person identifier for the ticket")]
		public int PersonId { get; set; }
	}
}
