using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ServiceLayer.Shared
{
    public static class PathBuilder
    {
        public const string IdSeparator = "_";
        private const string studyMaterialsRootDirectoryName = "studyMaterials";
        private const string studyMaterialsCourseDirectoryNamePrefix = "course";
        private const string studyMaterialsCategoryNamePrefix = "category";
        private const string profilePictureRootDirectoryName = "profile_pictures";
        private const string profilePictureNamePrefix = "picture";
        private const string fileNameDataFormat = "dd-MM-yyyy-HH-mm-ss";

        public static string BuildPathForStudyMaterial(int courseId, int categoryId, string fileName) =>
            Path.Combine(studyMaterialsRootDirectoryName,
                         BuildName(studyMaterialsCourseDirectoryNamePrefix, courseId),
                         BuildName(studyMaterialsCategoryNamePrefix, categoryId),
                         DateTime.Now.ToString(fileNameDataFormat) + IdSeparator + fileName);

        public static string BuildPathForProfilePictures(int studentId) =>
            Path.Combine(profilePictureRootDirectoryName,
                         BuildName(profilePictureNamePrefix, studentId));


        public static string BuildName(string prefix, long id) => prefix + IdSeparator + id;
    }
}
