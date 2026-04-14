using System.ComponentModel;
using System.Runtime.CompilerServices;
using University.Services.Interfaces;

namespace University.UI.ViewModels;

public class TeacherDetailsViewModel : INotifyPropertyChanged
{
    private readonly IUniversityService _service;

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

    private string _fullName = string.Empty;
    public string FullName
    {
        get => _fullName;
        set { _fullName = value; OnPropertyChanged(); }
    }

    private string _position = string.Empty;
    public string Position
    {
        get => _position;
        set { _position = value; OnPropertyChanged(); }
    }

    private string _email = string.Empty;
    public string Email
    {
        get => _email;
        set { _email = value; OnPropertyChanged(); }
    }

    private string _experienceText = string.Empty;
    public string ExperienceText
    {
        get => _experienceText;
        set { _experienceText = value; OnPropertyChanged(); }
    }

    public TeacherDetailsViewModel(IUniversityService service)
    {
        _service = service;
    }

    public async Task LoadTeacherAsync(int teacherId)
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            var teacher = await _service.GetTeacherByIdAsync(teacherId);
            if (teacher is null)
                return;

            Id = teacher.Id;
            FullName = teacher.FullName;
            Position = teacher.Position;
            Email = teacher.Email;
            ExperienceText = $"Стаж: {teacher.ExperienceYears} років";
        }
        finally
        {
            IsBusy = false;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}