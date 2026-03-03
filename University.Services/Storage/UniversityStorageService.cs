using University.Models.Data;

namespace University.Services.Storage;

public class UniversityStorageService
{
    public IReadOnlyList<DepartmentData> GetDepartments()
    {
        return FakeUniversityStorage.Departments;
    }

    public IReadOnlyList<TeacherData> GetTeachersByDepartmentId(int departmentId)
    {
        return FakeUniversityStorage.Teachers
            .Where(t => t.DepartmentId == departmentId)
            .ToList();
    }
}