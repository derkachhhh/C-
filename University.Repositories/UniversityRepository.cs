using University.Models.Data;
using University.Repositories.Interfaces;
using University.Repositories.Storage;

namespace University.Repositories;

public class UniversityRepository : IUniversityRepository
{
    private readonly JsonUniversityStorage _storage;

    public UniversityRepository()
    {
        _storage = new JsonUniversityStorage();
    }

    public async Task<IReadOnlyList<DepartmentData>> GetAllDepartmentsAsync()
    {
        var database = await _storage.LoadAsync();
        return database.Departments
            .OrderBy(d => d.Id)
            .ToList();
    }

    public async Task<DepartmentData?> GetDepartmentByIdAsync(int id)
    {
        var database = await _storage.LoadAsync();
        return database.Departments.FirstOrDefault(d => d.Id == id);
    }

    public async Task<IReadOnlyList<TeacherData>> GetTeachersByDepartmentIdAsync(int departmentId)
    {
        var database = await _storage.LoadAsync();
        return database.Teachers
            .Where(t => t.DepartmentId == departmentId)
            .OrderBy(t => t.Id)
            .ToList();
    }

    public async Task<TeacherData?> GetTeacherByIdAsync(int id)
    {
        var database = await _storage.LoadAsync();
        return database.Teachers.FirstOrDefault(t => t.Id == id);
    }

    public async Task AddDepartmentAsync(DepartmentData department)
    {
        var database = await _storage.LoadAsync();

        var nextId = database.Departments.Any()
            ? database.Departments.Max(d => d.Id) + 1
            : 1;

        department.Id = nextId;
        database.Departments.Add(department);

        await _storage.SaveAsync(database);
    }

    public async Task UpdateDepartmentAsync(DepartmentData department)
    {
        var database = await _storage.LoadAsync();

        var existing = database.Departments.FirstOrDefault(d => d.Id == department.Id);
        if (existing is null)
            return;

        existing.Name = department.Name;
        existing.Type = department.Type;
        existing.Building = department.Building;

        await _storage.SaveAsync(database);
    }

    public async Task DeleteDepartmentAsync(int departmentId)
    {
        var database = await _storage.LoadAsync();

        var department = database.Departments.FirstOrDefault(d => d.Id == departmentId);
        if (department is null)
            return;

        database.Departments.Remove(department);

        var relatedTeachers = database.Teachers
            .Where(t => t.DepartmentId == departmentId)
            .ToList();

        foreach (var teacher in relatedTeachers)
        {
            database.Teachers.Remove(teacher);
        }

        await _storage.SaveAsync(database);
    }

    public async Task AddTeacherAsync(TeacherData teacher)
    {
        var database = await _storage.LoadAsync();

        var nextId = database.Teachers.Any()
            ? database.Teachers.Max(t => t.Id) + 1
            : 1;

        teacher.Id = nextId;
        database.Teachers.Add(teacher);

        await _storage.SaveAsync(database);
    }

    public async Task UpdateTeacherAsync(TeacherData teacher)
    {
        var database = await _storage.LoadAsync();

        var existing = database.Teachers.FirstOrDefault(t => t.Id == teacher.Id);
        if (existing is null)
            return;

        existing.FullName = teacher.FullName;
        existing.Rank = teacher.Rank;
        existing.Email = teacher.Email;
        existing.ExperienceYears = teacher.ExperienceYears;
        existing.DepartmentId = teacher.DepartmentId;

        await _storage.SaveAsync(database);
    }

    public async Task DeleteTeacherAsync(int teacherId)
    {
        var database = await _storage.LoadAsync();

        var teacher = database.Teachers.FirstOrDefault(t => t.Id == teacherId);
        if (teacher is null)
            return;

        database.Teachers.Remove(teacher);

        await _storage.SaveAsync(database);
    }
}