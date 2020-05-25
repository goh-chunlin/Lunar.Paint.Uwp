using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace Lunar.Paint.Uwp.Tests.WinAppDriver
{
    [TestClass]
    public class MainPageTests : UITest
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
        public void HideCanvas()
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
