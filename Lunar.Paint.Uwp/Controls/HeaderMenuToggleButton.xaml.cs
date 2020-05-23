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
    public sealed partial class HeaderMenuToggleButton : UserControl
    {
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(HeaderMenuToggleButton), new PropertyMetadata(""));

        public string Glyph
        {
            get { return (string)GetValue(GlyphProperty); }
            set { SetValue(GlyphProperty, value); }
        }

        public static readonly DependencyProperty GlyphProperty =
            DependencyProperty.Register("Glyph", typeof(string), typeof(HeaderMenuToggleButton), new PropertyMetadata(""));

        private bool IsButtonEnabled
        {
            get { return (bool)GetValue(IsButtonEnabledProperty); }
            set { SetValue(IsButtonEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsButtonEnabledProperty =
            DependencyProperty.Register("IsEnabled", typeof(bool), typeof(HeaderMenuToggleButton), new PropertyMetadata(true));

        public event RoutedEventHandler Click;

        public HeaderMenuToggleButton()
        {
            this.InitializeComponent();
        }

        private void OnAppBarToggleButtonClick(object sender, RoutedEventArgs args)
        {
            Click?.Invoke(sender, args);
        }
    }
}
