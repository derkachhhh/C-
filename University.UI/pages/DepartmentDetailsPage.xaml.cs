using University.UI.ViewModels;

namespace University.UI;

public partial class DepartmentDetailsPage : ContentPage
{
    private readonly DepartmentDetailsViewModel _viewModel;
    private readonly int _departmentId;

    public DepartmentDetailsPage(DepartmentDetailsViewModel viewModel, int departmentId)
    {
        InitializeComponent();
        _viewModel = viewModel;
        _departmentId = departmentId;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadDepartmentAsync(_departmentId);
    }
}