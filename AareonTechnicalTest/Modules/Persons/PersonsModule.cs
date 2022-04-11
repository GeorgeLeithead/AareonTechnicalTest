namespace AareonTechnicalTest.Modules.Persons
{
	/// <summary>Persons module.</summary>
	public class PersonsModule : IModule
	{
		/// <summary>Register a module.</summary>
		/// <param name="services">Service collection.</param>
		/// <returns>Service collection.</returns>
		public IServiceCollection RegisterModule(IServiceCollection services)
		{
			services.AddScoped<IPersonsRepository, PersonsRepository>();
			return services;
		}

		/// <summary>Map endpoints.</summary>
		/// <param name="endpoints">Endpoint route builder.</param>
		/// <returns>Endpoint route builder.</returns>
		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			endpoints.MapGet("/person/{id}", GetPersonById.Handler)
				.Produces<IPerson>(StatusCodes.Status200OK)
				.Produces(StatusCodes.Status404NotFound)
				.WithName("GetPersonById")
				.WithTags("Person")
				.AllowAnonymous();
			endpoints.MapGet("/person", GetAllPersons.Handler)
				.Produces<List<IPerson>>(StatusCodes.Status200OK)
				.Produces(StatusCodes.Status404NotFound)
				.WithName("GetAllPersons")
				.WithTags("Person")
				.AllowAnonymous();
			endpoints.MapPut("/person/{id}", UpdatePerson.Handler)
				.Accepts<Person>("application/json")
				.Produces<IPerson>(StatusCodes.Status200OK)
				.Produces(StatusCodes.Status404NotFound)
				.WithName("UpdatePerson")
				.WithTags("Person")
				.AllowAnonymous();
			endpoints.MapPost("/person", AddPerson.Handler)
				.Accepts<IPerson>("application/json")
				.Produces<IPerson>(StatusCodes.Status201Created)
				.Produces(StatusCodes.Status404NotFound)
				.WithName("AddPerson")
				.WithTags("Person")
				.AllowAnonymous();
			endpoints.MapDelete("/person/{id}", DeletePerson.Handler)
				.Produces(StatusCodes.Status204NoContent)
				.Produces(StatusCodes.Status404NotFound)
				.WithName("DeletePerson")
				.WithTags("Person")
				.AllowAnonymous();

			return endpoints;
		}
	}
}
