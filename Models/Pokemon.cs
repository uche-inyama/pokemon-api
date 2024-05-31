namespace pokemonapi.Models
{
  public class Pokemon
  {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public ICollection<Review> Reviews { get; set; } = null!;
    public ICollection<PokemonCategory> PokemonCategories { get; set; } = null!;
    public ICollection<PokemonOwner> PokemonOwners { get; set; } = null!;
  }
  
}