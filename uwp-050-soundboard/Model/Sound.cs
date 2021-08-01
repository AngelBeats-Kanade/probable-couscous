using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP开发入门.Model
{
    public class Sound
    {
        public string Name { get; set; }
        public SoundCategory Category { get; set; }
        public string AudioFile { get; set; }
        public string ImageFile { get; set; }

        public Sound(string name, SoundCategory category)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Category = category;
            AudioFile = string.Format($"/Assets/Audio/{category}/{name}.wav");
            ImageFile = string.Format($"/Assets/Images/{category}/{name}.png");
        }
    }

    public enum SoundCategory
    {
        Animals,
        Cartoons,
        Taunts,
        Warnings
    }
}
