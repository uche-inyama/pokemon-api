using pokemonapi.Models;
using pokemonapi.Interfaces;
using pokemonapi.Data;
using pokemonapi.DTO;

namespace pokemonapi.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        public readonly DataContext _context;
        public PokemonRepository(DataContext context)
        {
            _context = context;
        }
    
        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.OrderBy(p => p.Id).ToList();
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var OwnerObject = _context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var CategoryObject = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = OwnerObject,
                Pokemon = pokemon
            };

            _context.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = CategoryObject,
                Pokemon = pokemon
            };

            _context.Add(pokemonCategory);

            _context.Add(pokemon);

            return Save();
        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemons.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(String name)
        {
            return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public bool DeletePokemon(Pokemon pokemon) 
        {
            _context.Remove(pokemon);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool PokemonExists(int PokemonId)
        {
            return _context.Pokemons.Any(p => p.Id == PokemonId);
        }

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            _context.Pokemons.Update(pokemon);
            return Save();
        }

        public Pokemon GetPokemonTrimToUpper(PokemonDTO pokemonCreate)
        {
          return GetPokemons().Where(c => c.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();
        }
  }
}
