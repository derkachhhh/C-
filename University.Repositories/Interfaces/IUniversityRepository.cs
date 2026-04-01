using University.Models.Data;

namespace University.Repositories.Interfaces;

public interface IUniversityRepository
{
    IReadOnlyList<DepartmentData> GetAllDepartments();
    DepartmentData? GetDepartmentById(int id);
    IReadOnlyList<TeacherData> GetTeachersByDepartmentId(int departmentId);
    TeacherData? GetTeacherById(int id);
}