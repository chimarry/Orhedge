using DatabaseLayer.Enums;
using ServiceLayer.DTO;
using ServiceLayer.Students.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.DTO
{
    public class DetailedSemesterDTO
    {
        public Semester Semester { get; set; }

        public Dictionary<StudyProgram, List<CourseDTO>> Courses { get; }

        public DetailedSemesterDTO()
        {
            Courses = new Dictionary<StudyProgram, List<CourseDTO>>();
            foreach (StudyProgram sp in Enum.GetValues(typeof(StudyProgram)))
                Courses.Add(sp, new List<CourseDTO>());
        }

        public override bool Equals(object obj)
        {
            return obj is DetailedSemesterDTO model &&
                   Semester == model.Semester;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Semester);
        }

        public static implicit operator DetailedSemesterDTO(Semester name)
        {
            return new DetailedSemesterDTO() { Semester = name };
        }
    }
}

public static class SemesterExtensionMethods
{
    public static List<CourseDTO> GetCourses(this DetailedSemesterDTO semester, StudyProgram studyProgram)
    {
        return semester.Courses[studyProgram];
    }
}
