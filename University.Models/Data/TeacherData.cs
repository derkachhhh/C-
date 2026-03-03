using University.Models.Enums;

namespace University.Models.Data;

public class TeacherData
{
    public int Id { get; }
    public int DepartmentId { get; }

    public string FullName { get; private set; }
    public AcademicRank Rank { get; private set; }
    public string Email { get; private set; }
    public int ExperienceYears { get; private set; }

    public TeacherData(
        int id,
        int departmentId,
        string fullName,
        AcademicRank rank,
        string email,
        int experienceYears)
    {
        Id = id;
        DepartmentId = departmentId;
        FullName = fullName;
        Rank = rank;
        Email = email;
        ExperienceYears = experienceYears;
    }
}