/// \Project ProjectCleaner
/// \File CleanerPage.xaml.cs
/// \Brief Contains the definition of the CleanerPage class.
/// \Author Alec Piette
/// \History 24.10.2023 - Created File

using ProjectCleaner.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace ProjectCleaner.Views.Pages
{
    public partial class CleanerPage : INavigableView<CleanerViewModel>
    {
        public CleanerViewModel ViewModel { get; }

        public CleanerPage(CleanerViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
