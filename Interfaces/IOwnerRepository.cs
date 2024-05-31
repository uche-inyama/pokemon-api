using pokemonapi.Models;

namespace pokemonapi.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();
        Owner GetOwner(int ownerId);
        ICollection<Owner> GetOwnerofAPokemon(int pokemonId);
        ICollection<Pokemon> GetPokemonByOwner(int ownerId);
        bool OwnerExists(int ownerId); 
        bool CreateOwner(Owner owner);
        bool Save();
        bool UpdateOwner(Owner owner);
        bool DeleteOwner(Owner owner);
    }
}
