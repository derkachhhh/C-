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

    public IReadOnlyList<DepartmentListItemDto> GetAllDepartments()
    {
        return _repository.GetAllDepartments()
            .Select(d => new DepartmentListItemDto
            {
                Id = d.Id,
                Name = d.Name
            })
            .ToList();
    }

    public DepartmentDetailsDto? GetDepartmentById(int id)
    {
        var department = _repository.GetDepartmentById(id);
        if (department is null)
            return null;

        var teachers = _repository.GetTeachersByDepartmentId(id)
            .Select(t => new TeacherListItemDto
            {
                Id = t.Id,
                FullName = t.FullName,
                Position = t.Rank.ToString(),
                Email = t.Email
            })
            .ToList();

        return new DepartmentDetailsDto
        {
            Id = department.Id,
            Name = department.Name,
            Building = department.Building,
            Type = department.Type.ToString(),
            Teachers = teachers
        };
    }

    public TeacherDetailsDto? GetTeacherById(int id)
    {
        var teacher = _repository.GetTeacherById(id);
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
}