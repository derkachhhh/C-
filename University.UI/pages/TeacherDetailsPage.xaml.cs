using University.UI.ViewModels;

namespace University.UI;

public partial class TeacherDetailsPage : ContentPage
{
    public TeacherDetailsPage(TeacherDetailsViewModel viewModel, int teacherId)
    {
        InitializeComponent();
        viewModel.LoadTeacher(teacherId);
        BindingContext = viewModel;
    }
}