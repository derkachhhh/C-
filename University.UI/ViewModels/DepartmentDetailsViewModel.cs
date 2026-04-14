using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using University.Models.Enums;
using University.Services.DTO;
using University.Services.Interfaces;

namespace University.UI.ViewModels;

public class DepartmentDetailsViewModel : INotifyPropertyChanged
{
    private readonly IUniversityService _service;
    private List<TeacherListItemDto> _allTeachers = new();

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

    private int _id;
    public int Id
    {
        get => _id;
        set { _id = value; OnPropertyChanged(); }
    }

    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); }
    }

    private string _building = string.Empty;
    public string Building
    {
        get => _building;
        set { _building = value; OnPropertyChanged(); }
    }

    private string _type = string.Empty;
    public string Type
    {
        get => _type;
        set { _type = value; OnPropertyChanged(); }
    }

    private string _teacherSearchText = string.Empty;
    public string TeacherSearchText
    {
        get => _teacherSearchText;
        set
        {
            if (_teacherSearchText != value)
            {
                _teacherSearchText = value;
                OnPropertyChanged();
                ApplyTeacherFilterAndSort();
            }
        }
    }

    private string _selectedTeacherSortOption = "ПІБ A-Я";
    public string SelectedTeacherSortOption
    {
        get => _selectedTeacherSortOption;
        set
        {
            if (_selectedTeacherSortOption != value)
            {
                _selectedTeacherSortOption = value;
                OnPropertyChanged();
                ApplyTeacherFilterAndSort();
            }
        }
    }

    public List<string> TeacherSortOptions { get; } = new()
    {
        "ПІБ A-Я",
        "ПІБ Я-A",
        "Посада A-Я"
    };

    private TeacherListItemDto? _selectedTeacher;
    public TeacherListItemDto? SelectedTeacher
    {
        get => _selectedTeacher;
        set
        {
            if (_selectedTeacher != value)
            {
                _selectedTeacher = value;
                OnPropertyChanged();
            }
        }
    }

    public ObservableCollection<TeacherListItemDto> Teachers { get; } = new();

    public ICommand OpenTeacherCommand { get; }
    public ICommand AddTeacherCommand { get; }
    public ICommand EditTeacherCommand { get; }
    public ICommand DeleteTeacherCommand { get; }

    public DepartmentDetailsViewModel(IUniversityService service)
    {
        _service = service;

        OpenTeacherCommand = new Command<TeacherListItemDto>(async teacher =>
        {
            if (teacher is null)
                return;

            await Application.Current!.Windows[0].Page!.Navigation.PushAsync(
                new TeacherDetailsPage(
                    new TeacherDetailsViewModel(_service),
                    teacher.Id
                )
            );
        });

        AddTeacherCommand = new Command(async () => await AddTeacherAsync());
        EditTeacherCommand = new Command(async () => await EditSelectedTeacherAsync());
        DeleteTeacherCommand = new Command(async () => await DeleteSelectedTeacherAsync());
    }

    public async Task LoadDepartmentAsync(int departmentId)
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            var department = await _service.GetDepartmentByIdAsync(departmentId);
            if (department is null)
                return;

            Id = department.Id;
            Name = department.Name;
            Building = department.Building;
            Type = department.Type;

            _allTeachers = department.Teachers.ToList();
            ApplyTeacherFilterAndSort();
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void ApplyTeacherFilterAndSort()
    {
        IEnumerable<TeacherListItemDto> query = _allTeachers;

        if (!string.IsNullOrWhiteSpace(TeacherSearchText))
        {
            var search = TeacherSearchText.Trim().ToLower();
            query = query.Where(t =>
                t.FullName.ToLower().Contains(search) ||
                t.Position.ToLower().Contains(search) ||
                t.Email.ToLower().Contains(search));
        }

        query = SelectedTeacherSortOption switch
        {
            "ПІБ Я-A" => query.OrderByDescending(t => t.FullName),
            "Посада A-Я" => query.OrderBy(t => t.Position),
            _ => query.OrderBy(t => t.FullName)
        };

        Teachers.Clear();
        foreach (var teacher in query)
        {
            Teachers.Add(teacher);
        }
    }

    private async Task AddTeacherAsync()
    {
        if (IsBusy || Id == 0)
            return;

        string? fullName = await Application.Current!.Windows[0].Page!.DisplayPromptAsync(
            "Новий викладач",
            "Введіть ПІБ викладача:");

        if (string.IsNullOrWhiteSpace(fullName))
            return;

        string? email = await Application.Current!.Windows[0].Page!.DisplayPromptAsync(
            "Новий викладач",
            "Введіть email:");

        if (string.IsNullOrWhiteSpace(email))
            return;

        string? experienceInput = await Application.Current!.Windows[0].Page!.DisplayPromptAsync(
            "Новий викладач",
            "Введіть стаж у роках:",
            initialValue: "1",
            keyboard: Keyboard.Numeric);

        if (!int.TryParse(experienceInput, out int experienceYears))
            return;

        var rank = AcademicRank.Assistant;

        await _service.AddTeacherAsync(Id, fullName, rank, email, experienceYears);
        await LoadDepartmentAsync(Id);
    }

    private async Task EditSelectedTeacherAsync()
    {
        if (IsBusy || SelectedTeacher is null)
            return;

        var teacherDetails = await _service.GetTeacherByIdAsync(SelectedTeacher.Id);
        if (teacherDetails is null)
            return;

        string? newFullName = await Application.Current!.Windows[0].Page!.DisplayPromptAsync(
            "Редагування викладача",
            "Введіть ПІБ:",
            initialValue: teacherDetails.FullName);

        if (string.IsNullOrWhiteSpace(newFullName))
            return;

        string? newEmail = await Application.Current!.Windows[0].Page!.DisplayPromptAsync(
            "Редагування викладача",
            "Введіть email:",
            initialValue: teacherDetails.Email);

        if (string.IsNullOrWhiteSpace(newEmail))
            return;

        string currentYears = teacherDetails.ExperienceYears.ToString();
        string? newExperienceInput = await Application.Current!.Windows[0].Page!.DisplayPromptAsync(
            "Редагування викладача",
            "Введіть стаж у роках:",
            initialValue: currentYears,
            keyboard: Keyboard.Numeric);

        if (!int.TryParse(newExperienceInput, out int newExperienceYears))
            return;

        AcademicRank rank = teacherDetails.Position switch
        {
            nameof(AcademicRank.SeniorLecturer) => AcademicRank.SeniorLecturer,
            nameof(AcademicRank.AssociateProfessor) => AcademicRank.AssociateProfessor,
            nameof(AcademicRank.Professor) => AcademicRank.Professor,
            _ => AcademicRank.Assistant
        };

        await _service.UpdateTeacherAsync(
            SelectedTeacher.Id,
            Id,
            newFullName,
            rank,
            newEmail,
            newExperienceYears);

        await LoadDepartmentAsync(Id);
    }

    private async Task DeleteSelectedTeacherAsync()
    {
        if (IsBusy || SelectedTeacher is null)
            return;

        bool confirm = await Application.Current!.Windows[0].Page!.DisplayAlertAsync(
            "Підтвердження",
            $"Видалити викладача \"{SelectedTeacher.FullName}\"?",
            "Так",
            "Ні");

        if (!confirm)
            return;

        await _service.DeleteTeacherAsync(SelectedTeacher.Id);
        SelectedTeacher = null;
        await LoadDepartmentAsync(Id);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}