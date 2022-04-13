namespace AareonTechnicalTest.Models
{
	/// <summary>Ticket note POCO.</summary>
	public class TicketNote : ITicketNote
	{
		/// <summary>Gets or the note identifier.</summary>
		[Key]
		[DisplayName("Note Id")]
		[Description("Note unique identifier")]
		public int Id { get; set; }

		/// <summary>Gets or sets the note for a ticket.</summary>
		[DisplayName("Note")]
		[Description("Note for a ticket.")]
		[MaxLength(254, ErrorMessage = "A note cannot be longer than 254 characters")]
		[StringLength(254, ErrorMessage = "A note cannot be longer than 254 characters")]
		[MinLength(1, ErrorMessage = "A note cannot be shorter than 1 character")]
		[Required]
		public string Note { get; set; }

		/// <summary>Gets or sets the person who created the note.</summary>
		[JsonIgnore]
		public Person Person { get; set; }

		/// <summary>Gets or sets the person identifier for the ticket note.</summary>
		public int PersonId { get; set; }

		/// <summary>Gets or sets the ticket object.</summary>
		[JsonIgnore]
		public Ticket Ticket { get; set; }

		/// <summary>Gets or sets the ticket identifier for the ticket note.</summary>
		public int TicketId { get; set; }
	}
}