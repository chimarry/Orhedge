using DatabaseLayer;
using DatabaseLayer.Entity;
using DatabaseLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTests.Common
{
    public static class DataGenerator
    {
        public static void Initialize(OrhedgeContext context)
        {
            InitializeStudents(context);
        }

        private static void InitializeStudents(OrhedgeContext context)
        {
            (string password, string salt) = Utilities.CreateHashAndSalt("lightPassword");
            Student student1 = new Student()
            {
                Index = "1161/16",
                Name = "Marija",
                LastName = "Novakovic",
                Email = "marija.novakovic@gmail.com",
                Privilege = 0,
                PasswordHash = password,
                Username = "light",
                Salt = salt,
                Description = "light description"
            };

            (password, salt) = Utilities.CreateHashAndSalt("darkPassword");
            Student student2 = new Student()
            {
                Index = "1189/16",
                Name = "Filip",
                LastName = "Ivic",
                Email = "filip.ivic@gmail.com",
                Privilege = 0,
                PasswordHash = password,
                Username = "dark",
                Salt = salt,
                Description = "dark description"
            };

            (password, salt) = Utilities.CreateHashAndSalt("bluePassword");
            Student student3 = new Student()
            {
                Index = "1101/19",
                Name = "Ivko",
                LastName = "Lukic",
                Email = "ivko@yahoo.com",
                Privilege = StudentPrivilege.Normal,
                PasswordHash = password,
                Username = "blue",
                Salt = salt,
                Description = "blue description"
            };
            context.Students.Add(student1);
            context.Students.Add(student2);
            context.Students.Add(student3);
            context.SaveChanges();
        }
    }
}
