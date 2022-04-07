namespace AareonTechnicalTest.Tests.Fixtures
{
	using Microsoft.AspNetCore.Mvc.Testing;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Storage;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;

	internal class ApiWebApplicationFactory : WebApplicationFactory<Program>
	{
		private readonly string _environment;

		public ApiWebApplicationFactory(string environment = "Development")
		{
			_environment = environment;
		}

		protected override IHost CreateHost(IHostBuilder builder)
		{
			InMemoryDatabaseRoot root = new();
			builder.UseEnvironment(_environment);

			// Add mock/test services to the builder here
			builder.ConfigureServices(services =>
			{
				ServiceDescriptor? context = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(DbContext));
				if (context != null)
				{
					services.Remove(context);
					//ServiceDescriptor[] options = services.Where(r => (r.ServiceType == typeof(DbContextOptions))
					//  || (r.ServiceType.IsGenericType && r.ServiceType.GetGenericTypeDefinition() == typeof(DbContextOptions<>))).ToArray();
					//foreach (ServiceDescriptor option in options)
					//{
					//	services.Remove(option);
					//}
				}

				services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("Testing", root));
				//services.AddScoped(sp =>
				//{
				//	// Replace SQLite with in-memory database for tests

				//	return new DbContextOptionsBuilder<ApplicationContext>()
				//		.UseInMemoryDatabase("Testing", root)
				//		.UseApplicationServiceProvider(sp)
				//		.Options;
				//});
			});

			return base.CreateHost(builder);
		}
	}
}