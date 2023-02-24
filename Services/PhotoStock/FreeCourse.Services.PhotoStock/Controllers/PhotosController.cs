using FreeCourse.Services.PhotoStock.Dtos;
using Microsoft.AspNetCore.Mvc;
using Shared.FreeCourse.Shared.ControllerBases;
using Shared.FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo != null && photo.Length > 0)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

                using FileStream stream = new FileStream(path, FileMode.CreateNew);
                await photo.CopyToAsync(stream, cancellationToken);

                string returnPath = photo.FileName;

                PhotoDto photoDto = new PhotoDto() { Url = returnPath };
                return CreateActionResultInstance<PhotoDto>(Response<PhotoDto>.Success(photoDto, 200));
            }
            return CreateActionResultInstance<PhotoDto>(Response<PhotoDto>.Fail("photo is empty", 400));
        }

        [HttpDelete]
        public IActionResult PhotoDelete(string photoUrl)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (!System.IO.File.Exists(path))
                return CreateActionResultInstance<NoContent>(Response<NoContent>.Fail("photo not found", 404));

            System.IO.File.Delete(path);
            return CreateActionResultInstance<NoContent>(Response<NoContent>.Success(204));
        }
    }
}