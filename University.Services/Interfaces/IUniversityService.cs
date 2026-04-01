using University.Services.DTO;

namespace University.Services.Interfaces;

public interface IUniversityService
{
    IReadOnlyList<DepartmentListItemDto> GetAllDepartments();
    DepartmentDetailsDto? GetDepartmentById(int id);
    TeacherDetailsDto? GetTeacherById(int id);
}