/// \Project ProjectCleaner
/// \File Project.cs
/// \Brief This file contains the Project class which is used to store project information
/// \Author Alec Piette
/// \History 24.10.2023 - Created File

using ProjectCleaner.Helpers;
using System.IO;

namespace ProjectCleaner.Models
{
    public class Project
    {
        // Fields
        private string _name;
        private string _path;
        private string _tempPath;

        // Properties
        public string Name { get => _name; set => _name = value; }
        public string ProjectPath { get => _path; set => _path = value; }
        public string TempPath { get => _tempPath; set => _tempPath = value; }

        // Constructor
        public Project() : this("", "") { }
        public Project(string pProjectName, string pProjectPath)
        {
            Name = pProjectName;
            ProjectPath = pProjectPath;
            TempPath = (!string.IsNullOrEmpty(Name)) ? Path.Combine(Path.GetTempPath(), Name) : "";
        }

        // Methods

        /// <summary>
        /// Opens a dialog to browse for a project
        /// </summary>
        /// <returns>New project with value</returns>
        public static Project OpenProject()
        {
            string path = FileHandler.BrowseFolder();

            if (string.IsNullOrEmpty(path))
            {
                return new Project();
            }
            else
            {
                string name = new DirectoryInfo(path).Name;
                return new Project(name, path);
            }
        }
    }
}
