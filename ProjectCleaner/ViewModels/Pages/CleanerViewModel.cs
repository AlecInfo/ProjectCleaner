/// \Project ProjectCleaner
/// \File CleanerViewModel.cs
/// \Brief Contains the CleanerViewModel class used to manage the Cleaner page
/// \Author Alec Piette
/// \History 24.10.2023 - Created File

using CommunityToolkit.Mvvm.Messaging;
using ProjectCleaner.Helpers;
using ProjectCleaner.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using Wpf.Ui.Controls;

namespace ProjectCleaner.ViewModels.Pages
{
    public partial class CleanerViewModel : ObservableObject, INavigationAware
    {
        private const string _cleanerDirectoryPath = "Resources/Cleaner";

        // Fields
        private bool _isInitialized = false;

        [ObservableProperty]
        private Project _selectedProject;

        [ObservableProperty]
        private ObservableCollection<Cleaner> _cleaners;

        [ObservableProperty]
        private Cleaner _selectedCleaner;

        [ObservableProperty]
        private string _zipName;

        // Propertie


        // Constructor
        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                Initialize();
        }

        public void OnNavigatedFrom() { }

        private void Initialize()
        {
            // Initialize fields
            Cleaners = new ObservableCollection<Cleaner>();
            SelectedCleaner = new Cleaner();
            SelectedProject = new Project();

            try
            {
                // Get all cleaners from directory (Resources/Cleaner)
                var files = Directory.GetFiles($"../../../{_cleanerDirectoryPath}", "*.cleaner");

                // Add cleaners to list
                foreach (var file in files)
                {
                    var cleaner = new Cleaner();
                    cleaner.Name = Path.GetFileNameWithoutExtension(file);
                    cleaner.Rules = new List<string>(File.ReadAllLines(file));
                    Cleaners.Add(cleaner);
                }

                // Order by name (ascending)
                Cleaners = new ObservableCollection<Cleaner>(Cleaners.OrderBy(c => c.Name));
            }
            catch (Exception)
            {
                MessageHandler.ShowAlert("Error", "An error occured while loading cleaners.");
            }

            _isInitialized = true;
        }

        // Methods
        private void Reset()
        {
            SelectedProject = new Project();
            ZipName = "";
            SelectedCleaner.CurrentLine = 0;
        }

        [RelayCommand]
        private void SelectProject_Click()
        {
            SelectedProject = Project.OpenProject();
            ZipName = SelectedProject.Name;
        }

        [RelayCommand]
        private void SelectCleaner_Click()
        {
            SelectedCleaner = Cleaner.OpenCleaner();
        }

        [RelayCommand]
        private void CleanProject_Click()
        {
            SelectedCleaner.CleanProject(SelectedProject);
        }

        [RelayCommand]
        private void ZipProject_Click()
        {
            FileHandler.ZipDirectory(SelectedProject.TempPath, ZipName);
            Reset();
        }

    }
}
