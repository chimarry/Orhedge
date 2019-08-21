using DatabaseLayer;
using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.AutoMapper;
using ServiceLayer.DTO;
using ServiceLayer.Enum;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Helpers;
using ServiceLayer.Students.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Services
{
    public class StudyMaterialService : IStudyMaterialService
    {


        private readonly OrhedgeContext _context;
        private readonly IErrorHandler _errorHandler;
        private readonly IServicesExecutor<StudyMaterialDTO, StudyMaterial> _servicesExecutor;
        public StudyMaterialService(OrhedgeContext context, IErrorHandler errorHandler, IServicesExecutor<StudyMaterialDTO, StudyMaterial> servicesExecutor)
        {
            _context = context;
            _errorHandler = errorHandler;
            _servicesExecutor = servicesExecutor;
        }

        public async Task<DbStatus> Add(StudyMaterialDTO studyMaterialDTO)
        {
            try
            {
                await _servicesExecutor.TryAdding(studyMaterialDTO, x => x.Name == studyMaterialDTO.Name && x.StudentId == studyMaterialDTO.StudentId && x.UploadDate == studyMaterialDTO.UploadDate && x.Deleted == false);
                return DbStatus.SUCCESS;
            }
            catch (Exception ex)
            {
                return await _errorHandler.HandleException(ex);
            }
        }

        public async Task<DbStatus> Delete(int id)
        {
            try
            {
                var dbStudyMaterial = await _servicesExecutor.GetSingleOrDefault((x => x.StudyMaterialId == id && x.Deleted == false));
                dbStudyMaterial.Deleted = true;
                await _servicesExecutor.TryDeleting(dbStudyMaterial);
                return DbStatus.SUCCESS;
            }
            catch (Exception ex)
            {
                return await _errorHandler.HandleException(ex);
            }
        }

        public async Task<IList<StudyMaterialDTO>> GetAll()
        {
            try
            {
                return await _servicesExecutor.TryGettingAll(x => x.Deleted == false);
            }
            catch (Exception ex)
            {
                await _errorHandler.HandleException(ex);
                return new List<StudyMaterialDTO>();
            }
        }


        public async Task<StudyMaterialDTO> GetById(int id)
        {
            try
            {
                return await _servicesExecutor.TryGettingOne(x => x.StudyMaterialId == id && x.Deleted == false);
            }
            catch (Exception ex)
            {
                await _errorHandler.HandleException(ex);
                return null;
            }
        }

        public Task<IList<StudyMaterialDTO>> GetRange(int startPosition, int numberOfItems)
        {
            throw new NotImplementedException();
        }

        public async Task<DbStatus> Update(StudyMaterialDTO studyMaterialDTO)
        {
            try
            {
                await _servicesExecutor.TryUpdating(studyMaterialDTO, x => x.StudyMaterialId == studyMaterialDTO.StudyMaterialId && x.Deleted == false);
                return DbStatus.SUCCESS;
            }
            catch (Exception ex)
            {
                return await _errorHandler.HandleException(ex);
            }
        }
    }
}


