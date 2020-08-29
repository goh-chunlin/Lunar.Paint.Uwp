using Lunar.Paint.Uwp.EventData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Lunar.Paint.Uwp.Controls
{
    public sealed partial class HeaderMenu : UserControl
    {
        public bool IsShowGrid { get; private set; }

        public HeaderMenu()
        {
            this.InitializeComponent();
        }

        public event RoutedEventHandler HeaderMenuEventOccur;

        private void HeaderMenuOpenButton_Click(object sender, RoutedEventArgs e)
        {
            HeaderMenuEventOccur?.Invoke(sender, new HeaderMenuUpdateEventArgs(HeaderMenuUpdateEvent.Open));
        }

        private void HeaderMenuSaveButton_Click(object sender, RoutedEventArgs e)
        {
            HeaderMenuEventOccur?.Invoke(sender, new HeaderMenuUpdateEventArgs(HeaderMenuUpdateEvent.Save));
        }

        private void HeaderMenuShowCanvasButton_Clicked(object sender, RoutedEventArgs e)
        {
            IsShowGrid = ((AppBarToggleButton)sender).IsChecked.HasValue && ((AppBarToggleButton)sender).IsChecked.Value;
            HeaderMenuEventOccur?.Invoke(sender, new HeaderMenuUpdateEventArgs(HeaderMenuUpdateEvent.ShowCanvas));
        }

        private void HeaderMenuShowGridButton_Clicked(object sender, RoutedEventArgs e)
        {
            IsShowGrid = ((AppBarToggleButton)sender).IsChecked.HasValue && ((AppBarToggleButton)sender).IsChecked.Value;
            HeaderMenuEventOccur?.Invoke(sender, new HeaderMenuUpdateEventArgs(HeaderMenuUpdateEvent.DrawGridOnCanvas));
        }
    }
}
