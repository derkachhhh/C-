using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using University.Services.DTO;
using University.Services.Interfaces;

namespace University.UI.ViewModels;

public class DepartmentDetailsViewModel
{
    private readonly IUniversityService _service;

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Building { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;

    public ObservableCollection<TeacherListItemDto> Teachers { get; } = new();

    public ICommand OpenTeacherCommand { get; }

    public DepartmentDetailsViewModel(IUniversityService service)
    {
        _service = service;

        OpenTeacherCommand = new Command<TeacherListItemDto>(async teacher =>
        {
            if (teacher is null)
                return;

            await Application.Current!.MainPage!.Navigation.PushAsync(
                new TeacherDetailsPage(
                    new TeacherDetailsViewModel(_service),
                    teacher.Id
                )
            );
        });
    }

    public void LoadDepartment(int departmentId)
    {
        var department = _service.GetDepartmentById(departmentId);
        if (department is null)
            return;

        Id = department.Id;
        Name = department.Name;
        Building = department.Building;
        Type = department.Type;

        Teachers.Clear();
        foreach (var teacher in department.Teachers)
        {
            Teachers.Add(teacher);
        }
    }
}