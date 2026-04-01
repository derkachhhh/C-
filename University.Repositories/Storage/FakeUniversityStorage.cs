using University.Models.Data;
using University.Models.Enums;

namespace University.Repositories.Storage;

internal static class FakeUniversityStorage
{
    internal static List<DepartmentData> Departments { get; } = new();
    internal static List<TeacherData> Teachers { get; } = new();

    static FakeUniversityStorage()
    {
        Seed();
    }

    private static void Seed()
    {
        Departments.AddRange(new[]
        {
            new DepartmentData(1, "Кафедра комп’ютерних наук", DepartmentType.ComputerScience, "Корпус 1"),
            new DepartmentData(2, "Кафедра економіки", DepartmentType.Economics, "Корпус 5"),
            new DepartmentData(3, "Кафедра міжнародних відносин", DepartmentType.International_Relations, "Корпус 7")
        });

        Teachers.AddRange(new[]
        {
            new TeacherData(1, 1, "Глибовець Микола Миколайович", AcademicRank.Professor, "a.glybovets@ukma.edu.ua", 20),
            new TeacherData(2, 1, "Гулаєва Наталія Михайлівна", AcademicRank.AssociateProfessor, "gulayeva@ukma.edu.ua", 13),
            new TeacherData(3, 1, "Гороховський Семен Самуїлови", AcademicRank.SeniorLecturer, "gor@ukma.edu.ua", 40),
            new TeacherData(4, 1, "Заславський Володимир Анатолійович", AcademicRank.Assistant, "v.zaslavskyi@ukma.edu.ua", 15),
            new TeacherData(5, 1, "Проценко Володимир Семенович", AcademicRank.SeniorLecturer, "v.protsenko@ukma.edu.ua", 10),

            new TeacherData(11, 2, "Біла Ірина Сергіївна", AcademicRank.SeniorLecturer, "i.bila@ukma.edu.ua", 6),
            new TeacherData(12, 2, "Шевченко Олена Олександрівна ", AcademicRank.Assistant, "olena.shevchenko@ukma.edu.ua", 1),

            new TeacherData(13, 3, "Яковлєв Максим Володимирович", AcademicRank.SeniorLecturer, "yakovlevmv@ukma.edu.ua", 11),
            new TeacherData(14, 3, "Гриценко Олена Миколаївна", AcademicRank.SeniorLecturer, "olena.hrytsenko@ukma.edu.ua", 5),
            new TeacherData(15, 3, "Кисельова Тетяна Сергіївна", AcademicRank.SeniorLecturer, "t.kyselova@ukma.edu.ua", 6),
        });
    }
}
