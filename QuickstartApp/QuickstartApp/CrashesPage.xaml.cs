using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Microsoft.Azure.Mobile.Crashes;
using Xamarin.Forms;

namespace QuickstartApp
{
    public partial class CrashesPage : ContentPage
    {
        /// <summary>
        /// To demonstrate user confirmation feature for crash reports, this field is set to true by default.
        /// If set to false, module sends crash reports automatically.
        /// </summary>
        private static bool promptToSendErrorReport = true;

        private static string errorReportMessage;

        public CrashesPage()
        {
            InitializeComponent();
            this.SetModuleSwitchText();

            if (Crashes.HasCrashedInLastSession && promptToSendErrorReport)
            {
                this.PromptToSendErrorReport();
            }

            if (!string.IsNullOrEmpty(errorReportMessage))
            {
                this.NotifyAboutErrorReport();
            }
        }

        /// <summary>
        /// Gets or sets a value that indicating whether Crashes module is enabled.
        /// </summary>
        public bool Enabled
        {
            get { return Crashes.Enabled; }
            set
            {
                Crashes.Enabled = value;
                this.SetModuleSwitchText();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the app crashed during last session.
        /// </summary>
        public bool HasCrashedInLastSession
        {
            get { return Crashes.HasCrashedInLastSession; }
        }

        internal static void Initialize()
        {
            // Set this callback if you'd like to decide if a particular crash needs to be processed or not.
            // For example, there could be some system level crashes that you'd want to ignore.
            Crashes.ShouldProcessErrorReport = report =>
            {
                // Return true if the crash report should be processed, otherwise false.
                return true;
            };

            // You can use this callback to tell the module to await user confirmation before sending any crash reports.
            // If you return true, your app should obtain user permission and notify the module by calling Crashes.NotifyUserConfirmation().
            Crashes.ShouldAwaitUserConfirmation = () =>
            {
                // Return true if module should await user confirmation, otherwise false.
                return promptToSendErrorReport;
            };

            // This callback will be invoked just before the crash is sent to Mobile Center.
            Crashes.SendingErrorReport += (s, args) =>
            {
                Debug.WriteLine($"Sending error report: Id = {args.Report.Id}");
            };

            // This callback will be invoked when crash report was successfully sent.
            Crashes.SentErrorReport += (s, args) =>
            {
                Debug.WriteLine($"Error report sent: Id = {args.Report.Id}");

                errorReportMessage = $"Application crashed during last session. Error report sent.";
                OnErrorReportProcessed();
            };

            // This callback will be invoked when the module failed to send crash report.
            Crashes.FailedToSendErrorReport += (s, args) =>
            {
                Debug.WriteLine($"Failed to send error report.");
                Debug.WriteLine(args.Exception);

                errorReportMessage = $"Application crashed during last session. Failed to send error report.";
                OnErrorReportProcessed();
            };
        }

        private static CrashesPage GetCurrentPage()
        {
            var mainPage = Application.Current.MainPage;
            return mainPage.Navigation.NavigationStack.Last() as CrashesPage;
        }

        private static void OnErrorReportProcessed()
        {
            var crashesPage = GetCurrentPage();
            if (crashesPage != null)
            {
                crashesPage.NotifyAboutErrorReport();
            }
        }

        private void SetModuleSwitchText()
        {
            if (this.moduleSwitchLabel != null)
            {
                this.moduleSwitchLabel.Text = "Crashes module is now " + (Enabled ? "enabled  " : "disabled ");
            }
        }

        private void OnSimulateCrashClicked(object sender, EventArgs e)
        {
            Crashes.GenerateTestCrash();
        }

        private async void OnGetCrashReportClicked(object sender, EventArgs e)
        {
            var report = await Crashes.GetLastSessionCrashReportAsync();

            var builder = new StringBuilder();
            builder.AppendLine("Here are some details:");
            builder.AppendLine();
            builder.AppendLine($"Exception = \"{report.Exception.GetType()}: {report.Exception.Message}\"");
            builder.AppendLine();
            builder.AppendLine($"Device = \"{report.Device.Model}\"");
            builder.AppendLine();
            builder.AppendLine($"OS = \"{report.Device.OsName} {report.Device.OsVersion}\"");
            this.crashReport.Text = builder.ToString();
        }

        private async void PromptToSendErrorReport()
        {
            var action = await DisplayActionSheet("Error report action", null, null, Enum.GetNames(typeof(UserConfirmation)));
            var userConfirmation = (UserConfirmation)Enum.Parse(typeof(UserConfirmation), action);
            promptToSendErrorReport = false;

            Crashes.NotifyUserConfirmation(userConfirmation);
        }

        private void NotifyAboutErrorReport()
        {
            DisplayAlert("Message", errorReportMessage, "OK");
            errorReportMessage = null;
        }
    }
}
