using pokemonapi.DTO;
using pokemonapi.Models;

namespace pokemonapi.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        bool CreatePokemon(int ownerId, int CategoryId, Pokemon pokemon);
        bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon);
        Pokemon GetPokemonTrimToUpper(PokemonDTO pokemonCreate);      
        bool DeletePokemon(Pokemon pokemon);
        bool PokemonExists(int id);
        bool Save();
    
  }
}
