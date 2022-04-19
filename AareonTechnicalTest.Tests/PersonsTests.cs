namespace AareonTechnicalTest.Tests
{
	using AareonTechnicalTest.Models;
	using AareonTechnicalTest.Tests.Fixtures;
	using System.Net;
	using System.Net.Http.Json;
	using System.Text;
	using System.Text.Json;
	using Xunit;

	/// <summary>Seed the Persons table.</summary>
	public static class Seed_Persons
	{
		/// <summary>Person test data.</summary>
		public static List<Person> DataPerson =>
			new()
			{
				new Person { Forename = "Able", Surname = "Baker", IsAdmin = true },
				new Person { Forename = "Charlie", Surname = "Daniels", IsAdmin = false }
			};
	}

	[Collection("Context Collection")]
	/// <summary>Tests for Person.</summary>
	public class PersonsTests
	{
		private readonly ApplicationFixture appFixture;

		/// <summary>Initialises a new instance of the <see cref="PersonsTests"/> class.</summary>
		/// <param name="fixture">Class fixture.</param>
		public PersonsTests(ApplicationFixture fixture)
		{
			appFixture = fixture;
		}

		/// <summary>Test for POST/Create Person.</summary>
		/// <remarks>Passed in: Valid person object.<br />
		///   Expected response: Status201Created
		/// </remarks>
		[Fact]
		public async void AddPerson_ValidPerson_ReturnsCreatedResult()
		{
			// Arrange

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			HttpResponseMessage? response = await client.PostAsJsonAsync(
				"/person",
				new StringContent(
					JsonSerializer.Serialize(Seed_Persons.DataPerson[0]),
					Encoding.UTF8,
					"application/json")
				);

			// Assert
			Assert.Equal(HttpStatusCode.Created, response.StatusCode);
		}

		/// <summary>Test for DELETE/Delete person.</summary>
		/// <remarks>Passed In: Valid person identifier (seed expected).br />
		///  - Expected response: Status204NoContent
		/// </remarks>
		[Fact]
		public async void DeletePerson_ExistingIdPassed_ResultsNoContentResult()
		{
			// Arrange
			int knownId = 1;

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			HttpResponseMessage? response = await client.DeleteAsync($"/person/{knownId}");

			// Assert
			Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
		}

		/// <summary>Test for DELETE/Delete person.</summary>
		/// <remarks>Passed In: Invalid/Unknown person identifier (seed expected).<br />
		///  - Expected response: Status404NotFound
		/// </remarks>
		[Fact]
		public async void DeletePerson_UnknownIdPassed_ResultsNotFound()
		{
			// Arrange
			int unknownId = Seed_Persons.DataPerson.Count + 99;

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			HttpResponseMessage? response = await client.DeleteAsync($"/person/{unknownId}");

			// Assert
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}

		/// <summary>Test for GET/Get all persons.</summary>
		/// <remarks>Expected: Is Assignable to return type.<br />
		///  - Model count greater than 0 (seed expected to be at least 1).
		/// </remarks>
		[Fact]
		public async void GetPersonAll_WhenCalled_ReturnsAllItems()
		{
			// Arrange

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			List<Person>? response = await client.GetFromJsonAsync<List<Person>>("/person");
			List<Person>? model = Assert.IsAssignableFrom<List<Person>>(response);
			Assert.True(model.Count > 0);
		}

		/// <summary>Test for GET/Get all persons.</summary>
		/// <remarks>Expected response: Status200OK</remarks>
		[Fact]
		public async void GetPersonAll_WhenCalled_ReturnsOkResult()
		{
			// Arrange

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			HttpResponseMessage? response = await client.GetAsync("/person");

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		/// <summary>Test for GET/Get person by id.</summary>
		/// <remarks>Passed In: Valid person identifier (seed expected).<br />
		///  - Expected response: Status200OK
		/// </remarks>
		[Fact]
		public async void GetPersonById_ExistingIdPassed_ReturnsOkResult()
		{
			// Arrange
			int knownId = 2;

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			HttpResponseMessage? response = await client.GetAsync($"/person/{knownId}");

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		/// <summary>Test for GET/Get person by id.</summary>
		/// <remarks>Passed In: Invalid/Unknown person identifier (seed expected).<br />
		///  - Expected response: Status404NotFound
		/// </remarks>
		[Fact]
		public async void GetPersonById_UnknownIdPassed_ReturnsNotFoundResult()
		{
			// Arrange
			int unknownId = Seed_Persons.DataPerson.Count + 99;

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			HttpResponseMessage? response = await client.GetAsync($"/person/{unknownId}");

			// Assert
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}

		/// <summary>Test for PUT/Update person.</summary>
		/// <remarks>Passed In: Valid person identifier (seed expected).<br />
		///  - Expected response: Status200OK
		/// </remarks>
		[Fact]
		public async void UpdatePerson_ExistingIdPassed_ResultsOkResult()
		{
			// Arrange
			int knownId = 2;
			Person updatePerson = new() { Forename = "Tiger", Surname = "Woods", IsAdmin = false };

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			HttpResponseMessage? response = await client.PutAsync(
				$"/person/{knownId}",
				new StringContent(
					JsonSerializer.Serialize(updatePerson),
					Encoding.UTF8,
					"application/json")
				);

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		/// <summary>Test for PUT/Update person.</summary>
		/// <remarks>Passed In: Invalid/Unknown person identifier (seed expected).<br />
		///  - Expected response: Status404NotFound
		///  </remarks>
		[Fact]
		public async void UpdatePerson_UnknownIdPassed_ResultsNotFoundResult()
		{
			// Arrange
			int unknownId = Seed_Persons.DataPerson.Count + 99;
			Person updatePerson = new() { Forename = "Tiger", Surname = "Woods", IsAdmin = false };

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			HttpResponseMessage? response = await client.PutAsync(
				$"/person/{unknownId}",
				new StringContent(
					JsonSerializer.Serialize(updatePerson),
					Encoding.UTF8,
					"application/json")
				);

			// Assert
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}
	}
}