using System;

namespace DatabaseLayer.Entity
{
    public class Registration
    {
        public int RegistrationId { get; set; }
        public string RegistrationCode { get; set; }
        public string Email { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Used { get; set; }
    }
}
