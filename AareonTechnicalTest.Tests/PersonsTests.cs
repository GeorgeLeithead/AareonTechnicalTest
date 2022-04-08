namespace AareonTechnicalTest.Tests
{
	using AareonTechnicalTest.Models;
	using AareonTechnicalTest.Tests.Fixtures;
	using Microsoft.Extensions.DependencyInjection;
	using System.Net;
	using System.Net.Http.Json;
	using System.Text;
	using System.Text.Json;
	using System.Threading.Tasks;
	using Xunit;

	public class PersonsTests : IClassFixture<ApplicationFixture>
	{
		private readonly ApplicationFixture appFixture;

		public PersonsTests(ApplicationFixture fixture)
		{
			appFixture = fixture;
		}

		[Fact]
		public async Task Get_WhenCalled_HttpStatusCodeOk()
		{
			// Arrange
			HttpClient client = appFixture.application.CreateClient();

			// Act
			HttpResponseMessage? response = await client.GetAsync("/person");

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		[Fact]
		public async Task Get_WhenCalled_ReturnsAllItems()
		{
			// Arrange
			HttpClient client = appFixture.application.CreateClient();

			// Act
			List<Person>? response = await client.GetFromJsonAsync<List<Person>>("/person");
			List<Person>? model = Assert.IsAssignableFrom<List<Person>>(response);
			Assert.True(model.Count > 0);
		}

		[Fact]
		public async Task Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
		{
			// Arrange
			HttpClient client = appFixture.application.CreateClient();

			// Act
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

		[Fact]
		public async Task GetById_UnknownIdPassed_ReturnsNotFoundResult()
		{
			// Arrange
			HttpClient client = appFixture.application.CreateClient();
			int unknownId = 99;

			// Act
			HttpResponseMessage? response = await client.GetAsync($"/person/{unknownId}");

			// Assert
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}

		[Fact]
		public async Task GetById_ExistingIdPassed_ReturnsOkResult()
		{
			// Arrange
			HttpClient client = appFixture.application.CreateClient();
			int knownId = 2;

			// Act
			HttpResponseMessage? response = await client.GetAsync($"/person/{knownId}");

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}
	}

	public static class Seed_Persons
	{
		public static List<Person> DataPerson =>
			new()
			{
				new Person { Forename = "Able", Surname = "Baker", IsAdmin = true },
				new Person { Forename = "Charlie", Surname = "Daniels", IsAdmin = false }
			};
	}

	public class ApplicationFixture : IDisposable
	{
		public ApiWebApplicationFactory application;


		public ApplicationFixture()
		{
			Task<ApiWebApplicationFactory> result = ArrangeAsync();
			application = result.Result;
		}

		public static async Task<ApiWebApplicationFactory> ArrangeAsync()
		{
			ApiWebApplicationFactory application = new("Development");
			using (IServiceScope scope = application.Services.CreateScope())
			{
				IServiceProvider provider = scope.ServiceProvider;
				using ApplicationContext notesDbContext = provider.GetRequiredService<ApplicationContext>();
				await notesDbContext.Database.EnsureDeletedAsync();
				await notesDbContext.Database.EnsureCreatedAsync();
				await notesDbContext.AddRangeAsync(Seed_Persons.DataPerson);
				await notesDbContext.SaveChangesAsync();
			}

			// Act
			return application;
		}

		public void Dispose()
		{
		}
	}
}
