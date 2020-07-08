using ServiceLayer.ErrorHandling;

namespace Orhedge.Enums
{
    public static class HttpReponseStatusMapper
    {
        /// <summary>
        /// Maps operation status returned by lower layer into matching higher layer status, returned through http calls.
        /// </summary>
        public static HttpReponseStatusCode Map(this OperationStatus operationStatus)
        {
            switch (operationStatus)
            {
                case OperationStatus.Success: return HttpReponseStatusCode.Success;
                case OperationStatus.NotFound: return HttpReponseStatusCode.NotFound;
                case OperationStatus.NotSupported: return HttpReponseStatusCode.NotSupported;
                case OperationStatus.Exists: return HttpReponseStatusCode.Exists;
                case OperationStatus.FileSystemError: return HttpReponseStatusCode.FileSystemError;
                case OperationStatus.DatabaseError: return HttpReponseStatusCode.DatabaseError;
                case OperationStatus.InvalidData: return HttpReponseStatusCode.InvalidData;
                case OperationStatus.UnknownError: return HttpReponseStatusCode.UnknownError;
                default: return HttpReponseStatusCode.NoStatus;
            }
        }
    }
}
