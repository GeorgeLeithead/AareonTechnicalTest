namespace AareonTechnicalTest.Models
{
	/// <summary>Ticket note POCO.</summary>
	public class TicketNote : ITicketNote
	{
		/// <summary>Gets or the note identifier.</summary>
		[Key]
		[DisplayName("Note Id")]
		[Description("Note unique identifier")]
		[JsonIgnore]
		public int Id { get; }

		/// <summary>Gets or sets the note for a ticket.</summary>
		[DisplayName("Note")]
		[Description("Note for a ticket.")]
		[MaxLength(254, ErrorMessage = "A note cannot be longer than 254 characters")]
		[StringLength(254, ErrorMessage = "A note cannot be longer than 254 characters")]
		public string? Note { get; set; }

		/// <summary>Gets or sets the ticket foreign key identifier.</summary>
		[JsonIgnore]
		public int TicketId { get; set; }

		/// <summary>Gets or sets the ticket object.</summary>
		[JsonIgnore]
		public virtual Ticket? Ticket { get; set; }

		/// <summary>Gets or sets the person unique identifier who created the note.</summary>
		public int PersonId { get; set; }

		/// <summary>Gets or sets the person who created the note.</summary>
		[JsonIgnore]
		public virtual Person? Person { get; set; }
	}
}