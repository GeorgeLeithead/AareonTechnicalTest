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

	public class PersonsTests
	{
		[Fact]
		public async Task Valid_Get_GetAllPersons_Response()
		{
			// Arrange
			await using ApiWebApplicationFactory application = new("Development");
			using (IServiceScope scope = application.Services.CreateScope())
			{
				IServiceProvider provider = scope.ServiceProvider;
				using ApplicationContext notesDbContext = provider.GetRequiredService<ApplicationContext>();
				await notesDbContext.Database.EnsureDeletedAsync();
				await notesDbContext.Database.EnsureCreatedAsync();
			}

			// Act
			HttpClient client = application.CreateClient();
			HttpResponseMessage? response = await client.GetAsync("/person");

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		[Fact]
		public async Task Valid_Get_GetAllPersons_Empty()
		{
			// Arrange
			await using ApiWebApplicationFactory application = new("Development");
			using (IServiceScope scope = application.Services.CreateScope())
			{
				IServiceProvider provider = scope.ServiceProvider;
				using ApplicationContext notesDbContext = provider.GetRequiredService<ApplicationContext>();
				await notesDbContext.Database.EnsureDeletedAsync();
				await notesDbContext.Database.EnsureCreatedAsync();
			}

			// Act
			HttpClient client = application.CreateClient();
			List<IPerson>? response = await client.GetFromJsonAsync<List<IPerson>>("/person");

			// Assert
			Assert.Empty(response);
		}

		[Fact]
		public async Task Valid_Get_GetAllPersons_Array()
		{
			// Arrange
			await using ApiWebApplicationFactory application = new("Development");
			using (IServiceScope scope = application.Services.CreateScope())
			{
				IServiceProvider provider = scope.ServiceProvider;
				using ApplicationContext notesDbContext = provider.GetRequiredService<ApplicationContext>();
				await notesDbContext.Database.EnsureDeletedAsync();
				await notesDbContext.Database.EnsureCreatedAsync();
				await notesDbContext.AddRangeAsync(NotEmpty_GetAllPersonsData.DataPerson);
				await notesDbContext.SaveChangesAsync();
			}

			// Act
			HttpClient client = application.CreateClient();
			List<Person>? response = await client.GetFromJsonAsync<List<Person>>("/person");

			List<Person>? model = Assert.IsAssignableFrom<List<Person>>(response);
			Assert.Equal(2, model.Count);
			List<Person> data = NotEmpty_GetAllPersonsData.DataPerson;
			Assert.Equal(data[0].Forename, model[0].Forename);
			Assert.Equal(data[0].Surname, model[0].Surname);
			Assert.Equal(data[1].Forename, model[1].Forename);
			Assert.Equal(data[1].Surname, model[1].Surname);
		}

		[Fact]
		public async Task Valid_Post_PostPerson()
		{
			// Arrange
			await using ApiWebApplicationFactory application = new("Development");
			using (IServiceScope scope = application.Services.CreateScope())
			{
				IServiceProvider provider = scope.ServiceProvider;
				using ApplicationContext notesDbContext = provider.GetRequiredService<ApplicationContext>();
				await notesDbContext.Database.EnsureDeletedAsync();
				await notesDbContext.Database.EnsureCreatedAsync();
			}

			// Act
			HttpClient client = application.CreateClient();
			HttpResponseMessage? response = await client.PostAsJsonAsync(
				"/person",
				new StringContent(
					JsonSerializer.Serialize(NotEmpty_GetAllPersonsData.DataPerson[0]),
					Encoding.UTF8,
					"application/json")
				);

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);

			// Act
			List<Person> responseCheckPost = await client.GetFromJsonAsync<List<Person>>("/person");

			// Assert
			Assert.Single(responseCheckPost);
		}

	}

	public static class NotEmpty_GetAllPersonsData
	{
		public static List<Person> DataPerson =>
			new()
			{
				new Person { Forename = "Able", Surname = "Baker", IsAdmin = true },
				new Person { Forename = "Charlie", Surname = "Daniels", IsAdmin = false }
			};
	}
}
