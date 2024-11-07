using StorageService.Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Application.Commands.DeleteFile
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, bool>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IEventPublisher _eventPublisher;

        public DeleteFileCommandHandler(IFileRepository fileRepository, IEventPublisher eventPublisher)
        {
            _fileRepository = fileRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task<bool> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var fileDeleted = await _fileRepository.DeleteFileAsync(request.FileId);
            if (!fileDeleted) return false;

            // Publish FileDeletedEvent after file is deleted
            var fileDeletedEvent = new FileDeletedEvent
            {
                FileId = request.FileId,
            };
            await _eventPublisher.PublishFileDeletedEvent(fileDeletedEvent);

            return true;
        }
    }
}
