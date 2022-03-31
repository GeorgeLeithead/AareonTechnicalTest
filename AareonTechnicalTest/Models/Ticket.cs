namespace AareonTechnicalTest.Models
{
	/// <summary>Ticket POCO</summary>
	public class Ticket
	{
		/// <summary>Unique Identifier</summary>
		[Key]
		public int Id { get; }

		/// <summary>Content of ticket.</summary>
		public string? Content { get; set; }

		/// <summary>Person identifier for ticket.</summary>
		public int PersonId { get; set; }
	}
}
