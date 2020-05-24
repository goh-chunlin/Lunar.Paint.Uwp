using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace Lunar.Paint.Uwp.Tests.WinAppDriver
{
    [TestClass]
    public class FileOperationTests : UITest
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            
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
        public void TestSaveFile()
        {
            // Test popup dialog
            //
            // References: 
            // 1. https://github.com/microsoft/WinAppDriver/blob/master/Samples/C%23/NotepadTest/ScenarioPopupDialog.cs
            // 2. https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-read-from-a-text-file
            var headerMenuSaveButton = AppSession.FindElementByAccessibilityId("HeaderMenuSaveButton");
            headerMenuSaveButton.Click();

            Thread.Sleep(TimeSpan.FromSeconds(1)); // Wait for 1 second until the save dialog appears

            AppSession.FindElementByAccessibilityId("FileNameControlHost").SendKeys("sample-test");
            AppSession.FindElementByName("Save").Click();

            // Check if the Save As dialog appears when there's a leftover test file from previous test run
            try
            {
                Thread.Sleep(TimeSpan.FromSeconds(1)); // Wait for 1 second in case save as dialog appears
                AppSession.FindElementByName("Confirm Save As").FindElementByName("Yes").Click();
            }
            catch { }

            Thread.Sleep(TimeSpan.FromSeconds(1.5)); // Wait for 1.5 seconds

            var fileOutput = AppSession.FindElementByAccessibilityId("FileOutput");

            Assert.AreEqual("File is saved.", fileOutput.Text);
        }

        [TestMethod]
        public void TestOpenFile() 
        {
            var headerMenuOpenButton = AppSession.FindElementByAccessibilityId("HeaderMenuOpenButton");
            headerMenuOpenButton.Click();

            Thread.Sleep(TimeSpan.FromSeconds(1)); // Wait for 1 second until the save dialog appears

            AppSession.FindElementByAccessibilityId("1148").Click();
            Thread.Sleep(TimeSpan.FromSeconds(1.5)); // Wait for 1.5 seconds
            AppSession.Keyboard.SendKeys("sample-test.txt");

            AppSession.FindElementByAccessibilityId("1").Click();

            Thread.Sleep(TimeSpan.FromSeconds(1.5)); // Wait for 1.5 seconds

            var fileOutput = AppSession.FindElementByAccessibilityId("FileOutput");

            Assert.AreEqual("Something", fileOutput.Text);
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
