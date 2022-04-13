namespace AareonTechnicalTest.Modules.Tickets.EndPoints
{
	/// <summary>Real tickets endpoint.</summary>
	public static class Read
	{
		/// <summary>GET/Read all tickets.</summary>
		/// <param name="ticketRepository">Ticket Repository.</param>
		/// <param name="logger">Logger.</param>
		/// <returns>Status 200 Ok.</returns>
		/// <returns>Status 404 Not Found.</returns>
		public static async Task<IResult> HandlerAll(ITicketsRepository ticketRepository, ILogger logger)
		{
			logger.LogInformation("[Modules.Tickets.Read.HandlerAll] Return All Tickets @{LogTime}", DateTimeOffset.UtcNow);
			List<Ticket>? tickets = await ticketRepository.Read();
			if (tickets == null)
			{
				logger.LogError("[Modules.Tickets.Read.HandlerAll] Tickets not found @{LogTime}", DateTimeOffset.UtcNow);
				return Results.NotFound();
			}
			else
			{
				logger.LogInformation("[Modules.Tickets.Read.HandlerAll] Returned All Tickets @{LogTime}", DateTimeOffset.UtcNow);
				return Results.Ok(tickets);
			}
		}

		/// <summary>GET/Read ticket by unique identifier.</summary>
		/// <param name="id">Ticket identifier.</param>
		/// <param name="ticketRepository">Ticket Repository.</param>
		/// <param name="logger">Logger.</param>
		/// <returns>Status 200 Ok.</returns>
		/// <returns>Status 404 Not Found.</returns>
		public static async Task<IResult> HandlerById(int id, ITicketsRepository ticketRepository, ILogger logger)
		{
			logger.LogInformation("[Modules.Tickets.Read.HandlerById] Return Ticket for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
			Ticket? ticket = await ticketRepository.ReadByIdAsync(id);
			if (ticket == null)
			{
				logger.LogError("[Modules.Tickets.Read.HandlerById] Ticket not found for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
				return Results.NotFound();
			}
			else
			{
				logger.LogInformation("[Modules.Tickets.Read.HandlerById] Returned Ticket for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
				return Results.Ok(ticket);
			}
		}

	}
}
