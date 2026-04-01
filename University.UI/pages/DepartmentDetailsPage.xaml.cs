using University.UI.ViewModels;

namespace University.UI;

public partial class DepartmentDetailsPage : ContentPage
{
    public DepartmentDetailsPage(DepartmentDetailsViewModel viewModel, int departmentId)
    {
        InitializeComponent();
        viewModel.LoadDepartment(departmentId);
        BindingContext = viewModel;
    }
}