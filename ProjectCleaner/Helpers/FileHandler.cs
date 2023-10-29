/// \Project ProjectCleaner
/// \File FileHandler.cs
/// \Brief This file contains the FileHandler class which is used to handle files
/// \Author Alec Piette
/// \History 24.10.2023 - Created File

using Microsoft.Win32;
using System.IO;
using System.IO.Compression;

namespace ProjectCleaner.Helpers
{
    public class FileHandler
    {

        // Fields

        // Constructor

        // Methods
        /// <summary>
        /// Opens a dialog to browse for a file
        /// </summary>
        /// <param name="filter">The filter</param>
        /// <param name="defaultExtension">The default extension</param>
        /// <returns>The path to the folder</returns>
        public static string BrowseFile(string filter, string defaultExtension)
        {
            try
            {
                var dialog = new OpenFileDialog
                {
                    Title = "Select File",
                    Filter = filter,
                    DefaultExt = defaultExtension
                };

                var result = dialog.ShowDialog();

                if (result == true)
                {
                    var path = dialog.FileName;
                    return path;
                }

                return null; // Return null if no file is selected
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                // You can also show an error message to the user
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Opens a dialog to browse for a folder
        /// </summary>
        /// <returns>The path to the project</returns>
        public static string BrowseFolder()
        {
            try
            {
                var dialog = new OpenFileDialog
                {
                    Title = "Select Project Directory",
                    Filter = "Directory|*.this.directory",
                    CheckFileExists = false,
                    CheckPathExists = true,
                    FileName = "Select Folder"
                };

                var result = dialog.ShowDialog();

                if (result == true)
                {
                    var path = dialog.FileName;

                    return Path.GetDirectoryName(path);
                }


                return null; // Return null if no directory is selected
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                // You can also show an error message to the user
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Compresses a directory into a zip file
        /// </summary>
        /// <param name="sourceDirectory">The path to the directory to compress</param>
        /// <param name="zipFileName">The name of the zip file</param>
        public static void ZipDirectory(string sourceDirectory, string zipFileName = "")
        {
            // Check if the source directory path is valid
            if (string.IsNullOrWhiteSpace(sourceDirectory))
            {
                MessageHandler.ShowAlert("Zip Error", "Invalid source directory path.");
                return;
            }

            try
            {
                // Get the desktop path
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                // Generate a zip file name if not provided
                if (string.IsNullOrWhiteSpace(zipFileName))
                {
                    zipFileName = $"CleanedProject_{DateTime.Now:yyyyMMddHHmmssfff}";
                }

                // Construct the initial path for the zip file
                string zipFilePath = Path.Combine(desktopPath, $"{zipFileName}.zip");
                int counter = 1;

                // Handle the case where a file with the same name already exists
                while (File.Exists(zipFilePath))
                {
                    zipFilePath = Path.Combine(desktopPath, $"{zipFileName}_{counter}.zip");
                    counter++;
                }

                // Check if the source directory exists
                if (Directory.Exists(sourceDirectory))
                {
                    // Create the zip file from the source directory
                    ZipFile.CreateFromDirectory(sourceDirectory, zipFilePath, CompressionLevel.Fastest, true);

                    // Delete the source directory after zipping
                    Directory.Delete(sourceDirectory, true);

                    // Show success message with the path to the created zip file
                    MessageHandler.ShowAlert("Zip Complete", $"Project has been zipped to '{zipFilePath}'");
                }
                else
                {
                    // Show an error message if the source directory does not exist
                    MessageHandler.ShowAlert("Zip Error", "The source directory does not exist.");
                }
            }
            // Handle unauthorized access to files
            catch (UnauthorizedAccessException)
            {
                MessageHandler.ShowAlert("Zip Error", "Unauthorized access to files.");
            }
            // Handle I/O exceptions
            catch (IOException ex)
            {
                MessageHandler.ShowAlert("Zip Error", $"An I/O error occurred: {ex.Message}");
            }
            // Handle any other unexpected exceptions
            catch (Exception ex)
            {
                MessageHandler.ShowAlert("Zip Error", $"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}