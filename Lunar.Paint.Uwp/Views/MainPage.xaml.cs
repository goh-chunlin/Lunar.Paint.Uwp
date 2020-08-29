using System;
using System.Collections.Generic;
using System.Numerics;
using Lunar.Paint.Uwp.Constants;
using Lunar.Paint.Uwp.EventData;
using Lunar.Paint.Uwp.ViewModels;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Lunar.Paint.Uwp.Views
{
    public sealed partial class MainPage : Page
    {
        private bool _isShowingCanvas = true;
        private bool _isDisplayingGrid = false;

        public MainPage()
        {
            InitializeComponent();

            MainCanvas.Width = CommonConstants.DEFAULT_CANVAS_WIDTH;
            MainCanvas.Height = CommonConstants.DEFAULT_CANVAS_HEIGHT;

            MainCanvas.Draw += MainCanvas_Draw;
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
                        FileOperationOutput.Text = "File is successfully loaded.";
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

                        if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                        {
                            FileOperationOutput.Text = "File is successfully saved.";
                        }
                    }
                    break;

                case HeaderMenuUpdateEvent.ShowCanvas:
                    _isShowingCanvas = !_isShowingCanvas;

                    MainCanvas.Visibility = _isShowingCanvas ? Visibility.Visible : Visibility.Collapsed;

                    break;

                case HeaderMenuUpdateEvent.DrawGridOnCanvas:

                    _isDisplayingGrid = !_isDisplayingGrid;

                    MainCanvas.Invalidate();

                    break;

                default:
                    break;
            }
        }

        private void MainCanvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            if (_isDisplayingGrid)
            {
                using (var renderTarget = new CanvasRenderTarget(sender.Device, CommonConstants.DEFAULT_CANVAS_WIDTH, CommonConstants.DEFAULT_CANVAS_HEIGHT, sender.Dpi))
                {
                    using (var ds = renderTarget.CreateDrawingSession())
                    {
                        ds.Clear(Colors.White);

                        for (float i = 0; i < CommonConstants.DEFAULT_CANVAS_WIDTH; i += CommonConstants.GRID_WIDTH)
                        {
                            ds.DrawLine(new Vector2(i, 0), new Vector2(i, CommonConstants.DEFAULT_CANVAS_HEIGHT),
                                Colors.LightGray, 0.5f, CanvasStrokeStyles.GridLineStyle);

                            if (i < CommonConstants.DEFAULT_CANVAS_HEIGHT)
                            {
                                ds.DrawLine(new Vector2(0, i), new Vector2(CommonConstants.DEFAULT_CANVAS_WIDTH, i),
                                    Colors.LightGray, 0.5f, CanvasStrokeStyles.GridLineStyle);
                            }
                        }
                    }

                    args.DrawingSession.DrawImage(renderTarget);
                }
                
            }
        }
    }
}
