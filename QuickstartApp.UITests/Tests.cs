using System.Linq;

using NUnit.Framework;
using Xamarin.UITest;

namespace QuickstartApp.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        // Uncomment the test below to use REPL that Xamarin.UITest provides.
        // It allows developers to interact with a screen while the application is running and simplifies creating tests.

        //[Test]
        //public void Repl()
        //{
        //    app.Repl();
        //}

        [Test]
        public void TestAnalyticsModuleSwitch()
        {
            // Act
            app.Tap(c => c.Marked("GoToAnalytics")); // Tap a button to go to Analytics page

            var label = app.Query(c => c.Marked("ModuleSwitchLabel")).Single();
            var originalText = label.Text;

            app.Tap(c => c.Marked("ModuleSwitch")); // Tap a switch to enable or disable module

            // Assert
            Assert.AreEqual("Analytics module is now enabled  ", originalText); // Module is enabled by default

            label = app.Query(c => c.Marked("ModuleSwitchLabel")).Single();
            Assert.AreEqual("Analytics module is now disabled ", label.Text); // Module should be disabled now
        }

        [Test]
        public void TestTrackEventButton()
        {
            // Act
            app.Tap(c => c.Marked("GoToAnalytics")); // Tap a button to go to Analytics page
            app.ClearText(c => c.Marked("EventNameEntry")); // Clear text field

            var button = app.Query(c => c.Marked("TrackEventButton")).Single();
            var enabledAfterClearingEventName = button.Enabled;

            app.EnterText(c => c.Marked("EventNameEntry"), "Sample Event");

            // Assert
            Assert.IsFalse(enabledAfterClearingEventName); // Button should be disabled if text field is empty

            button = app.Query(c => c.Marked("TrackEventButton")).Single();
            Assert.IsTrue(button.Enabled); // Button should be enabled if text field is not empty
        }
    }
}

