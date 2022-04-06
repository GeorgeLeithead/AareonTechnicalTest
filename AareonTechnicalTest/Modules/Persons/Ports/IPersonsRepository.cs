namespace AareonTechnicalTest.Modules.Persons.Ports
{
	using System.Threading.Tasks;

	/// <summary>Persons repository interface.</summary>
	public interface IPersonsRepository
	{
		/// <summary>POST/Add person.</summary>
		/// <param name="person">Person POCO.</param>
		/// <returns>List of persons.</returns>
		Task<Person?> AddPerson(Person person);

		/// <summary>DELETE/Delete a person.</summary>
		/// <param name="person">Person POCO.</param>
		/// <returns>Number of records affected.</returns>
		Task<int> DeletePerson(Person person);

		/// <summary>GET/Get person by unique identifier.</summary>
		/// <param name="id">Person identifier.</param>
		/// <returns>A person POCO.</returns>
		Person? GetPersonById(int id);

		/// <summary>GET/Get person asynchronously by unique identifier.</summary>
		/// <param name="id">Person identifier.</param>
		/// <returns>A person POCO.</returns>
		Task<Person?> GetPersonByIdAsync(int id);

		/// <summary>GET/Get persons.</summary>
		/// <returns>List of all persons.</returns>
		Task<List<Person>?> GetAllPersons();

		/// <summary>Does the person exist in the repository</summary>
		/// <param name="id">Person identifier.</param>
		/// <returns>true if exists; otherwise false.</returns>
		Task<bool> PersonExistsAsync(int id);

		/// <summary>PUT/Update person.</summary>
		/// <param name="person">Person POCO.</param>
		/// <returns>Number of records affected.</returns>
		Task<int> PutPerson(Person person);
	}
}