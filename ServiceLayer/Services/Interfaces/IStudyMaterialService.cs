using ServiceLayer.DTO;

namespace ServiceLayer.Services
{
    public interface IStudyMaterialService : ICRUDServiceTemplate<StudyMaterialDTO>, ISelectableServiceTemplate<StudyMaterialDTO>
    {
    }
}
