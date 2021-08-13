using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroExplorer.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class TextObject
    {
        public string type { get; set; }
        public string language { get; set; }
        public string text { get; set; }
    }

    public class ComicDate
    {
        public string type { get; set; }
        public DateTime date { get; set; }
    }

    public class ComicPrice
    {
        public string type { get; set; }
        public float price { get; set; }
    }

    public class CreatorList
    {
        public int available { get; set; }
        public int returned { get; set; }
        public string collectionURI { get; set; }
        public List<CreatorSummary> items { get; set; }
    }

    public class CreatorSummary
    {
        public string resourceURI { get; set; }
        public string name { get; set; }
        public string role { get; set; }
    }

    public class CharacterList
    {
        public int available { get; set; }
        public int returned { get; set; }
        public string collectionURI { get; set; }
        public List<CharacterSummary> items { get; set; }
    }

    public class CharacterSummary
    {
        public string resourceURI { get; set; }
        public string name { get; set; }
        public string role { get; set; }
    }

    public class Comic
    {
        public int id { get; set; }
        public int digitalId { get; set; }
        public string title { get; set; }
        public double issueNumber { get; set; }
        public string variantDescription { get; set; }
        public string description { get; set; }
        public DateTime modified { get; set; }
        public string isbn { get; set; }
        public string upc { get; set; }
        public string diamondCode { get; set; }
        public string ean { get; set; }
        public string issn { get; set; }
        public string format { get; set; }
        public int pageCount { get; set; }
        public List<TextObject> textObjects { get; set; }
        public string resourceURI { get; set; }
        public List<Url> urls { get; set; }
        public SeriesSummary series { get; set; }
        public List<ComicSummary> variants { get; set; }
        public List<ComicSummary> collections { get; set; }
        public List<ComicSummary> collectedIssues { get; set; }
        public List<ComicDate> dates { get; set; }
        public List<ComicPrice> prices { get; set; }
        public Image thumbnail { get; set; }
        public List<Image> images { get; set; }
        public CreatorList creators { get; set; }
        public CharacterList characters { get; set; }
        public StoryList stories { get; set; }
        public EventList events { get; set; }
    }

    public class ComicDataContainer
    {
        public int offset { get; set; }
        public int limit { get; set; }
        public int total { get; set; }
        public int count { get; set; }
        public List<Comic> results { get; set; }
    }

    public class ComicDataWrapper
    {
        public int code { get; set; }
        public string status { get; set; }
        public string copyright { get; set; }
        public string attributionText { get; set; }
        public string attributionHTML { get; set; }
        public ComicDataContainer data { get; set; }
        public string etag { get; set; }
    }
}