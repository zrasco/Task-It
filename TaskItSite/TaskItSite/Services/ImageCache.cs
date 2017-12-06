using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TaskItSite.Controllers;
using ImageSharp;
using ImageSharp.Formats;
using System.IO;
using ImageSharp.Processing;

namespace TaskItSite.Services
{
    public class ImageCache : IImageCache
    {
        public ImageCache()
        {
            Configuration.Default.AddImageFormat(new JpegFormat());
            Configuration.Default.AddImageFormat(new PngFormat());
        }

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

        public void SetImage(IHostingEnvironment env, string key, string imagePath, bool isURL)
        {
            if (isURL)
            {
                var webClient = new WebClient();
                // Download an image into the cache from the specified URL
                try
                {
                    _imageCacheDictionary[key] = webClient.DownloadData(imagePath);
                    
                    // Last minute change to make cached images 20x20 for deployment

                    string extn = "";

                    if (imagePath.Contains(".jpg"))
                        extn = ".jpg";
                    else if (imagePath.Contains(".png"))
                        extn = ".png";

                    string tmpFileNameIn = System.IO.Path.GetTempFileName() + extn;
                    string tmpFileNameOut = System.IO.Path.GetTempFileName() + extn;

                    File.WriteAllBytes(tmpFileNameIn, _imageCacheDictionary[key]);
                    
                    using (var input = File.OpenRead(tmpFileNameIn))
                    {
                        using (var output = File.OpenWrite(tmpFileNameOut))
                        {
                            var image = new Image(input)
                                .Resize(new ResizeOptions
                                {
                                    Size = new Size(20, 20),
                                    Mode = ResizeMode.Max
                                });
                            //image.ExifProfile = null;
                            //image.Quality = quality;
                            image.Save(output);
                        }
                    }

                    _imageCacheDictionary[key] = File.ReadAllBytes(tmpFileNameOut);

                    File.Delete(tmpFileNameIn);
                    File.Delete(tmpFileNameOut);


                }
                catch (Exception ex)
                // Use task-it image as fallback. Hackish for now
                {
                    _imageCacheDictionary[key] = System.IO.File.ReadAllBytes(env.WebRootPath + @"\images\logoattempt.png");
                }

            }                
            else
            {
                _imageCacheDictionary[key] = System.IO.File.ReadAllBytes(imagePath);
            }
        }

        private string GetBaseURL(HttpContext context)
        {
            var request = context.Request;

            var host = request.Host.ToUriComponent();

            var pathBase = request.PathBase.ToUriComponent();

            return $"{request.Scheme}://{host}{pathBase}";
        }
    }
}