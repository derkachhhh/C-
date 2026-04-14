using University.Models.Data;
using University.Models.Enums;
using University.Repositories.Interfaces;
using University.Services.DTO;
using University.Services.Interfaces;

namespace University.Services;

public class UniversityService : IUniversityService
{
    private readonly IUniversityRepository _repository;

    public UniversityService(IUniversityRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<DepartmentListItemDto>> GetAllDepartmentsAsync()
    {
        var departments = await _repository.GetAllDepartmentsAsync();

        return departments
    .Select(d => new DepartmentListItemDto
    {
        Id = d.Id,
        Name = d.Name,
        Building = d.Building,
        Type = d.Type.ToString()
    })
    .ToList();
    }

    public async Task<DepartmentDetailsDto?> GetDepartmentByIdAsync(int id)
    {
        var department = await _repository.GetDepartmentByIdAsync(id);
        if (department is null)
            return null;

        var teachers = await _repository.GetTeachersByDepartmentIdAsync(id);

        return new DepartmentDetailsDto
        {
            Id = department.Id,
            Name = department.Name,
            Building = department.Building,
            Type = department.Type.ToString(),
            Teachers = teachers
                .Select(t => new TeacherListItemDto
                {
                    Id = t.Id,
                    FullName = t.FullName,
                    Position = t.Rank.ToString(),
                    Email = t.Email
                })
                .ToList()
        };
    }

    public async Task<TeacherDetailsDto?> GetTeacherByIdAsync(int id)
    {
        var teacher = await _repository.GetTeacherByIdAsync(id);
        if (teacher is null)
            return null;

        return new TeacherDetailsDto
        {
            Id = teacher.Id,
            FullName = teacher.FullName,
            Position = teacher.Rank.ToString(),
            Email = teacher.Email,
            ExperienceYears = teacher.ExperienceYears
        };
    }

    public async Task AddDepartmentAsync(string name, DepartmentType type, string building)
    {
        var department = new DepartmentData(0, name, type, building);
        await _repository.AddDepartmentAsync(department);
    }

    public async Task UpdateDepartmentAsync(int id, string name, DepartmentType type, string building)
    {
        var department = new DepartmentData(id, name, type, building);
        await _repository.UpdateDepartmentAsync(department);
    }

    public async Task DeleteDepartmentAsync(int id)
    {
        await _repository.DeleteDepartmentAsync(id);
    }

    public async Task AddTeacherAsync(int departmentId, string fullName, AcademicRank rank, string email, int experienceYears)
    {
        var teacher = new TeacherData(0, departmentId, fullName, rank, email, experienceYears);
        await _repository.AddTeacherAsync(teacher);
    }

    public async Task UpdateTeacherAsync(int id, int departmentId, string fullName, AcademicRank rank, string email, int experienceYears)
    {
        var teacher = new TeacherData(id, departmentId, fullName, rank, email, experienceYears);
        await _repository.UpdateTeacherAsync(teacher);
    }

    public async Task DeleteTeacherAsync(int id)
    {
        await _repository.DeleteTeacherAsync(id);
    }
}