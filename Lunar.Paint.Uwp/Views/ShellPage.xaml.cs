using System;

using Lunar.Paint.Uwp.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace Lunar.Paint.Uwp.Views
{
    // TODO WTS: Change the icons and titles for all NavigationViewItems in ShellPage.xaml.
    public sealed partial class ShellPage : Page
    {
        private ShellViewModel ViewModel => DataContext as ShellViewModel;

        public Frame ShellFrame => shellFrame;

        public ShellPage()
        {
            InitializeComponent();
        }

        public void SetRootFrame(Frame frame)
        {
            shellFrame.Content = frame;
            navigationViewHeaderBehavior.Initialize(frame);
            ViewModel.Initialize(frame, navigationView);
        }

        private void WelcomeScreenEnterButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            WelcomeScreen.Visibility = Visibility.Collapsed;
        }
    }
}
