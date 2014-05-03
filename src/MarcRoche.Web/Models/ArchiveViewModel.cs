using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using MarcRoche.Domain.Blog;

namespace MarcRoche.Web.Models
{
    public class ArchiveViewModel
    {
        private DateTimeFormatInfo _dateTimeFormatInfo = new DateTimeFormatInfo();

        public ArchiveViewModel(IEnumerable<BlogPost> posts)
        {
            Items = new Dictionary<string, List<ArchiveItem>>();

            foreach (var x in posts.OrderByDescending(x => x.PublishDate).GroupBy(x => new { x.PublishDate.Year }))
            {
                List<ArchiveItem> items = new List<ArchiveItem>();
                posts.Where(p => p.PublishDate.Year == x.Key.Year).OrderByDescending(b => b.PublishDate)
                    .ToList().ForEach(i =>
                    {
                        items.Add(new ArchiveItem
                            {
                                Title = i.Title,
                                Link = "/blog/" + x.Key.Year + "/" + string.Format("{0:00}", i.PublishDate.Month) + "/" + i.Title.ToLower().Replace(" ", "-"),
                            });
                    });

                Items.Add(x.Key.Year.ToString(), items);
            }
        }

        public Dictionary<string, List<ArchiveItem>> Items { get; set; }
    }

    public class ArchiveItem
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public List<ArchiveItem> Items { get; set; }

        public ArchiveItem()
        {
            Items = new List<ArchiveItem>();
        }
    }
}