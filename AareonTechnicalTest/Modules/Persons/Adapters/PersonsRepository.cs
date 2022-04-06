namespace AareonTechnicalTest.Modules.Persons.Adapters
{
	/// <summary>Persons repository.</summary>
	public class PersonsRepository : IPersonsRepository
	{
		private readonly ApplicationContext db;

		/// <summary>Initialises a new instance of the <see cref="PersonsRepository"/> class.</summary>
		/// <param name="context">Application Context.</param>
		public PersonsRepository(ApplicationContext context)
		{
			db = context;
		}

		/// <summary>POST/Add person.</summary>
		/// <param name="person">Person POCO.</param>
		/// <returns>A person POCO.</returns>
		public async Task<Person?> AddPerson(Person person)
		{
			db.Persons.Add(person);
			await db.SaveChangesAsync();
			return await GetPersonByIdAsync(person.Id);
		}

		/// <summary>DELETE/Delete a person.</summary>
		/// <param name="person">Person POCO.</param>
		/// <returns>Number of records affected.</returns>
		public async Task<int> DeletePerson(Person person)
		{
			db.Persons.Remove(person);
			return await db.SaveChangesAsync();
		}

		/// <summary>GET/Get person by unique identifier.</summary>
		/// <param name="id">Person identifier.</param>
		/// <returns>A person POCO.</returns>
		public Person? GetPersonById(int id)
		{
			return db.Persons.FirstOrDefault(p => p.Id == id);
		}

		/// <summary>GET/Get person asynchronously by unique identifier.</summary>
		/// <param name="id">Person identifier.</param>
		/// <returns>A person POCO.</returns>
		public Task<Person?> GetPersonByIdAsync(int id)
		{
			return db.Persons.FirstOrDefaultAsync(p => p.Id == id);
		}

		/// <summary>GET/Get persons.</summary>
		/// <returns>List of all persons.</returns>
		public async Task<List<Person>?> GetAllPersons()
		{
			return await db.Persons.ToListAsync();
		}

		/// <summary>PUT/Update person.</summary>
		/// <param name="person">Person POCO.</param>
		/// <returns>Number of records affected.</returns>
		public async Task<int> PutPerson(Person person)
		{
			db.Persons.Update(person);
			return await db.SaveChangesAsync();
		}

		/// <summary>Does the person exist in the repository</summary>
		/// <param name="id">Person identifier.</param>
		/// <returns>true if exists; otherwise false.</returns>
		public Task<bool> PersonExistsAsync(int id)
		{
			return db.Persons.AnyAsync(p => p.Id == id);
		}
	}
}