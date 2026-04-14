using University.UI.ViewModels;

namespace University.UI;

public partial class DepartmentsPage : ContentPage
{
    private readonly DepartmentsViewModel _viewModel;

    public DepartmentsPage(DepartmentsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadDepartmentsAsync();
    }
}