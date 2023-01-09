using FreeCourse.Web.Models;
using FreeCourse.Web.Models.CatalogServiceModels;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services
{
    public class CatalogService : ICatalogService
    {
        #region Ctor
        private readonly HttpClient _httpClient;
        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        } 
        #endregion

        #region Course
        public async Task<List<CourseViewModel>> GetAllCoursesAsync()
        {
            // localhost:5000/services/catalog/course
            //var response = await _httpClient.GetAsync("course");
            throw new NotImplementedException();

        }

        public Task<CourseViewModel> GetByCourseIdAsync(string courseId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCourseAsync(string courseId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Category
        public Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CategoryViewModel> GetByCategoryIdAsync(string categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateCategoryAsync(CategoryCreateInput categoryCreateInput)
        {
            throw new NotImplementedException();
        }
        #endregion                
    }
}