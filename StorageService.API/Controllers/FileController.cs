
using StorageService.Application.Commands.DeleteFile;

namespace StorageService.API.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddFile([FromForm] AddFileCommand command)
        {
            var fileId = await _mediator.Send(command);
            return Ok(fileId);
        }

        [HttpGet("{fileId}")]
        public async Task<IActionResult> RetrieveFile(string fileId)
        {
            var fileContent = await _mediator.Send(new RetrieveFileQuery(fileId));
            if (fileContent == null)
                return NotFound();

            return File(fileContent, "application/octet-stream");
        }

        [HttpDelete("{fileId}")]
        public async Task<IActionResult> DeleteFile(string fileId)
        {
            var result = await _mediator.Send(new DeleteFileCommand(fileId));
            if (!result)
            {
                return NotFound($"File with ID {fileId} not found.");
            }

            return NoContent();
        }
    }
}
