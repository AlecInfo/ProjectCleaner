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
using System.IO.Packaging;
using System.Reflection;
using System.Resources;
using System.Windows.Resources;
using Wpf.Ui.Controls;

namespace ProjectCleaner.ViewModels.Pages
{
    public partial class CleanerViewModel : ObservableObject, INavigationAware
    {
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
                string[] files = new string[] {
                    "pack://application:,,,/Resources/Cleaner/VisualStudio.cleaner",
                    // Add ligne ..
                };

                foreach (string file in files)
                {
                    var fileStreamInfo = Application.GetResourceStream(new Uri(file));

                    if (fileStreamInfo != null)
                    {
                        using (var reader = new StreamReader(fileStreamInfo.Stream))
                        {
                            var fileContent = reader.ReadToEnd();
                            // Utilisez le contenu du fichier comme vous le souhaitez ici

                            var cleaner = new Cleaner();

                            cleaner.Name = Path.GetFileNameWithoutExtension(file);
                            cleaner.Rules = new List<string>(fileContent.Split(Environment.NewLine));

                            Cleaners.Add(cleaner);
                        }
                    }
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
