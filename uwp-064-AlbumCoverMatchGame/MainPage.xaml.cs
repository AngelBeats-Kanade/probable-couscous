using AlbumCoverMatchGame.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace AlbumCoverMatchGame
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly ObservableCollection<Song> songs = new ObservableCollection<Song>();
        private readonly ObservableCollection<StorageFile> allSongsStorageFiles = new ObservableCollection<StorageFile>();
        private bool _isMusicPlaying = false;
        private int _round = 0;
        private int _score = 0;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            StartUpProgressRing.IsActive = true;

            StorageFolder musicFolder = KnownFolders.MusicLibrary;
            await RetrieveFilesInFoldersAsync(allSongsStorageFiles, musicFolder);

            List<StorageFile> randomSongs = await GetRandomSongsAsync(allSongsStorageFiles);
            await PopulateSongListAsync(randomSongs);

            StartUpProgressRing.IsActive = false;

            InstructionTextBlock.Text = string.Format($"Get ready for round {++_round} ...");
            CountDown.Begin();
        }

        private async Task RetrieveFilesInFoldersAsync(ObservableCollection<StorageFile> storageFiles, StorageFolder storageFolder)
        {
            foreach (StorageFile item in await storageFolder.GetFilesAsync())
            {
                if (item.FileType == ".mp3")
                {
                    storageFiles.Add(item);
                }
            }
            foreach (StorageFolder item in await storageFolder.GetFoldersAsync())
            {
                await RetrieveFilesInFoldersAsync(storageFiles, item);
            }
        }

        private async Task<List<StorageFile>> GetRandomSongsAsync(ObservableCollection<StorageFile> allSongs)
        {
            Random random = new Random();
            List<StorageFile> randomSongs = new List<StorageFile>();

            while (randomSongs.Count < 10)
            {
                int randomNumber = random.Next(allSongs.Count);
                MusicProperties randomSongMusicProperties = await allSongs[randomNumber].Properties.GetMusicPropertiesAsync();
                bool isDuplicate = false;

                foreach (StorageFile song in randomSongs)
                {
                    MusicProperties songMusicProperties = await song.Properties.GetMusicPropertiesAsync();
                    if (string.IsNullOrEmpty(randomSongMusicProperties.Album) || randomSongMusicProperties.Album == songMusicProperties.Album)
                    {
                        isDuplicate = true;
                    }
                }

                if (!isDuplicate)
                {
                    randomSongs.Add(allSongs[randomNumber]);
                }
            }

            return randomSongs;
        }

        private async Task PopulateSongListAsync(List<StorageFile> files)
        {
            int id = 0;

            songs.Clear();

            foreach (StorageFile file in files)
            {
                MusicProperties songProperities = await file.Properties.GetMusicPropertiesAsync();
                StorageItemThumbnail currentThumb = await file.GetThumbnailAsync(ThumbnailMode.MusicView, 200, ThumbnailOptions.UseCurrentScale);
                BitmapImage albumCover = new BitmapImage();
                albumCover.SetSource(currentThumb);
                Song song = new Song
                {
                    Id = id,
                    Artist = songProperities.Artist,
                    Album = songProperities.Album,
                    Title = songProperities.Title,
                    SongFile = file,
                    Cover = albumCover,
                    Selected = false,
                    Used = false
                };

                songs.Add(song);
                id++;
            }
        }

        private async void SongGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            CountDown.Pause();
            MyMediaElement.Stop();

            Song clickedSong = (Song)e.ClickedItem;
            Uri uri;
            if (clickedSong.Selected)
            {
                uri = new Uri("ms-appx:///Assets/correct.png");
                _score += (int)MyProgressBar.Value;
            }
            else
            {
                uri = new Uri("ms-appx:///Assets/incorrect.png");
                _score -= (int)MyProgressBar.Value;
            }
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            Windows.Storage.Streams.IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
            await clickedSong.Cover.SetSourceAsync(fileStream);

            ShowAnswer();
        }

        private void StartCoolDown()
        {
            SolidColorBrush solidColorBrush = new SolidColorBrush();
            _isMusicPlaying = false;
            solidColorBrush.Color = Windows.UI.Colors.Blue;
            MyProgressBar.Foreground = solidColorBrush;
            InstructionTextBlock.Text = string.Format($"Get ready for round {++_round} ...");
            InstructionTextBlock.Foreground = solidColorBrush;

            SongGridView.IsItemClickEnabled = false;
            CountDown.Begin();
        }

        private void StartCountDown()
        {
            SolidColorBrush solidColorBrush = new SolidColorBrush();
            _isMusicPlaying = true;
            solidColorBrush.Color = Windows.UI.Colors.Red;
            MyProgressBar.Foreground = solidColorBrush;
            InstructionTextBlock.Text = "GO!";
            InstructionTextBlock.Foreground = solidColorBrush;

            SongGridView.IsItemClickEnabled = true;
            CountDown.Begin();
        }

        private async void CountDown_Completed(object sender, object e)
        {
            if (_isMusicPlaying)
            {
                MyMediaElement.Stop();
                _score -= 100;
                ShowAnswer();
            }
            else
            {
                Song randomSong = GetSong();
                MyMediaElement.SetSource(await randomSong.SongFile.OpenAsync(FileAccessMode.Read), randomSong.SongFile.ContentType);
                StartCountDown();
            }
        }

        private async void PlayAgainButton_Click(object sender, RoutedEventArgs e)
        {
            PlayAgainButton.Visibility = Visibility.Collapsed;
            List<StorageFile> randomSongs = await GetRandomSongsAsync(allSongsStorageFiles);
            await PopulateSongListAsync(randomSongs);
            StartCoolDown();
            ResultTextBlock.Text = "";
            TitleTextBlock.Text = "";
            ArtistTextBlock.Text = "";
            AlbumTextBlock.Text = "";
        }

        private Song GetSong()
        {
            Random random = new Random();
            IEnumerable<Song> unUsedSong = from song in songs
                                           where song.Used == false
                                           select song;
            int randomNumber = random.Next(unUsedSong.Count());
            unUsedSong.ElementAt(randomNumber).Used = true;
            for (int i = 0; i < 10; i++)
            {
                songs.ElementAt(i).Selected = i == randomNumber;
            }
            return unUsedSong.ElementAt(randomNumber);
        }

        private void ShowAnswer()
        {
            Song correctSong = songs.FirstOrDefault(p => p.Selected);
            ResultTextBlock.Text = string.Format($"Total score: {_score}");
            TitleTextBlock.Text = string.Format($"Correct song: {correctSong.Title}");
            ArtistTextBlock.Text = string.Format($"Performed by: {correctSong.Artist}");
            AlbumTextBlock.Text = string.Format($"On Album: {correctSong.Album}");

            if (_round >= 5)
            {
                InstructionTextBlock.Text = string.Format($"Game over... Your total score is {_score}");
                _round = 0;
                _score = 0;
                PlayAgainButton.Visibility = Visibility.Visible;
            }
            else
            {
                StartCoolDown();
            }
        }
    }
}
