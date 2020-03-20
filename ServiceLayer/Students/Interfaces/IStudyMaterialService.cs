using ServiceLayer.DTO;

namespace ServiceLayer.Students.Interfaces
{
    public interface IStudyMaterialService : ICRUDServiceTemplate<StudyMaterialDTO>, ISelectableServiceTemplate<StudyMaterialDTO>
    {
    }
}
