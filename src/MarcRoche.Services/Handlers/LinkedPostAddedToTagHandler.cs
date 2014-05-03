using System;
using MarcRoche.Domain.Events;

namespace MarcRoche.Services.Handlers
{
    public class LinkedPostAddedToTagHandler : IHandles<LinkedPostAddedToTagEvent>
    {
        public void Handle(LinkedPostAddedToTagEvent args)
        {
            throw new NotImplementedException();
        }
    }
}
