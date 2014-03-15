namespace MarcRoche.Model.Events
{
    public interface IHandles<T> where T : IDomainEvent
    {
        void Handle(T args);
    } 
}