using ProjectPRN222.DataAccessLayers.Interface;
using ProjectPRN222.Models;

namespace ProjectPRN222.DataAccessLayers.Impl
{
    public class CategoryDAO : ICategoryDA
    {
        private readonly QuizOnlineContext _context = new();
        public List<Category> GetAllCategories()
        {
            return [.. _context.Categories];
        }
    }
}
