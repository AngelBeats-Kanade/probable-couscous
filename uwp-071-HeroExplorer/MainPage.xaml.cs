using HeroExplorer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace HeroExplorer
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<Character> Characters { get; set; } = new ObservableCollection<Character>();
        public ObservableCollection<Comic> Comics { get; set; } = new ObservableCollection<Comic>();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MyProgressRing.IsActive = true;

            AttributionTextBlock.Text = (await MarvelFacade.GetCharacterDataWrapperAsync()).attributionText;
            _ = await MarvelFacade.AddCharactersAsync(Characters);

            MyProgressRing.IsActive = false;
        }

        private async void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ComicClear();
            MyProgressRing.IsActive = true;

            Character selectedCharacter = (Character)e.ClickedItem;
            DetailNameTextBlock.Text = selectedCharacter.name;
            DetailDescriptionTextBlock.Text = selectedCharacter.description;
            BitmapImage characterImage = new BitmapImage
            {
                UriSource = new Uri(selectedCharacter.thumbnail.large, UriKind.Absolute)
            };
            DetailImage.Source = characterImage;

            Comics.Clear();
            _ = await MarvelFacade.AddComicsAsync(Comics, selectedCharacter.id);

            MyProgressRing.IsActive = false;
        }

        private void ComicGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            MyProgressRing.IsActive = true;

            Comic selectedComic = (Comic)e.ClickedItem;
            ComicDetailNameTextBlock.Text = selectedComic.title;
            ComicDetailDescriptionTextBlock.Text = selectedComic.description ?? "";
            BitmapImage comicImage = new BitmapImage
            {
                UriSource = new Uri(selectedComic.thumbnail.large, UriKind.Absolute)
            };
            ComicDetailImage.Source = comicImage;

            MyProgressRing.IsActive = false;
        }

        private void ComicClear()
        {
            ComicDetailNameTextBlock.Text = string.Empty;
            ComicDetailDescriptionTextBlock.Text = string.Empty;
            ComicDetailImage.Source = null;
        }
    }
}
