using DatabaseLayer;
using DatabaseLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

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
            Student student1 = new Student()
            {
                Index = "1161/16",
                Name = "Marija",
                LastName = "Novakovic",
                Email = "marija.novakovic@gmail.com",
                Privilege = 0,
                PasswordHash = "sahdd...12143",
                Username = "light",
                Salt = "123"
            };
            Student student2 = new Student()
            {
                Index = "1189/16",
                Name = "Filip",
                LastName = "Ivic",
                Email = "filip.ivic@gmail.com",
                Privilege = 0,
                PasswordHash = "sa.8...12143",
                Username = "dark",
                Salt = "789"
            };
            Student student3 = new Student()
            {
                Index = "1101/19",
                Name = "Ivko",
                LastName = "Lukic",
                Email = "ivko@yahoo.com",
                Privilege = 1,
                PasswordHash = "sooo...10p3",
                Username = "blue",
                Salt = "993"
            };
            context.Students.Add(student1);
            context.Students.Add(student2);
            context.Students.Add(student3);
            context.SaveChanges();
        }
    }
}
