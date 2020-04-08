using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Shared;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface IStudyMaterialService : ICRUDServiceTemplate<StudyMaterialDTO>, ISelectableServiceTemplate<StudyMaterialDTO>
    {
    }
}
