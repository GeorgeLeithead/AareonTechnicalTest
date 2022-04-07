using AareonTechnicalTest.Modules;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationContext>(c =>
{
	string? envDir = Environment.CurrentDirectory;
	string DatabasePath = $"{envDir}{Path.DirectorySeparatorChar}Ticketing.db";
	c.UseSqlite($"Filename={DatabasePath}");
});
builder.Services.RegisterModules();
builder.Services.AddScoped<ILogger, Logger<Program>>();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "AareonTechnicalTest", Version = "v1" });
	string? docFilePath = Path.Combine(System.AppContext.BaseDirectory, "AareonTechnicalTest.xml");
	c.IncludeXmlComments(docFilePath);
});

WebApplication app = builder.Build();

//await EnsureDb(app.Services, app.Logger);

app.UseSwagger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapSwagger();
	app.UseSwagger();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapEndpoints();
app.UseRouting();
app.UseAuthorization();

// <summary>READ/GET all tickets.</summary>
app.MapGet("/tickets", async (ApplicationContext db) => await db.Tickets.Include(t => t.Notes).ToListAsync())
	.Produces<List<ITicket>>(StatusCodes.Status200OK)
	.WithName("GetAllTickets")
	.WithTags("TicketGetters")
	.AllowAnonymous();

// <summary>CREATE/POST a new ticket.</summary>
app.MapPost("/tickets", async ([FromBody] Ticket addTicket, [FromServices] ApplicationContext db, HttpResponse response) =>
	{
		db.Tickets.Add(addTicket);
		await db.SaveChangesAsync();
		return Results.Created("tickets", addTicket);
	})
	.Accepts<ITicket>("application/json")
	.Produces<ITicket>(StatusCodes.Status201Created)
	.WithName("AddNewTicket")
	.WithTags("TicketSetters")
	.AllowAnonymous();

// <summary>UPDATE/PUT an existing ticket.</summary>
// <remarks>Should not be able to update a ticket that does not exist.</remarks>
app.MapPut("/tickets", async (int Id, string Content, int PersonId, [FromServices] ApplicationContext db, HttpResponse response) =>
	{
		ITicket? theTicket = db.Tickets.Include(t => t.Notes).SingleOrDefault(t => t.Id == Id);
		if (theTicket == null)
		{
			return Results.NotFound();
		}

		theTicket.Content = Content;
		theTicket.PersonId = PersonId;
		theTicket.Notes = theTicket.Notes;
		await db.SaveChangesAsync();
		return Results.Created("/tickets", theTicket);
	})
	.Produces<ITicket>(StatusCodes.Status201Created)
	.Produces(StatusCodes.Status404NotFound)
	.WithName("UpdateTicket")
	.WithTags("TicketSetters")
	.AllowAnonymous();

// <summary>READ/GET a ticket by its unique identifier.</summary>
app.MapGet("/tickets/{Id}", async ([FromServices] ApplicationContext db, int Id) => await db.Tickets.Include(t => t.Notes).SingleOrDefaultAsync(t => t.Id == Id) is ITicket theTicket ? Results.Ok(theTicket) : Results.NotFound())
	.Produces<ITicket>(StatusCodes.Status200OK)
	.WithName("GetTicketById")
	.WithTags("TicketGetters");

// <summary>DELETE/DELETE a ticket by its unique identifier.</summary>
app.MapDelete("/tickets/{Id}", async ([FromServices] ApplicationContext db, int Id) =>
	{
		ITicket? theTicket = db.Tickets.SingleOrDefault(t => t.Id == Id);
		if (theTicket == null)
		{
			return Results.NotFound();
		}

		db.Remove(theTicket);
		await db.SaveChangesAsync();
		return Results.NoContent();
	})
	.Produces(StatusCodes.Status404NotFound)
	.Produces(StatusCodes.Status204NoContent)
	.WithName("DeleteTicketById")
	.WithTags("TicketSetters")
	.AllowAnonymous();

// <summary>READ/GET all notes for a ticket.</summary>
app.MapGet("/tickets/{ticketId}/Notes", async ([FromServices] ApplicationContext db, int ticketId) =>
	{
		ITicket? theTicket = db.Tickets.SingleOrDefault(t => t.Id == ticketId);
		if (theTicket == null)
		{
			return Results.BadRequest();
		}

		List<TicketNote>? theTicketNotes = await db.TicketNotes.Where(tn => tn.Ticket == theTicket).ToListAsync();
		if (theTicketNotes == null)
		{
			return Results.NotFound();
		}

		return Results.Ok(theTicketNotes);
	})
	.Produces<List<ITicketNote>>(StatusCodes.Status200OK)
	.Produces(StatusCodes.Status404NotFound)
	.Produces(StatusCodes.Status400BadRequest)
	.WithName("GetTicketNotesByTicketId")
	.WithTags("TicketNoteGetters")
	.AllowAnonymous();

