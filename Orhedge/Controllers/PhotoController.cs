using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.Students.Shared;

namespace Orhedge.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IProfileImageService _profImgService;

        public PhotoController(IProfileImageService profImgService)
        {
            _profImgService = profImgService;
        }

        
        public async Task<IActionResult> Profile(int id, int version)
        {
            // Version is used to prevent browsers loading old cached profile image when new is submitted 
            _ = version;
            ResultMessage<BasicFileInfo> result = await _profImgService.GetStudentProfileImage(id);
            if (result.IsSuccess)
                return File(result.Result.FileData, MediaTypeNames.Image.Jpeg);
            else
                return BadRequest();
        }
    }
}
