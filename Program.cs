using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using pokemonapi.Data;
using pokemonapi.Interfaces;
using pokemonapi.Repositories;
using pokemonapi.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewerRepository, ReviewerRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPort = Environment.GetEnvironmentVariable("DB_PORT");

// var connectionString = "Data Source=pokemondb,1433;Initial Catalog=pokemon;User ID=sa;Password=pa55word;Integrated Security=True;" +
//     "Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
 
// builder.Services.AddDbContext<DataContext>(o => o.UseSqlServer(connectionString));

// builder.Services.AddDbContext<DataContext>(options =>
// {
//     options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
// });

var connectionString ="Host=server;Database=pokemon;port=5432;Username=postgres;Password=pa55word";
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(connectionString));

// builder.Services.AddDbContext<DataContext>(options =>
// {
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
// });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



