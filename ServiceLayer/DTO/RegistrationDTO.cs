using System;

namespace ServiceLayer.DTO
{
    public class RegistrationDTO
    {
        public int RegistrationId { get; set; }
        public string RegistrationCode { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// Registration date and time
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Used registrations can not be used anymore
        /// </summary>
        public bool Used { get; set; }
    }
}
