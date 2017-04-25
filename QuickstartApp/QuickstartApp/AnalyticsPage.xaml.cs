using System;
using System.Collections.Generic;

using Microsoft.Azure.Mobile.Analytics;
using Xamarin.Forms;

namespace QuickstartApp
{
    public partial class AnalyticsPage : ContentPage
    {
        public AnalyticsPage()
        {
            InitializeComponent();
            this.SetModuleSwitchText();
        }

        /// <summary>
        /// Gets or sets a value indicating whether Analytics module is enabled.
        /// </summary>
        public bool Enabled
        {
            get { return Analytics.Enabled; }
            set
            {
                Analytics.Enabled = value;
                this.SetModuleSwitchText();
            }
        }

        private void SetModuleSwitchText()
        {
            if (this.moduleSwitchLabel != null)
            {
                this.moduleSwitchLabel.Text = "Analytics module is now " + (Enabled ? "enabled  " : "disabled ");
            }
        }

        private void OnTrackEventClicked(object sender, EventArgs e)
        {
            // Track event with some properties
            Analytics.TrackEvent(
                this.eventName.Text,
                new Dictionary<string, string>
                {
                    { "Sample Property 1", "Sample Value 1" },
                    { "Sample Property 2", "Sample Value 2" }
                });
        }

        private void OnEventNameChanged(object sender, EventArgs e)
        {
            var eventName = ((Entry)sender).Text;
            this.trackEvent.IsEnabled = !string.IsNullOrEmpty(eventName);
        }
    }
}
