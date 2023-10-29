/// \Project ProjectCleaner
/// \File Cleaner.cs
/// \Brief Contains the Cleaner class used to clean a project
/// \Author Alec Piette
/// \History 24.10.2023 - Created File

using ProjectCleaner.Models;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ProjectCleaner.Helpers
{
    public class Cleaner
    {
        // Field
        private string _name;
        private List<string> _rules;
        private int _currentLine = 0;

        // Properties
        public string Name { get => _name; set => _name = value; }
        public List<string> Rules { get => _rules; set => _rules = value; }
        public int CurrentLine { get => _currentLine; set => _currentLine = value; }
        public int MaxLine { 
            get 
            { 
                if (Rules.Count == 0)
                    return 1;
                else
                    return Rules.Count;
            } 
        }

        public string RulesString
        {
            get
            {
                return string.Join("\n", Rules);
            }
            set
            {
                Rules = new List<string>(value.Split('\n'));
            }
        }


        // Constructor
        public Cleaner() : this("", new List<string>()) { }
        public Cleaner(string pName, List<string> pRules)
        {
            Name = pName;
            Rules = pRules;
            CurrentLine = 0;
        }

        // Methods

        /// <summary>
        /// Opens a dialog to browse for a cleaner
        /// </summary>
        /// <returns>New Cleaner with value</returns>
        public static Cleaner OpenCleaner()
        {
            var path = FileHandler.BrowseFile("Cleaner Files (*.cleaner)|*.cleaner", ".cleaner");

            if (path == null)
            {
                return new Cleaner();
            }
            else
            {
                var name = Path.GetFileNameWithoutExtension(path);
                var content = File.ReadAllLines(path);
                return new Cleaner(name, new List<string>(content));
            }
        }

        /// <summary>
        /// Clean the project based on the rules specified in the cleaner
        /// </summary>
        /// <param name="projectPath">The path to the project</param>
        public void CleanProject(Project project)
        {
            if (string.IsNullOrEmpty(project.ProjectPath))
            {
                MessageHandler.ShowAlert("Project", "No project selected!");
                return;
            }

            if (Rules.Count == 0)
            {
                MessageHandler.ShowAlert("Cleaner", "No rules specified!");
                return;
            }

            // Copy the project to the temp folder

            CopyDirectory(new DirectoryInfo(project.ProjectPath), new DirectoryInfo(project.TempPath));

            try
            {
                // Apply the rules line by line
                foreach (var rule in Rules)
                {
                    ApplyRules(project.TempPath, rule);
                    CurrentLine++;
                }

                MessageHandler.ShowAlert("Cleaner", "Cleaning complete!");
            }
            catch (Exception ex)
            {
                MessageHandler.ShowAlert("Error", $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Apply the rules to the project
        /// </summary>
        /// <param name="projectPath">The path to the project</param>
        /// <param name="rule">The rule to apply</param>
        private void ApplyRules(string projectPath, string rule)
        {
            if (IsCommentOrEmpty(rule))
            {
                return;
            }

            // If the rule ends with a /, it is a directory
            if (rule.EndsWith("/"))
            {
                // Remove the / at the end of the rule
                string directoryName = rule.TrimEnd('/');

                // Convert the rule to a regex pattern
                string regexPattern = ConvertToRegexPattern(directoryName);
                Regex regex = new Regex(regexPattern, RegexOptions.IgnoreCase);

                //  Get all directories in the project
                string[] directories = Directory.GetDirectories(projectPath, "*", SearchOption.AllDirectories);

                // Delete all directories that match the rule
                foreach (string directory in directories)
                {
                    // Get the name of the directory only
                    string directoryNameOnly = new DirectoryInfo(directory).Name;

                    // Check if the directory matches the rule
                    if (regex.IsMatch(directoryNameOnly))
                    {
                        Directory.Delete(directory, true);
                    }
                }
            }
            else
            {
                // Convert the rule to a regex pattern
                string regexPattern = ConvertToRegexPattern(rule);
                Regex regex = new Regex(regexPattern, RegexOptions.IgnoreCase);

                // Get all files in the project
                string[] files = Directory.GetFiles(projectPath, "*", SearchOption.AllDirectories);

                // Delete all files that match the rule
                foreach (string file in files)
                {
                    // Get the name of the file only
                    string fileNameOnly = Path.GetFileName(file);

                    // Check if the file matches the rule
                    if (regex.IsMatch(fileNameOnly))
                    {
                        File.Delete(file);
                    }
                }
            }
        }

        /// <summary>
        /// Check if the line is a comment or empty
        /// </summary>
        /// <param name="line">The line to check</param>
        /// <returns>If the line is a comment or empty</returns>
        private bool IsCommentOrEmpty(string line)
        {
            return string.IsNullOrWhiteSpace(line) || line.Trim().StartsWith("#");
        }

        private string ConvertToRegexPattern(string rule)
        {
            // Convert the rule to a regex pattern
            string regexPattern = Regex.Escape(rule);
            regexPattern = regexPattern.Replace(@"\[", "[").Replace(@"\]", "]");

            // Replace * with .* to match any character
            regexPattern = regexPattern.Replace("*", ".*");

            return regexPattern;
        }

        /// <summary>
        /// Copy the content of a directory to another
        /// </summary>
        /// <param name="source">The source directory</param>
        /// <param name="target">The target directory</param>
        private static void CopyDirectory(DirectoryInfo source, DirectoryInfo target)
        {
            if (!target.Exists)
            {
                target.Create();
            }

            // Copy each file into the new directory.
            foreach (var file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (var directory in source.GetDirectories())
            {
                CopyDirectory(directory, target.CreateSubdirectory(directory.Name));
            }
        }
    }
}