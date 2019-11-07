using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Helpers;
using ServiceLayer.Students.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Services
{
    public class StudyMaterialService : IStudyMaterialService
    {

        private readonly IServicesExecutor<StudyMaterialDTO, StudyMaterial> _servicesExecutor;
        public StudyMaterialService(IServicesExecutor<StudyMaterialDTO, StudyMaterial> servicesExecutor)
        {
            _servicesExecutor = servicesExecutor;
        }

        public async Task<Status> Add(StudyMaterialDTO studyMaterialDTO)
        {
            return await _servicesExecutor.Add(studyMaterialDTO, x => x.Name == studyMaterialDTO.Name && x.StudentId == studyMaterialDTO.StudentId && x.UploadDate == studyMaterialDTO.UploadDate && x.Deleted == false);
        }

        public async Task<Status> Delete(int id)
        {
            StudyMaterial dbStudyMaterial = await _servicesExecutor.GetSingleOrDefault((x => x.StudyMaterialId == id && x.Deleted == false));
            dbStudyMaterial.Deleted = true;
            return await _servicesExecutor.Delete(dbStudyMaterial);
        }

        public async Task<List<StudyMaterialDTO>> GetAll()
        {
            return await _servicesExecutor.GetAll(x => x.Deleted == false);
        }


        public async Task<StudyMaterialDTO> GetById(int id)
        {
            return await _servicesExecutor.GetOne(x => x.StudyMaterialId == id && x.Deleted == false);
        }

        public Task<StudyMaterialDTO> GetOne(Predicate<StudyMaterialDTO> condition)
        {
            throw new NotImplementedException();
        }

        public async Task<List<StudyMaterialDTO>> GetRange(int startPosition, int numberOfItems)
        {
            return await _servicesExecutor.GetRange(startPosition, numberOfItems, x => x.Deleted == false);
        }

        public async Task<Status> Update(StudyMaterialDTO studyMaterialDTO)
        {
            await _servicesExecutor.Update(studyMaterialDTO, x => x.StudyMaterialId == studyMaterialDTO.StudyMaterialId && x.Deleted == false);
            return Status.SUCCESS;
        }
    }
}


