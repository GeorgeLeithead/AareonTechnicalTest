namespace AareonTechnicalTest
{
	/// <summary>Definition for the EF application context.</summary>
	public class ApplicationContext : DbContext
	{
		/// <summary>Initialises a new instance of the <see cref="ApplicationContext"/> class.</summary>
		/// <param name="options">Options to be used by a DB context.</param>
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{
			string? envDir = Environment.CurrentDirectory;
			DatabasePath = $"{envDir}{Path.DirectorySeparatorChar}Ticketing.db";
		}

		/// <summary>Local environment path to database.</summary>
		public string DatabasePath { get; set; }

		/// <summary>Used to query and save instances of <see cref="Person"/>.</summary>
		public virtual DbSet<Person> Persons { get; set; } = null!;

		/// <summary>Used to query and save instances of <see cref="Ticket"/>.</summary>
		public virtual DbSet<Ticket> Tickets { get; set; } = null!;

		/// <summary>Used to query and save instances of <see cref="TicketNote"/>.</summary>
		public virtual DbSet<TicketNote> TicketNotes { get; set; } = null!;

		/// <summary>Configuration of the database.</summary>
		/// <param name="options">Configure the database connection and other options.</param>
		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			options.UseSqlite($"Data Source={DatabasePath}");
		}

		/// <summary>Further configure the model.</summary>
		/// <param name="modelBuilder">Model the defines the shape of entities, their relationships and how they map to the database.</param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			PersonConfig.Configure(modelBuilder);
			TicketConfig.Configure(modelBuilder);
			TicketNoteConfig.Configure(modelBuilder);
		}
	}
}