namespace pokemonapi.DTO
{
  public class ReviewDTO
  {
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;
  }
}