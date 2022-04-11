namespace AareonTechnicalTest.Modules.Tickets.Adapters
{
	/// <summary>Tickets repository.</summary>
	public class TicketsRepository : ITicketsRepository
	{
		private readonly ApplicationContext db;

		/// <summary>Initialises a new instance of the <see cref="TicketsRepository"/> class.</summary>
		/// <param name="context">Application Context.</param>
		public TicketsRepository(ApplicationContext context)
		{
			db = context;
		}

		/// <summary>POST/Add ticket.</summary>
		/// <param name="ticket">Ticket POCO.</param>
		/// <returns>A ticket POCO.</returns>
		public async Task<Ticket?> AddTicket(Ticket ticket)
		{
			db.Tickets.Add(ticket);
			await db.SaveChangesAsync();
			return await GetTicketByIdAsync(ticket.Id);
		}

		/// <summary>DELETE/Delete a ticket.</summary>
		/// <param name="ticket">Ticket POCO.</param>
		/// <returns>Number of records affected.</returns>
		public async Task<int> DeleteTicket(Ticket ticket)
		{
			db.Tickets.Remove(ticket);
			return await db.SaveChangesAsync();
		}

		/// <summary>GET/Get Tickets.</summary>
		/// <returns>List of all Tickets.</returns>
		public async Task<List<Ticket>?> GetAllTickets()
		{
			return await db.Tickets.Include(e => e.Notes).ToListAsync();
		}

		/// <summary>GET/Get ticket by unique identifier.</summary>
		/// <param name="id">Ticket identifier.</param>
		/// <returns>A ticket POCO.</returns>
		public Ticket? GetTicketById(int id)
		{
			return db.Tickets.Include(e => e.Notes).FirstOrDefault(e => e.Id == id);
		}

		/// <summary>GET/Get ticket asynchronously by unique identifier.</summary>
		/// <param name="id">Ticket identifier.</param>
		/// <returns>A ticket POCO.</returns>
		public Task<Ticket?> GetTicketByIdAsync(int id)
		{
			return db.Tickets.Include(e => e.Notes).FirstOrDefaultAsync(e => e.Id == id);
		}

		/// <summary>PUT/Update ticket.</summary>
		/// <param name="ticket">Ticket POCO.</param>
		/// <returns>Number of records affected.</returns>
		public async Task<int> PutTicket(Ticket ticket)
		{
			db.Tickets.Update(ticket);
			return await db.SaveChangesAsync();
		}

		/// <summary>Does the ticket exist in the repository</summary>
		/// <param name="id">Ticket identifier.</param>
		/// <returns>true if exists; otherwise false.</returns>
		public Task<bool> TicketExistsAsync(int id)
		{
			return db.Tickets.AnyAsync(e => e.Id == id);
		}
	}
}