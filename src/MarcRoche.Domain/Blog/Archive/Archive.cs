using System.Collections.Generic;

namespace MarcRoche.Domain.Blog.Archive
{
    public class Archive
    {
        private IDictionary<int, IList<ArchiveItem>> Items { get; set; }

        public Archive()
        {
            Items = new Dictionary<int, IList<ArchiveItem>>();
        }

        public void AddYear(int year)
        {
            if (Items.ContainsKey(year) == false)
            {
                Items.Add(year, new List<ArchiveItem>());
            }
        }

        public void AddValue(int year, ArchiveItem item)
        {
            AddYear(year);
            Items[year].Add(item);
        }
    }
}
