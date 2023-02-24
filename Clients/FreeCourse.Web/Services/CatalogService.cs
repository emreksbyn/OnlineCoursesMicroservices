using FreeCourse.Web.Helpers;
using FreeCourse.Web.Models;
using FreeCourse.Web.Models.CatalogServiceModels;
using FreeCourse.Web.Services.Interfaces;
using Shared.FreeCourse.Shared.Dtos;
using System.Collections.Generic;

namespace FreeCourse.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;
        public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
        }

        #region Course
        public async Task<List<CourseViewModel>> GetAllCoursesAsync()
        {
            // localhost:5000/services/catalog/courses
            var response = await _httpClient.GetAsync("courses");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            Response<List<CourseViewModel>> responseData = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            responseData.Data.ForEach(x =>
            {
                x.Picture = _photoHelper.GetPhotoStockUrl(x.Picture);
            });

            return responseData.Data;
        }

        public async Task<CourseViewModel> GetByCourseIdAsync(string courseId)
        {
            // localhost:5000/services/catalog/courses/{courseId}
            var response = await _httpClient.GetAsync($"courses/{courseId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            Response<CourseViewModel> responseData = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();
            return responseData.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            // localhost:5000/services/catalog/courses/GetAllByUserId/{userId}
            var response = await _httpClient.GetAsync($"courses/GetAllByUserId/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            Response<List<CourseViewModel>> responseData = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            responseData.Data.ForEach(x =>
            {
                x.Picture = _photoHelper.GetPhotoStockUrl(x.Picture);
            });

            return responseData.Data;
        }

        public async Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
        {
            var resultPhoto = await _photoStockService.UploadPhoto(courseCreateInput.PhotoFormFile);

            if (resultPhoto != null) courseCreateInput.Picture = resultPhoto.Url;

            var response = await _httpClient.PostAsJsonAsync<CourseCreateInput>("courses", courseCreateInput);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            var resultPhoto = await _photoStockService.UploadPhoto(courseUpdateInput.PhotoFormFile);

            if (resultPhoto != null)
            {
                await _photoStockService.DeletePhoto(courseUpdateInput.Picture);
                courseUpdateInput.Picture = resultPhoto.Url;
            }

            var response = await _httpClient.PutAsJsonAsync<CourseUpdateInput>("courses", courseUpdateInput);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"courses/{courseId}");
            return response.IsSuccessStatusCode;
        }
        #endregion

        #region Category
        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            // localhost:5000/services/catalog/categories
            var response = await _httpClient.GetAsync("categories");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            Response<List<CategoryViewModel>> responseData = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();
            return responseData.Data;
        }

        public async Task<CategoryViewModel> GetByCategoryIdAsync(string categoryId)
        {
            // localhost:5000/services/catalog/categories/{categoryId}
            var response = await _httpClient.GetAsync($"categories/{categoryId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            Response<CategoryViewModel> responseData = await response.Content.ReadFromJsonAsync<Response<CategoryViewModel>>();
            return responseData.Data;
        }

        public async Task<bool> CreateCategoryAsync(CategoryCreateInput categoryCreateInput)
        {
            var response = await _httpClient.PostAsJsonAsync<CategoryCreateInput>("categories", categoryCreateInput);
            return response.IsSuccessStatusCode;
        }
        #endregion                
    }
}