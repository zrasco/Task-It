using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItSite.Services
{
    public interface IImageCache
    {
        void SetImage(IHostingEnvironment env, string key, string imagePath, bool isURL);
        byte[] GetImageBytes(string key);
        string GetImageEmbed(string key);
    }
}
