namespace AareonTechnicalTest.Modules.Persons.EndPoints
{
	/// <summary>Add person.</summary>
	public static class AddPerson
	{
		/// <summary>POST/Add person.</summary>
		/// <param name="person">Person to add.</param>
		/// <param name="personRepository">Person Repository.</param>
		/// <param name="logger">Logger.</param>
		/// <returns>Status 200 Ok.</returns>
		/// <returns>Status 404 Not Found.</returns>
		public static async Task<IResult> Handler(Person person, IPersonsRepository personRepository, ILogger logger)
		{
			logger.LogInformation("[Modules.Persons.AddPerson.Handler] Adding person @{LogTime}", DateTimeOffset.UtcNow);
			Person? newPerson = await personRepository.AddPerson(person);
			if (newPerson == null)
			{
				logger.LogError("[Modules.Persons.AddPerson.Handler] Person not added @{LogTime}", DateTimeOffset.UtcNow);
				return Results.NotFound();
			}
			else
			{
				logger.LogInformation("[Modules.Persons.AddPerson.Handler] Added Person with id:={id} @{LogTime}", newPerson.Id, DateTimeOffset.UtcNow);
				return Results.Created("/person/" + newPerson.Id, newPerson);
			}
		}
	}
}
