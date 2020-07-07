using DatabaseLayer.Enums;

namespace ServiceLayer.Helpers
{
    public static class SemesterExtensionMethods
    {
        /// <summary>
        /// Each semester belongs to specific study year, and this method enables 
        /// user to get corresponding study year based on semester value. 
        /// </summary>
        /// <param name="semester">Specified semester</param>
        /// <returns></returns>
        public static int GetStudyYear(this Semester semester)
        {
            switch (semester)
            {
                case Semester.First:
                case Semester.Second: return 0;
                case Semester.Third:
                case Semester.Forth: return 1;
                case Semester.Fifth:
                case Semester.Sixth: return 2;
                case Semester.Seventh:
                case Semester.Eighth: return 3;
            }
            return 0;
        }
    }
}
