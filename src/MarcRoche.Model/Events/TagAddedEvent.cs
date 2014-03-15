using MarcRoche.Model.Admin;

namespace MarcRoche.Model.Events
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
