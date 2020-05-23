using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Lunar.Paint.Uwp.EventData
{
    public class HeaderMenuUpdateEventArgs : RoutedEventArgs
    {
        public HeaderMenuUpdateEvent UpdateEvent { get; set; }

        public HeaderMenuUpdateEventArgs(HeaderMenuUpdateEvent updateEvent) : base()
        {
            UpdateEvent = updateEvent;
        }
    }

    public enum HeaderMenuUpdateEvent
    {
        Save,
        Open,
        DrawGridOnCanvas
    }
}
