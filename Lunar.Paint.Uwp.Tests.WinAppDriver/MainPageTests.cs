using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace Lunar.Paint.Uwp.Tests.WinAppDriver
{
    [TestClass]
    public class MainPageTests : UITest
    {
        //private static string _screenshotFolder;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            //// TODO WTS: change the location where screenshots are saved.
            //// Create separate folders for saving the results of each test run.
            //_screenshotFolder = $"{Path.GetPathRoot(Environment.CurrentDirectory)}\\Temp\\Screenshots\\{DateTime.Now.ToString("dd_HHmm")}\\";

            //// Make sure the folder exists or saving screenshots will fail.
            //if (!Directory.Exists(_screenshotFolder))
            //{
            //    Directory.CreateDirectory(_screenshotFolder);
            //}
        }

        [TestInitialize]
        public void LaunchApp()
        {
            if (AppSession == null)
            {
                var appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("app", AppToLaunch);
                appiumOptions.AddAdditionalCapability("deviceName", "WindowsPC");
                AppSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);

                Assert.IsNotNull(AppSession, "Unable to launch app.");

                AppSession.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);

                // Maximize the window to have a consistent size and position.
                AppSession.Manage().Window.Maximize();

                var welcomeScreenEnterButton = AppSession.FindElementByAccessibilityId("WelcomeScreenEnterButton");
                welcomeScreenEnterButton.Click();
            }
        }

        [TestMethod]
        public void HideCnavas()
        {
            var mainCanvas = AppSession.FindElementByAccessibilityId("MainCanvas");

            var headerMenuShowGridButton = AppSession.FindElementByAccessibilityId("HeaderMenuShowGridButton");
            headerMenuShowGridButton.Click();

            Assert.AreEqual(false, mainCanvas.Displayed);
        }

        [TestCleanup]
        public void TearDown()
        {
            if (AppSession != null)
            {
                AppSession.Dispose();
                AppSession = null;
            }
        }
    }
}
