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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace UWP开发入门
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CoffeePage : Page
    {
        public CoffeePage()
        {
            this.InitializeComponent();
        }

        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ((MenuFlyoutItem)sender)?.Name.ToString();
            var selectedText = ((MenuFlyoutItem)sender)?.Text.ToString();

            if (selectedItem == "RoastButtonNone" || selectedItem == "RoastButtonDark" || selectedItem == "RoastButtonMedium")
            {
                MyRoastTextBlock.Text = selectedText;
                if (MyRoastTextBlock.Text == "None")
                {
                    MySweetenerTextBlock.Text = "";
                    MyCreamTextBlock.Text = "";
                }
            }
            else if (selectedItem == "SweetenerButtonNone" || selectedItem == "SweetenerButtonSugar")
            {
                MySweetenerTextBlock.Text = MyRoastTextBlock.Text == "None" ||
                                            string.IsNullOrEmpty(MyRoastTextBlock.Text) ||
                                            selectedItem == "SweetenerButtonNone"
                                            ? ""
                                            : string.Format(" + {0}", selectedText);
            }
            else if (selectedItem == "CreamButtonNone" || selectedItem == "CreamButtonMilk" || selectedItem == "CreamButtonWholeMilk")
            {
                MyCreamTextBlock.Text = MyRoastTextBlock.Text == "None" ||
                                        string.IsNullOrEmpty(MyRoastTextBlock.Text) ||
                                        selectedItem == "CreamButtonNone"
                                        ? ""
                                        : string.Format(" + {0}", selectedText);
            }
        }
    }
}
