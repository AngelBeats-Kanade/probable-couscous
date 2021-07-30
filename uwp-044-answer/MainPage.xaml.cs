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
using UWP开发入门.Models;
using System.Collections.ObjectModel;

namespace UWP开发入门
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly ObservableCollection<NewsItem> newsItems = new ObservableCollection<NewsItem>();

        public MainPage()
        {
            InitializeComponent();
            NewsItem.GetNews("Financial", newsItems);
            MyTitleTextBlock.Text = "Financial";
            MyNavigationView.SelectedItem = MyNavigationView.MenuItems.OfType<NavigationViewItem>().First(n => n.Content.Equals("Financial"));
        }

        private void MyNavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            string item = args.InvokedItemContainer.Content.ToString();
            if (item == "Food")
            {
                MyTitleTextBlock.Text = "Food";
                NewsItem.GetNews("Food", newsItems);
            }
            else if (item == "Financial")
            {
                MyTitleTextBlock.Text = "Financial";
                NewsItem.GetNews("Financial", newsItems);
            }
        }
    }
}
