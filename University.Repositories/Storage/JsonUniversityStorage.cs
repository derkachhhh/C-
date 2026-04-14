using System.Text.Json;
using University.Models.Data;
using University.Models.Enums;

namespace University.Repositories.Storage;

public class JsonUniversityStorage
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true
    };

    public JsonUniversityStorage()
{
    var appDataPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "UniversityApp");

    _filePath = Path.Combine(appDataPath, "university-data.json");
}

    public async Task<UniversityDatabase> LoadAsync()
    {
        if (!File.Exists(_filePath))
        {
            var initialData = CreateInitialData();
            await SaveAsync(initialData);
            return initialData;
        }

        await using var stream = File.OpenRead(_filePath);
        var data = await JsonSerializer.DeserializeAsync<UniversityDatabase>(stream, _jsonOptions);

        return data ?? new UniversityDatabase();
    }

    public async Task SaveAsync(UniversityDatabase database)
    {
        var directory = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        await using var stream = File.Create(_filePath);
        await JsonSerializer.SerializeAsync(stream, database, _jsonOptions);
    }

private static UniversityDatabase CreateInitialData()
{
    return new UniversityDatabase
    {
        Departments = new List<DepartmentData>
        {
            new(1, "Кафедра комп'ютерних наук", DepartmentType.ComputerScience, "1 корпус"),
            new(2, "Кафедра економіки", DepartmentType.Economics, "2 корпус"),
            new(3, "Кафедра міжнародних відносин", DepartmentType.International_Relations, "3 корпус")
        },
        Teachers = new List<TeacherData>
        {
            new(1, 1, "Іваненко Іван Іванович", AcademicRank.Professor, "ivanenko@ukma.edu.ua", 20),
            new(2, 1, "Петренко Петро Петрович", AcademicRank.AssociateProfessor, "petrenko@ukma.edu.ua", 12),
            new(3, 2, "Сидоренко Олена Василівна", AcademicRank.SeniorLecturer, "sydorenko@ukma.edu.ua", 9),
            new(4, 3, "Коваленко Марія Андріївна", AcademicRank.Assistant, "kovalenko@ukma.edu.ua", 4)
        }
    };
}
}