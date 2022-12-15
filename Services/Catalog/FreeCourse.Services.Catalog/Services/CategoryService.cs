using AutoMapper;
using FreeCourse.Services.Catalog.Dtos.Category;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using Shared.FreeCourse.Shared.Dtos;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;
        public CategoryService(IMapper mapper, IDatabaseSettings dbSettings)
        {
            MongoClient client = new MongoClient(dbSettings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(dbSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(dbSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            List<Category> categories = await _categoryCollection.Find(category => true).ToListAsync();
            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            Category category = await _categoryCollection.Find<Category>(c => c.Id == id).FirstOrDefaultAsync();
            if (category == null)
                return Response<CategoryDto>.Fail("Category not found", 404);

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }

        public async Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            await _categoryCollection.InsertOneAsync(_mapper.Map<Category>(categoryCreateDto));
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(categoryCreateDto), 200);
        }
    }
}