using University.Services.Interfaces;

namespace University.UI.ViewModels;

public class TeacherDetailsViewModel
{
    private readonly IUniversityService _service;

    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ExperienceText { get; set; } = string.Empty;

    public TeacherDetailsViewModel(IUniversityService service)
    {
        _service = service;
    }

    public void LoadTeacher(int teacherId)
    {
        var teacher = _service.GetTeacherById(teacherId);
        if (teacher is null)
            return;

        Id = teacher.Id;
        FullName = teacher.FullName;
        Position = teacher.Position;
        Email = teacher.Email;
        ExperienceText = $"Стаж: {teacher.ExperienceYears} років";
    }
}