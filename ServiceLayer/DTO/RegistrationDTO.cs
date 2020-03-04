using System;

namespace ServiceLayer.DTO
{
    public class RegistrationDTO
    {
        public int RegistrationId { get; set; }
        public string RegistrationCode { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Index { get; set; }
        public int Privilege { get; set; }
        /// <summary>
        /// Date and time when registration is created
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Used registrations can not be used anymore
        /// </summary>
        public bool Used { get; set; }
    }
}
