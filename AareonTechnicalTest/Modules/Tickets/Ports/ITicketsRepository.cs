namespace AareonTechnicalTest.Modules.Tickets.Ports
{
	/// <summary>Tickets repository interface.</summary>
	public interface ITicketsRepository
	{
		/// <summary>POST/Add ticket.</summary>
		/// <param name="ticket">Ticket POCO.</param>
		/// <returns>List of Tickets.</returns>
		Task<Ticket?> AddTicket(Ticket ticket);

		/// <summary>DELETE/Delete a ticket.</summary>
		/// <param name="ticket">Ticket POCO.</param>
		/// <returns>Number of records affected.</returns>
		Task<int> DeleteTicket(Ticket ticket);

		/// <summary>GET/Get Tickets.</summary>
		/// <returns>List of all Tickets.</returns>
		Task<List<Ticket>?> GetAllTickets();

		/// <summary>GET/Get ticket by unique identifier.</summary>
		/// <param name="id">Ticket identifier.</param>
		/// <returns>A ticket POCO.</returns>
		Ticket? GetTicketById(int id);

		/// <summary>GET/Get ticket asynchronously by unique identifier.</summary>
		/// <param name="id">Ticket identifier.</param>
		/// <returns>A ticket POCO.</returns>
		Task<Ticket?> GetTicketByIdAsync(int id);

		/// <summary>PUT/Update ticket.</summary>
		/// <param name="ticket">Ticket POCO.</param>
		/// <returns>Number of records affected.</returns>
		Task<int> PutTicket(Ticket ticket);

		/// <summary>Does the ticket exist in the repository</summary>
		/// <param name="id">Ticket identifier.</param>
		/// <returns>true if exists; otherwise false.</returns>
		Task<bool> TicketExistsAsync(int id);
	}
}