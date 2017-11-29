using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItSite.Services
{
    public interface IImageCache
    {
        void SetImage(string key, string imagePath, bool isURL);
        byte[] GetImageBytes(string key);
        string GetImageEmbed(string key);
    }
}
