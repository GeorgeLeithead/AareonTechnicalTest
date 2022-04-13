namespace AareonTechnicalTest.Modules.Notes.EndPoints
{
	/// <summary>Read ticket notes endpoint.</summary>
	public static class Read
	{
		/// <summary>GET/Read all ticket notes.</summary>
		/// <param name="noteRepository">Note Repository.</param>
		/// <param name="logger">Logger.</param>
		/// <returns>Status 200 Ok.</returns>
		/// <returns>Status 404 Not Found.</returns>
		public static async Task<IResult> HandlerAll(INotesRepository noteRepository, ILogger logger)
		{
			logger.LogInformation("[Modules.Notes.Read.HandlerAll] Query All Notes @{LogTime}", DateTimeOffset.UtcNow);
			List<TicketNote>? notes = await noteRepository.Read();
			if (notes == null)
			{
				logger.LogError("[Modules.Notes.Read.HandlerAll] Notes not found @{LogTime}", DateTimeOffset.UtcNow);
				return Results.NotFound();
			}
			else
			{
				logger.LogInformation("[Modules.Notes.Read.HandlerAll] Queried All Notes @{LogTime}", DateTimeOffset.UtcNow);
				return Results.Ok(notes);
			}
		}

		/// <summary>GET/Read note by unique identifier.</summary>
		/// <param name="id">Note identifier.</param>
		/// <param name="noteRepository">Note Repository.</param>
		/// <param name="logger">Logger.</param>
		/// <returns>Status 200 Ok.</returns>
		/// <returns>Status 404 Not Found.</returns>
		public static async Task<IResult> HandlerById(int id, INotesRepository noteRepository, ILogger logger)
		{
			logger.LogInformation("[Modules.Notes.Read.HandlerById] Query Note for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
			TicketNote? note = await noteRepository.ReadByIdAsync(id);
			if (note == null)
			{
				logger.LogError("[Modules.Notes.Read.HandlerById] Note not found for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
				return Results.NotFound();
			}
			else
			{
				logger.LogInformation("[Modules.Notes.Read.HandlerById] Queried Note for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
				return Results.Ok(note);
			}
		}

	}
}
