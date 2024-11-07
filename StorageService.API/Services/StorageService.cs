

namespace StorageService.API.Services
{
    public class StorageService : Protos.StorageService.StorageServiceBase
    {
        private readonly IMediator _mediator;

        public StorageService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<AddFileResponse> AddFile(AddFileRequest request, ServerCallContext context)
        {
            var Model = new Application.Dtos.AddFileDto
            {
                Content = request.Content.ToByteArray(),
                FileName = request.FileName,
                ContentType = request.ContentType,
                UploadedBy = request.UploadedBy
            };

            var command = new AddFileCommand(Model);

            var fileId = await _mediator.Send(command);
            return new AddFileResponse { FileId = fileId };
        }

        public override async Task<RetrieveFileResponse> RetrieveFile(RetrieveFileRequest request, ServerCallContext context)
        {
            var query = new RetrieveFileQuery(request.FileId);
            var fileContent = await _mediator.Send(query);

            if (fileContent == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "File not found"));
            }

            var fileName = "example.txt"; // Replace with actual file name
            var contentType = "text/plain"; // Replace with actual content type

            return new RetrieveFileResponse
            {
                Content = Google.Protobuf.ByteString.CopyFrom(fileContent),
                FileName = fileName,
                ContentType = contentType
            };
        }

        public override async Task<DeleteFileResponse> DeleteFile(DeleteFileRequest request, ServerCallContext context)
        {
            var command = new DeleteFileCommand(request.FileId);
            var success = await _mediator.Send(command);

            return new DeleteFileResponse { Success = success };
        }
    }
}
