using StorageService.Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Application.Commands.AddFile
{
    public class AddFileCommandHandler : IRequestHandler<AddFileCommand, string>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IEventPublisher _eventPublisher;

        public AddFileCommandHandler(IFileRepository fileRepository, IEventPublisher eventPublisher)
        {
            _fileRepository = fileRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task<string> Handle(AddFileCommand request, CancellationToken cancellationToken)
        {
            // Create a new File entity
            var file = new FILE
            {
                Id = Guid.NewGuid().ToString(),
                FileName = request.Model.FileName,
                ContentType = request.Model.ContentType,
                Size = request.Model.Content.Length,
                UploadedAt = DateTime.UtcNow,
                UploadedBy = request.Model.UploadedBy
            };

            // Save file to repository
            await _fileRepository.SaveFileAsync(file, request.Model.Content);

            // Publish FileAddedEvent after file is saved
            var fileAddedEvent = new FileAddedEvent
            {
                FileId = file.Id,
                FileName = file.FileName,
                ContentType = file.ContentType,
                Size = file.Size
            };
            await _eventPublisher.PublishFileAddedEvent(fileAddedEvent);

            return file.Id;
        }
    }
}
