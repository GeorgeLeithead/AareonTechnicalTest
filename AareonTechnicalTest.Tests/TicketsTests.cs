namespace AareonTechnicalTest.Tests
{
	using AareonTechnicalTest.Models;
	using AareonTechnicalTest.Tests.Fixtures;
	using System.Net;
	using System.Net.Http.Json;
	using System.Text;
	using System.Text.Json;
	using Xunit;

	/// <summary>Seed the Tickets table.</summary>
	public static class Seed_Tickets
	{
		/// <summary>Ticket test data.</summary>
		public static List<Ticket> DataTicket =>
			new()
			{
				new Ticket { Content = "Ticket 1", PersonId = (Seed_Persons.DataPerson.Count - 1) },
				new Ticket { Content = "Ticket 2", PersonId = (Seed_Persons.DataPerson.Count) },
				new Ticket { Content = "Ticket 3", PersonId = (Seed_Persons.DataPerson.Count - 1) },
			};
	}


	[Collection("Context Collection")]
	/// <summary>Tests for Ticket.</summary>
	public class TicketsTests
	{
		private readonly ApplicationFixture appFixture;

		/// <summary>Initialises a new instance of the <see cref="TicketsTests"/> class.</summary>
		/// <param name="fixture">Class fixture.</param>
		public TicketsTests(ApplicationFixture fixture)
		{
			appFixture = fixture;
		}

		/// <summary>Test for POST/Create Ticket.</summary>
		/// <remarks>Passed in: Valid ticket object.<br />
		///   Expected response: Status201Created
		/// </remarks>
		[Fact]
		public async void AddTicket_ValidTicket_ReturnsCreatedResult()
		{
			// Arrange
			int knownPersonId = 2;
			Ticket newTicket = new() { Content = "New Ticket", PersonId = knownPersonId };

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			HttpResponseMessage? responsePerson = await client.GetAsync($"/person/{knownPersonId}");

			HttpResponseMessage? response = await client.PostAsJsonAsync<Ticket>("/ticket", newTicket);

			// Assert
			Assert.Equal(HttpStatusCode.OK, responsePerson.StatusCode);
			Assert.Equal(HttpStatusCode.Created, response.StatusCode);
		}

		/// <summary>Test for DELETE/Delete ticket.</summary>
		/// <remarks>Passed In: Valid ticket identifier (seed expected).br />
		///  - Expected response: Status204NoContent
		/// </remarks>
		[Fact]
		public async void DeleteTicket_ExistingIdPassed_ResultsNoContentResult()
		{
			// Arrange
			int knownId = 1;

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			HttpResponseMessage? response = await client.DeleteAsync($"/ticket/{knownId}");

			// Assert
			Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
		}

		/// <summary>Test for DELETE/Delete ticket.</summary>
		/// <remarks>Passed In: Invalid/Unknown ticket identifier (seed expected).<br />
		///  - Expected response: Status404NotFound
		/// </remarks>
		[Fact]
		public async void DeleteTicket_UnknownIdPassed_ResultsNotFound()
		{
			// Arrange
			int unknownId = Seed_Tickets.DataTicket.Count + 99;

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			HttpResponseMessage? response = await client.DeleteAsync($"/ticket/{unknownId}");

			// Assert
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}

		/// <summary>Test for GET/Get all tickets.</summary>
		/// <remarks>Expected: Is Assignable to return type.<br />
		///  - Model count greater than 0 (seed expected to be at least 1).
		/// </remarks>
		[Fact]
		public async void GetAllTickets_WhenCalled_ReturnsAllItems()
		{
			// Arrange

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			List<Ticket>? response = await client.GetFromJsonAsync<List<Ticket>>("/ticket");
			List<Ticket>? model = Assert.IsAssignableFrom<List<Ticket>>(response);
			Assert.True(model.Count > 0);
		}

		/// <summary>Test for GET/Get all tickets.</summary>
		/// <remarks>Expected response: Status200OK</remarks>
		[Fact]
		public async void GetAllTickets_WhenCalled_ReturnsOkResult()
		{
			// Arrange

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			HttpResponseMessage? response = await client.GetAsync("/ticket");

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		/// <summary>Test for GET/Get ticket by id.</summary>
		/// <remarks>Passed In: Valid ticket identifier (seed expected).<br />
		///  - Expected response: Status200OK
		/// </remarks>
		[Fact]
		public async void GetTicketById_ExistingIdPassed_ReturnsOkResult()
		{
			// Arrange
			int knownId = 2;

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			HttpResponseMessage? response = await client.GetAsync($"/ticket/{knownId}");

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		/// <summary>Test for GET/Get ticket by id.</summary>
		/// <remarks>Passed In: Invalid/Unknown ticket identifier (seed expected).<br />
		///  - Expected response: Status404NotFound
		/// </remarks>
		[Fact]
		public async void GetTicketById_UnknownIdPassed_ReturnsNotFoundResult()
		{
			// Arrange
			int unknownId = Seed_Tickets.DataTicket.Count + 99;

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			HttpResponseMessage? response = await client.GetAsync($"/ticket/{unknownId}");

			// Assert
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}

		/// <summary>Test for PUT/Update ticket.</summary>
		/// <remarks>Passed In: Valid ticket identifier (seed expected).<br />
		///  - Expected response: Status200OK
		/// </remarks>
		[Fact]
		public async void UpdateTicket_ExistingIdPassed_ResultsOkResult()
		{
			// Arrange
			int knownTicketId = 2;
			int knownPersonId = 2;
			Ticket updateTicket = new() { Content = "Updated ticket", PersonId = knownPersonId };

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			HttpResponseMessage? responsePerson = await client.GetAsync($"/person/{knownPersonId}");
			HttpResponseMessage? responseTicket = await client.GetAsync($"/ticket/{knownTicketId}");
			HttpResponseMessage? response = await client.PutAsJsonAsync<Ticket>($"/ticket/{knownTicketId}", updateTicket);

			// Assert
			Assert.Equal(HttpStatusCode.OK, responsePerson.StatusCode);
			Assert.Equal(HttpStatusCode.OK, responseTicket.StatusCode);
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		/// <summary>Test for PUT/Update ticket.</summary>
		/// <remarks>Passed In: Invalid/Unknown ticket identifier (seed expected).<br />
		///  - Expected response: Status404NotFound
		///  </remarks>
		[Fact]
		public async void UpdateTicket_UnknownIdPassed_ResultsNotFoundResult()
		{
			// Arrange
			int unknownId = Seed_Tickets.DataTicket.Count + 99;
			Ticket updateTicket = new() { Content = "Invalid Update" };

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			HttpResponseMessage? response = await client.PutAsync(
				$"/ticket/{unknownId}",
				new StringContent(
					JsonSerializer.Serialize(updateTicket),
					Encoding.UTF8,
					"application/json")
				);

			// Assert
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}
	}
}