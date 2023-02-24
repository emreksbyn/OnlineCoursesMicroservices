using FreeCourse.Web.Models.PhotoStockServiceModels;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services
{
    public class PhotoStockService : IPhotoStockService
    {
        private readonly HttpClient _httpClient;
        public PhotoStockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> DeletePhoto(string photoUrl)
        {
            var response = await _httpClient.DeleteAsync($"photos?photoUrl={photoUrl}");
            return response.IsSuccessStatusCode;
        }

        public async Task<PhotoViewModel> UploadPhoto(IFormFile photo)
        {
            if (photo == null || photo.Length <= 0) return null;

            var randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(photo.FileName)}";

            using MemoryStream memoryStream = new MemoryStream();
            await photo.CopyToAsync(memoryStream);

            MultipartFormDataContent multipartContent = new();
            // PhotosController da PhotoSave metoduna IFormFile nesnesine photo dedigimiz icin asagida "photo" dedik.
            multipartContent.Add(new ByteArrayContent(memoryStream.ToArray()), "photo", randomFileName);

            var response = await _httpClient.PostAsync("photos", multipartContent);

            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<PhotoViewModel>();
        }
    }
}