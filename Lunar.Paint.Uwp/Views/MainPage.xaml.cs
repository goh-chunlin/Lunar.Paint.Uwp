using System;
using System.Collections.Generic;
using Lunar.Paint.Uwp.EventData;
using Lunar.Paint.Uwp.ViewModels;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Lunar.Paint.Uwp.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void HeaderMenu_HeaderMenuEventOccur(object sender, RoutedEventArgs e)
        {
            switch (((HeaderMenuUpdateEventArgs)e).UpdateEvent)
            {
                case HeaderMenuUpdateEvent.Open:
                    var picker = new Windows.Storage.Pickers.FileOpenPicker();
                    picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
                    picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
                    picker.FileTypeFilter.Add(".txt");

                    StorageFile openingFile = await picker.PickSingleFileAsync();
                    if (openingFile != null)
                    {
                        string fileContent = await FileIO.ReadTextAsync(openingFile);

                        FileOutput.Text = fileContent;
                    }
                    break;

                case HeaderMenuUpdateEvent.Save:
                    var savePicker = new Windows.Storage.Pickers.FileSavePicker
                    {
                        SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary
                    };

                    savePicker.FileTypeChoices.Add("Sample Text", new List<string>() { ".txt" });

                    savePicker.SuggestedFileName = "sample";

                    StorageFile savingFile = await savePicker.PickSaveFileAsync();
                    if (savingFile != null)
                    {
                        CachedFileManager.DeferUpdates(savingFile);

                        await FileIO.WriteTextAsync(savingFile, "Something");

                        Windows.Storage.Provider.FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(savingFile);
                    }
                    break;

                case HeaderMenuUpdateEvent.DrawGridOnCanvas:
                    MainCanvas.Visibility = Visibility.Collapsed;
                    break;

                default:
                    break;
            }
        }
    }
}
