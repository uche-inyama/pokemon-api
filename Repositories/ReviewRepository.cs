using pokemonapi.Data;
using pokemonapi.Interfaces;
using pokemonapi.Models;

namespace pokemonapi.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        public readonly DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Review> GetReviews()
        { 
            return _context.Reviews.OrderBy(review => review.Id).ToList();
        }
        public Review GetReview(int ReviewId)
        {
            return _context.Reviews.Where(r => r.Id == ReviewId).FirstOrDefault();
        }
        public ICollection<Review> GetReviewsForAPokemon(int PokemonId)
        {
            return _context.Reviews.Where(r => r.Pokemon.Id == PokemonId).ToList();  
        }
        public bool ReviewExists(int reviewId)
        { 
            return _context.Reviews.Any(r => r.Id == reviewId);
        }
        public bool CreateReview(Review review)
        {
            _context.Add(review);
            return Save();
        }
        public bool UpdateReview(Review review)
        { 
           var saved = _context.Update(review);
            return Save();
        }

        public bool Save()
        { 
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return Save();
        }
    }
}
