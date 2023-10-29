<a name="readme-top"></a>

<h1 align="center">
  <br>
    <img src="https://github.com/AlecInfo/ProjectCleaner/blob/main/ProjectCleaner/Assets/accessoriessystemcleaner_512.png" alt="Logo" width="250">
  <br>
</h1>

<h1 align="center">Project Cleaner</h1>

<p align="center">
  <a href=""><img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white"></a>
  <a href=""><img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white"></a>
</p>


<p align="center">
  <a href="#description">Description</a> •
  <a href="#installation">Installation</a> •
  <a href="#usage">Usage</a> •
  <a href="#about-cleaner-files">About .cleaner Files</a> •
  <a href="#license">License</a>
</p>

![Guard image](https://github.com/AlecInfo/Investicard-Pro/blob/main/imgs/temporary2.png)

## Description

ProjectCleaner is a utility tool developed using the [WPF-UI framework](https://wpfui.lepo.co/) to help users clean and organize their project directories. It simplifies the process of removing unnecessary files and folders from a project, making it easier to share or archive projects effectively.

## Installation

To use Project Cleaner, follow these steps:

1. Clone the repository to your local machine.
2. Open the solution file in Visual Studio.
3. Build the solution to ensure all necessary dependencies are restored.
4. Run the application within the development environment or deploy it to a target environment as needed.

## Usage

1. Launch the Project Cleaner application.
2. Select a project directory that you want to clean.
3. Choose a .cleaner file that contains the rules for cleaning the project.
4. Click on the "Clean Project" button to apply the cleaning rules to the selected project directory.
5. Use the "Zip Project" button to create a compressed backup of the cleaned project.

## About .cleaner Files

.cleaner files are text files that contain rules for cleaning project directories. These rules specify patterns that are used to identify files and directories that should be removed during the cleaning process. Each rule in a .cleaner file can target specific file types, directories, or files with specific naming patterns. The tool processes these rules sequentially to perform the cleaning operations.

### Structure

The structure of a .cleaner file typically consists of one rule per line. Each rule can be one of the following types:

1. File patterns using wildcards: You can use wildcards to specify certain types of files to be removed. For example:

    ```plaintext
    *.txt
    *.exe
    ```
2. Directory patterns using regular expressions: You can specify directory patterns using regular expressions to target specific directories for removal. For instance:

    ```plaintext
    [Bb]in/
    [Oo]bj/
    ```
3. Comments: You can provide explanations or context for specific rules in the file using comments. Comments start with the `#` character. For example:

    ```plaintext
    # This rule removes all log files
    *.log
    ```
4. Special characters for matching: Special characters like asterisks (*) for wildcard matches and forward slashes (/) for denoting directories can be used. For example:

    ```plaintext
    # Remove all temporary files
    *tmp*
    ```
5. Mixed rules: You can combine these different types of rules in a single .cleaner file for comprehensive cleaning operations. Here's an example of a complete .cleaner file:

    ```plaintext
    # Remove all temporary files
    *tmp*

    # Remove all log files
    *.log

    # Remove directories
    [Bb]in/
    [Oo]bj/
    ```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

<p align="right">(<a href="#readme-top">Back to top</a>)</p>

---

> GitHub [@AlecInfo](https://github.com/AlecInfo) &nbsp;&middot;&nbsp;
