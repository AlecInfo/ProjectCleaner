/// \Project ProjectCleaner
/// \File MessageHandler.cs
/// \Brief Contains the MessageHandler class used to show messages
/// \Author Alec Piette
/// \History 26.10.2023 - Created File

using ProjectCleaner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCleaner.Helpers
{
    public class MessageHandler
    {
        // Fields
        private const string CLOSEBUTTONTEXT = "Close";
        private const string PRIMARYBUTTONTEXT = "OK";
        private const string SECONDARYBUTTONTEXT = "Cancel";


        /// <summary>
        /// Show a simple message dialog
        /// </summary>
        /// <param name="pTitle">The title</param>
        /// <param name="pMessage">The message</param>
        /// <param name="pCloseButtonText">The close button text</param>
        /// <param name="pPrimaryButtonText">The primary button text</param>
        /// <param name="pSecondaryButtonText">The secondary button text</param>
        public static void ShowMessage(string pTitle, string pMessage, string pCloseButtonText = CLOSEBUTTONTEXT, string pPrimaryButtonText = PRIMARYBUTTONTEXT, string pSecondaryButtonText = SECONDARYBUTTONTEXT)
        {
            var contentDialogService = App.GetService<IContentDialogService>();
            if (contentDialogService != null)
            {
                contentDialogService.ShowSimpleDialogAsync(new SimpleContentDialogCreateOptions()
                {
                    Title = pTitle,
                    Content = pMessage,
                    CloseButtonText = pCloseButtonText,
                    PrimaryButtonText = pPrimaryButtonText,
                    SecondaryButtonText = pSecondaryButtonText
                });
            }
            else
            {
                // Gérer le cas où le service de dialogue de contenu n'est pas disponible
                Console.WriteLine("Content dialog service is not available.");
            }
        }


        /// <summary>
        /// Show a alert message dialog
        /// </summary>
        /// <param name="pTitle">The title</param>
        /// <param name="pMessage">The message</param>
        /// <param name="pCloseButtonText">The close button text</param>
        public static void ShowAlert(string pTitle, string pMessage, string pCloseButtonText = CLOSEBUTTONTEXT)
        {
            var contentDialogService = App.GetService<IContentDialogService>();
            if (contentDialogService != null)
            {
                contentDialogService.ShowAlertAsync(pTitle, pMessage, pCloseButtonText);
            }
            else
            {
                // Gérer le cas où le service de dialogue de contenu n'est pas disponible
                Console.WriteLine("Content dialog service is not available.");
            }
        }
    }
}


