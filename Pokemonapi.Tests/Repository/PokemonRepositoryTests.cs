using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using pokemonapi.Data;
using pokemonapi.Models;
using pokemonapi.Repositories;
using Xunit;
using FluentAssertions;

namespace Pokemonapi.Tests.Repository
{
  public class PokemonRepositoryTests
  {
    private async Task<DataContext> GetDatabaseContext()
    {
      var options = new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;
      var databaseContext = new DataContext(options);
      databaseContext.Database.EnsureCreated();
      if(await databaseContext.PokemonCategories.CountAsync() <= 0)
      {
        for(int i = 1; i <= 0; i++)
        {
          databaseContext.Pokemons.Add(
            new Pokemon()
            {
              Name = "Pikachu",
              BirthDate = new DateTime(1903, 1, 1),
              PokemonCategories = new List<PokemonCategory>()
              {
                new PokemonCategory { Category = new Category() { Name = "Electric"}}
              },
              Reviews = new List<Review>()
              {
                new Review { Title="Pikachu",Text = "Pickahu is the best pokemon, because it is electric",
                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                new Review { Title="Pikachu", Text = "Pickachu is the best a killing rocks",
                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                new Review { Title="Pikachu",Text = "Pickchu, pickachu, pikachu",
                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
              }
            });
          await databaseContext.SaveChangesAsync();
        }
      }
      return databaseContext;
    }

    [Fact]
    public async void PokemonRepository_GetPokemon_ReturnsPokemon()
    {
      var name = "Pikachu";
      var dbContext = await GetDatabaseContext();
      var pokemonRepository = new PokemonRepository(dbContext);

      var result = pokemonRepository.GetPokemon(name);

      result.Should().NotBeNull();
      result.Should().BeOfType<Pokemon>();
    }
  }
}