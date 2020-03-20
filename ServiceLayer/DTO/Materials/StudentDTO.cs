using DatabaseLayer.Enums;
using System;

namespace ServiceLayer.DTO
{
    public class StudentDTO
    {
        public int StudentId { get; set; }


        public string Name { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Index { get; set; }

        public string Photo { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public double Rating { get; set; }

        public StudentPrivilege Privilege { get; set; }

        public bool Deleted { get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public override bool Equals(object obj)
        {
            return obj is StudentDTO dTO &&
                   Name == dTO.Name &&
                   LastName == dTO.LastName &&
                   Username == dTO.Username &&
                   Index == dTO.Index &&
                   Email == dTO.Email;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, LastName, Username, Index, Email);
        }
    }
}
