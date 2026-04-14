using University.Models.Data;

namespace University.Repositories.Storage;

public class UniversityDatabase
{
    public List<DepartmentData> Departments { get; set; } = new();
    public List<TeacherData> Teachers { get; set; } = new();
}