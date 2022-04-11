namespace AareonTechnicalTest.Modules.Tickets.EndPoints
{
	/// <summary>PUT/Update ticket endpoint.</summary>
	public static class Update
	{
		/// <summary>PUT/Update a ticket.</summary>
		/// <param name="id">Ticket identifier.</param>
		/// <param name="ticket">Ticket object.</param>
		/// <param name="ticketRepository">Ticket Repository.</param>
		/// <param name="logger">Logger.</param>
		/// <returns>Status 200 Ok.</returns>
		/// <returns>Status 404 Not Found.</returns>
		public static async Task<IResult> Handler(int id, Ticket ticket, ITicketsRepository ticketRepository, ILogger logger)
		{
			logger.LogInformation("[Modules.Tickets.UpdateTicket.Handler] Update Ticket for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
			Ticket? thisTicket = await ticketRepository.GetTicketByIdAsync(id);
			if (thisTicket == null)
			{
				logger.LogError("[Modules.Tickets.UpdateTicket.Handler] Ticket not found for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
				return Results.NotFound();
			}

			try
			{
				thisTicket.Content = ticket.Content;
				thisTicket.PersonId = ticket.PersonId;
				await ticketRepository.PutTicket(thisTicket);
			}
			catch (DbUpdateConcurrencyException ex)
			{
				logger.LogError("[Modules.Tickets.UpdateTicket.Handler] Error Updating Ticket for id:={id} @{LogTime}. Error:= {ex}", id, DateTimeOffset.UtcNow, ex);
				if (!await ticketRepository.TicketExistsAsync(id))
				{
					logger.LogError("[Modules.Tickets.UpdateTicket.Handler] Ticket not found for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
					return Results.NotFound();
				}

				throw;
			}

			logger.LogInformation("[Modules.Tickets.UpdateTicket.Handler] Updated Ticket for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
			return Results.Ok(thisTicket);
		}
	}
}
