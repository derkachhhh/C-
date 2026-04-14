using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using University.Models.Enums;
using University.Services.DTO;
using University.Services.Interfaces;

namespace University.UI.ViewModels;

public class DepartmentsViewModel : INotifyPropertyChanged
{
    private readonly IUniversityService _service;
    private List<DepartmentListItemDto> _allDepartments = new();

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            if (_isBusy != value)
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
    }

    private string _searchText = string.Empty;
    public string SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText != value)
            {
                _searchText = value;
                OnPropertyChanged();
                ApplyFilterAndSort();
            }
        }
    }

    private string _selectedSortOption = "Назва A-Я";
    public string SelectedSortOption
    {
        get => _selectedSortOption;
        set
        {
            if (_selectedSortOption != value)
            {
                _selectedSortOption = value;
                OnPropertyChanged();
                ApplyFilterAndSort();
            }
        }
    }

    public List<string> SortOptions { get; } = new()
    {
        "Назва A-Я",
        "Назва Я-A",
        "Корпус A-Я"
    };

    private DepartmentListItemDto? _selectedDepartment;
    public DepartmentListItemDto? SelectedDepartment
    {
        get => _selectedDepartment;
        set
        {
            if (_selectedDepartment != value)
            {
                _selectedDepartment = value;
                OnPropertyChanged();
            }
        }
    }

    public ObservableCollection<DepartmentListItemDto> Departments { get; } = new();

    public ICommand OpenDepartmentCommand { get; }
    public ICommand AddDepartmentCommand { get; }
    public ICommand EditDepartmentCommand { get; }
    public ICommand DeleteDepartmentCommand { get; }

    public DepartmentsViewModel(IUniversityService service)
    {
        _service = service;

        OpenDepartmentCommand = new Command<DepartmentListItemDto>(async department =>
        {
            if (department is null)
                return;

            await Application.Current!.Windows[0].Page!.Navigation.PushAsync(
                new DepartmentDetailsPage(
                    new DepartmentDetailsViewModel(_service),
                    department.Id
                )
            );
        });

        AddDepartmentCommand = new Command(async () => await AddDepartmentAsync());
        EditDepartmentCommand = new Command(async () => await EditSelectedDepartmentAsync());
        DeleteDepartmentCommand = new Command(async () => await DeleteSelectedDepartmentAsync());
    }

    public async Task LoadDepartmentsAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            _allDepartments = (await _service.GetAllDepartmentsAsync()).ToList();
            ApplyFilterAndSort();
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void ApplyFilterAndSort()
    {
        IEnumerable<DepartmentListItemDto> query = _allDepartments;

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            var search = SearchText.Trim().ToLower();
            query = query.Where(d =>
                d.Name.ToLower().Contains(search) ||
                d.Building.ToLower().Contains(search) ||
                d.Type.ToLower().Contains(search));
        }

        query = SelectedSortOption switch
        {
            "Назва Я-A" => query.OrderByDescending(d => d.Name),
            "Корпус A-Я" => query.OrderBy(d => d.Building),
            _ => query.OrderBy(d => d.Name)
        };

        Departments.Clear();
        foreach (var department in query)
        {
            Departments.Add(department);
        }
    }

    private async Task AddDepartmentAsync()
    {
        if (IsBusy)
            return;

        string? name = await Application.Current!.Windows[0].Page!.DisplayPromptAsync(
            "Нова кафедра",
            "Введіть назву кафедри:");

        if (string.IsNullOrWhiteSpace(name))
            return;

        string? building = await Application.Current!.Windows[0].Page!.DisplayPromptAsync(
            "Нова кафедра",
            "Введіть корпус:");

        if (string.IsNullOrWhiteSpace(building))
            return;

        var type = DepartmentType.ComputerScience;

        await _service.AddDepartmentAsync(name, type, building);
        await LoadDepartmentsAsync();
    }

    private async Task EditSelectedDepartmentAsync()
    {
        if (IsBusy || SelectedDepartment is null)
            return;

        var details = await _service.GetDepartmentByIdAsync(SelectedDepartment.Id);
        if (details is null)
            return;

        string? newName = await Application.Current!.Windows[0].Page!.DisplayPromptAsync(
            "Редагування кафедри",
            "Введіть нову назву кафедри:",
            initialValue: details.Name);

        if (string.IsNullOrWhiteSpace(newName))
            return;

        string? newBuilding = await Application.Current!.Windows[0].Page!.DisplayPromptAsync(
            "Редагування кафедри",
            "Введіть новий корпус:",
            initialValue: details.Building);

        if (string.IsNullOrWhiteSpace(newBuilding))
            return;

        DepartmentType type = details.Type switch
        {
            nameof(DepartmentType.Economics) => DepartmentType.Economics,
            nameof(DepartmentType.International_Relations) => DepartmentType.International_Relations,
            _ => DepartmentType.ComputerScience
        };

        await _service.UpdateDepartmentAsync(SelectedDepartment.Id, newName, type, newBuilding);
        await LoadDepartmentsAsync();
    }

    private async Task DeleteSelectedDepartmentAsync()
    {
        if (IsBusy || SelectedDepartment is null)
            return;

        bool confirm = await Application.Current!.Windows[0].Page!.DisplayAlertAsync(
            "Підтвердження",
            $"Видалити кафедру \"{SelectedDepartment.Name}\"?",
            "Так",
            "Ні");

        if (!confirm)
            return;

        await _service.DeleteDepartmentAsync(SelectedDepartment.Id);
        SelectedDepartment = null;
        await LoadDepartmentsAsync();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}