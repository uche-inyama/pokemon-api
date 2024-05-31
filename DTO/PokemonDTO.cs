namespace pokemonapi.DTO
{
  public class PokemonDTO
  {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime BirthDate { get; set; }
  }
}