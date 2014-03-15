using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarcRoche.Model.Events;

namespace MarcRoche.Services.Handlers
{
    public class TagAddedHandler : IHandles<TagAddedEvent>
    {
        public TagAddedHandler()
        {

        }

        public void Handle(TagAddedEvent args)
        {
            throw new NotImplementedException();
        }
    }
}
