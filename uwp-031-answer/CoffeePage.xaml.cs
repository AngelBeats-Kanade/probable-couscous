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

        private bool isRoast = false;

        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ((MenuFlyoutItem)sender)?.Name.ToString();
            var selectedText = ((MenuFlyoutItem)sender)?.Text.ToString();

            if (selectedItem == "RoastButtonNone")
            {
                isRoast = false;
                MyRoastTextBlock.Text = selectedText;
            }
            else if (selectedItem == "RoastButtonDark" || selectedItem == "RoastButtonMedium")
            {
                isRoast = true;
            }

            if (isRoast == true)
            {
                if (selectedItem == "RoastButtonDark" || selectedItem == "RoastButtonMedium")
                {
                    MyRoastTextBlock.Text = selectedText;
                }
                else if (selectedItem == "SweetnerButtonNone" || selectedItem == "SweetnerButtonSugar")
                {
                    if(selectedItem == "SweetnerButtonNone")
                    {
                        MySweetnerTextBlock.Text = "";
                    }
                    else 
                    {
                        MySweetnerTextBlock.Text = string.Format(" + {0}", selectedText);
                    }
                }
                else if (selectedItem == "CreamButtonNone" || selectedItem == "CreamButtonMilk" || selectedItem == "CreamButtonWholeMilk")
                {
                    if (selectedItem == "CreamButtonNone")
                    {
                        MyCreamTextBlock.Text = "";
                    }
                    else
                    {
                        MyCreamTextBlock.Text = string.Format(" + {0}", selectedText);
                    }
                }
            }
        }
    }
}
