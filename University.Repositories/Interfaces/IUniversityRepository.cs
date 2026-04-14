using University.Models.Data;

namespace University.Repositories.Interfaces;

public interface IUniversityRepository
{
    Task<IReadOnlyList<DepartmentData>> GetAllDepartmentsAsync();
    Task<DepartmentData?> GetDepartmentByIdAsync(int id);
    Task<IReadOnlyList<TeacherData>> GetTeachersByDepartmentIdAsync(int departmentId);
    Task<TeacherData?> GetTeacherByIdAsync(int id);

    Task AddDepartmentAsync(DepartmentData department);
    Task UpdateDepartmentAsync(DepartmentData department);
    Task DeleteDepartmentAsync(int departmentId);

    Task AddTeacherAsync(TeacherData teacher);
    Task UpdateTeacherAsync(TeacherData teacher);
    Task DeleteTeacherAsync(int teacherId);
}