namespace AareonTechnicalTest.Modules.Persons.EndPoints
{
	/// <summary>PUT/Update person endpoint.</summary>
	public static class UpdatePerson
	{
		/// <summary>PUT/Update a person.</summary>
		/// <param name="id">Person identifier.</param>
		/// <param name="person">Person object.</param>
		/// <param name="personRepository">Person Repository.</param>
		/// <param name="logger">Logger.</param>
		/// <returns>Status 200 Ok.</returns>
		/// <returns>Status 404 Not Found.</returns>
		public static async Task<IResult> Handler(int id, Person person, IPersonsRepository personRepository, ILogger logger)
		{
			logger.LogInformation("[Modules.Persons.UpdatePerson.Handler] Update Person for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
			Person? thisPerson = await personRepository.GetPersonByIdAsync(id);
			if (thisPerson == null)
			{
				logger.LogError("[Modules.Persons.UpdatePerson.Handler] Person not found for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
				return Results.NotFound();
			}

			try
			{
				thisPerson.Forename = person.Forename;
				thisPerson.IsAdmin = person.IsAdmin;
				thisPerson.Surname = person.Surname;
				await personRepository.PutPerson(thisPerson);
			}
			catch (DbUpdateConcurrencyException ex)
			{
				logger.LogError("[Modules.Persons.UpdatePerson.Handler] Error Updating Person for id:={id} @{LogTime}. Error:= {ex}", id, DateTimeOffset.UtcNow, ex);
				if (!await personRepository.PersonExistsAsync(id))
				{
					logger.LogError("[Modules.Persons.UpdatePerson.Handler] Person not found for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
					return Results.NotFound();
				}

				throw;
			}

			logger.LogInformation("[Modules.Persons.UpdatePerson.Handler] Updated Person for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
			return Results.Ok(thisPerson);
		}
	}
}
