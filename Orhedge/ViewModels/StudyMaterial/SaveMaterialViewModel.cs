using System;
using System.Buffers.Text;
using System.Text;

namespace Orhedge.ViewModels.StudyMaterial
{
    public class SaveMaterialViewModel
    {
        public string Course { get; set; }

        public int Category { get; set; }

        public string FileExtension { get; set; }

        public string FileEncoding { get; set; }

        public string FileData { get; set; }
    }

    public static class SaveMaterialExtensionMethods
    {
        public static byte[] GetData(this SaveMaterialViewModel model)
           => Convert.FromBase64String(model.FileData);
    }

}
