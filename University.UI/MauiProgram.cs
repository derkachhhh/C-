using Microsoft.Extensions.Logging;
using University.Repositories;
using University.Repositories.Interfaces;
using University.Services;
using University.Services.Interfaces;
using University.UI.ViewModels;

namespace University.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<IUniversityRepository, UniversityRepository>();
        builder.Services.AddSingleton<IUniversityService, UniversityService>();

        builder.Services.AddTransient<DepartmentsViewModel>();
        builder.Services.AddTransient<DepartmentDetailsViewModel>();
        builder.Services.AddTransient<TeacherDetailsViewModel>();

        builder.Services.AddTransient<DepartmentsPage>();

        return builder.Build();
    }
}