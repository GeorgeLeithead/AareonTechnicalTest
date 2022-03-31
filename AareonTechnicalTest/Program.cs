WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationContext>(c => c.UseSqlite());

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "AareonTechnicalTest", Version = "v1" });
});

WebApplication app = builder.Build();

app.UseSwagger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseRouting();
app.UseAuthorization();

/// <summary>READ/GET all tickets.</summary>
app.MapGet("/tickets", async (ApplicationContext db) => await db.Tickets.ToListAsync()
).Produces<List<Ticket>>(StatusCodes.Status200OK).WithName("GetAllTickets").WithTags("Getters");

/// <summary>CREATE/POST a new ticket.</summary>
app.MapPost("/tickets", async ([FromBody] Ticket addTicket, [FromServices] ApplicationContext db, HttpResponse response) =>
	{
		db.Tickets.Add(addTicket);
		await db.SaveChangesAsync();
		return Results.Created("tickets", addTicket);
	}
).Accepts<Ticket>("application/json").Produces<Ticket>(StatusCodes.Status201Created).WithName("AddNewTicket").WithTags("Setters");

/// <summary>UPDATE/PUT an existing ticket.</summary>
/// <remarks>Should not be able to update a ticket that does not exist.</remarks>
app.MapPut("/tickets", async (int Id, string Content, int PersonId, [FromServices] ApplicationContext db, HttpResponse response) =>
	{
		global::AareonTechnicalTest.Models.Ticket? theTicket = db.Tickets.SingleOrDefault(t => t.Id == Id);
		if (theTicket == null)
		{
			return Results.NotFound();
		}

		theTicket.Content = Content;
		theTicket.PersonId = PersonId;
		await db.SaveChangesAsync();
		return Results.Created("/tickets", theTicket);
	}
).Produces<Ticket>(StatusCodes.Status201Created).Produces(StatusCodes.Status404NotFound).WithName("UpdateTicket").WithTags("Setters");

/// <summary>READ/GET a ticket by its unique identifier.</summary>
app.MapGet("/tickets/{Id}", async ([FromServices] ApplicationContext db, int Id) =>
		await db.Tickets.SingleOrDefaultAsync(t => t.Id == Id) is Ticket theTicket ? Results.Ok(theTicket) : Results.NotFound()
).Produces<Ticket>(StatusCodes.Status200OK).WithName("GetTicketById").WithTags("Getters");

/// <summary>DELETE/DELETE a ticket by its unique identifier.</summary>
app.MapDelete("/tickets/{Id}", async ([FromServices] ApplicationContext db, int Id) =>
	{
		global::AareonTechnicalTest.Models.Ticket? theTicket = db.Tickets.SingleOrDefault(t => t.Id == Id);
		if (theTicket == null)
		{
			return Results.NotFound();
		}

		db.Remove(theTicket);
		await db.SaveChangesAsync();
		return Results.NoContent();
	}
).Produces(StatusCodes.Status404NotFound).Produces(StatusCodes.Status204NoContent).WithName("DeleteTicketById").WithTags("Setters");

app.Logger.LogInformation("Starting AareonTechnicalTest {date}", DateTime.UtcNow);
app.Run();