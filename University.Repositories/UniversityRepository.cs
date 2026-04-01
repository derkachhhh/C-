using University.Models.Data;
using University.Repositories.Interfaces;
using University.Repositories.Storage;

namespace University.Repositories;

public class UniversityRepository : IUniversityRepository
{
    public IReadOnlyList<DepartmentData> GetAllDepartments()
    {
        return FakeUniversityStorage.Departments;
    }

    public DepartmentData? GetDepartmentById(int id)
    {
        return FakeUniversityStorage.Departments.FirstOrDefault(d => d.Id == id);
    }

    public IReadOnlyList<TeacherData> GetTeachersByDepartmentId(int departmentId)
    {
        return FakeUniversityStorage.Teachers
            .Where(t => t.DepartmentId == departmentId)
            .ToList();
    }

    public TeacherData? GetTeacherById(int id)
    {
        return FakeUniversityStorage.Teachers.FirstOrDefault(t => t.Id == id);
    }
}