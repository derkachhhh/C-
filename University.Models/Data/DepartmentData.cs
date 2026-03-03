using University.Models.Enums;

namespace University.Models.Data;

public class DepartmentData
{
    public int Id { get; }
    public string Name { get; private set; }
    public DepartmentType Type { get; private set; }
    public string Building { get; private set; }

    public DepartmentData(int id, string name, DepartmentType type, string building)
    {
        Id = id;
        Name = name;
        Type = type;
        Building = building;
    }
}