/// \Project ProjectCleaner
/// \File DashboardViewModel.cs
/// \Brief Contains the DashboardViewModel class used to manage the Dashboard page
/// \Author Alec Piette
/// \History 24.10.2023 - Created File

using ProjectCleaner.Views.Pages;
using ProjectCleaner.Views.Windows;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;

namespace ProjectCleaner.ViewModels.Pages
{
    public partial class DashboardViewModel : ObservableObject
    {
        [RelayCommand]
        private void OnGoToCleaner()
        {
            // This is how you can navigate to another page (CleanerPage).
            App.GetService<INavigationService>()?.Navigate(typeof(CleanerPage));
        }
    }
}
