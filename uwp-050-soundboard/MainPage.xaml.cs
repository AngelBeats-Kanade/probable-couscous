using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UWP开发入门.Model;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace UWP开发入门
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly ObservableCollection<Sound> Sounds = new ObservableCollection<Sound>();
        private readonly ObservableCollection<MenuItem> MenuItems = new ObservableCollection<MenuItem>();

        public MainPage()
        {
            InitializeComponent();
            //MyNavigationView.SelectedItem = MyNavigationView.MenuItems.OfType<NavigationViewItem>().FirstOrDefault(p => p.Content.Equals("Animals"));
            _ = SoundManager.GetSounds(Sounds, SoundCategory.Animals, SoundCategory.Cartoons, SoundCategory.Taunts, SoundCategory.Warnings);
            _ = MenuItem.GetMenuItems(MenuItems);
        }

        private void MyAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            IEnumerable<string> suggestions = from suggestion in SoundManager.GetSounds()
                                              where suggestion.Name.StartsWith(sender.Text)
                                              select suggestion.Name;

            MyAutoSuggestBox.ItemsSource = suggestions.ToList();
        }

        private void MyAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            MyTitleTextBlock.Text = sender.Text;
            MyNavigationView.SelectedItem = null;
            _ = SoundManager.GetSounds(Sounds, sender.Text);
            MyBackButton.Visibility = Visibility.Visible;
        }

        private void MyNavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            MyTitleTextBlock.Text = args.InvokedItem.ToString();
            _ = SoundManager.GetSounds(Sounds, (SoundCategory)args.InvokedItem);
            MyBackButton.Visibility = Visibility.Visible;
        }

        private void MyGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            MyMediaElement.Source = new Uri(BaseUri, ((Sound)e.ClickedItem).AudioFile);
        }

        private void MyBackButton_Click(object sender, RoutedEventArgs e)
        {
            MyTitleTextBlock.Text = "All Sounds";
            MyNavigationView.SelectedItem = null;
            _ = SoundManager.GetSounds(Sounds, SoundCategory.Animals, SoundCategory.Cartoons, SoundCategory.Taunts, SoundCategory.Warnings);
            MyBackButton.Visibility = Visibility.Collapsed;
        }

        private void MyGridView_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;

            e.DragUIOverride.Caption = "Drop to create a custom sound and title";
            e.DragUIOverride.IsCaptionVisible = true;
            e.DragUIOverride.IsContentVisible = true;
            e.DragUIOverride.IsGlyphVisible = true;
        }

        private async void MyGridView_Drop(object sender, DragEventArgs e)
        {

            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                IReadOnlyList<IStorageItem> items = await e.DataView.GetStorageItemsAsync();

                if (items.Any())
                {
                    StorageFile storageFile = items[0] as StorageFile;
                    string contentType = storageFile.ContentType;

                    if (contentType.Equals("audio/wav") || contentType.Equals("audio/mpeg"))
                    {
                        MyMediaElement.SetSource(await storageFile.OpenAsync(FileAccessMode.Read), contentType);
                        MyMediaElement.Play();
                    }
                }
            }
        }
    }
}
