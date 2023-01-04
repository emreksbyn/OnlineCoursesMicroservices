using FreeCourse.Web.Models.CatalogServiceModels;
using System.Collections.Generic;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        #region Course
        Task<List<CourseViewModel>> GetAllCoursesAsync();
        Task<CourseViewModel> GetByCourseIdAsync(string courseId);
        Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId);

        Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput);
        Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput);
        Task<bool> DeleteCourseAsync(string courseId);
        #endregion

        #region Category
        Task<List<CategoryViewModel>> GetAllCategoriesAsync();
        Task<CategoryViewModel> GetByCategoryIdAsync(string categoryId);
        Task<bool> CreateCategoryAsync(CategoryCreateInput categoryCreateInput);
        #endregion
    }
}