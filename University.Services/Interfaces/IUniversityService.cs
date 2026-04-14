using University.Models.Enums;
using University.Services.DTO;

namespace University.Services.Interfaces;

public interface IUniversityService
{
    Task<IReadOnlyList<DepartmentListItemDto>> GetAllDepartmentsAsync();
    Task<DepartmentDetailsDto?> GetDepartmentByIdAsync(int id);
    Task<TeacherDetailsDto?> GetTeacherByIdAsync(int id);

    Task AddDepartmentAsync(string name, DepartmentType type, string building);
    Task UpdateDepartmentAsync(int id, string name, DepartmentType type, string building);
    Task DeleteDepartmentAsync(int id);

    Task AddTeacherAsync(int departmentId, string fullName, AcademicRank rank, string email, int experienceYears);
    Task UpdateTeacherAsync(int id, int departmentId, string fullName, AcademicRank rank, string email, int experienceYears);
    Task DeleteTeacherAsync(int id);
}
