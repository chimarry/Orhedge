namespace DatabaseLayer.Enums
{
    /// <summary>
    /// A student can have one of the privileges contained in enum.
    /// </summary>
    public enum StudentPrivilege
    {
        Reduced,
        Normal,
        JuniorAdmin,
        SeniorAdmin
    }

    /// <summary>
    /// Possible values for study program.
    /// </summary>
    public enum StudyProgram
    {
        CommonYear,

        ComputerEngineeringAndInformatics,

        ElectronicsAndTelecommunications,

        PowerEngineeringAndIndustrialSystems
    }

    /// <summary>
    /// Possible values for semester.
    /// </summary>
    public enum Semester
    {
        First,
        Second,
        Third,
        Forth, 
        Fifth, 
        Sixth, 
        Seventh, 
        Eighth
    }
}