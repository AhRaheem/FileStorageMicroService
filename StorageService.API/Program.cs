
namespace StorageService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var mongoSettings = builder.Configuration.GetSection("MongoDB");
            var mongoConnectionString = mongoSettings.GetValue<string>("ConnectionString");
            var mongoDatabaseName = mongoSettings.GetValue<string>("DatabaseName");

            builder.Services.AddSingleton(new MongoDbContext(mongoConnectionString, mongoDatabaseName));

            builder.Services.AddScoped<IFileRepository, FileRepository>();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddFileCommand).Assembly));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteFileCommand).Assembly));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RetrieveFileQuery).Assembly));

            builder.Services.AddGrpc();

            builder.Services.AddSingleton<RabbitMqConnectionFactory>();
            builder.Services.AddScoped<IEventPublisher, EventPublisher>();

            // Register other dependencies and services
            builder.Services.AddControllers();

            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Run();
        }
    }
}
