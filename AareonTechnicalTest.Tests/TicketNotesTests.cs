namespace AareonTechnicalTest.Tests
{
	using AareonTechnicalTest.Models;
	using AareonTechnicalTest.Tests.Fixtures;
	using System.Collections.Generic;
	using System.Net;
	using System.Net.Http.Json;
	using System.Text;
	using System.Text.Json;
	using Xunit;


	/// <summary>Seed the Tickets table.</summary>
	public static class Seed_TicketNotes
	{
		/// <summary>Ticket test data.</summary>
		public static List<TicketNote> DataTicketNote =>
			new()
			{
				new TicketNote { Note = "Note 1 for person 1 ticket 2", PersonId = (Seed_Persons.DataPerson.Count) - 1, TicketId = (Seed_Tickets.DataTicket.Count) - 1 },
				new TicketNote { Note = "Note 2 for person 1 ticket 2", PersonId = (Seed_Persons.DataPerson.Count) - 1, TicketId = (Seed_Tickets.DataTicket.Count) - 1 },
				new TicketNote { Note = "Note One for person 2 ticket 2", PersonId = (Seed_Persons.DataPerson.Count), TicketId = (Seed_Tickets.DataTicket.Count) - 1 },
				new TicketNote { Note = "Note Two for person 2 ticket 1", PersonId = (Seed_Persons.DataPerson.Count), TicketId = (Seed_Tickets.DataTicket.Count) - 2 },
			};
	}


	[Collection("Context Collection")]
	/// <summary>Tests for Ticket Notes.</summary>
	public class TicketNotesTests
	{
		private readonly ApplicationFixture appFixture;

		/// <summary>Initialises a new instance of the <see cref="TicketNotesTests"/> class.</summary>
		/// <param name="fixture">Class fixture.</param>
		public TicketNotesTests(ApplicationFixture fixture)
		{
			appFixture = fixture;
		}

		/// <summary>Test for GET/Get all ticket notes.</summary>
		/// <remarks>Expected: Is Assignable to return type.<br />
		///  - Model count greater than 0 (seed expected to be at least 4).
		/// </remarks>
		[Fact]
		public async void GetTicketNoteAll_WhenCalled_ReturnsAllItems()
		{
			// Arrange

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			List<TicketNote>? response = await client.GetFromJsonAsync<List<TicketNote>>("/ticket/note");
			List<TicketNote>? model = Assert.IsAssignableFrom<List<TicketNote>>(response);
			Assert.True(model.Count > 0);
		}


		/// <summary>Test for GET/Get all ticket notes.</summary>
		/// <remarks>Expected response: Status200OK</remarks>
		[Fact]
		public async void GetTicketNoteAll_WhenCalled_ReturnsOkResult()
		{
			// Arrange

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			HttpResponseMessage? response = await client.GetAsync("/ticket/note");

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		/// <summary>Test for GET/Get ticket note by id.</summary>
		/// <remarks>Passed In: Valid ticket note identifier (seed expected).<br />
		///  - Expected response: Status200OK
		/// </remarks>
		[Fact]
		public async void GetTicketNoteById_ExistingIdPassed_ReturnsOkResult()
		{
			// Arrange
			int knownId = 2;

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			HttpResponseMessage? response = await client.GetAsync($"/ticket/note/{knownId}");

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		/// <summary>Test for GET/Get ticket note by id.</summary>
		/// <remarks>Passed In: Invalid/Unknown ticket note identifier (seed expected).<br />
		///  - Expected response: Status404NotFound
		/// </remarks>
		[Fact]
		public async void GetTicketNoteById_UnknownIdPassed_ReturnsNotFoundResult()
		{
			// Arrange
			int unknownId = Seed_TicketNotes.DataTicketNote.Count + 99;

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			HttpResponseMessage? response = await client.GetAsync($"/ticket/note/{unknownId}");

			// Assert
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}


		/// <summary>Test for PUT/Update ticket note.</summary>
		/// <remarks>Passed In: Valid ticket note identifier (seed expected).<br />
		///  - Expected response: Status200OK
		/// </remarks>
		[Fact]
		public async void UpdateTicketNote_ExistingIdPassed_ResultsOkResult()
		{
			// Arrange
			int knownTicketNoteId = 2;
			int knownPersonId = 2;
			int knownTicketId = 2;
			TicketNote updateTicketNote = new() { Note = "Updated note", PersonId = knownPersonId, TicketId = knownTicketId };

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			HttpResponseMessage? responsePerson = await client.GetAsync($"/person/{knownPersonId}");
			HttpResponseMessage? responseTicket = await client.GetAsync($"/ticket/{knownTicketNoteId}");
			HttpResponseMessage? response = await client.PutAsJsonAsync<TicketNote>($"/ticket/note/{knownTicketNoteId}", updateTicketNote);
			TicketNote? responseGet = await client.GetFromJsonAsync<TicketNote>($"/ticket/note/{knownTicketNoteId}");

			// Assert
			Assert.Equal(HttpStatusCode.OK, responsePerson.StatusCode);
			Assert.Equal(HttpStatusCode.OK, responseTicket.StatusCode);
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);

			TicketNote? model = Assert.IsAssignableFrom<TicketNote>(responseGet);
			Assert.Equal(updateTicketNote.Note, model.Note);
			Assert.Equal(knownTicketNoteId, model.Id);
			Assert.Equal(updateTicketNote.TicketId, model.TicketId);
			Assert.Equal(updateTicketNote.PersonId, model.PersonId);
		}

		/// <summary>Test for PUT/Update ticket note.</summary>
		/// <remarks>Passed In: Invalid/Unknown ticket note identifier (seed expected).<br />
		///  - Expected response: Status404NotFound
		///  </remarks>
		[Fact]
		public async void UpdateTicketNote_UnknownIdPassed_ResultsNotFoundResult()
		{
			// Arrange
			int unknownId = Seed_TicketNotes.DataTicketNote.Count + 99;
			TicketNote updateTicketNote = new() { Note = "Updated note", PersonId = 0, TicketId = 0 };

			// Act
			HttpClient client = appFixture.Application.CreateClient();
			HttpResponseMessage? response = await client.PutAsync(
				$"/ticket/note/{unknownId}",
				new StringContent(
					JsonSerializer.Serialize(updateTicketNote),
					Encoding.UTF8,
					"application/json")
				);

			// Assert
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}
	}
}
