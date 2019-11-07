using DatabaseLayer;
using DatabaseLayer.Entity;
using ServiceLayer.AutoMapper;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Helpers;
using ServiceLayer.Students.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly OrhedgeContext _context;
        private readonly IErrorHandler _errorHandler;
        private readonly IServicesExecutor<RegistrationDTO, Registration> _servicesExecutor;

        public RegistrationService(
            OrhedgeContext context, 
            IErrorHandler errorHandler, 
            IServicesExecutor<RegistrationDTO, Registration> servicesExecutor)
            =>
            (_context, _errorHandler, _servicesExecutor) = (context, errorHandler, servicesExecutor);
        

        public async Task<Status> Add(RegistrationDTO registrationDTO)
            => await _servicesExecutor.Add(registrationDTO, reg => reg.RegistrationCode == registrationDTO.RegistrationCode);


        public async Task<Status> Delete(int id)
        {
            throw new NotSupportedException();
        }

        public async Task<List<RegistrationDTO>> GetAll()
            => await _servicesExecutor.GetAll(x => true);


        public async Task<RegistrationDTO> GetById(int id) 
            => await _servicesExecutor.GetOne(x => x.RegistrationId == id);


        public async Task<RegistrationDTO> GetOne(Predicate<RegistrationDTO> condition)
            => await _servicesExecutor.GetOne(
                    reg => condition(Mapping.Mapper.Map<RegistrationDTO>(reg)));

        public Task<List<RegistrationDTO>> GetRange(int startPosition, int numberOfItems)
        {
            throw new NotImplementedException();
        }

        public async Task<Status> Update(RegistrationDTO registrationDTO) 
            => await _servicesExecutor.Update(registrationDTO, reg => reg.RegistrationId == registrationDTO.RegistrationId);

    }
}
