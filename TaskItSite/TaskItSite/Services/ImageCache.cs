using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TaskItSite.Services
{
    public class ImageCache : IImageCache
    {
        // For a larger server we'd use a DB
        private Dictionary<string, byte[]> _imageCacheDictionary = new Dictionary<string, byte[]>();
        
        public byte[] GetImageBytes(string key)
        {
            if (_imageCacheDictionary.ContainsKey(key))
                return _imageCacheDictionary[key];
            else
                return null;
        }

        public string GetImageEmbed(string key)
        {
            byte[] imageBytes = GetImageBytes(key);

            if (imageBytes == null)
                return null;
            else
            {
                string base64Data = Convert.ToBase64String(GetImageBytes(key));
                return string.Format("data:image/png;base64,{0}", base64Data);
            }
        }

        public void SetImage(string key, string imagePath, bool isURL)
        {
            if (isURL)
            {
                var webClient = new WebClient();
                // Download an image into the cache from the specified URL
                try
                {                    
                    _imageCacheDictionary[key] = webClient.DownloadData(imagePath);
                }
                catch (Exception ex)
                // Use task-it image as fallback. Hackish for now
                {                    
                    _imageCacheDictionary[key] = webClient.DownloadData("https://localhost:44395/images/logoattempt.png");
                }

            }                
            else
            {
                _imageCacheDictionary[key] = System.IO.File.ReadAllBytes(imagePath);
            }
        }
    }
}