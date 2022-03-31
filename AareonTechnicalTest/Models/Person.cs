namespace AareonTechnicalTest.Models
{
	/// <summary>Person POCO.</summary>
	public class Person
	{
		/// <summary>Unique Person Identifier.</summary>
		[Key]
		public int Id { get; }

		/// <summary>Forename of person.</summary>
		public string? Forename { get; set; }

		/// <summary>Surname of person.</summary>
		public string? Surname { get; set; }

		/// <summary>Is the person an admin?</summary>
		public bool IsAdmin { get; set; }
	}
}
