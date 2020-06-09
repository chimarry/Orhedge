using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class RegistrationService : IRegistrationService
    {

        private readonly IServicesExecutor<RegistrationDTO, Registration> _servicesExecutor;

        public RegistrationService(
            IServicesExecutor<RegistrationDTO, Registration> servicesExecutor)
            => _servicesExecutor = servicesExecutor;


        public async Task<ResultMessage<RegistrationDTO>> Add(RegistrationDTO registrationDTO)
            => await _servicesExecutor.Add(registrationDTO, reg => reg.RegistrationCode == registrationDTO.RegistrationCode);

        public Task<ResultMessage<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultMessage<RegistrationDTO>> GetSingleOrDefault(Predicate<RegistrationDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);

        public async Task<ResultMessage<RegistrationDTO>> Update(RegistrationDTO registrationDTO)
            => await _servicesExecutor.Update(registrationDTO, reg => reg.RegistrationId == registrationDTO.RegistrationId);

    }
}
