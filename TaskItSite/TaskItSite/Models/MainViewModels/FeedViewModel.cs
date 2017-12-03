using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItSite.Models.MainViewModels
{
    public enum FeedItemType { Default, Task, Achievement, Post }
    public class FeedItem
    {
        public FeedItem() { }

        public FeedItem(FeedItem baseFI)
        {
            FromUser = baseFI.FromUser;
            ImageData = baseFI.ImageData;
            Text = baseFI.Text;
            Occured = baseFI.Occured;
            ItemType = baseFI.ItemType;
            Taskid = baseFI.Taskid;
        }

        public ApplicationUser FromUser { get; set; }
        public string Text { get; set; }
        public string ImageData { get; set; }
        public DateTime Occured { get; set; }
        public FeedItemType ItemType { get; set; }
        public int? Taskid { get; set; }
    }
    public class FeedViewModel
    {
        public ApplicationUser CurrentUser { get; set; }
        public List<FeedItem> FeedItems { get; set; }

        [Display(Name = "Tell everyone what's on your mind!")]
        public string MyPostText { get; set; }

        public string StatusMessage { get; set; }
    }
}
