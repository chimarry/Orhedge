using DatabaseLayer;
using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
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

        public async Task<ResultMessage<bool>> ChangeRating(int studyMaterialId, double rating)
        {
            StudyMaterial studyMaterial = await _servicesExecutor.GetSingleOrDefault((StudyMaterial x) => x.StudyMaterialId == studyMaterialId && !x.Deleted);
            if (studyMaterial == null)
                return new ResultMessage<bool>(false, OperationStatus.NotFound);
            studyMaterial.TotalRating = rating;
            return await _servicesExecutor.SaveChanges();
        }

        public async Task<ResultMessage<bool>> Delete(int id)
            => await _servicesExecutor.Delete((StudyMaterial x) => x.StudyMaterialId == id && !x.Deleted, x => { x.Deleted = true; return x; });

        public async Task<ResultMessage<StudyMaterialDTO>> GetSingleOrDefault(Predicate<StudyMaterialDTO> condition)
             => await _servicesExecutor.GetSingleOrDefault(condition);

        public async Task<ResultMessage<StudyMaterialDTO>> Update(StudyMaterialDTO studyMaterialDTO)
             => await _servicesExecutor.Update(studyMaterialDTO, x => x.StudyMaterialId == studyMaterialDTO.StudyMaterialId && x.Deleted == false);
    }
}


