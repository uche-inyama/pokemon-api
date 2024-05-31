using pokemonapi.Data;
using pokemonapi.Models;
using pokemonapi.Interfaces;

namespace pokemonapi.Repositories
{
    public class ReviewerRepository : IReviewerRepository
    {
        public readonly DataContext _context;

        public ReviewerRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Reviewer> GetReviewers()
        { 
            return _context.Reviewers.OrderBy(reviewer => reviewer.Id).ToList();
        }
        public Reviewer GetReviewer(int reviewerId)
        {
            return _context.Reviewers.Where(r => r.Id == reviewerId).FirstOrDefault();
        }
        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _context.Reviews.Where(r => r.Id == reviewerId).ToList();
        }
        public bool ReviewerExists(int reviewerId)
        {
            return _context.Reviewers.Any(r => r.Id == reviewerId);
        }
        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Add(reviewer);
            return Save();
        }
        public bool UpdateReviewer(Reviewer reviewer)
        {
            _context.Update(reviewer);
            return Save();
        }
        public bool DeleteReviwer(Reviewer reviewer)
        {
            _context.Remove(reviewer);
            return Save();
        }
        public bool Save()
        { 
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
