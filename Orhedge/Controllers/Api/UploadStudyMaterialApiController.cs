using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orhedge.Helpers;
using Orhedge.ViewModels.StudyMaterial;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Services;
using ServiceLayer.Students.Shared;
using System;
using System.Threading.Tasks;

namespace Orhedge.Controllers
{
    [Route("api/UploadMaterialApi")]
    [Authorize]
    public class UploadStudyMaterialApiController : ControllerBase
    {
        private readonly IStudyMaterialManagementService _studyMaterialManagementService;

        public UploadStudyMaterialApiController(IStudyMaterialManagementService studyMaterialManagementService)
        {
            _studyMaterialManagementService = studyMaterialManagementService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Post([FromBody]SaveMaterialViewModel value)
        {
            // TODO: Upload form must be changed, so material will be filled in different way
            byte[] fileData = value.GetData();
            BasicFileInfo fileInfo = new BasicFileInfo(4, fileData);
            fileInfo.GenerateFileName(value.FileExtension);
            StudyMaterialDTO studyMaterial = new StudyMaterialDTO()
            {
                CategoryId = value.Category,
                Name = fileInfo.FileName,
                StudentId = this.GetUserId(),
                UploadDate = DateTime.Now,
            };
            ResultMessage<bool> isSavedResult = await _studyMaterialManagementService.SaveMaterial(studyMaterial, fileInfo);
            string newUrl = Url.Link("Default", new
            {
                Controller = "StudyMaterial",
                Action = "Index"
            });
            return Redirect(newUrl);
        }
    }
}
