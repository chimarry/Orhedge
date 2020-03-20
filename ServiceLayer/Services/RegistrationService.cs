using DatabaseLayer;
using DatabaseLayer.Entity;
using ServiceLayer.AutoMapper;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.Services
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

        public async Task<RegistrationDTO> GetSingleOrDefault(Predicate<RegistrationDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);

        public async Task<Status> Update(RegistrationDTO registrationDTO)
            => await _servicesExecutor.Update(registrationDTO, reg => reg.RegistrationId == registrationDTO.RegistrationId);

    }
}
