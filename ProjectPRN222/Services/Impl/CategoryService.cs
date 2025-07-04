using ProjectPRN222.DataAccessLayers.Interface;
using ProjectPRN222.Models;
using ProjectPRN222.Services.Interface;

namespace ProjectPRN222.Services.Impl
{
    public class CategoryService(ICategoryDA categoryDA) : ICategoryService
    {
        private readonly ICategoryDA _categoryDA = categoryDA;

        public List<Category> GetAllCategories()
        {
            return _categoryDA.GetAllCategories();
        }
    }
}
