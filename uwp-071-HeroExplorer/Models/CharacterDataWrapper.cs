using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroExplorer.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Url
    {
        public string type { get; set; }
        public string url { get; set; }
    }

    public class Image
    {
        public string path { get; set; }
        public string extension { get; set; }
        public string small { get; set; }
        public string large { get; set; }
    }

    public class ComicSummary
    {
        public string resourceURI { get; set; }
        public string name { get; set; }
    }

    public class StorySummary
    {
        public string resourceURI { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class EventSummary
    {
        public string resourceURI { get; set; }
        public string name { get; set; }
    }

    public class SeriesSummary
    {
        public string resourceURI { get; set; }
        public string name { get; set; }
    }

    public class ComicList
    {
        public int available { get; set; }
        public int returned { get; set; }
        public string collectionURI { get; set; }
        public List<ComicSummary> items { get; set; }
    }

    public class StoryList
    {
        public int available { get; set; }
        public int returned { get; set; }
        public string collectionURI { get; set; }
        public List<StorySummary> items { get; set; }
    }

    public class EventList
    {
        public int available { get; set; }
        public int returned { get; set; }
        public string collectionURI { get; set; }
        public List<EventSummary> items { get; set; }
    }

    public class SeriesList
    {
        public int available { get; set; }
        public int returned { get; set; }
        public string collectionURI { get; set; }
        public List<SeriesSummary> items { get; set; }
    }

    public class Character
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime modified { get; set; }
        public string resourceURI { get; set; }
        public List<Url> urls { get; set; }
        public Image thumbnail { get; set; }
        public ComicList comics { get; set; }
        public StoryList stories { get; set; }
        public EventList events { get; set; }
        public SeriesList series { get; set; }
    }

    public class CharacterDataContainer
    {
        public int offset { get; set; }
        public int limit { get; set; }
        public int total { get; set; }
        public int count { get; set; }
        public List<Character> results { get; set; }
    }

    public class CharacterDataWrapper
    {
        public int code { get; set; }
        public string status { get; set; }
        public string copyright { get; set; }
        public string attributionText { get; set; }
        public string attributionHTML { get; set; }
        public CharacterDataContainer data { get; set; }
        public string etag { get; set; }
    }
}
