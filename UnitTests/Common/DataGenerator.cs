using DatabaseLayer;
using DatabaseLayer.Entity;
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

        public static async Task AddCategory(OrhedgeContext context, string name, int order)
        {
            ForumCategory forumCategory = new ForumCategory
            {
                Name = name,
                Order = order
            };

            context.ForumCategories.Add(forumCategory);
            await context.SaveChangesAsync();
        }

        public static async Task AddDiscussions(OrhedgeContext context, string catName, int count)
        {
            // We expect to have a least one student
            Student student = context.Students.First();
            ForumCategory forumCategory = context.ForumCategories.First(cat => cat.Name == catName);
            IEnumerable<Discussion> disc = Enumerable.Range(1, count).Select(n =>
            new Discussion
            {
                Student = student,
                ForumCategory = forumCategory,
                Content = "Test",
                Created = DateTime.UtcNow,
                LastPost = DateTime.UtcNow,
                Title = $"Title({n})"
            });
            await context.Discussions.AddRangeAsync(disc);
            await context.SaveChangesAsync();
        }

        public static async Task AddQuestions(OrhedgeContext context, string catName, int count)
        {
            // We expect to have a least one student
            Student student = context.Students.First();
            ForumCategory forumCategory = context.ForumCategories.First(cat => cat.Name == catName);
            IEnumerable<Question> questions = Enumerable.Range(1, count).Select(n =>
            new Question
            {
                Student = student,
                ForumCategory = forumCategory,
                Content = "Test",
                Created = DateTime.UtcNow,
                LastPost = DateTime.UtcNow,
                Title = $"Title({n})"
            });
            await context.Questions.AddRangeAsync(questions);
            await context.SaveChangesAsync();
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
