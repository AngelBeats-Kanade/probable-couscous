using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace HeroExplorer.Models
{
    public class MarvelFacade
    {
        private const string _privateKey = "dc8b9c6c89e932e252e87867177225eb0d3c88b8";
        private const string _publicKey = "b08e3abcfa711def20d94b6ee7bfbd96";
        private const string _imageNotAvailable = "http://i.annihil.us/u/prod/marvel/i/mg/b/40/image_not_available";

        public static async Task<ObservableCollection<Character>> AddCharactersAsync(ObservableCollection<Character> characters)
        {
            List<Character> characterData = await GetCharacterDataAsync();

            IEnumerable<Character> filteredCharacters = from character in characterData
                                                        where character.thumbnail != null && character.thumbnail.path != "" && character.thumbnail.path != _imageNotAvailable
                                                        select character;

            foreach (Character character in filteredCharacters)
            {
                character.thumbnail.small = string.Format($"{character.thumbnail.path}/standard_small.{character.thumbnail.extension}");
                character.thumbnail.large = string.Format($"{character.thumbnail.path}/portrait_xlarge.{character.thumbnail.extension}");
                characters.Add(character);
            }

            return characters;
        }

        public static async Task<ObservableCollection<Comic>> AddComicsAsync(ObservableCollection<Comic> comics, int characterId)
        {
            List<Comic> comicData = await GetComicDataAsync(characterId);

            IEnumerable<Comic> filteredComics = from comic in comicData
                                                where comic.thumbnail != null && comic.thumbnail.path != "" && comic.thumbnail.path != _imageNotAvailable
                                                select comic;

            foreach (Comic comic in filteredComics)
            {
                comic.thumbnail.small = string.Format($"{comic.thumbnail.path}/portrait_medium.{comic.thumbnail.extension}");
                comic.thumbnail.large = string.Format($"{comic.thumbnail.path}/portrait_xlarge.{comic.thumbnail.extension}");
                comics.Add(comic);
            }

            return comics;
        }

        public static async Task<List<Character>> GetCharacterDataAsync()
        {
            CharacterDataWrapper characterDataWrapper = await GetCharacterDataWrapperAsync();
            List<Character> characterDataContainer = characterDataWrapper.data.results;

            return characterDataContainer;
        }

        public static async Task<List<Comic>> GetComicDataAsync(int characterId)
        {
            ComicDataWrapper comicDataWrapper = await GetComicDataWrapperAsync(characterId);
            List<Comic> comicDataContainer = comicDataWrapper.data.results;

            return comicDataContainer;
        }

        public static async Task<CharacterDataWrapper> GetCharacterDataWrapperAsync()
        {
            string timeStamp = DateTime.Now.Ticks.ToString();
            string hash = GetHash(timeStamp);
            string url = string.Format($"https://gateway.marvel.com:443/v1/public/characters?limit=100&ts={timeStamp}&apikey={_publicKey}&hash={hash}");
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(url);
            string jsonResponse = await response.Content.ReadAsStringAsync();
            CharacterDataWrapper characterDataWrapper = JsonConvert.DeserializeObject<CharacterDataWrapper>(jsonResponse);
            return characterDataWrapper;
        }

        public static async Task<ComicDataWrapper> GetComicDataWrapperAsync(int characterId)
        {
            string timeStamp = DateTime.Now.Ticks.ToString();
            string hash = GetHash(timeStamp);
            string url = string.Format($"https://gateway.marvel.com:443/v1/public/comics?characters={characterId}&limit=10&ts={timeStamp}&apikey={_publicKey}&hash={hash}");
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage httpResponse = await httpClient.GetAsync(url);
            string jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            ComicDataWrapper comicDataWrapper = JsonConvert.DeserializeObject<ComicDataWrapper>(jsonResponse);
            return comicDataWrapper;
        }

        private static string GetHash(string timeStamp)
        {
            return GetMD5(timeStamp + _privateKey + _publicKey);
        }

        private static string GetMD5(string str)
        {
            HashAlgorithmProvider alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            IBuffer buffer = CryptographicBuffer.ConvertStringToBinary(str, BinaryStringEncoding.Utf8);
            IBuffer hashed = alg.HashData(buffer);
            return CryptographicBuffer.EncodeToHexString(hashed);
        }
    }
}
