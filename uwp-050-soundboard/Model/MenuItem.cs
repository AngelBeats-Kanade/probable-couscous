using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP开发入门.Model
{
    public class MenuItem
    {
        public string IconFile { get; set; }
        public SoundCategory Category { get; set; }
        public static List<MenuItem> GetMenuItems()
        {
            List<MenuItem> menuItems = new List<MenuItem>
            {
                new MenuItem { IconFile = "/Assets/Icons/animals.png", Category = SoundCategory.Animals },
                new MenuItem { IconFile = "/Assets/Icons/cartoon.png", Category = SoundCategory.Cartoons },
                new MenuItem { IconFile = "/Assets/Icons/taunt.png", Category = SoundCategory.Taunts },
                new MenuItem { IconFile = "/Assets/Icons/warning.png", Category = SoundCategory.Warnings }
            };

            return menuItems;
        }
        public static ObservableCollection<MenuItem> GetMenuItems(ObservableCollection<MenuItem> menuItems)
        {
            menuItems.Clear();
            foreach (MenuItem menuItem in GetMenuItems())
            {
                menuItems.Add(menuItem);
            }
            return menuItems;
        }

        public Uri GetIcon(string iconPath)
        {
            return new Uri(string.Format($"ms-appx://{iconPath}"));
        }
    }
}
