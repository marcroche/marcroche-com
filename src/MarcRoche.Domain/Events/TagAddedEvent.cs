using MarcRoche.Domain.Admin;

namespace MarcRoche.Domain.Events
{
    public class TagAddedEvent : IDomainEvent
    {
        public Tag Tag { get; private set; }

        public TagAddedEvent(Tag tag)
        {
            Tag = tag;
        }
    }
}
