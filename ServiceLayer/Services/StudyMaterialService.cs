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

        public async Task<ResultMessage<StudyMaterialDTO>> Add(StudyMaterialDTO studyMaterialDTO)
             => await _servicesExecutor.Add(studyMaterialDTO, x => x.Name == studyMaterialDTO.Name && x.StudentId == studyMaterialDTO.StudentId
                                                                     && x.UploadDate == studyMaterialDTO.UploadDate && x.Deleted == false);

        public async Task<ResultMessage<bool>> Delete(int id)
            => await _servicesExecutor.Delete((StudyMaterial x) => x.StudyMaterialId == id && !x.Deleted, x => { x.Deleted = true; return x; });

        public async Task<ResultMessage<StudyMaterialDTO>> GetSingleOrDefault(Predicate<StudyMaterialDTO> condition)
             => await _servicesExecutor.GetSingleOrDefault(condition);

        public async Task<ResultMessage<StudyMaterialDTO>> Update(StudyMaterialDTO studyMaterialDTO)
             => await _servicesExecutor.Update(studyMaterialDTO, x => x.StudyMaterialId == studyMaterialDTO.StudyMaterialId && x.Deleted == false);
    }
}


