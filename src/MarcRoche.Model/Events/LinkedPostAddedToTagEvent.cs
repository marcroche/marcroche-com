namespace MarcRoche.Model.Events
{
    public class LinkedPostAddedToTagEvent : IDomainEvent
    {
        public string PostName { get; private set; }

        public LinkedPostAddedToTagEvent(string postName)
        {
            PostName = postName;
        }
    }
}