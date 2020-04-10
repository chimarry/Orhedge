namespace ServiceLayer.ErrorHandling
{
    public enum OperationStatus
    {
        Success, DatabaseError, FileSystemError, NotFound, Exists, InvalidData, UnknownError, NotSupported, Blocked
    }
}
