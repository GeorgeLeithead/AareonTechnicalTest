namespace AareonTechnicalTest.Models
{
	/// <summary>Person POCO.</summary>
	public class Person
	{
		/// <summary>Gets the Unique Person Identifier.</summary>
		[Key]
		public int Id { get; }

		/// <summary>Gets or sets the Forename of person.</summary>
		public string? Forename { get; set; }

		/// <summary>Gets or sets the Surname of person.</summary>
		public string? Surname { get; set; }

		/// <summary>Is the person an admin?</summary>
		public bool IsAdmin { get; set; }
	}
}
