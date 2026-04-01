using University.UI.ViewModels;

namespace University.UI;

public partial class DepartmentsPage : ContentPage
{
    public DepartmentsPage(DepartmentsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}