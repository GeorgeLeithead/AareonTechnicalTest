namespace AareonTechnicalTest.Modules.Persons.EndPoints
{
	/// <summary>Get Person By Id endpoint.</summary>
	public static class GetPersonById
	{
		/// <summary>GET/Get person by unique identifier.</summary>
		/// <param name="id">Person identifier.</param>
		/// <param name="personRepository">Person Repository.</param>
		/// <param name="logger">Logger.</param>
		/// <returns>Status 200 Ok.</returns>
		/// <returns>Status 404 Not Found.</returns>
		public static async Task<IResult> Handler(int id, IPersonsRepository personRepository, ILogger logger)
		{
			logger.LogInformation("[Modules.Persons.GetPersonById.Handler] Return Person for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
			Person? person = await personRepository.GetPersonByIdAsync(id);
			if (person == null)
			{
				logger.LogError("[Modules.Persons.GetPersonById.Handler] Person not found for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
				return Results.NotFound();
			}
			else
			{
				logger.LogInformation("[Modules.Persons.GetPersonById.Handler] Returned Person for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
				return Results.Ok(person);
			}
		}
	}
}
