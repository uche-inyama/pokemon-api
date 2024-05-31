using pokemonapi.Data;
using pokemonapi.Interfaces;
using pokemonapi.Models;


// Repository is where you put all your database calls

namespace pokemonapi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonByCategoryId(int CategoryId)
        {
            return _context.PokemonCategories.Where(e => e.CategoryId == CategoryId)
                .Select(c => c.Pokemon).ToList();
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }

        public bool DeleteCategory(Category category) 
        {
            _context.Remove(category);
            return Save();
        }

    }
    
}
