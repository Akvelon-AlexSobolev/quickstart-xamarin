//Define DEBUG symbol in order Crashes.GenerateTestCrash() to work in release mode
#define DEBUG

using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickstartApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.GoToPageCommand = new Command<Type>(this.GoToPage);
            InitializeComponent();
        }

        public ICommand GoToPageCommand { get; private set; }

        private void GoToPage(Type pageType)
        {
            var page = (Page)Activator.CreateInstance(pageType);
            Navigation.PushAsync(page);
        }

        private void OnGoToPageClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CrashesPage());
        }
    }
}
