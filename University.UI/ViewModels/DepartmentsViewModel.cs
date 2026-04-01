using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using University.Services.DTO;
using University.Services.Interfaces;

namespace University.UI.ViewModels;

public class DepartmentsViewModel
{
    private readonly IUniversityService _service;

    public ObservableCollection<DepartmentListItemDto> Departments { get; } = new();

    public ICommand OpenDepartmentCommand { get; }

    public DepartmentsViewModel(IUniversityService service)
    {
        _service = service;

        OpenDepartmentCommand = new Command<DepartmentListItemDto>(async department =>
        {
            if (department is null)
                return;

            await Application.Current!.MainPage!.Navigation.PushAsync(
                new DepartmentDetailsPage(
                    new DepartmentDetailsViewModel(_service),
                    department.Id
                )
            );
        });

        LoadDepartments();
    }

    private void LoadDepartments()
    {
        Departments.Clear();

        foreach (var department in _service.GetAllDepartments())
        {
            Departments.Add(department);
        }
    }
}