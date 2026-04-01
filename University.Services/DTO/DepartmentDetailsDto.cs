namespace University.Services.DTO;

public class DepartmentDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Building { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;

    public List<TeacherListItemDto> Teachers { get; set; } = new();
}