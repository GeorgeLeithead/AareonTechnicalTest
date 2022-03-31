﻿namespace AareonTechnicalTest.Models
{
	/// <summary>Person POCO.</summary>
	public class Person
	{
		/// <summary>Gets the Unique Person Identifier.</summary>
		[Key]
		[DisplayName("Person ID")]
		[Description("Unique person identifier")]
		public int Id { get; }

		/// <summary>Gets or sets the Forename of person.</summary>
		[DisplayName("Person Forename")]
		[Description("The persons forename (first name)")]
		public string? Forename { get; set; }

		/// <summary>Gets or sets the Surname of person.</summary>
		[DisplayName("Person Surname")]
		[Description("The persons surname (family name)")]
		public string? Surname { get; set; }

		/// <summary>Gets or sets a value indicating whether the person is an admin.</summary>
		[DisplayName("Is Admin?")]
		[Description("true if the person is an administrator; otherwise false")]
		public bool IsAdmin { get; set; }
	}
}
