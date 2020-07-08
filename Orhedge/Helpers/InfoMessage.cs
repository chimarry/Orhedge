using Microsoft.Extensions.Localization;
using Orhedge.Enums;

namespace Orhedge.Helpers
{
    public class InfoMessage
    {
        public IStringLocalizer<SharedResource> _stringLocalizer;

        public string CssClass { get; }

        public string Message { get; }

        public bool IsSet { get; } = true;

        public InfoMessage(IStringLocalizer<SharedResource> stringLocalizer, HttpReponseStatusCode code)
        {
            _stringLocalizer = stringLocalizer;
            switch (code)
            {
                case HttpReponseStatusCode.DatabaseError:
                    Message = _stringLocalizer[HttpReponseStatusCode.DatabaseError.ToString()];
                    CssClass = "alert";
                    break;
                case HttpReponseStatusCode.Exists:
                    Message = _stringLocalizer[HttpReponseStatusCode.Exists.ToString()];
                    CssClass = "alert";
                    break;
                case HttpReponseStatusCode.FileSystemError:
                    Message = _stringLocalizer[HttpReponseStatusCode.FileSystemError.ToString()];
                    CssClass = "alert";
                    break;
                case HttpReponseStatusCode.InvalidData:
                    Message = _stringLocalizer[HttpReponseStatusCode.InvalidData.ToString()];
                    CssClass = "alert";
                    break;
                case HttpReponseStatusCode.NotFound:
                    Message = _stringLocalizer[HttpReponseStatusCode.NotFound.ToString()];
                    CssClass = "alert info";
                    break;
                case HttpReponseStatusCode.NotSupported:
                    Message = _stringLocalizer[HttpReponseStatusCode.NotSupported.ToString()];
                    CssClass = "alert";
                    break;
                case HttpReponseStatusCode.Success:
                    Message = _stringLocalizer[HttpReponseStatusCode.Success.ToString()];
                    CssClass = "alert success";
                    break;
                case HttpReponseStatusCode.UnknownError:
                    Message = _stringLocalizer[HttpReponseStatusCode.UnknownError.ToString()];
                    CssClass = "alert";
                    break;
                case HttpReponseStatusCode.NoStatus:
                    IsSet = false;
                    break;
            }
        }
    }
}
