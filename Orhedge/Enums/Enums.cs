namespace Orhedge.Enums
{
    public enum StudentSortingCriteria
    {
        NoSorting,
        RatingAsc,
        RatingDesc,
        NameAsc,
        NameDesc,
        PrivilegeAsc,
        PrivilegeDesc
    }

    public enum SendConfirmEmailStatus
    {
        EmailAlreadyExists,
        IndexAlreadyExists
    };

    public enum StudyMaterialSortingCriteria
    {
        NoSorting,
        RatingAsc,
        RatingDesc,
        UploadDateAsc,
        UploadDateDesc
    }

    public enum HttpReponseStatusCode
    {
        NoStatus,
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
