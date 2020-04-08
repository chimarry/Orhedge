using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using ServiceLayer.Shared;
using ServiceLayer.Students.Shared;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class StudyMaterialService : BaseService<StudyMaterialDTO, StudyMaterial>, IStudyMaterialService
    {
        public StudyMaterialService(IServicesExecutor<StudyMaterialDTO, StudyMaterial> servicesExecutor)
            : base(servicesExecutor)
        {
        }

        public async Task<Status> Add(StudyMaterialDTO studyMaterialDTO)
             => await _servicesExecutor.Add(studyMaterialDTO, x => x.Name == studyMaterialDTO.Name && x.StudentId == studyMaterialDTO.StudentId
                                                                     && x.UploadDate == studyMaterialDTO.UploadDate && x.Deleted == false);

        public async Task<Status> Delete(int id)
        {
            StudyMaterial dbStudyMaterial = await _servicesExecutor.GetOne((x => x.StudyMaterialId == id && x.Deleted == false));
            dbStudyMaterial.Deleted = true;
            return await _servicesExecutor.Delete(dbStudyMaterial);
        }

        public async Task<StudyMaterialDTO> GetSingleOrDefault(Predicate<StudyMaterialDTO> condition)
             => await _servicesExecutor.GetSingleOrDefault(condition);

        public async Task<Status> Update(StudyMaterialDTO studyMaterialDTO)
             => await _servicesExecutor.Update(studyMaterialDTO, x => x.StudyMaterialId == studyMaterialDTO.StudyMaterialId && x.Deleted == false);
    }
}


