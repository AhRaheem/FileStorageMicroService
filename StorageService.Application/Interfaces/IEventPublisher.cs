using StorageService.Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Application.Interfaces
{
    public interface IEventPublisher
    {
        Task PublishFileAddedEvent(FileAddedEvent fileAddedEvent);
        Task PublishFileDeletedEvent(FileDeletedEvent fileDeletedEvent);
    }
}
