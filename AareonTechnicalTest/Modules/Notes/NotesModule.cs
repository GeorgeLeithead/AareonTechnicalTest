namespace AareonTechnicalTest.Modules.Notes
{
	/// <summary>Notes module.</summary>
	public class NotesModule : IModule
	{
		/// <summary>Register a module.</summary>
		/// <param name="services">Service collection.</param>
		/// <returns>Service collection.</returns>
		public IServiceCollection RegisterModule(IServiceCollection services)
		{
			services.AddScoped<INotesRepository, NotesRepository>();
			return services;
		}

		/// <summary>Map endpoints.</summary>
		/// <param name="endpoints">Endpoint route builder.</param>
		/// <returns>Endpoint route builder.</returns>
		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			endpoints.MapGet("/tickets/notes/{id}", EndPoints.Read.HandlerById)
				.Produces<ITicketNote>(StatusCodes.Status200OK)
				.Produces(StatusCodes.Status404NotFound)
				.WithName("GetNotesById")
				.WithTags("TicketNotes")
				.AllowAnonymous();
			endpoints.MapGet("/tickets/notes", EndPoints.Read.HandlerAll)
				.Produces<IList<ITicketNote>>(StatusCodes.Status200OK)
				.Produces(StatusCodes.Status404NotFound)
				.WithName("GetAllNotes")
				.WithTags("TicketNotes")
				.AllowAnonymous();
			//endpoints.MapPut("/person/{id}", EndPoints.Update.Handler)
			//	.Accepts<IPerson>("application/json")
			//	.Produces<IPerson>(StatusCodes.Status200OK)
			//	.Produces(StatusCodes.Status404NotFound)
			//	.WithName("UpdatePerson")
			//	.WithTags("Person")
			//	.AllowAnonymous();
			endpoints.MapPost("/tickets/notes", EndPoints.Create.Handler)
				.Accepts<ITicketNote>("application/json")
				.Produces<ITicketNote>(StatusCodes.Status201Created)
				.Produces(StatusCodes.Status404NotFound)
				.WithName("AddNote")
				.WithTags("TicketNotes")
				.AllowAnonymous();
			//endpoints.MapDelete("/person/{id}", EndPoints.Delete.Handler)
			//	.Produces(StatusCodes.Status204NoContent)
			//	.Produces(StatusCodes.Status404NotFound)
			//	.WithName("DeletePerson")
			//	.WithTags("Person")
			//	.AllowAnonymous();

			return endpoints;
		}
	}
}
