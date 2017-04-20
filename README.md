# Mobile Center - Quickstart for Xamarin.Forms

This sample app will help you onboard to Mobile Center Build, Analytics and Crashes services within few minutes.

## Steps:
1.  Login to the [portal](https://mobile.azure.com) and create an app representing this quickstart application (you need to do it twice - one for iOS and one for Android).
2.  If you want to onboard to Build service, you can fork this repo and create a copy of your own. Then select the iOS or Android app that you just created, go to Build service, select a service (GitHub) and set up the master branch to build every time a commit happens.
3.  If you don't need to set up Build service at this point, you can just download this repo. Once you download, you should easily be able to run the app in Visual Studio.
4.  Open App.xaml.cs file and replace {IOS_APP_SECRET} and {ANDROID_APP_SECRET} placeholders with actual app secrets. You can get app secrets in the portal - go to 'Getting Started' page for the app and click 'Manage app' buton.
5.  Run the app in the simulator.
6.  You should now be able to see Analytics data in the portal right away.
7.  From the main page of the proposed Xamarin app you can go to the page with samples for either Analytics or Crashes service.
8.  Go to Crashes. You can:
      - Enable/disable module.
      - Simulate crash. By default after restarting the app prompts to send report (user confirmation), but it can be turned off in code. Also it shows alert to notify if the report was sent successfully or not.
      - Get error report. The app will display some details.
9. Go to Analytics. You can:
     - Enable/disable module.
	 - Track an event with the specified name.
