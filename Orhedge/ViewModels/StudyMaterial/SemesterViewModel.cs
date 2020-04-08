using DatabaseLayer.Enums;
using Orhedge.ViewModels.StudyMaterial;
using System;
using System.Collections.Generic;

namespace Orhedge.ViewModels.StudyMaterial
{
    public class SemesterViewModel
    {
        public Semester Semester;

        public Dictionary<StudyProgram, List<IndexCourseViewModel>> Courses { get; }

        public SemesterViewModel()
        {
            Courses = new Dictionary<StudyProgram, List<IndexCourseViewModel>>();
            foreach (StudyProgram sp in Enum.GetValues(typeof(StudyProgram)))
                Courses.Add(sp, new List<IndexCourseViewModel>());
        }


        public override bool Equals(object obj)
        {
            return obj is SemesterViewModel model &&
                   Semester == model.Semester;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Semester);
        }
    }
}

public static class SemesterExtensionMethods
{
    public static List<IndexCourseViewModel> GetCourses(this SemesterViewModel semester, StudyProgram studyProgram)
    {
        return semester.Courses[studyProgram];
    }
}


