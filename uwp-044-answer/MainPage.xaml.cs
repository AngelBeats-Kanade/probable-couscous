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
        private ObservableCollection<NewsItem> newsItems = new ObservableCollection<NewsItem>();

        public MainPage()
        {
            InitializeComponent();
            
            MyTitleTextBlock.Text = "Financial";
            MyNavigationView.SelectedItem = MyNavigationView.MenuItems.OfType<NavigationViewItem>().First(n => n.Content.Equals("Financial"));

            NewsItemsChangeToFinancial();
        }

        private void MyNavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            string item = args.InvokedItemContainer.Content.ToString();
            if (item == "Food")
            {
                MyTitleTextBlock.Text = "Food";
                NewsItemsChangeToFood();
            }
            else if (item == "Financial")
            {
                MyTitleTextBlock.Text = "Financial";
                NewsItemsChangeToFinancial();
            }
        }

        private void NewsItemsChangeToFinancial()
        {
            newsItems.Clear();
            newsItems.Add(new NewsItem() { Id = 1, Category = "Financial", Headline = "Lorem Ipsum", Subhead = "doro sit amet", Dateline = "Nunc tristique nec", Image = "Assets/Financial1.png" });
            newsItems.Add(new NewsItem() { Id = 2, Category = "Financial", Headline = "Etiam ac felis viverra", Subhead = "vulputate nisl ac, aliquet nisi", Dateline = "tortor porttitor, eu fermentum ante congue", Image = "Assets/Financial2.png" });
            newsItems.Add(new NewsItem() { Id = 3, Category = "Financial", Headline = "Integer sed turpis erat", Subhead = "Sed quis hendrerit lorem, quis interdum dolor", Dateline = "in viverra metus facilisis sed", Image = "Assets/Financial3.png" });
            newsItems.Add(new NewsItem() { Id = 4, Category = "Financial", Headline = "Proin sem neque", Subhead = "aliquet quis ipsum tincidunt", Dateline = "Integer eleifend", Image = "Assets/Financial4.png" });
            newsItems.Add(new NewsItem() { Id = 5, Category = "Financial", Headline = "Mauris bibendum non leo vitae tempor", Subhead = "In nisl tortor, eleifend sed ipsum eget", Dateline = "Curabitur dictum augue vitae elementum ultrices", Image = "Assets/Financial5.png" });
        }

        private void NewsItemsChangeToFood()
        {
            newsItems.Clear();
            newsItems.Add(new NewsItem() { Id = 6, Category = "Food", Headline = "Lorem ipsum", Subhead = "dolor sit amet", Dateline = "Nunc tristique nec", Image = "Assets/Food1.png" });
            newsItems.Add(new NewsItem() { Id = 7, Category = "Food", Headline = "Etiam ac felis viverra", Subhead = "vulputate nisl ac, aliquet nisi", Dateline = "tortor porttitor, eu fermentum ante congue", Image = "Assets/Food2.png" });
            newsItems.Add(new NewsItem() { Id = 8, Category = "Food", Headline = "Integer sed turpis erat", Subhead = "Sed quis hendrerit lorem, quis interdum dolor", Dateline = "in viverra metus facilisis sed", Image = "Assets/Food3.png" });
            newsItems.Add(new NewsItem() { Id = 9, Category = "Food", Headline = "Proin sem neque", Subhead = "aliquet quis ipsum tincidunt", Dateline = "Integer eleifend", Image = "Assets/Food4.png" });
            newsItems.Add(new NewsItem() { Id = 10, Category = "Food", Headline = "Mauris bibendum non leo vitae tempor", Subhead = "In nisl tortor, eleifend sed ipsum eget", Dateline = "Curabitur dictum augue vitae elementum ultrices", Image = "Assets/Food5.png" });
        }
    }
}
