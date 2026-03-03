using University.Services.Storage;
using University.Models.Data;

class Program
{
    static void Main()
    {
        var storage = new UniversityStorageService();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("СПИСОК КАФЕДР:\n");

            var departments = storage.GetDepartments();

            foreach (var d in departments)
            {
                Console.WriteLine($"{d.Id}. {d.Name} ({d.Type})");
            }

            Console.WriteLine("\nВведи номер кафедри (або 0 для вихіда):");

            var input = Console.ReadLine();

            if (input == "0")
                break;

            if (!int.TryParse(input, out int depId))
                continue;

            var selected = departments.FirstOrDefault(d => d.Id == depId);

            if (selected == null)
                continue;

            ShowTeachers(storage, selected);
        }
    }

    static void ShowTeachers(UniversityStorageService storage, DepartmentData department)
    {
        Console.Clear();
        Console.WriteLine($"Кафедра: {department.Name}");
        Console.WriteLine($"Корпус: {department.Building}\n");

        var teachers = storage.GetTeachersByDepartmentId(department.Id);

        Console.WriteLine("Викладачі:\n");

        foreach (var t in teachers)
        {
            Console.WriteLine($"{t.Id}. {t.FullName} - {t.Rank}, {t.ExperienceYears} років досвіду");
        }

        Console.WriteLine("\nНатисни Enter щоб повернутись...");
        Console.ReadLine();
    }
}