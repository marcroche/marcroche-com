using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarcRoche.Domain.Events;

namespace MarcRoche.Domain.Admin
{
    public class Tags
    {
        private List<Tag> _tags;
        public IEnumerable<Tag> Tagss { get; private set; }

        public Tags ()
	    {
            _tags = new List<Tag>();
	    }
        
        public void AddTag(string tagName)
        {
            DomainEvents.Raise(new TagAddedEvent(new Tag(tagName)));
        }
    }

    public class Tag
    {
        private List<string> _linkedPosts;
        
        public string Name { get; set; }
        public IEnumerable<string> LinkedPosts { get; set; }

        public Tag(string name)
        {
            _linkedPosts = new List<string>();
        }

        public void AddLinkedPost(string postName)
        {
            DomainEvents.Raise(new LinkedPostAddedToTagEvent(postName));
        }
    }
}