// <summary>CREATE/POST a new ticket note.</summary>
app.MapPost("/tickets/{ticketId}/Notes", async ([FromBody] TicketNote addTicketNote, int ticketId, [FromServices] ApplicationContext db, HttpResponse response) =>
	{
		Ticket? theTicket = db.Tickets.SingleOrDefault(t => t.Id == ticketId);
		if (theTicket == null)
		{
			return Results.BadRequest();
		}

		addTicketNote.Ticket = theTicket;
		db.TicketNotes.Add(addTicketNote);
		await db.SaveChangesAsync();
		return Results.Created($"tickets/{ticketId}/Notes", addTicketNote);
	})
	.Accepts<TicketNote>("application/json")
	.Produces<ITicketNote>(StatusCodes.Status201Created)
	.Produces(StatusCodes.Status400BadRequest)
	.WithName("AddNewTicketNote")
	.WithTags("TicketNoteSetters")
	.AllowAnonymous();


// <summary>READ/GET a ticket note by its unique identifier.</summary>
app.MapGet("/tickets/Notes/{Id}", async ([FromServices] ApplicationContext db, int Id) => await db.TicketNotes.Include(tn => tn.Ticket).Include(tn => tn.Person).SingleOrDefaultAsync(tn => tn.Id == Id) is ITicketNote theTicketNote ? Results.Ok(theTicketNote) : Results.NotFound())
	.Produces<ITicketNote>(StatusCodes.Status200OK)
	.WithName("GetTicketNoteById")
	.WithTags("TicketNoteGetters");

// <summary>UPDATE/PUT an existing ticket note.</summary>
// <remarks>Should not be able to update a ticket note that does not exist.</remarks>
app.MapPut("/tickets/Notes/{Id}", async (int Id, string Note, int PersonId, [FromServices] ApplicationContext db, HttpResponse response) =>
{
	TicketNote? theTicketNote = db.TicketNotes.Include(t => t.Ticket).SingleOrDefault(t => t.Id == Id);
	if (theTicketNote == null)
	{
		return Results.NotFound();
	}

	theTicketNote.Note = Note;
	theTicketNote.PersonId = PersonId;
	theTicketNote.Ticket = theTicketNote.Ticket;
	await db.SaveChangesAsync();
	return Results.Created("/tickets/Notes", theTicketNote);
})
	.Produces<ITicketNote>(StatusCodes.Status201Created)
	.Produces(StatusCodes.Status404NotFound)
	.WithName("UpdateTicketNote")
	.WithTags("TicketNoteSetters")
	.AllowAnonymous();

// <summary>DELETE/DELETE a ticket note by its unique identifier.</summary>
// TODO: Update this to only allow authenticated person to call this API
// TODO: Update this so that only an authenticated person, with the IsAdmin = true, can delete.
app.MapDelete("/tickets/Notes/{Id}", async ([FromServices] ApplicationContext db, int Id) =>
{
	ITicketNote? theTicketNote = db.TicketNotes.SingleOrDefault(t => t.Id == Id);
	if (theTicketNote == null)
	{
		return Results.NotFound();
	}

	db.Remove(theTicketNote);
	await db.SaveChangesAsync();
	return Results.NoContent();
})
	.Produces(StatusCodes.Status404NotFound)
	.Produces(StatusCodes.Status204NoContent)
	.WithName("DeleteTicketNoteById")
	.WithTags("TicketNoteSetters")
	.AllowAnonymous();

app.Logger.LogInformation("Starting AareonTechnicalTest {date}", DateTime.UtcNow);
app.Run();

async Task EnsureDb(IServiceProvider services, ILogger logger)
{
	using ApplicationContext? db = services.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>();
	if (db.Database.IsRelational())
	{
		logger.LogInformation("Ensuring database exists and is up to date");
		await db.Database.MigrateAsync();
	}
}

/// <summary>Program class.</summary>
/// <remarks>Make the implicit Program class public so test projects can access it.</remarks>
public partial class Program { }