using University.Models.Enums;

namespace University.Models.Data;

public class DepartmentData
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DepartmentType Type { get; set; }
    public string Building { get; set; } = string.Empty;

    public DepartmentData()
    {
    }

    public DepartmentData(int id, string name, DepartmentType type, string building)
    {
        Id = id;
        Name = name;
        Type = type;
        Building = building;
    }
}