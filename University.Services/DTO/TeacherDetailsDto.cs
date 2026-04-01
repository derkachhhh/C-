namespace University.Services.DTO;

public class TeacherDetailsDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }
}