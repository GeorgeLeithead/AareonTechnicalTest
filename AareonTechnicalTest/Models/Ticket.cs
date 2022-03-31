namespace AareonTechnicalTest.Models
{
	/// <summary>Ticket POCO</summary>
	public class Ticket
	{
		/// <summary>Gets the Unique Ticket Identifier</summary>
		[Key]
		public int Id { get; }

		/// <summary>Gets or sets the Content of a ticket.</summary>
		public string? Content { get; set; }

		/// <summary>Gets or sets the Person identifier for ticket.</summary>
		public int PersonId { get; set; }
	}
}
