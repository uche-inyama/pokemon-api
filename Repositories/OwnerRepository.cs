using pokemonapi.Interfaces;
using pokemonapi.Data;
using pokemonapi.Models;


namespace pokemonapi.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {

        public readonly DataContext _context;
        public OwnerRepository(DataContext context) 
        {
            _context = context;
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.OrderBy(p => p.Id).ToList();
        }

        public Owner GetOwner(int ownerId)
        { 
            return _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerofAPokemon(int pokemonId)
        { 
            return _context.PokemonOwners.Where(p => p.PokemonId == pokemonId).Select(p => p.Owner).ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
        { 
            return _context.PokemonOwners.Where(o => o.OwnerId == ownerId).Select(o => o.Pokemon).ToList();
        }

        public bool CreateOwner(Owner owner)
        {
            _context.Add(owner);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool UpdateOwner(Owner owner)
        {
            _context.Update(owner);
            return Save();  
        }

        public bool DeleteOwner(Owner owner)
        {
            _context.Remove(owner);
            return Save();
        }

        public bool OwnerExists(int ownerId)
        {
            return _context.Owners.Any(o => o.Id == ownerId);
        }
    }
}
