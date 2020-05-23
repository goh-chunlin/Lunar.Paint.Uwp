using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunar.Paint.Uwp.Tests.WinAppDriver
{
    public abstract class UITest
    {
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";

        // The part before "!App" will be in Package.Appxmanifest > Packaging > Package Family Name.
        // The app must also be installed (or launched for debugging) for WinAppDriver to be able to launch it.
        protected const string AppToLaunch = "2D64C875-81B7-4FCA-A474-7EB4747A4CF7_rpnzaatjdy8by!App";

        protected static WindowsDriver<WindowsElement> AppSession { get; set; }
    }
}
