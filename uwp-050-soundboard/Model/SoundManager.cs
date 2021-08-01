using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace UWP开发入门.Model
{
    public class SoundManager
    {
        public static List<Sound> GetSounds()
        {
            List<Sound> sounds = new List<Sound>
            {
                new Sound("Cow", SoundCategory.Animals),
                new Sound("Cat", SoundCategory.Animals),
                new Sound("Gun", SoundCategory.Cartoons),
                new Sound("Spring", SoundCategory.Cartoons),
                new Sound("Clock", SoundCategory.Taunts),
                new Sound("LOL", SoundCategory.Taunts),
                new Sound("Ship", SoundCategory.Warnings),
                new Sound("Siren", SoundCategory.Warnings)
            };

            return sounds;
        }

        public static ObservableCollection<Sound> GetSounds(ObservableCollection<Sound> sounds, params SoundCategory[] categories)
        {
            IEnumerable<Sound> filteredSounds;
            sounds.Clear();
            foreach (SoundCategory category in categories)
            {
                filteredSounds = from sound in GetSounds()
                                 where sound.Category == category
                                 select sound;
                foreach (Sound sound in filteredSounds)
                {
                    sounds.Add(sound);
                }
            }

            return sounds;
        }

        public static ObservableCollection<Sound> GetSounds(ObservableCollection<Sound> sounds, params string[] names)
        {
            IEnumerable<Sound> filteredSounds;
            sounds.Clear();
            foreach (string name in names)
            {
                filteredSounds = from sound in GetSounds()
                                     where sound.Name == name
                                     select sound;
                foreach (Sound sound in filteredSounds)
                {
                    sounds.Add(sound);
                }
            }
            return sounds;
        }
    }
}
