namespace AareonTechnicalTest.Modules.Persons.EndPoints
{
	/// <summary>Get all persons endpoint.</summary>
	public static class GetAllPersons
	{
		/// <summary>GET/Get all persons.</summary>
		/// <param name="personRepository">Person Repository.</param>
		/// <param name="logger">Logger.</param>
		/// <returns>Status 200 Ok.</returns>
		/// <returns>Status 404 Not Found.</returns>
		public static async Task<IResult> Handler(IPersonsRepository personRepository, ILogger logger)
		{
			logger.LogInformation("[Modules.Persons.GetAllPersons.Handler] Return All Persons @{LogTime}", DateTimeOffset.UtcNow);
			List<Person>? persons = await personRepository.GetAllPersons();
			if (persons == null)
			{
				logger.LogError("[Modules.Persons.GetAllPersons.Handler] Persons not found @{LogTime}", DateTimeOffset.UtcNow);
				return Results.NotFound();
			}
			else
			{
				logger.LogInformation("[Modules.Persons.GetPersonById.Handler] Returned All Persons @{LogTime}", DateTimeOffset.UtcNow);
				return Results.Ok(persons);
			}
		}
	}
}
