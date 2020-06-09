﻿namespace Orhedge.Enums
{
    public enum StudentSortingCriteria
    {
        NoSorting, RatingAsc, RatingDesc, NameAsc, NameDesc, PrivilegeAsc, PrivilegeDesc
    }

    public enum StudyProgramRang
    {
        CommonYear,

        ComputerEngineeringAndInformatics,

        ElectronicsAndTelecommunications,

        PowerEngineeringAndIndustrialSystems
    }

    public enum Semester
    {
        First, Second, Third, Forth, Fifth, Sixth, Seventh, Eighth,
        Unknown
    }

    public enum SendConfirmEmailStatus { Success, AlreadyExists };
}
