namespace ServiceLayer.ErrorHandling
{
    /// <summary>
    /// Defines statuses that can indicate status of an executed operation.
    /// </summary>
    public enum OperationStatus
    {
        Success,
        DatabaseError,
        FileSystemError,
        NotFound,
        Exists,
        InvalidData,
        UnknownError,
        NotSupported
    }
}
