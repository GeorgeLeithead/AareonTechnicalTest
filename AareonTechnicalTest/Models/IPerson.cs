namespace AareonTechnicalTest.Models
{
	/// <summary>Person model interface.</summary>
	public interface IPerson
	{
		/// <summary>Gets or sets the Forename of person.</summary>
		string? Forename { get; set; }

		/// <summary>Gets the Unique Person Identifier.</summary>
		int Id { get; }

		/// <summary>Gets or sets a value indicating whether the person is an admin.</summary>
		bool IsAdmin { get; set; }

		/// <summary>Gets or sets the Surname of person.</summary>
		string? Surname { get; set; }
	}
}