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

namespace UWP开发入门
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private readonly List<(string Name, Type Page)> _pages = new List<(string Name, Type Page)>
        {
            ("Donuts", typeof(DonutPage)),
            ("Coffee", typeof(CoffeePage)),
            ("Schedule",typeof(SchedulePage)),
            ("Complete",typeof(CompletePage)),
        };

        private void MyNavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if(args.InvokedItemContainer != null)
            {
                var item = _pages.FirstOrDefault(p => p.Name.Equals(args.InvokedItem.ToString()));
                MyFrame.Navigate(item.Page);
            }
        }

        private void MyFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if(MyFrame.SourcePageType != null)
            {
                var item = _pages.FirstOrDefault(p => p.Page == e.SourcePageType);
                MyNavigationView.SelectedItem = MyNavigationView.MenuItems
                                                    .OfType<NavigationViewItem>()
                                                        .First(n => n.Name.Equals(item.Name));
            }
        }

        private void MyFrame_Loaded(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(DonutPage));
        }

        private void MyFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load page:" + e.SourcePageType.FullName);
        }
    }
}
