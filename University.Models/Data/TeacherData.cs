using University.Models.Enums;

namespace University.Models.Data;

public class TeacherData
{
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public AcademicRank Rank { get; set; }
    public string Email { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }

    public TeacherData()
    {
    }

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