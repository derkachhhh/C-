using University.UI.ViewModels;

namespace University.UI;

public partial class TeacherDetailsPage : ContentPage
{
    private readonly TeacherDetailsViewModel _viewModel;
    private readonly int _teacherId;

    public TeacherDetailsPage(TeacherDetailsViewModel viewModel, int teacherId)
    {
        InitializeComponent();
        _viewModel = viewModel;
        _teacherId = teacherId;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadTeacherAsync(_teacherId);
    }
}