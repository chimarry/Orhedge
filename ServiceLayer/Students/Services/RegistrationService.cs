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

        private readonly IServicesExecutor<RegistrationDTO, Registration> _servicesExecutor;

        public RegistrationService(
            IServicesExecutor<RegistrationDTO, Registration> servicesExecutor)
            => _servicesExecutor = servicesExecutor;


        public async Task<Status> Add(RegistrationDTO registrationDTO)
            => await _servicesExecutor.Add(registrationDTO, reg => reg.RegistrationCode == registrationDTO.RegistrationCode);

        public Task<Status> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<RegistrationDTO> GetById(int id)
            => await _servicesExecutor.GetSingleOrDefault(x => x.RegistrationId == id);

        public async Task<RegistrationDTO> GetSingleOrDefault(Predicate<RegistrationDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(
                    reg => condition(Mapping.Mapper.Map<RegistrationDTO>(reg)));

        public async Task<Status> Update(RegistrationDTO registrationDTO)
            => await _servicesExecutor.Update(registrationDTO, reg => reg.RegistrationId == registrationDTO.RegistrationId);

        public async Task<int> Count()
            => await _servicesExecutor.Count();
    }
}
