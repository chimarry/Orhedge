using DatabaseLayer.Enums;
using System;

namespace DatabaseLayer.Entity
{
    public class Registration
    {
        public int RegistrationId { get; set; }
        public string RegistrationCode { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Index { get; set; }
        public StudentPrivilege Privilege { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Used { get; set; }
    }
}
